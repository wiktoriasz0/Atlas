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
    public class MushroomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MushroomController(ApplicationDbContext content)
        {
            _context = content;
        }

        [Route("/Pulpit/Grzyby")]
        // lista grzybów, które są w bazie
        public IActionResult Index()
        {
            List<Mushroom> allMushrooms = _context.Mushrooms.ToList(); // odczytanie z tabeli wszystkich rekordów

            List<MushroomsListVM> nameOnly = new(); // utworzenie nowej PUSTEJ listy

            foreach(var item in allMushrooms)
            {
                MushroomsListVM oneName = new(); // tworzę nowy obiekt typu MushroomListVM, obiekt o nazwie oneName
                oneName.Name = item.Name;// do obiektu oneName do pola Name wpisuję wartość pola Name z obiektu item
                oneName.ID = item.ID.ToString();
                nameOnly.Add(oneName); // dodaję do listy nameOnly obiekt 'onename' utworzony 2 linijki wcześniej
            }

            return View(nameOnly);  // widok listy grzybów
        }

        [Route("/Pulpit/Grzyby/Usun")]
        // usuwanie z bazy
        public IActionResult Delete()
        {
            return View(); // potwierdzenie usunięcia z bazy
        }

        [Route("/Pulpit/Grzyby/Nowy")]
        // utworzenie nowego wpisu - dodanie grzyba do bazy
        public IActionResult Create()
        {
            return View(); // pusty formularz do wypełnienia
        }

        [HttpPost]
        [Route("/Pulpit/Grzyby/Nowy")]
        // utworzenie nowego wpisu - dodanie grzyba do bazy
        public IActionResult Create(MushroomVM model)
        {
            Mushroom newMushroom = new();
            newMushroom.ID = Guid.NewGuid(); //nowy identyfikator typu GUID
            newMushroom.Name = model.Name;
            newMushroom.Description = model.Description;
            newMushroom.Edibility = model.Edibility;
            newMushroom.Family = model.Family;
            newMushroom.Genre = model.Genre;
            newMushroom.Kind = model.Kind;
            newMushroom.LatinName = model.LatinName;
            newMushroom.Occurence = model.Occurence;
            newMushroom.Create = DateTime.Now;

            _context.Mushrooms.Add(newMushroom);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
            //return View(); 
        }




        [Route("/Pulpit/Grzyby/Edycja")]
        // aktualizacja wpisu, w bazie
        public IActionResult Update()
        {
            return View(); // wypełniony formularz z danymi wybranego grzyba
        }
    }
}
