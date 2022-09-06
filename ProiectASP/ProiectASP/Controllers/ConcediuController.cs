using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    public class ConcediuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<ConcediuController> _logger;
        private readonly StrangerThingsContext _context;

        public ConcediuController(ILogger<ConcediuController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }
        //here

        [HttpGet("GetAllConcediuAngajati")]
        public List<Concediu> GetAllConcediuAngajati()
        {
            return _context.Concedius.Include(c => c.Angajat).Include(c => c.Inlocuitor).Include(c => c.StareConcediu).Select(c => c).ToList();
            //_context.Concedius.Include(c => c.Angajat.Manager).Include(c => c.StareConcediu).Select(c => c).ToList();

        }

        [HttpPut("PutConcediu")]
        public bool UpdateStareCncediu([FromQuery]int idConcediu, [FromQuery] int idStareConcediu)
        {
            Concediu concediu = _context.Concedius.Where(c => c.Id == idConcediu).FirstOrDefault();
            if(concediu != null)
            {
                concediu.StareConcediuId = idStareConcediu;

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        [HttpGet("GetAllIstoricConcedii")]

        public List <Concediu> GetAllIstoricConcedii()
        {
            return _context.Concedius.Include(c => c.Angajat.Manager).Include(c => c.StareConcediu).Include(c => c.TipConcediu).Select(c => c).ToList();
        }
    }
}
