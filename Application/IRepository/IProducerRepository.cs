using Data.Entities;

namespace Application.IRepository;

public interface IProducerRepository : IRepository<Producer> {
    Task<IEnumerable<Producer>> GetAllProducersAsync();
}