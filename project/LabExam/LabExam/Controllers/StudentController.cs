using LabExam.DataSource;
using LabExam.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabExam.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IEncryptionDataService _ncryption;
        private readonly LabContext _context;
        private readonly IHttpContextAccessor _accessor;

        public StudentController(IEncryptionDataService ncryption, LabContext context, IHttpContextAccessor accessor)
        {
            _ncryption = ncryption;
            _context = context;
            _accessor = accessor;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


  
    }
}
