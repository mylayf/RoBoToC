using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoBoToC.WebUI.ApiProcess.Abstract;
using RoBoToC.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RoBoToC.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserApiService userApiService;
        public HomeController(IUserApiService UserApiService, ILogger<HomeController> logger)
        {
            _logger = logger;
            userApiService = UserApiService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var response = await userApiService.Login(new Entity.Dto.User.UserLoginDto
            {
                Password = "1s2a3f4a",
                Username = "mylayf"
            }, cancellationToken);
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
