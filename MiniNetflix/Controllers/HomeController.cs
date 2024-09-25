using Application.IServices;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiniNetflix.Controllers;

public class HomeController : Controller
{
    private readonly ISeriesService _seriesService;
    private readonly IProducerService _producerService; 
    private readonly IGenresService _genreService; 

    public HomeController(ISeriesService seriesService, IProducerService producerService, IGenresService genreService)
    {
        _seriesService = seriesService;
        _producerService = producerService;
        _genreService = genreService;
    }

    public async Task<IActionResult> Index(string searchString, int? producerId, int? genreId)
    {
        var series = await _seriesService.GetAllSeriesAsync();

      
        if (!string.IsNullOrEmpty(searchString))
        {
            series = series.Where(s => s.Name != null && s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }
        if (producerId.HasValue)
        {
            series = series.Where(s => s.ProducerId == producerId.Value);
        }
        if (genreId.HasValue)
        {
            series = series.Where(s => s.PrimaryGenreId == genreId.Value || s.SecondaryGenreId == genreId.Value);
        }

        
        var producers = await _producerService.GetProducersAsync();
        var genres = await _genreService.GetAllGenresAsync();

        ViewBag.Producers = producers.Select(p => new SelectListItem
        {
            Value = p.Id.ToString(),
            Text = p.Name
        });

        ViewBag.Genres = genres.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Name
        });

        return View(series);
   }

    public IActionResult PlayVideo(int id)
    {
        var series = _seriesService.GetSeriesById(id); 
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
        };

        return View("PlayVideo", model);
    }

}