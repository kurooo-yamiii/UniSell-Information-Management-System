using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUniform.Interface;
using WebUniform.Models;
using WebUniform.ViewModel;

namespace WebUniform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardRepository _dashboardRepository;
        public HomeController(ILogger<HomeController> logger, IDashboardRepository dashboardRepository)
        {
            _logger = logger;
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var curUser = HttpContext.Session.GetString("UserId");
            int.TryParse(curUser, out int userId);

            List<Slack> slacks = await _dashboardRepository.GetSlacksByUserID(userId);
            List<Uniform> uniforms = await _dashboardRepository.GetUniformsByUserID(userId);
           
            var viewModel = new DashboardViewModel
            {
                Slacks = slacks,
                Uniforms = uniforms
            };

            return View(viewModel);

        }

        public async Task<IActionResult> Home()
        {
            List<Slack> slacks = await _dashboardRepository.GetRecentSlack();
            List<Uniform> uniforms = await _dashboardRepository.GetRecentUniform();

            var viewModel = new DashboardViewModel
            {
                Slacks = slacks,
                Uniforms = uniforms
            };

            return View(viewModel);
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