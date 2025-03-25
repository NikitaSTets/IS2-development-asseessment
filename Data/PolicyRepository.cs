using Data.Interfaces;
using Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data;

public class PolicyRepository : IPolicyRepository
{
    private readonly ExporterDbContext _dbContext;


    public PolicyRepository(ExporterDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Add(Policy policy)
    {
        _dbContext.Policies.Add(policy);
    }

    public IQueryable<Policy> GetAllAsync()
    {
        return _dbContext.Policies.AsQueryable();
    }

    public async Task<Policy?> GetByIdAsync(int id)
    {
        var policy = await _dbContext.Policies.FindAsync(id);

        return policy;
    }

    public async Task<IEnumerable<Policy>> GetWhereAsync(Expression<Func<Policy, bool>> condition)
    {
        var policis = await GetAllAsync().Include(p => p.Notes).Where(condition).ToListAsync();

        return policis;
    }

    //Move to UoW
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
