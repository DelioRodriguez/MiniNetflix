using Application.ViewModel;
using Data.Entities;

namespace Application.IRepository;

public interface ISeriesRepository : IRepository<Series>
{
    Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync(); 
    Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId);
    Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreId);
    Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name);
}