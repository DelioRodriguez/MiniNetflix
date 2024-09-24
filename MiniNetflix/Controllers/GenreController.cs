using Application.IServices;
using Application.ViewModel;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MiniNetflix.Controllers
{
    public class GenreController : Controller
    {
     private readonly IGenresService _genresService;

     public GenreController(IGenresService genresService)
     {
         _genresService = genresService;
     }

     public IActionResult Index()
     {
         var genres = _genresService.GetAllGenres();
         return View(genres);
     }

     public IActionResult Detail(int id)
     {
         var genre = _genresService.GetById(id);
         if (genre == null)
         {
             return NotFound();

         }
         return View(genre);
     }

     public IActionResult Create()
     {
         return View();
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Create(GenreViewModel genre)
     {
         if (ModelState.IsValid)
         {
             _genresService.Add(genre);
             return RedirectToAction(nameof(Index));
         }
         return View(genre);
     }

     public IActionResult Edit(int id)
     {
         var genre = _genresService.GetById(id);
         if (genre == null)
         {
             return NotFound();
         }
         return View(genre);
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Edit(int id, GenreViewModel genre)
     {
         if (id != genre.Id)
         {
             return NotFound();
         }

         if (ModelState.IsValid)
         {
             _genresService.Update(genre);
             return RedirectToAction(nameof(Index));
         }
         return View(genre);
     }

     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Delete(int id)
     {
         var genre = _genresService.GetById(id);
         if (genre == null)
         {
             return NotFound();
         }

        
         if (_genresService.HasSeries(id))
         {
      
             ModelState.AddModelError("", "No se puede eliminar el género porque tiene series asociadas.");
             return RedirectToAction(nameof(Index));
         }

       
         _genresService.Delete(id);

         return RedirectToAction(nameof(Index));
     }


    }
}
