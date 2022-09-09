using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OriceController : ControllerBase
    {

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

        [HttpPut("PutNewAngajat")]

        public void PutNewAngajat([FromBody] Angajat ang)
        {
           /* Angajat a = new Angajat();
            a.Nume = ang.Nume;
            a.Prenume = ang.Prenume;
            a.Cnp = ang.Cnp;
            a.Email = ang.Email;
            a.DataNasterii = ang.DataNasterii;
            a.No = ang.No;
            a.Serie = ang.Serie;
            a.NrTelefon = ang.NrTelefon;
            a.Parola = ang.Parola;
            a.DepartamentId = 7;
            a.FunctieId = 5;
            a.ManagerId = 30;
            a.DataAngajare = DateTime.Now;*/

            _context.Angajats.Add(ang);
            _context.SaveChanges();
        }

       /* [HttpPost("UpdateManagerId")]  
        public void UpdateAngajat(Angajat a)
        {
          
             List<Angajat> angajati = _context.Angajats.Select(x => x).Where(x => x.ManagerId == a.Id).ToList();
            angajati.Add(_context.Angajats.Select(x => x).Where(x =>  x.Id == a.Id).FirstOrDefault());
            foreach (Angajat asn in angajati)
            {
                asn.ManagerId = 30;
                
            }         
        }*/

    }
}

