using LAB08_AriasApazaEdsonAnthony.Models;
using System.Linq.Expressions;

namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate);
        Task AddAsync(Client client);
        void Remove(Client client);
    }
}