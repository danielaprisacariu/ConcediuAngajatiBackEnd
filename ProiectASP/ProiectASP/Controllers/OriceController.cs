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

    }
}

