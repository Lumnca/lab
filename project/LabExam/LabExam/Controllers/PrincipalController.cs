using LabExam.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Principal")]
    public class PrincipalController : Controller
    {
        private readonly LabContext _context;

        public PrincipalController(LabContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

    }
}
