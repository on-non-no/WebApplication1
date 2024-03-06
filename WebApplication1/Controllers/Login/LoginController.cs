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
                var ����� = this.context.����ڵ�.FirstOrDefault(m => m.���̵�.Equals(model.���̵�) && m.��й�ȣ.Equals(model.��й�ȣ));

                if (����� != null)
                {
                    /* httpcontext.session.setint32(key, value); */
                    /* "user_login_key"��� �̸����� session�� ��´�. */
                    HttpContext.Session.SetInt32("USER_LOGIN_KEY", �����.Id);

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
