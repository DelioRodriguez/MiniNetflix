using Application.IRepository;
using Application.IServices;
using Application.ViewModel;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class SeriesService : ISeriesService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly ApplicationContext _context;

    public SeriesService(ISeriesRepository seriesRepository, ApplicationContext application)
    {
        _seriesRepository = seriesRepository;
        _context = application;
    }

    public IEnumerable<SeriesViewModel> GetAllSeries()
    {
        return _seriesRepository.GetAll().Select(s => new SeriesViewModel
        {
            Id = s.Id,
            Name = s.Name,
            ProducerId = s.ProducerId,
            PrimaryGenreId = s.PrimaryGenreId,
            SecondaryGenreId = s.SecondaryGenreId
            
        });
    }

    public SeriesViewModel? GetById(int id)
    {
        var s = _seriesRepository.GetById(id);
        if (s == null)
        {
            return null;
        }

        return new SeriesViewModel
        {
            Id = s.Id,
            Name = s.Name,
            ProducerId = s.ProducerId,
            PrimaryGenreId = s.PrimaryGenreId,
            SecondaryGenreId = s.SecondaryGenreId,
            VideoLink = s.VideoLink, 
            ImgLink = s.ImgLink
        };
    }


    public void Add(SeriesViewModel seriesViewModel)
    {
        var s = new Series
        {
            Id = 0,
            Name = seriesViewModel.Name,
            ProducerId = seriesViewModel.ProducerId,
            PrimaryGenreId = seriesViewModel.PrimaryGenreId,
            SecondaryGenreId = seriesViewModel.SecondaryGenreId,
            VideoLink = seriesViewModel.VideoLink,
            ImgLink = seriesViewModel.ImgLink
        };
        _seriesRepository.Add(s);
    }
    public Series GetSeriesById(int id)
    {
        return _context.Series.FirstOrDefault(s => s.Id == id);
    }


    public void Update(SeriesViewModel seriesViewModel)
    {
        var s = new Series
        {
            Id = seriesViewModel.Id.Value,
            Name = seriesViewModel.Name,
            ProducerId = seriesViewModel.ProducerId,
            PrimaryGenreId = seriesViewModel.PrimaryGenreId,
            SecondaryGenreId = seriesViewModel.SecondaryGenreId,
            VideoLink = seriesViewModel.VideoLink,
            ImgLink = seriesViewModel.ImgLink,
            
        };
        _seriesRepository.Update(s);
    }

    public void Delete(int id)
    {
        var s = _seriesRepository.GetById(id);
        if (s != null)
        {
            _seriesRepository.Delete(s);
        }
    }

    public async Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync()
    {
        return await _seriesRepository.GetAllSeriesAsync(); 
    }

    public async Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId)
    {
        return await _seriesRepository.GetSeriesByProducerAsync(producerId); 
    }

    public async Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name)
    {
        return await _seriesRepository.SearchByNameAsync(name); 
    }

    public async Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreID)
    {
        return await _seriesRepository.GetSeriesByGenreAsync(genreID); 
    }
}