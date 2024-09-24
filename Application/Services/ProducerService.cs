using Application.IRepository;
using Application.IServices;
using Application.ViewModel;
using Data.Entities;

namespace Application.Services;

public class ProducerService : IProducerService
{
    private readonly IProducerRepository _producerRepository;
    private readonly ISeriesRepository _seriesRepository;

    public ProducerService(IProducerRepository producerRepository,ISeriesRepository seriesRepository)
    {
        _producerRepository = producerRepository;
        _seriesRepository = seriesRepository;
    }
    public async Task<IEnumerable<Producer>> GetProducersAsync()
    {
        return await _producerRepository.GetAllProducersAsync();
    }

    public IEnumerable<ProducerViewModel> GetAllProducer()
    {
        return _producerRepository.GetAll().Select(g => new ProducerViewModel
        {
            Id = g.Id,
            Name = g.Name,
        });
    }

    public ProducerViewModel? GetById(int id)
    {
        var prod = _producerRepository.GetById(id);
        if (prod == null) return null;

        return new ProducerViewModel
        {
            Id = prod.Id,
            Name = prod.Name,
        };
    }

    public void Add(ProducerViewModel producerViewModel)
    {
        var prod = new Producer
        {
            Id = 0,
            Name = producerViewModel.Name
        };
        _producerRepository.Add(prod);
    }

    public void Update(ProducerViewModel producerViewModel)
    {
        var prod = new Producer
        {
            Id = producerViewModel.Id.Value,
            Name = producerViewModel.Name
        };
        _producerRepository.Update(prod);
    }

    public void Delete(int id)
    {
        var prod = _producerRepository.GetById(id);
        if (prod != null)
        {
            _producerRepository.Delete(prod);
        }
    }

    public bool HasSeries(int producer)
    {
        return _seriesRepository.GetAll().Any(s => s.ProducerId == producer);
    }
}