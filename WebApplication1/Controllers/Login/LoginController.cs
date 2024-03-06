using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using TestAPP.Controllers.Login;
using TestAPP.Data;
using TestAPP.Models.Login;
using TestAPP.Models.Admin;

namespace TestAPP.Controllers.Login
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Check(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var 사용자 = this.context.사용자들.FirstOrDefault(m => m.아이디.Equals(model.아이디) && m.비밀번호.Equals(model.비밀번호));

                if (사용자 != null)
                {
                    /* httpcontext.session.setint32(key, value); */
                    /* "user_login_key"라는 이름으로 session에 담는다. */
                    HttpContext.Session.SetInt32("USER_LOGIN_KEY", 사용자.Id);

                    return RedirectToAction("Index", "Admin");
                }
            }
            TempData["ErrorMessage"] = "ON";

            return RedirectToAction("Index");
        }



        private readonly TestDataContext context;

        public LoginController(TestDataContext context)
        {
            this.context = context;
        }
    }
}
