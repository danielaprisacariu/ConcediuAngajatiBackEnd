using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;
using System.Linq;

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
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("GetAllIstoricConcedii")]

        public List <Concediu> GetAllIstoricConcedii()
        {
            return _context.Concedius
                .Include(c => c.Angajat)
                .Include(c => c.Inlocuitor)
                .Include(c => c.StareConcediu)
                .Include(c => c.TipConcediu)
                .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume}
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu { Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume}
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                ) )

                .ToList();
        }

      
    }
}
