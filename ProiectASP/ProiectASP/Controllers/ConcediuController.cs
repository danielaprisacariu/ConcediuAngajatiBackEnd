﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;
using System.Linq;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConcediuController : ControllerBase
    {
        private readonly ILogger<ConcediuController> _logger;
        private readonly StrangerThingsContext _context;

        public ConcediuController(ILogger<ConcediuController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllConcediuAngajati")]
        public List<Concediu> GetAllConcediuAngajati()
        {
            return _context.Concedius
                .Include(c => c.Angajat)
                .Include(c => c.Inlocuitor)
                .Include(c => c.StareConcediu)
                .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu { Nume = c.TipConcediu.Nume }
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume}
                )).ToList();

        }

        [HttpGet("GetConcediiAngajatiFiltrati")]
        public List<Concediu> GetAllConcediuAngajati(string? nume, string? prenume, int? idTipConcediu, int? idStareConcediu)
        {
            var v = (IQueryable<Concediu>)_context.Concedius
                                                .Include(c => c.Angajat)
                                                .Include(c => c.Inlocuitor)
                                                .Include(c => c.StareConcediu);

            if (!String.IsNullOrEmpty(nume))
            {
                v = v.Where(c => c.Angajat.Nume.ToLower().Contains(nume.ToLower()));
            }
            if (!String.IsNullOrEmpty(prenume))
            {
                v = v.Where(c => c.Angajat.Prenume.ToLower().Contains(prenume.ToLower()));
            }
            if (idTipConcediu != null)
            {
                v = v.Where(c => c.TipConcediu.Id == idTipConcediu);
            }
            if (idStareConcediu != null)
            {
                v = v.Where(c => c.StareConcediu.Id == idTipConcediu);
            }

            return v
                .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu {Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                )).ToList();

        }



        [HttpGet("GetAllConcediuManager")]
        public List<Concediu> GetAllConcediuManager()
        {
            return _context.Concedius
                .Include(c => c.Angajat)
                .Include(c => c.Inlocuitor)
                .Include(c => c.StareConcediu)
                .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume ,ManagerId = c.Angajat.ManagerId}
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu { Nume = c.TipConcediu.Nume }
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                )).ToList().Where(c=>c.Angajat.ManagerId==26).ToList();

            //_context.Concedius.Include(c => c.Angajat.Manager).Include(c => c.StareConcediu).Select(c => c).ToList();

        }

        [HttpGet("UpdateStareConcediu")]
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

        [HttpPut("InserareConcediu")]

        public void InserareConcediu([FromBody] Concediu con)
        {
            _context.Concedius.Add(con);
            _context.SaveChanges();
        
        }

      
    }
}
