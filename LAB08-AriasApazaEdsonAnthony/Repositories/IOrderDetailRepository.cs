using LAB08_AriasApazaEdsonAnthony.Models;
using LAB08_AriasApazaEdsonAnthony.DTOs;
namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<object>> GetProductosPorOrdenAsync(int orderId);
        IEnumerable<Orderdetail> GetAll();
        Orderdetail GetById(int id);
        void Add(Orderdetail orderDetail);
        void Remove(Orderdetail orderDetail);
        
        Task<int> ObtenerCantidadTotalDeProductosPorOrdenAsync(int orderId);  
        Task<ProductoMasCaroDTO> ObtenerProductoMasCaroAsync();
        
        Task<IEnumerable<Order>> ObtenerPedidosDespuésDeFechaAsync(DateTime fecha);
        
        Task<decimal> ObtenerPromedioDePrecioAsync();
        
        Task<IEnumerable<Product>> ObtenerProductosSinDescripcionAsync();
        
        Task<object> ObtenerClienteConMayorNumeroDePedidosAsync();
        
        Task<IEnumerable<object>> ObtenerTodosLosPedidosYDetallesAsync();
        
        Task<IEnumerable<object>> ObtenerProductosVendidosPorClienteAsync(int clientId);
        
        Task<IEnumerable<object>> ObtenerClientesQueHanCompradoProductoAsync(int productId);
    }
}