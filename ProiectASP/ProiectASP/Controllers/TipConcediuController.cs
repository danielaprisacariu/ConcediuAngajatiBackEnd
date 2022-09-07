using Microsoft.AspNetCore.Mvc;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipConcediuController : ControllerBase
    {

        private readonly ILogger<TipConcediuController> _logger;
        private readonly StrangerThingsContext _context;

        public TipConcediuController(ILogger<TipConcediuController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllTipConcediu")]
        public List<TipConcediu> GetAllTipConcediu()
        {
            return _context.TipConcedius.Select(tc => tc ).ToList();
        }

    }

}
