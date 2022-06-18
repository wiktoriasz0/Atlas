using Atlas.Data;
using Atlas.Models;
using Atlas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Atlas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext content)
        {
            _context = content;
        }
        [Route("/")]
        public IActionResult Index()
        {
            List<Mushroom> allMushrooms = _context.Mushrooms.ToList(); // odczytanie z tabeli wszystkich rekordów

            List<MushroomsListVM> nameOnly = new(); // utworzenie nowej PUSTEJ listy

            foreach (var item in allMushrooms)
            {
                MushroomsListVM oneName = new(); // tworzę nowy obiekt typu MushroomListVM, obiekt o nazwie oneName
                oneName.Name = item.Name;// do obiektu oneName do pola Name wpisuję wartość pola Name z obiektu item
                oneName.ID = item.ID.ToString();
                oneName.Url = item.Url;
                oneName.Image = item.Image;
                nameOnly.Add(oneName); // dodaję do listy nameOnly obiekt 'onename' utworzony 2 linijki wcześniej
            }

            return View(nameOnly);  // widok listy grzybów
        }

        [Route("/Grzyb/{url}")]
        public IActionResult Detail(string url)
        {
            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.Url == url);

            if (mushroom == null)
                return View("NoMushroom");

            MushroomVM model = new()
            {
                Description = mushroom.Description,
                Edibility = mushroom.Edibility,
                Family = mushroom.Family,
                Genre = mushroom.Genre,
                Kind = mushroom.Kind,
                LatinName = mushroom.LatinName,
                Name = mushroom.Name,
                ID = mushroom.ID.ToString(),
                ImageName = mushroom.Image
            };

            return View(model); // wypełniony formularz z danymi wybranego grzyba
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
