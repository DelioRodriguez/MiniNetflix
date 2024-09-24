using Application.IRepository;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class GenresRepository : IGenresRepository
{
    private readonly ApplicationContext _context;

    public GenresRepository(ApplicationContext context)
    {
        _context = context;
    }
    public IEnumerable<Genre> GetAll() => _context.Genres.ToList();
    public Genre? GetById(int id) => _context.Genres.Find(id);
    public void Add(Genre entity)
    {
        _context.Genres.Add(entity);
        SaveChanges();
    }

    public void Update(Genre entity)
    {

        _context.Genres.Update(entity);
        SaveChanges();
    }

    public void Delete(Genre entity)
    {
        _context.Genres.Remove(entity);
        SaveChanges();
    }
    public void SaveChanges() => _context.SaveChanges();
    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}