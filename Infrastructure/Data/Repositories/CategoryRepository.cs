using Domain.Categories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository : IAsyncRepository<Category>
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Category> AddAsync(Category entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Category entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<List<Category>> GetAllAsync(Expression<Func<Category, bool>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        return _context.Categories.Where(expression).ToListAsync();
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<List<Category>> GetPagedAsync(int page, int pageSize)
    {
        return await _context.Categories.OrderBy(obj => obj.Id).Skip(
                (page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public Task<Category?> GetAsync(Expression<Func<Category, bool>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        return _context.Categories.FirstOrDefaultAsync(expression);
    }

    public async Task<Category> UpdateAsync(Category entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

}