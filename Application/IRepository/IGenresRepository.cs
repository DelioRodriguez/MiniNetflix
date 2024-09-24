using Data.Entities;

namespace Application.IRepository;

public interface IGenresRepository : IRepository<Genre>
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
}