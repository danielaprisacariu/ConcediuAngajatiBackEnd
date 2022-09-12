using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetNrTotalConcedii")]
        public int GetAllConcediuAngajati(string? nume, string? prenume, int? idTipConcediu, int? idStareConcediu,bool EsteAdmin,int idManager)
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
                v = v.Where(c => c.StareConcediu.Id == idStareConcediu);
            }
            if (!EsteAdmin)
            {
                v = v.Where(c => c.Angajat.ManagerId == idManager);
                v = v.Where(c => c.Angajat.Id != idManager);
            }

            v = v.Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu { Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                ));

            return v.Count();
            //return v
            //    .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
            //    , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
            //    , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
            //    , new TipConcediu {Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
            //    , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
            //    )).Skip(nrInceputInregistrari).Take(nrTotalInregistrariDeAdus).ToList();

        }


        [HttpGet("GetConcediiAngajatiFiltrati")]
        public List<Concediu> GetAllConcediuAngajati(string? nume, string? prenume, int? idTipConcediu, int? idStareConcediu, int? nrInceputInregistrari, int? nrTotalInregistrariDeAdus, bool EsteAdmin,int idManager)
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
                v = v.Where(c => c.StareConcediu.Id == idStareConcediu);
            }
            if (!EsteAdmin)
            {
                v = v.Where(c => c.Angajat.ManagerId == idManager);
                v = v.Where(c => c.Angajat.Id != idManager);
            }

            v = v.Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                , new TipConcediu { Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
                , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                ));

            if (nrInceputInregistrari != null && nrTotalInregistrariDeAdus != null)
            {
                return v.Skip((int)nrInceputInregistrari).Take((int)nrTotalInregistrariDeAdus).ToList();
            }
            else
            {
                return v.ToList();
            }

            //return v
            //    .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
            //    , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
            //    , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
            //    , new TipConcediu {Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
            //    , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
            //    )).Skip(nrInceputInregistrari).Take(nrTotalInregistrariDeAdus).ToList();

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

        public List <Concediu> GetAllIstoricConcedii([FromQuery] int angajatId, int? nrInceputInregistrari, int? nrTotalInregistrariDeAdus)
        {
            var v = (IQueryable<Concediu>)_context.Concedius
                 .Include(c => c.Angajat)
                 .Include(c => c.Inlocuitor)
                 .Include(c => c.StareConcediu)
                 .Include(c => c.TipConcediu);
            if (angajatId != null)
            {
                v = v.Where(c => c.Angajat.Id == angajatId);
            }

            v = v.Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                  , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                  , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                  , new TipConcediu { Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
                  , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                  ));
            if (nrInceputInregistrari != null && nrTotalInregistrariDeAdus != null)
            {
                return v.Skip((int)nrInceputInregistrari).Take((int)nrTotalInregistrariDeAdus).ToList();
            }
            else
            {
                return v.ToList();
            }

        }

        [HttpGet("GetAllIstoricConcediiVerificareDate")]
        public List<Concediu> GetAllIstoricConcediiVerificareDate([FromQuery] int angajatId)
        {
            return _context.Concedius
                .Include(c => c.Angajat)
                .Include(c => c.StareConcediu)
                .Where(c => c.Angajat.Id == angajatId && c.StareConcediu.Id != 3)
                .Select(c => new Concediu { Id = c.Id, DataInceput = c.DataInceput, DataSfarsit = c.DataSfarsit })
                .ToList();
        }

        [HttpGet("GetConcediiInlocuitori")]
        public List<Concediu> GetConcediiInLocuitori([FromQuery] int angajatId)
        {
            int idManager = (int) _context.Angajats
                             .Where(a => a.Id == angajatId)
                             .Select(a => a.ManagerId).FirstOrDefault();

            return _context.Concedius
                .Include(c => c.Angajat)
                .Where(c => c.Angajat.Id != angajatId && c.Angajat.ManagerId == idManager)
                .Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit
                , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume, ManagerId = c.Angajat.ManagerId }
                
              )).ToList();
        }

        [HttpGet("GetNumarZileConcediuRamase")]

        public int GetNumarZileConcediuRamase([FromQuery] int tipConcediuId, [FromQuery] int angajatId)
        {
            int sumaZileConcediu = (int)_context.Concedius
                .Where(c => c.TipConcediuId == tipConcediuId && c.StareConcediuId == 2 && c.AngajatId == angajatId)
                .Select(c => c.ZileConcediu)
                .Sum();

            int sumaZileDisponibile = (int)_context.TipConcedius
                .Where(c => c.Id == tipConcediuId)
                .Select(c => c.ZileTotaleConcediu)
                .FirstOrDefault();
            return sumaZileDisponibile - sumaZileConcediu;
        }

        [HttpGet("GetNrTotalIstoricConcedii")]
        public int GetAllIstoricConcediuAngajati(int idAngajat)
        {
            var v = (IQueryable<Concediu>)_context.Concedius
                                                .Include(c => c.Angajat)
                                                .Include(c => c.Inlocuitor)
                                                .Include(c => c.StareConcediu);

            if (idAngajat != null)
            {
                v = v.Where(c => c.Angajat.Id == idAngajat);
            }
            v = v.Select(c => new Concediu(c.Id, c.DataInceput, c.DataSfarsit, c.Comentarii
                    , new Angajat { Id = c.Angajat.Id, Nume = c.Angajat.Nume, Prenume = c.Angajat.Prenume }
                    , new Angajat { Id = c.Inlocuitor.Id, Nume = c.Inlocuitor.Nume, Prenume = c.Inlocuitor.Prenume }
                    , new TipConcediu { Id = c.TipConcediu.Id, Nume = c.TipConcediu.Nume }
                    , new StareConcediu { Id = c.StareConcediu.Id, Nume = c.StareConcediu.Nume }
                    ));

            return v.Count();
        }

            [HttpPut("InserareConcediu")]

        public void InserareConcediu([FromBody] Concediu con)
        {
            _context.Concedius.Add(con);
            _context.SaveChanges();
        
        }



      
    }

}
