using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    public class AngajatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<AngajatController> _logger;
        private readonly StrangerThingsContext _context;

        public AngajatController(ILogger<AngajatController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet("GetAllAngajati/{:id}")]
        //public List<Angajat> GetAllAngajati(int id)
        //{
        //    return _context.Angajats.Select(a => new Angajat() { Id = a.Id, Nume = a.Nume, Prenume = a.Prenume, Email = a.Email, Parola = a.Parola, DataAngajare = a.DataAngajare, DataNasterii = a.DataNasterii, Cnp = a.Cnp, Serie = a.Serie, No = a.No, NrTelefon = a.NrTelefon, EsteAdmin = a.EsteAdmin, ManagerId = a.ManagerId, DepartamentId = a.DepartamentId, FunctieId = a.FunctieId}).ToList();
        //}
    }
}
