using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly StrangerThingsContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet(Name = "GetWeatherForecast")]
        public List<Concediu> Get2()
        {
            return _context.Concedius.Include(x => x.TipConcediu).Select(x => new Concediu() { Id = x.Id, DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit, InlocuitorId = x.InlocuitorId, TipConcediuId = x.TipConcediuId, TipConcediu = x.TipConcediu }).Where(x => x.TipConcediu.Nume == "Odihna").ToList();
        }
    }
}