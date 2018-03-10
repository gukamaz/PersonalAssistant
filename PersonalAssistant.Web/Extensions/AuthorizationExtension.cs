using PersonalAssistant.BLL.Managers;
using PersonalAssistant.BLL.Models;
using PersonalAssistant.BLL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PersonalAssistant.Web.Extensions
{
    public static class AuthorizationExtension
    {
        public const string LOGINTOKEN = "loginToken";


        public static string GetCookie(this HttpRequestBase request, string cookieName)
        {
            var cookie = request.Cookies[cookieName];
            return cookie?.Value;
        }

        public static void SetCookie(this HttpResponseBase response, string cookieName, string cookieValue)
        {
            response.Cookies.Add(new HttpCookie(cookieName, cookieValue));
        }

        public static UserInfo GetLogginedUser(this Controller controller)
        {
            var token = controller.Request.GetCookie(LOGINTOKEN);
            if (string.IsNullOrEmpty(token))
                return null;
            IUserManager userManager = new UserManager(new Encryptor());
            return userManager.GetLogginedUser(Guid.Parse(token));
        }

        public static bool IsAuthorized(this Controller controller)
        {
            return controller.Request.IsAuthorized();
        }

        public static bool IsAuthorized(this HttpRequestBase request)
        {
            var token = request.GetCookie(LOGINTOKEN);
            if (string.IsNullOrEmpty(token))
                return false;
            UserManager userManager = new UserManager(new Encryptor());
            return userManager.IsAuthorized(Guid.Parse(token));
        }
    }
}