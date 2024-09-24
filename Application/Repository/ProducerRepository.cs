using Application.IRepository;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class ProducerRepository : IProducerRepository
{
    
    private readonly ApplicationContext _context;

    public ProducerRepository(ApplicationContext context)
    {
        _context = context; 
    }
    public IEnumerable<Producer> GetAll() { return _context.Producers.ToList(); } 

    public Producer? GetById(int id)
    {
        return _context.Producers.Find(id);
    }

    public void Add(Producer entity)
    {
        _context.Producers.Add(entity);
        SaveChanges();
    }

    public void Update(Producer entity)
    {
        _context.Producers.Update(entity);
        SaveChanges();
    }

    public void Delete(Producer entity)
    {
        _context.Producers.Remove(entity);
        SaveChanges();
    }

    public void SaveChanges() => _context.SaveChanges();

    public async Task<IEnumerable<Producer>> GetAllProducersAsync()
    {
        return await _context.Producers.ToListAsync();
    }
}