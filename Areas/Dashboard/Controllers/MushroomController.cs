using Atlas.Data;
using Atlas.Models;
using Atlas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Route("/Pulpit/Grzyby/Usun/{id}")]
        //[Route("/KasowanieGrzyba/{id}")]
        // usuwanie z bazy
        public IActionResult Delete(string id)
        {
            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.ID == Guid.Parse(id)); // wybierz z tabeli Mushrooms pierwszego napotkanego grzyba o id takim jak podane w akcji

            _context.Mushrooms.Remove(mushroom);
            _context.SaveChanges();

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

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var fileType = model.Image.ContentType;
            string orginalFileName = model.Image.FileName;

            int miejsceOstatniejKropki = orginalFileName.LastIndexOf('.');

            string nazwaWlasciwa = orginalFileName.Substring(0, miejsceOstatniejKropki);
            string rozszerzenie = orginalFileName.Substring(miejsceOstatniejKropki);

            string fileName = String.Concat(PrepareUrl(nazwaWlasciwa), rozszerzenie);

            string pathToFile = Path.Combine(path, fileName);

            using(var stream= new FileStream(pathToFile, FileMode.Create))
            {
                model.Image.CopyTo(stream);
            }
            
            Mushroom newMushroom = new();
            newMushroom.ID = Guid.NewGuid(); //nowy identyfikator typu GUID
            newMushroom.Name = model.Name;
            newMushroom.Description = model.Description;
            newMushroom.Edibility = model.Edibility;
            newMushroom.Family = model.Family;
            newMushroom.Genre = model.Genre;
            newMushroom.Kind = model.Kind;
            newMushroom.LatinName = model.LatinName;
            newMushroom.Create = DateTime.Now;
            newMushroom.Url = PrepareUrl(model.Name);
            newMushroom.Image = fileName;

            _context.Mushrooms.Add(newMushroom);
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }


        [Route("/Pulpit/Grzyby/Edycja/{id}")]
        // aktualizacja wpisu, w bazie
        public IActionResult Update(string id)
        {
            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.ID == Guid.Parse(id));

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
                MushroomOccurences = CheckOccurence(mushroom.ID),
            };

            return View(model); // wypełniony formularz z danymi wybranego grzyba
        }

        private List<MushroomOccurenceVM> CheckOccurence(Guid Id)
        {
            List<MushroomOccurenceVM> result = new();
            var occurences = _context.Occurences.ToList();

            foreach (var item in occurences)
            {
                MushroomOccurenceVM resultItem = new();
                resultItem.OccurenceId = item.ID.ToString();
                resultItem.OccurenceName = item.Name;
                resultItem.IsChecked = CheckList(Id, item.ID);

                result.Add(resultItem);
            }
            return result;
        }

        private bool CheckList(Guid mushroomId, Guid occurenceId)
        {
            bool result = false;
            var mushroomInOccurences = _context.MushroomInOccurences.Where(grzyb => grzyb.MushroomID == mushroomId).ToList();

            var item = mushroomInOccurences.FirstOrDefault(x => x.OccurenceID == occurenceId);
            if(item != null)
                result = true;

            return result;
        }


        [HttpPost]
        [Route("/Pulpit/Grzyby/Edycja")]
        public IActionResult Update(MushroomVM model)
        {
            if (!ModelState.IsValid) 
            {
                //model.MushroomOccurences = CheckOccurence();
                return View(model);
            }
            ClerMushroomInOccurences(model.ID);

            var checkboxy = Request.Form["occurences"];
            var occurences = _context.Occurences.ToList();

            List<MushroomInOccurence> inOccurences = new();

            foreach (var item in occurences)
            {
                if (checkboxy.Contains(item.Name))
                {
                    MushroomInOccurence mushroomInOccurence = new()
                    {
                        ID = Guid.NewGuid(),
                        OccurenceID = item.ID,
                        MushroomID = Guid.Parse(model.ID)
                    };

                    inOccurences.Add(mushroomInOccurence);
                }
            }

            if(inOccurences.Any())
            {
                _context.MushroomInOccurences.AddRange(inOccurences);
            }


            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(grzyb => grzyb.ID == Guid.Parse(model.ID));
            
            mushroom.Name = model.Name;
            mushroom.Description = model.Description;
            mushroom.Edibility = model.Edibility;
            mushroom.Family = model.Family;
            mushroom.Genre = model.Genre;
            mushroom.Kind = model.Kind;
            mushroom.LatinName = model.LatinName;

            _context.Mushrooms.Update(mushroom);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private void ClerMushroomInOccurences(string mushroomId)
        {
            var temp = _context.MushroomInOccurences.Where(x => x.MushroomID == Guid.Parse(mushroomId));
            if (temp.Any())
            {
                _context.MushroomInOccurences.RemoveRange(temp);
                _context.SaveChanges();
            }

        }
        private string PrepareUrl(string name)
        {
            if (String.IsNullOrEmpty(name)) // zamiast name == ""
                return String.Empty;
           
            string url = name.ToLower(); // zamiana duzych liter na małe, ToUpper na duże


            // Pieczarka łąkowa

            string[,] literyDoZamiany = {
                { " ", "-" },
                { "ó", "o" },
                { "ą", "a" },
                { "ę", "e" },
                { "ś", "s" },
                { "ł", "l" },
                { "ż", "z" },
                { "ź", "z" },
                { "ć", "c" },
                { "ń", "n" },
                { "!", "" },
                { "@", "" },
                { "#", "" },
                { "$", "" },
                { "%", "" },
                { "^", "" },
                { "&", "" },
                { "*", "" },
                { "(", "" },
                { ")", "" },
                { "_", "" },
                { "+", "" },
                { "=", "" },
                { ";", "" },
                { "'", "" },
                { ",", "" },
                { ".", "" },
                { "?", "" },
                { "|", "" },
                { "/", "" },
                { "\\", "" },
            };

            for (int i = 0; i < 31; i++)
            {
                url = url.Replace(literyDoZamiany[i,0], literyDoZamiany[i,1]);
            }
            return url;
        }

    }
}
