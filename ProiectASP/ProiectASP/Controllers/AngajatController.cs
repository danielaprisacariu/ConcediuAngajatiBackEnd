using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngajatController : ControllerBase
    {


        private readonly ILogger<AngajatController> _logger;
        private readonly StrangerThingsContext _context;

        public AngajatController(ILogger<AngajatController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllAngajati")]
        public List<Angajat> GetAllAngajati()
        {
            return _context.Angajats
                           .Include(dp => dp.Departament)
                           .Include(f => f.Functie)
                           .Include(mn => mn.Manager)
                           .Select(a => new Angajat(a.Id, a.Nume, a.Prenume, a.Email, a.DataAngajare, a.DataNasterii, a.Cnp, a.Serie, a.No, a.NrTelefon, a.Poza
            , new Angajat { Id = a.Manager.Id, Nume = a.Manager.Nume, Prenume = a.Manager.Prenume }
            , new Departament { Id = a.Departament.Id, Denumire = a.Departament.Denumire }
            , new Functie { Id = a.Functie.Id, Denumire = a.Functie.Denumire }))
            .ToList();
        }


        [HttpGet("GetAngajatById")]
        public Angajat GetAngajatById([FromQuery] int id)
        {
            return _context.Angajats
                .Include(a => a.Departament)
                .Include(a => a.Functie)
                .Where(a => a.Id == id)
                .Select(a => new Angajat(a.Id, a.Nume, a.Prenume, a.Email, a.DataAngajare, a.DataNasterii, a.Cnp, a.Serie, a.No, a.NrTelefon, a.Poza
                , new Departament { Denumire = a.Departament.Denumire }
                , new Functie { Denumire = a.Functie.Denumire }
                )).FirstOrDefault();
        }

        [HttpGet("GetAllAngajatiNumeConcatenat")]
        public List<Angajat> GetAllAngajatiNumeConcatenat()
        {
            return _context.Angajats.Select(a => new Angajat() { Id = a.Id, Nume = a.Nume + " " + a.Prenume, Email = a.Email, Parola = a.Parola, DataAngajare = a.DataAngajare, DataNasterii = a.DataNasterii, Cnp = a.Cnp, Serie = a.Serie, No = a.No, NrTelefon = a.NrTelefon, EsteAdmin = a.EsteAdmin, ManagerId = a.ManagerId, DepartamentId = a.DepartamentId, FunctieId = a.FunctieId }).ToList();
        }


        [HttpGet("GetAngajatByUsername")]

        public Angajat GetAllAngajatByUsername([FromQuery] string username, [FromQuery] string parola)
        {
            Angajat a = _context.Angajats.Select(a => a).Where(a => a.Email.Equals(username)).FirstOrDefault();
            if (a != null)
            {
                if (a.Parola.Equals(parola))
                {
                    return a;
                }

            }
            return null;
        }
        [HttpGet("GetAllManagers")]
        public List<Angajat> GetAllManagers()
        {
            return _context.Angajats.Select(a => a).Where(a => a.ManagerId == 26).Where(a => a.Id != 26).ToList();
        }
        [HttpGet("GetManagersAngajat")]
        public List<Angajat> GetManagersAngajat([FromQuery] int idManag)
        {
            return _context.Angajats.Include(a => a.Manager).Select(a => a).Where(a => a.ManagerId == idManag).Where(a => a.Id != idManag).ToList();
        }
        [HttpPut("PutConcediat")]
        public bool PutConcediat([FromQuery] int idAngajat)
        {
            bool returnedBool = false;
            Angajat angajat = _context.Angajats.Where(a => a.Id == idAngajat).FirstOrDefault();
            if (angajat != null)
            {
                angajat.concediat = true;
                _context.SaveChanges();
                returnedBool = true;
            }
            return returnedBool;
        }
        


        [HttpGet("GetInlocuitori")]

        public List<Angajat> GetInlocuitori([FromQuery] int idAngajat, [FromQuery] int idManager)
        {
            return _context.Angajats
                .Select(a => new Angajat { Id = a.Id, Nume = a.Nume, Prenume = a.Prenume, ManagerId = a.ManagerId }
                 ).Where(a => a.ManagerId == idManager && a.Id != idAngajat).ToList();
        }
        [HttpPost("PostConcediat")]
        public void PostConcediat([FromBody] Angajat ang)
        {

            Angajat angajatConcediat = _context.Angajats.Select(a => a).Where(a => a.Id == ang.Id).FirstOrDefault();
            if (angajatConcediat != null)
            {
                angajatConcediat.concediat= true;
            
                _context.SaveChanges();
               
            }
       
        }

        [HttpGet("GetAllInlocuitoriNumeConcatenat")]
        public List<Angajat> GetAllAngajatiNumeConcatenat([FromQuery] int angajatId, int managerId)
        {
            return _context.Angajats
                .Select(a => new Angajat() { Id = a.Id, Nume = a.Nume + " " + a.Prenume, ManagerId = a.ManagerId, concediat = a.concediat })
                .Where(a => a.Id != angajatId && a.ManagerId == managerId && a.concediat == false)
                .ToList();
        }

        [HttpPost("PostTransfer")]
        public void PostTransfer([FromQuery] int AngajatId,int managerId)
        {

            Angajat angajatTransferat = _context.Angajats.Select(a => a).Where(a => a.Id == AngajatId).FirstOrDefault();
             angajatTransferat.ManagerId = managerId;
            _context.SaveChanges();
        }
        [HttpPost("PostStergereEchipa")]
        public void PostStergereEchipa([FromQuery] int angajatId)
        {

            List<Angajat> angajati = _context.Angajats.Select(x => x).Where(x => x.ManagerId == angajatId).ToList();
            angajati.Add(_context.Angajats.Select(x => x).Where(x => x.Id == angajatId).FirstOrDefault());
            foreach (Angajat asn in angajati)
            {
               asn.ManagerId = 30;

            }
            _context.SaveChanges();
        }
        [HttpPut("UpdateDateleMele")]

        public void UpdateDateleMele([FromBody] Angajat ang)
        {
            Angajat angajat = _context.Angajats
                .Select(a => a)
                .Where(a => a.Id == ang.Id)
                .FirstOrDefault();
            angajat.NrTelefon = ang.NrTelefon;
            angajat.Poza = ang.Poza;
            //angajat.Poza = ang.Poza;

            _context.SaveChanges();
        }

        
    }
}
