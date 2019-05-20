﻿using AuthorizationCenter.Define;
using AuthorizationCenter.Dto.Jsons;
using AuthorizationCenter.Dto.Requests;
using AuthorizationCenter.Managers;
using AuthorizationCenter.Entitys;
using AuthorizationCenter.Stores;
using AuthorizationCenter.ViewModels.Sign;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WS.Core;
using WS.Log;
using WS.Text;

namespace AuthorizationCenter.Controllers
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
        /// 用户管理
        /// </summary>
        public IUserManager<UserJson> UserManager { get; set; }

        /// <summary>
        /// Session
        /// </summary>
        public ISession Session => HttpContext.Session;
        
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="userManager"></param>
        public SignController(IUserManager<UserJson> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// 登陆主页
        /// </summary>
        /// <returns></returns>
        [Filters.NoSign]
        public ViewResult Index(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl??"/User/Index";
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Filters.NoSign]
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
        [Filters.NoSign]
        [HttpPost]
        public async Task<IActionResult> SignIn([Required]SignInViewModel request, string returnUrl = null)
        {
            Logger.Trace("returnUrl: " + returnUrl+ "\r\nrequest: " +JsonUtil.ToJson(request));

            ViewData["returnUrl"] = returnUrl;

            // 参数检查
            if (string.IsNullOrWhiteSpace(request.SignName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                Logger.Trace($"[{nameof(SignIn)}] 用户名或密码不能为空");
                ModelState.AddModelError("All", "用户名或密码不能为空");
                return View(request);
            }
            // 登陆成功
            if (await UserManager.Exist(user=>user.SignName== request.SignName&&user.PassWord==request.PassWord))
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

        /// <summary>
        /// 获取登陆用户简要信息 -每次都是新建一个UserBaseJson对象
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 登陆用户
        /// </summary>
        private UserJson SignUser
        {
            get
            {
                if (HttpContext.Session.GetString(Constants.USERID) == null)
                {
                    return null;
                }
                return new UserJson
                {
                    Id = HttpContext.Session.GetString(Constants.USERID),
                    SignName = HttpContext.Session.GetString(Constants.SIGNNAME),
                    PassWord = HttpContext.Session.GetString(Constants.PASSWORD)
                };
            }
            set
            {
                if (value == null)
                {
                    HttpContext.Session.Remove(Constants.USERID);
                    HttpContext.Session.Remove(Constants.SIGNNAME);
                    HttpContext.Session.Remove(Constants.PASSWORD);
                }
                else
                {
                    HttpContext.Session.SetString(Constants.USERID, value.Id);
                    HttpContext.Session.SetString(Constants.SIGNNAME, value.SignName);
                    HttpContext.Session.SetString(Constants.PASSWORD, value.PassWord);
                }
            }
        }

        /// <summary>
        /// 签出 Clear Session SignUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Filters.NoSign]
        public IActionResult SignOut(ModelRequest<UserJson> request)
        {
            SignUser = null;
            return View(nameof(Index));
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            Console.WriteLine("登陆跳转URL："+returnUrl);
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), HomeController.Name);
            }
        }

        /// <summary>
        /// 将ActionURL解析出ActionName与ControllerName
        /// 再重定向到Action并传递参数
        /// </summary>
        /// <param name="actionURL"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        private IActionResult RedirectUrlToAction(string actionURL, object routeValues)
        {
            Console.WriteLine("RedirectUrlToAction--跳转URL：" + actionURL);
            if (Url.IsLocalUrl(actionURL))
            {
                int lastIndex = actionURL.LastIndexOf('/');
                var ActionName = actionURL.Substring(lastIndex, actionURL.Length-lastIndex);
                Console.WriteLine("ActionName: " + ActionName);
                int secondIndex = actionURL.Substring(0, lastIndex).LastIndexOf('/');
                var ControllerName = actionURL.Substring(secondIndex, lastIndex-secondIndex);
                Console.WriteLine("ControllerName: " + ControllerName);
                return RedirectToAction(ActionName, ControllerName, routeValues);
            }
            else
            {
                // 不是本地地址的话就去主页
                return RedirectToAction(nameof(HomeController.Index), HomeController.Name);
            }
        }
    }
}
