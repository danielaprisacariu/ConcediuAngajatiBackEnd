using Microsoft.AspNetCore.Mvc;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    public class OriceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<StareConcediuController> _logger;
        private readonly StrangerThingsContext _context;

        public OriceController(ILogger<StareConcediuController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Test")]
        public List<Angajat> GetAllStareConcediu()
        {
            return _context.Angajats.Select(sc => new Angajat() { Id = sc.Id, Nume = sc.Nume, ManagerId = sc.ManagerId}).ToList();
        }


    }
}
