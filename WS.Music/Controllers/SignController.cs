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
            Logger.Trace($"[{nameof(SignUp)}] 请求体：\r\n{JsonUtil.ToJson(request)}");
            try
            {
                if(Context.User.Any(a => a.Name == request.UserName))
                {
                    Logger.Trace($"[{nameof(SignUp)}] 注册失败-用户名已存在");
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
                Logger.Trace($"[{nameof(SignUp)}] 注册成功");
            }
            catch (Exception e)
            {
                Logger.Error($"[{nameof(SignUp)}] 用户注册失败：\r\n{e.ToString()}");
                ModelState.AddModelError("All", e.Message);
                return View(nameof(Index), request);
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
            Logger.Trace($"[{nameof(SignIn)}] 用户登陆 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");

            ViewData["returnUrl"] = returnUrl;

            // 参数检查-在Model里面验证
            //if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
            //{
            //    Logger.Trace($"[{nameof(SignIn)}] 用户名或密码不能为空");
            //    ModelState.AddModelError("All", "用户名或密码不能为空");
            //    return View(nameof(Index), request);
            //}
            var user = Context.User.Where(u => u.Name == request.UserName).SingleOrDefault();
            if (user == null)
            {
                Logger.Trace($"[{nameof(SignIn)}] 登陆失败-用户名不存在\r\nrequest{JsonUtil.ToJson(request)}");
                ModelState.AddModelError("UserName", "用户名不存在");
                return View(nameof(Index), request);
            }

            if (!user.PassWord.Equals(request.PassWord))
            {
                Logger.Trace($"[{nameof(SignIn)}] 登陆失败-密码错误\r\nrequest{JsonUtil.ToJson(request)}");
                ModelState.AddModelError("PassWord", "密码错误");
                return View(nameof(Index), request);
            }

            Logger.Trace($"[{nameof(SignIn)}] 登陆成功");
            return RedirectToLocal(returnUrl);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult SetPassWord(string returnUrl = null)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetPassWord([Required]SetPassWordViewModel request, string returnUrl = null)
        {
            Logger.Trace($"[{nameof(SetPassWord)}] 设置密码 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");

            try
            {
                // 用户名是唯一的可变的
                var user = Context.User.Where(u => u.Name.Equals(request.UserName)).SingleOrDefault();
                if(user == null)
                {
                    Logger.Trace($"[{nameof(SetPassWord)}] 设置密码-用户名不存在 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");
                    ModelState.AddModelError("UserName", "用户名不存在");
                    return View(request);
                }
                if (!user.PassWord.Equals(request.PassWord))
                {
                    Logger.Trace($"[{nameof(SetPassWord)}] 设置密码-旧密码错误 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");
                    ModelState.AddModelError("PassWord", "旧密码错误");
                    return View(request);
                }
                if (request.PassWord.Equals(request.NewPassWord))
                {
                    Logger.Trace($"[{nameof(SetPassWord)}] 设置密码-新密码不能与旧密码相同 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");
                    ModelState.AddModelError("NewPassWord", "新密码不能与旧密码相同");
                    return View(request);
                }
                if (!request.NewPassWord.Equals(request.Confirm))
                {
                    Logger.Trace($"[{nameof(SetPassWord)}] 设置密码-密码确认失败 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}");
                    ModelState.AddModelError("Confirm", "密码确认失败");
                    return View(request);
                }
                user.PassWord = request.NewPassWord;
                Context.User.Update(user);
                Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Logger.Trace($"[{nameof(SetPassWord)}] 设置密码-服务器错误 returnUrl: {returnUrl}\r\nrequest: {JsonUtil.ToJson(request)}\r\n{e.ToString()}");
                return View();
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
