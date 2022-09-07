using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Angajats.Select(sc => new Angajat() { Id = sc.Id, Nume = sc.Nume, ManagerId = sc.ManagerId }).ToList();
        }


        [HttpGet("TotiAngajatii")]

        public List<Angajat> GetAllAngajati()
        {
            return _context.Angajats.Include(dp => dp.Departament).Include(mn => mn.Manager).Select(sc => sc).ToList();
        }

        [HttpGet("GetManagerId")]

        public List<Angajat> GetAllManagerId()
        {
            return _context.Angajats.Select(sc => new Angajat() { ManagerId = sc.ManagerId }).ToList();
        }

        //[HttpGet("GetAngajatId")]

        /* public bool VerificareId(int Id)
         {
            List <Angajat> a = (List<Angajat>)_context.Angajats.Select( sc => sc).Where( sc => sc.ManagerId == 26) ;
             //return (a = 26);
         }*/
    }
}

