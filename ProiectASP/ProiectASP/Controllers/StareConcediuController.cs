﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectASP.Models;

namespace ProiectASP.Controllers
{
    public class StareConcediuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILogger<StareConcediuController> _logger;
        private readonly StrangerThingsContext _context;

        public StareConcediuController(ILogger<StareConcediuController> logger, StrangerThingsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetAllStareConcediu")]
        public List<StareConcediu> GetAllStareConcediu()
        {
            return _context.StareConcedius.Select(sc => new StareConcediu() { Id = sc.Id, Nume = sc.Nume, Cod = sc.Cod }).ToList();
        }

    }
}
