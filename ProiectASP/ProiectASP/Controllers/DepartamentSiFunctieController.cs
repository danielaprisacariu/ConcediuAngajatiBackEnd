using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentSiFunctieController : ControllerBase
    {

        private readonly ILogger<DepartamentSiFunctieController> _logger;
        private readonly StrangerThingsContext _context;
        public DepartamentSiFunctieController(ILogger<DepartamentSiFunctieController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("GetAllDepartaments")]
        public List<Departament> GetAllDepartaments()
        {
            return _context.Departaments.Select(a => a).ToList();
        }
        [HttpGet("GetAllFuncties")]
        public List<Functie> GetAllFuncties()
        {
            return _context.Functies.Select(a => a).ToList();
        }

        [HttpGet("GetFunctieDepartament")]

        public List<Angajat> GetAllFunctieDepartament([FromQuery] int angajatId)
        {
            return _context.Angajats
                .Include(a => a.Departament)
                .Include(a => a.Functie)
                .Where(a => a.Id == angajatId)
                .Select(a => new Angajat(a.Id
                , new Departament { Id = a.Departament.Id, Denumire = a.Departament.Denumire }
                , new Functie { Id = a.Functie.Id, Denumire = a.Functie.Denumire }))

                .ToList();
        }
        [HttpPost("PostFunctie")]
        public void PostFunctie([FromQuery]int AngajatId,int functieID)
        {
            Angajat angajatModificat = _context.Angajats.Select(a => a).Where(a => a.Id == AngajatId).FirstOrDefault();
           angajatModificat.Functie=_context.Functies.Select(f=>f).Where(f=>f.Id==functieID).FirstOrDefault();

            _context.SaveChanges();

        }

        [HttpPost("PostDepartament")]
        public void PostDepartament([FromQuery] int AngajatId, int DepartamentID)
        {
            Angajat angajatModificat = _context.Angajats.Select(a => a).Where(a=>a.Id == AngajatId).FirstOrDefault();
            angajatModificat.Departament=_context.Departaments.Select(d=>d).Where(d=>d.Id==DepartamentID).FirstOrDefault();
            _context.SaveChanges();

        }
    }
}
