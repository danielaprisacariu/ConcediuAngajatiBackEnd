using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;
using System.Linq;

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
            return _context.Angajats
                .Include(dp => dp.Departament)
                .Include(mn => mn.Manager)
                .Where(sc => sc.Manager.Id != 26)
                .Select(sc => new Angajat(sc.Id, sc.Nume, sc.Prenume, sc.Email
                , new Angajat { Id = sc.Manager.Id, Nume = sc.Manager.Nume, Prenume = sc.Manager.Prenume }
                , new Departament { Id = sc.Departament.Id, Denumire = sc.Departament.Denumire }))

                .ToList();
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

        [HttpGet("NrTotalAngajati")]

        public int NrTotalAngajati(string? nume, string? prenume, int? IdDepartamentSelectat, int? IdManagerSelectat)
        {
            var v = (IQueryable<Angajat>)_context.Angajats
                                                .Include(c => c.Departament)
                                                .Include(c => c.Manager);

            if (!String.IsNullOrEmpty(nume))
            {
                v = v.Where(c => c.Nume.ToLower().Contains(nume.ToLower()));

            }
            if (!String.IsNullOrEmpty(prenume))
            {
                v = v.Where(c => c.Prenume.ToLower().Contains(prenume.ToLower()));
            }
            if (IdManagerSelectat != null)
            {
                v = v.Where(c => c.ManagerId == IdManagerSelectat);
            }
            if (IdDepartamentSelectat != null)
            {
                v = v.Where(c => c.DepartamentId == IdDepartamentSelectat);
            }
            v = v.Where(a => a.concediat == false);
            v.Select(a => new Angajat(a.Id, a.Nume, a.Prenume, a.Email,
                                             new Angajat { Id = a.Manager.Id, Nume = a.Manager.Nume, Prenume = a.Manager.Prenume },
                                             new Departament { Id = a.Departament.Id, Denumire = a.Departament.Denumire }));

            return v.Count();
        }

        [HttpGet("GetAngajatiFiltrat")]

        public List<Angajat> GetAllAngajatiFiltrati(string? nume, string? prenume, int? IdDepartamentSelectat, int? IdManagerSelectat, int? NrInregistrari, int? NrTotalAdus)
        {
            var v = (IQueryable<Angajat>)_context.Angajats
                                                .Include(c => c.Departament)
                                                .Include(c => c.Manager);/**/


            if (!String.IsNullOrEmpty(nume))
            {
                v = v.Where(c => c.Nume.ToLower().Contains(nume.ToLower()));

            }
            if (!String.IsNullOrEmpty(prenume))
            {
                v = v.Where(c => c.Prenume.ToLower().Contains(prenume.ToLower()));
            }
            if (IdManagerSelectat != null)
            {
                v = v.Where(c => c.ManagerId == IdManagerSelectat);
            }
            if (IdDepartamentSelectat != null)
            {
                v = v.Where(c => c.DepartamentId == IdDepartamentSelectat);
            }
            v = v.Where(a => a.concediat == false);

            v = v.Select(a => new Angajat(a.Id, a.Nume, a.Prenume, a.Email,
                                            new Angajat { Id = a.Manager.Id, Nume = a.Manager.Nume, Prenume = a.Manager.Prenume },
                                            new Departament { Id = a.Departament.Id, Denumire = a.Departament.Denumire }))
      ;


            if (NrInregistrari != null && NrTotalAdus != null)
            {
                
                return v.Skip((int)NrInregistrari).Take((int)NrTotalAdus).ToList();
            }
            else
            {
                return v.ToList();
            }
        }
    }
}



