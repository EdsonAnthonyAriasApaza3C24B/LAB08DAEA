using LAB08_AriasApazaEdsonAnthony.Models;

namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LinqexampleContext _context;

        public ProductRepository(LinqexampleContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate)
        {
            return await Task.Run(() => _context.Products.Where(predicate).ToList());
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}