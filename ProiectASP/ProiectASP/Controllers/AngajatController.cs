﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;
using System.ComponentModel.DataAnnotations;

namespace ProiectASP.Controllers
{
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
        public List<Angajat> GetAllAngajati([FromQuery]int id)
        {
            return _context.Angajats.Select(a => new Angajat() { Id = a.Id, Nume = a.Nume, Prenume = a.Prenume, Email = a.Email, Parola = a.Parola, DataAngajare = a.DataAngajare, DataNasterii = a.DataNasterii, Cnp = a.Cnp, Serie = a.Serie, No = a.No, NrTelefon = a.NrTelefon, EsteAdmin = a.EsteAdmin, ManagerId = a.ManagerId, DepartamentId = a.DepartamentId, FunctieId = a.FunctieId }).ToList();
        }

        [HttpGet("GetAngajatByUsername")]

        public Angajat GetAllAngajatByUsername([FromQuery]string username, [FromQuery] string parola)
        {
            Angajat a =  _context.Angajats.Select(a => a).Where(a => a.Email.Equals(username)).FirstOrDefault();
            if (a != null)
            {
                if(a.Parola.Equals(parola))
                {
                    return a;
                }
              
            }
            return null;
        }
        [HttpGet("GetAllManagers")]
        public List<Angajat> GetAllManagers()
        {
            return _context.Angajats.Include(a =>a.Manager).Select(a=>a).Where(a=>a.ManagerId==26).Where(a=>a.Id!=26).ToList();
        }
        [HttpGet("GetManagersAngajat")]
        public List<Angajat> GetManagersAngajat([FromQuery]int idManag)
        {
            return _context.Angajats.Include(a => a.Manager).Select(a => a).Where(a => a.ManagerId == idManag).Where(a => a.Id != idManag).ToList();
        }
        [HttpPut("PutConcediat")]
        public bool PutConcediat([FromQuery]  int idAngajat)
        {
            bool returnedBool = false;
            Angajat angajat = _context.Angajats.Where(a => a.Id == idAngajat).FirstOrDefault();
            if (angajat!=null){
                angajat.concediat = true;
                _context.SaveChanges();
                returnedBool = true;
            }
            return returnedBool;
        }
    }
}
