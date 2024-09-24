using Application.IServices;
using Application.Services;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniNetflix.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesService _seriesService;
        private readonly IProducerService _producerService;
        private readonly IGenresService _genreService; 

        public SeriesController(ISeriesService seriesService, IProducerService producerService, IGenresService genreService)
        {
            _seriesService = seriesService;
            _producerService = producerService;
            _genreService = genreService;
        }

        public IActionResult Index()
        {
            var series = _seriesService.GetAllSeries();
            return View(series);
        }

        public IActionResult Details(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }
            return View(series);
        }

        public IActionResult Create()
        {
            var model = new SeriesViewModel
            {
                Producers = GetProducers(),
                PrimaryGenres = GetPrimaryGenres(),
                SecondaryGenres = GetSecondaryGenres()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SeriesViewModel seriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                seriesViewModel.Producers = GetProducers();
                seriesViewModel.PrimaryGenres = GetPrimaryGenres();
                seriesViewModel.SecondaryGenres = GetSecondaryGenres();
                return View(seriesViewModel);
            }

            _seriesService.Add(seriesViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }

            var model = new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                VideoLink = series.VideoLink,
                ImgLink = series.ImgLink,
                ProducerId = series.ProducerId,
                PrimaryGenreId = series.PrimaryGenreId,
                SecondaryGenreId = series.SecondaryGenreId,
                Producers = GetProducers(),
                PrimaryGenres = GetPrimaryGenres(),
                SecondaryGenres = GetSecondaryGenres()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SeriesViewModel seriesViewModel)
        {
            if (!ModelState.IsValid)
            {
               
                seriesViewModel.Producers = GetProducers();
                seriesViewModel.PrimaryGenres = GetPrimaryGenres();
                seriesViewModel.SecondaryGenres = GetSecondaryGenres();
                return View(seriesViewModel);
            }

            _seriesService.Update(seriesViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }
            return View(series);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _seriesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetProducers()
        {
            var producers = _producerService.GetAllProducer(); 
            return producers.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
        }

        private List<SelectListItem> GetPrimaryGenres()
        {
            var genres = _genreService.GetAllGenres();
            return genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
        }

        private List<SelectListItem> GetSecondaryGenres()
        {
            var genres = _genreService.GetAllGenres(); 
            return genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
        }
    }
}
