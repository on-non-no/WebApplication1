using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPP.Data;
using TestAPP.Models.Admin;

namespace TestAPP.Controllers.Login
{
    public class AdminController : Controller
    {
        private readonly TestDataContext context;

        public AdminController(TestDataContext context)
        {
            this.context = context;
        }

        public IQueryable<AdminViewModel> 조회()
        {
            var 사용자 = this.context.사용자들.Include(m => m.계정권한들).Select(m => new AdminViewModel
            {
                아이디 = m.아이디,
                비밀번호 = m.비밀번호,
                이메일 = m.이메일,
                이름 = m.이름,
                소속 = m.소속,
                생년월일 = m.생년월일,
                전화번호 = m.전화번호,
                계정권한들 = String.Join(',', m.계정권한들.Select(a => a.권한분류))
            });

            return 사용자;
        }

        public void 생성(IFormCollection form)
        {
            this.context.사용자들.Add(new 사용자
            {
                아이디 = form[$"아이디"],
                비밀번호 = form[$"비밀번호"],
                이메일 = form[$"이메일"],
                이름 = form[$"이름"],
                소속 = form[$"소속"],
                생년월일 = form[$"생년월일"],
                전화번호 = form[$"전화번호"],
                계정권한들 = new List<계정권한>
                {
                    new 계정권한 { 권한분류 = form[$"계정권한"]}
                }
            });

            this.context.SaveChanges();
        }

        public void 삭제(IFormCollection form)
        {
            var 사용자들 = this.context.사용자들.Include(e => e.계정권한들).ToList();
            int 사용자수 = 사용자들.Count();

            for (int i = 0; i < 사용자수; i++)
            {
                string checkboxValue = form[$"checkbox_{i}"];

                if (!string.IsNullOrEmpty(checkboxValue) && checkboxValue.ToLower() == "on")
                {
                    var 삭제할사용자 = 사용자들[i];

                    foreach (var 계정권한 in 삭제할사용자.계정권한들.ToList())
                    {
                        this.context.계정권한들.Remove(계정권한);
                    }

                    this.context.사용자들.Remove(삭제할사용자);
                    this.context.SaveChanges();
                }
            }
        }

        public void 수정(IFormCollection form)
        {
            var 사용자들 = this.context.사용자들.Include(e => e.계정권한들).ToList();
            int 사용자수 = 사용자들.Count();

            for (int i = 0; i < 사용자수; i++)
            {
                string checkboxValue = form[$"checkbox_{i}"];

                if (!string.IsNullOrEmpty(checkboxValue) && checkboxValue.ToLower() == "on")
                {
                    var 수정할사용자 = 사용자들[i];
                    int 수정할사용자Id = 수정할사용자.Id;
                    var 사용자 = this.context.사용자들.Find(수정할사용자Id);

                    if (!string.IsNullOrEmpty(form[$"아이디"]))
                    {
                        사용자.아이디 = form[$"아이디"];
                    }
                    if (!string.IsNullOrEmpty(form[$"비밀번호"]))
                    {
                        사용자.비밀번호 = form[$"비밀번호"];
                    }
                    if (!string.IsNullOrEmpty(form[$"이메일"]))
                    {
                        사용자.이메일 = form[$"이메일"];
                    }
                    if (!string.IsNullOrEmpty(form[$"이름"]))
                    {
                        사용자.이름 = form[$"이름"];
                    }
                    if (!string.IsNullOrEmpty(form[$"소속"]))
                    {
                        사용자.이름 = form[$"소속"];
                    }
                    if (!string.IsNullOrEmpty(form[$"생년월일"]))
                    {
                        사용자.생년월일 = form[$"생년월일"];
                    }
                    if (!string.IsNullOrEmpty(form[$"전화번호"]))
                    {
                        사용자.전화번호 = form[$"전화번호"];
                    }
                    if (!string.IsNullOrEmpty(form[$"계정권한"]))
                    {
                        사용자.계정권한들 = new List<계정권한>
                            {
                                new 계정권한 { 권한분류 = form[$"계정권한"]}
                            };
                    }

                    this.context.SaveChanges();
                }
            }
        }

        public bool 중복검사(IFormCollection form)
        {
            bool result = false;

            string 아이디 = form["아이디"];

            var 사용자들 = this.context.사용자들.ToList();
            int 사용자수 = 사용자들.Count();

            for (int i = 0; i < 사용자수; i++)
            {
                var 조회할사용자 = 사용자들[i];
                if (조회할사용자 != null && 조회할사용자.아이디 == 아이디)
                {
                    result = true;
                }
            }

            return result;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") != null)
            {
                var 사용자 = 조회();
                return View(사용자);
            }

            TempData["ErrorMessage1"] = "ON";

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Create(IFormCollection form)
        {
            생성(form);

            return RedirectToAction("index");
        }

        public IActionResult Delete(IFormCollection form)
        {
            삭제(form);

            return RedirectToAction("index");
        }

        public IActionResult Update(IFormCollection form)
        {
            수정(form);

            return RedirectToAction("index");
        }
    }
}
