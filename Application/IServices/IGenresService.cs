using Application.ViewModel;
using Data.Entities;

namespace Application.IServices;

public interface IGenresService
{
    Task<IEnumerable<Genre>> GetAllGenresAsync();
    IEnumerable<GenreViewModel> GetAllGenres();
    GenreViewModel? GetById(int id);
    void Add(GenreViewModel genreViewModel);
    void Update(GenreViewModel genreViewModel);
    void Delete(int id);

    bool HasSeries(int genreId);
}