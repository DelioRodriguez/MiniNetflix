﻿using Application.ViewModel;
using Data.Entities;


namespace Application.IServices;

public interface ISeriesService
{
    IEnumerable<SeriesViewModel> GetAllSeries();
    SeriesViewModel? GetById(int id);
    void Add(SeriesViewModel seriesViewModel);
    void Update(SeriesViewModel seriesViewModel);
    void Delete(int id);
    Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync();
    Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId);
    Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name);
    Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreId);

    public Series? GetSeriesById(int id);

}