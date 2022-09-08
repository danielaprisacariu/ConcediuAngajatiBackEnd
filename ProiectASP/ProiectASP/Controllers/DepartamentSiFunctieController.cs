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
    }
}
