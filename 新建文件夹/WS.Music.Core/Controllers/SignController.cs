using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Controllers
{
    /// <summary>
    /// 登陆控制器
    /// </summary>
    public class SignController : Controller
    {

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
        public async Task<IActionResult> SignUp([FromForm]ModelRequest<UserJson> request)
        {
            // 响应体构建
            ResponseMessage<UserJson> response = new ResponseMessage<UserJson>();

            Console.WriteLine(JsonUtil.ToJson(request));

            try
            {
                // 用户创建
                await UserManager.Create(request.Data);
                //await UserManager.Create(response, request);
            }
            catch (Exception e)
            {
                Logger.Error($"[{nameof(SignUp)}] 用户创建失败：\r\n{e.ToString()}");
            }
            return RedirectToAction(nameof(UserController.Index), nameof(UserController));
            //return RedirectToRoute(new { controller = nameof(UserController), action = nameof(UserController.Index) });
        }

        /// <summary>
        /// 签入
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="returnUrl">返回URL（跳转）</param>
        /// <returns></returns>
        //[Filters.NoSign]
        [HttpPost]
        public async Task<IActionResult> SignIn([Required]SignInViewModel request, string returnUrl = null)
        {
            Logger.Trace("returnUrl: " + returnUrl + "\r\nrequest: " + JsonUtil.ToJson(request));

            ViewData["returnUrl"] = returnUrl;

            // 参数检查
            if (string.IsNullOrWhiteSpace(request.SignName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                Logger.Trace($"[{nameof(SignIn)}] 用户名或密码不能为空");
                ModelState.AddModelError("All", "用户名或密码不能为空");
                return View(request);
            }
            // 登陆成功
            if (await UserManager.Exist(user => user.SignName == request.SignName && user.PassWord == request.PassWord))
            {
                SignUser = await UserManager.FindByName(request.SignName).SingleOrDefaultAsync();
                Logger.Trace("登陆成功");
                if (returnUrl != null)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
                }
            }
            // 登陆失败
            else
            {
                Logger.Trace("登陆失败");
                ModelState.AddModelError("All", "用户名或密码错误");
                return View(nameof(Index), request);
            }
        }
    }
}
