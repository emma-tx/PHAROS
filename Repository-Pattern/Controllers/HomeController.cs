using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DotNetCore6.Models;
using DotNetCore6.Data;
using DotNetCore6.Data.EFCore;
namespace DotNetCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [ViewData]
        public string ResultsCount { get; set; }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ComputerDetails(int id)
        {
            var model = _unitOfWork.Computers.GetById(id);
            return View(model);
        }

        public IActionResult AdminUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminUsersSearch(string searchTerm)
        {
            var model = _unitOfWork.Users.GetAll().Where(m => m.UserName.Contains(searchTerm)).ToList();
            return PartialView("_UsersSearchResults", model);
        }


        public IActionResult UserDetails(int id)
        {
            var model = _unitOfWork.Users.GetById(id);
            return View(model);
        }

        public IActionResult AdminComputers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminComputersSearch(string searchTerm)
        {
            var model = _unitOfWork.Computers.GetAll().Where(m => m.Host.Contains(searchTerm)).ToList();
            return PartialView("_ComputersSearchResults", model);
        }

        public IActionResult AdminLabs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminLabSearch(string searchTerm)
        {
            var model = _unitOfWork.Labs.GetAll().Where(m => m.Name.Contains(searchTerm)).ToList();
            return PartialView("_LabSearchResults", model);
        }

        public IActionResult LabDetails(int id)
        {
            var model = _unitOfWork.Labs.GetById(id);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}