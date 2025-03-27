using Data.Model;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IPolicyRepository
{
    void Add(Policy policy);

    IQueryable<Policy> GetAll();

    Task<Policy?> GetByIdAsync(int id);

    Task<IEnumerable<Policy>> GetWhereAsync(Expression<Func<Policy, bool>> condition);

    Task SaveChangesAsync();
}
