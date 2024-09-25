using Application.IRepository;
using Application.ViewModel;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class SeriesRepository : ISeriesRepository
{
    private readonly ApplicationContext _context;

    public SeriesRepository( ApplicationContext context)
    {
        _context = context;
    }


    public IEnumerable<Series> GetAll()
    {
        return _context.Series.Include(s => s.Producer)
            .Include(s => s.PrimaryGenre)
            .Include(s => s.SecondaryGenre)
            .ToList();
    }

    public Series? GetById(int id)
    {
        return _context.Series.Include(s => s.Producer)
            .Include(s => s.PrimaryGenre)
            .Include(s => s.SecondaryGenre)
            .FirstOrDefault(s => s.Id == id);
    }

    public void Add(Series? entity)
    {
        _context.Series.Add(entity);
        SaveChanges();
    }

    public void Update(Series entity)
    {
        _context.Update(entity);
        SaveChanges();
    }

    public void Delete(Series? entity)
    {
      _context.Series.Remove(entity);
      SaveChanges();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync()
    {
        return await _context.Series
            .Include(s => s.Producer)
            .Include(s => s.PrimaryGenre)
            .Include(s => s.SecondaryGenre)
            .Select(s => new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                VideoLink = s.VideoLink,
                ImgLink = s.ImgLink,
                ProducerId = s.ProducerId,
                ProducerName = s.Producer.Name, 
                PrimaryGenreId = s.PrimaryGenreId,
                PrimaryGenreName = s.PrimaryGenre.Name, 
                SecondaryGenreId = s.SecondaryGenreId,
                SecondaryGenreName = s.SecondaryGenre != null ? s.SecondaryGenre.Name : null 
            }).ToListAsync();
    }


    public async Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId)
    {
        return await _context.Series
            .Where(s => s.ProducerId == producerId)
            .Select(s => new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                VideoLink = s.VideoLink,
                ImgLink = s.ImgLink,
                ProducerId = s.ProducerId,
                PrimaryGenreId = s.PrimaryGenreId,
                SecondaryGenreId = s.SecondaryGenreId,
            }).ToListAsync();
    }

    public async Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreId)
    {
        return await _context.Series
            .Where(s=>s.PrimaryGenreId==genreId || s.SecondaryGenreId == genreId)
            .Select(s=> new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                VideoLink = s.VideoLink,
                ImgLink = s.ImgLink,
                ProducerId = s.ProducerId,
                PrimaryGenreId = s.PrimaryGenreId,
                SecondaryGenreId = s.SecondaryGenreId,
            }).ToListAsync();
    }

    public async Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name)
    {
        return await  _context.Series
            .Where(s=>s.Name.Contains(name))
            .Select(s=> new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                VideoLink = s.VideoLink,
                ImgLink = s.ImgLink,
                ProducerId = s.ProducerId,
                PrimaryGenreId = s.PrimaryGenreId,
                SecondaryGenreId = s.SecondaryGenreId,
            }).ToListAsync();
    }
}