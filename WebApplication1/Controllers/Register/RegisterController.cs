using Microsoft.AspNetCore.Mvc;
using TestAPP.Data;
using TestAPP.Models.Login;
using TestAPP.Models.Register;

namespace TestAPP.Controllers.Register
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpPost("/Register/Signin")]
        public IActionResult Signin(RegisterViewModel model, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                var 사용자 = this.context.사용자들.FirstOrDefault(m => m.아이디.Equals(model.아이디));
                string checkboxValue = form[$"checkbox"];

                if (!string.IsNullOrEmpty(checkboxValue) && checkboxValue.ToLower() == "on")
                {
                    TempData["ErrorMessage1"] = "ON";

                    return RedirectToAction("Index");
                }

                if (사용자 == null)
                {
                    생성(model);

                    TempData["SuccessMessage"] = "ON";

                    return RedirectToAction("Index", "Login");
                }
            }
            TempData["ErrorMessage"] = "ON";

            return RedirectToAction("Index");
        }

        private readonly TestDataContext context;

        public RegisterController(TestDataContext context)
        {
            this.context = context;
        }

        public void 생성(RegisterViewModel model)
        {
            this.context.사용자들.Add(new 사용자
            {
                아이디 = model.아이디,
                비밀번호 = model.비밀번호,
                이메일 = model.이메일,
                이름 = model.이름,
                소속 = model.소속,
                생년월일 = model.생년월일,
                전화번호 = model.전화번호,
                계정권한들 = new List<계정권한>
                {
                    new 계정권한 { 권한분류 = "개발자"}
                }
            });

            this.context.SaveChanges();
        }
    }
}
