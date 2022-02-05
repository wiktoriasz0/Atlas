using Atlas.Data;
using Atlas.Models;
using Atlas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class OccurenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OccurenceController(ApplicationDbContext content)
        {
            _context = content;
        }
        [Route("/Pulpit/Wystepowanie")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Pulpit/Wystepowanie/Nowy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("/Pulpit/Wystepowanie/Nowy")]
        public IActionResult Create(OccurenceVM model)
        {
            Occurence newOccurence = new();
            newOccurence.ID = Guid.NewGuid(); //nowy identyfikator typu GUID
            newOccurence.Name = model.Name;

            _context.Occurences.Add(newOccurence);
            _context.SaveChanges();
            return View();
        }

    }
}
