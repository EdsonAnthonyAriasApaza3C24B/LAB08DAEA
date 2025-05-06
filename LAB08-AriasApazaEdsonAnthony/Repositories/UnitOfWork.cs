using LAB08_AriasApazaEdsonAnthony.Models;

namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LinqexampleContext _context;

        public IClientRepository Clients { get; private set; }
        public IProductRepository Products { get; private set; }

        public UnitOfWork(LinqexampleContext context)
        {
            _context = context;
            Clients = new ClientRepository(_context);
            Products = new ProductRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}