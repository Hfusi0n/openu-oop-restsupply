using RestSupplyMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSupplyDB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Host.SystemWeb;
using Owin;

namespace RestSupplyMVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                UsersSet user = userManager.Find(login.UserName, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(login.ReturnUrl ?? Url.Action("Index", "Navigation"));
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }
    }
}