namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IProductRepository Products { get; }
        Task<int> CompleteAsync();
    }
}