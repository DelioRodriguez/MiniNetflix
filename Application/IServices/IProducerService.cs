using Application.ViewModel;
using Data.Entities;

namespace Application.IServices;

public interface IProducerService
{
    Task<IEnumerable<Producer>> GetProducersAsync();
    IEnumerable<ProducerViewModel> GetAllProducer();
    ProducerViewModel? GetById(int id);
    void Add(ProducerViewModel producerViewModel);
    void Update(ProducerViewModel producerViewModel);
    void Delete(int id);
    bool HasSeries(int producer);
}