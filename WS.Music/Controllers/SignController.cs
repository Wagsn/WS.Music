using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Log;
using WS.Music.Core.Entitys;
using WS.Music.Models;
using WS.Text;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    public class SignController : Controller
    {
        /// <summary>
        /// 日志器
        /// </summary>
        public ILogger Logger = LoggerManager.GetLogger<SignController>();

        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected ApplicationDbContext Context { get; set; }


        public SignController(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 登陆注册页
        /// </summary>
        /// <returns></returns>
        //[Filters.NoSign]
        public ViewResult Index(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl ?? "/Sign/Index";
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[Filters.NoSign]
        public IActionResult SignUp([FromForm]SignViewModel request, string returnUrl = null)
        {
            // 响应体构建
            Logger.Trace($"注册、请求体：\r\n{JsonUtil.ToJson(request)}");
            try
            {
                if(Context.User.Any(a => a.Name == request.UserName))
                {
                    ModelState.AddModelError("UserName", "用户名已存在");
                    return View(nameof(Index), request);
                }
                // 用户创建
                Context.User.Add(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.UserName,
                    PassWord = request.PassWord
                });
                Context.SaveChanges();
                Logger.Trace("注册成功");
            }
            catch (Exception e)
            {
                Logger.Error($"[{nameof(SignUp)}] 用户创建失败：\r\n{e.ToString()}");
            }
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        /// <summary>
        /// 签入
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="returnUrl">返回URL（跳转）</param>
        /// <returns></returns>
        //[Filters.NoSign]
        [HttpPost]
        public IActionResult SignIn([Required]SignViewModel request, string returnUrl = null)
        {
            Logger.Trace("returnUrl: " + returnUrl + "\r\nrequest: " + JsonUtil.ToJson(request));

            ViewData["returnUrl"] = returnUrl;

            // 参数检查
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                Logger.Trace($"[{nameof(SignIn)}] 用户名或密码不能为空");
                ModelState.AddModelError("All", "用户名或密码不能为空");
                return View(nameof(Index), request);
            }
            if (Context.User.Any(user => user.Name == request.UserName && user.PassWord == request.PassWord))
            {
                // 登陆成功
                Logger.Trace("登陆成功");
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // 登陆失败
                Logger.Trace("登陆失败");
                ModelState.AddModelError("All", "用户名或密码错误");
                return View(nameof(Index), request);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            Console.WriteLine("登陆跳转URL：" + returnUrl);
            if ((!string.IsNullOrWhiteSpace(returnUrl)) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
