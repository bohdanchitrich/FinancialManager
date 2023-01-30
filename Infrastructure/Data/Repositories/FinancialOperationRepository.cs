using Domain.FinancialOperations;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories;

public class FinancialOperationRepository : IAsyncRepository<FinancialOperation>
{
    private readonly ApplicationDbContext _context;

    public FinancialOperationRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<FinancialOperation> AddAsync(FinancialOperation entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await _context.FinancialOperations.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(FinancialOperation entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.FinancialOperations.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FinancialOperation>> GetAllAsync(Expression<Func<FinancialOperation, bool>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        return await _context.FinancialOperations.Where(expression).ToListAsync();
    }

    public async Task<List<FinancialOperation>> GetAllAsync()
    {
        return await _context.FinancialOperations.ToListAsync();
    }

    public async Task<List<FinancialOperation>> GetPagedAsync(int page, int pageSize)
    {
        return await _context.FinancialOperations.OrderBy(obj => obj.Id).Skip(
                (page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public async Task<FinancialOperation?> GetAsync(Expression<Func<FinancialOperation, bool>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        return await _context.FinancialOperations.FirstOrDefaultAsync(expression);
    }

    public async Task<FinancialOperation> UpdateAsync(FinancialOperation entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _context.FinancialOperations.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}