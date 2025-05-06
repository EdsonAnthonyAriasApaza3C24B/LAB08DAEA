using LAB08_AriasApazaEdsonAnthony.Models;

namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Remove(Product product);
    }
}