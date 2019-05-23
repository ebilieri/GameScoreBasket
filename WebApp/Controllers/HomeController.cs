using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;          
        }

        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            try
            {
                var user =  _userManager.GetUserId(HttpContext.User);
                var name = _userManager.GetUserName(HttpContext.User);
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception) { }      

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
