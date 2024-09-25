using Application.IServices;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MiniNetflix.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        public IActionResult Index()
        {
            var producers = _producerService.GetAllProducer();
            return View(producers);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(producerViewModel);
            }

            _producerService.Add(producerViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var producer = _producerService.GetById(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(producerViewModel);
            }

            _producerService.Update(producerViewModel);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_producerService.HasSeries(id))
            {
                
                TempData["ErrorMessage"] = "No puedes eliminar un productor que contiene series.";
                return RedirectToAction(nameof(Index));
            }
            _producerService.Delete(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
