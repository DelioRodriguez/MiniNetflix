using Application.IRepository;
using Application.IServices;
using Application.ViewModel;
using Data.Entities;

namespace Application.Services;

public class GenreService : IGenresService
{
    private readonly IGenresRepository _genresRepository;
    private readonly ISeriesRepository _seriesRepository;

    public GenreService(IGenresRepository genresRepository, ISeriesRepository seriesRepository)
    {
        _genresRepository = genresRepository;
        _seriesRepository = seriesRepository;
    }
    
    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _genresRepository.GetAllGenresAsync();
    }

    public IEnumerable<GenreViewModel> GetAllGenres()
    {
        return _genresRepository.GetAll().Select(g => new GenreViewModel
        {
            Id = g.Id,
            Name = g.Name
        });
    }

    public GenreViewModel? GetById(int id)
    {
        var genre = _genresRepository.GetById(id);
        if (genre == null) return null;

        return new GenreViewModel
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public void Add(GenreViewModel genreViewModel)
    {
        var genre = new Genre
        {
            Id = 0,
            Name = genreViewModel.Name
        };
        _genresRepository.Add(genre);
    }

    public void Update(GenreViewModel genreViewModel)
    {
        var genre = new Genre
        {
            Id = genreViewModel.Id.Value,
            Name = genreViewModel.Name
        };
        _genresRepository.Update(genre);
    }

    public void Delete(int id)
    {
        var genre = _genresRepository.GetById(id);
        if (genre != null)
        {
            _genresRepository.Delete(genre);
        }
    }

    public bool HasSeries(int genreId)
    {
        return _seriesRepository.GetAll().Any(s => s.PrimaryGenreId == genreId || s.SecondaryGenreId == genreId);
    }
}