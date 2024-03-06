using Microsoft.AspNetCore.Mvc;
using TestAPP.Data;
using TestAPP.Models.Find;
using TestAPP.Models.Login;

namespace TestAPP.Controllers.Find
{
    public class FindController : Controller
    {
        [HttpGet("/FindID")]
        public IActionResult FindID()
        {
            return View("FindID");
        }

        [HttpGet("/FindPW")]
        public IActionResult FindPW()
        {
            return View("FindPW");
        }

        [HttpPost("/FindID")]
        public IActionResult FindID(FindViewModel model)
        {
            var 사용자 = this.context.사용자들.FirstOrDefault(m => m.이름.Equals(model.이름) && m.이메일.Equals(model.이메일) && m.전화번호.Equals(model.전화번호));

            if (사용자 != null)
            {
                TempData["SuccessMessage"] = 사용자.아이디;

                return RedirectToAction("FindID");
            }

            TempData["ErrorMessage"] = "ON";

            return RedirectToAction("FindID");
        }

        [HttpPost("/FindPW")]
        public IActionResult FindPW(FindViewModel model)
        {
            var 사용자 = this.context.사용자들.FirstOrDefault(m => m.아이디.Equals(model.아이디) && m.이름.Equals(model.이름) && m.이메일.Equals(model.이메일) && m.전화번호.Equals(model.전화번호));

            if (사용자 != null)
            {
                TempData["SuccessMessage"] = 사용자.비밀번호;

                return RedirectToAction("FindPW");
            }

            TempData["ErrorMessage"] = "ON";

            return RedirectToAction("FindPW");
        }

        private readonly TestDataContext context;

        public FindController(TestDataContext context)
        {
            this.context = context;
        }
    }
}
