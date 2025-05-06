using LAB08_AriasApazaEdsonAnthony.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LinqexampleContext _context;

        public ClientRepository(LinqexampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public void Remove(Client client)
        {
            _context.Clients.Remove(client);
        }
    }
}