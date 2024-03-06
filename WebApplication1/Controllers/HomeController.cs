using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using TestAPP.Controllers.Login;
using TestAPP.Data;
using TestAPP.Models.Login;

namespace TestAPP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
