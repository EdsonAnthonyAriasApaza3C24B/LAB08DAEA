using LAB08_AriasApazaEdsonAnthony.Models;
using Microsoft.EntityFrameworkCore;
using LAB08_AriasApazaEdsonAnthony.DTOs;
namespace LAB08_AriasApazaEdsonAnthony.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly LinqexampleContext _context;

        public OrderDetailRepository(LinqexampleContext context)
        {
            _context = context;
        }

        // Método para obtener los productos por orden
        public async Task<IEnumerable<object>> GetProductosPorOrdenAsync(int orderId)
        {
            return await _context.Orderdetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Product)
                .Select(od => new {
                    NombreProducto = od.Product.Name,
                    Cantidad = od.Quantity
                })
                .ToListAsync();
        }

        // Método para obtener todos los detalles de orden
        public IEnumerable<Orderdetail> GetAll()
        {
            return _context.Orderdetails.ToList();
        }

        // Método para obtener un detalle de orden por ID
        public Orderdetail GetById(int id)
        {
            return _context.Orderdetails.Find(id);
        }

        // Método para agregar un nuevo detalle de orden
        public void Add(Orderdetail orderDetail)
        {
            _context.Orderdetails.Add(orderDetail);
        }

        // Método para eliminar un detalle de orden
        public void Remove(Orderdetail orderDetail)
        {
            _context.Orderdetails.Remove(orderDetail);
        }

        // Método para obtener la cantidad total de productos por orden
        public async Task<int> ObtenerCantidadTotalDeProductosPorOrdenAsync(int orderId)
        {
            return await _context.Orderdetails
                .Where(od => od.OrderId == orderId)
                .SumAsync(od => od.Quantity);
        }

        public async Task<ProductoMasCaroDTO> ObtenerProductoMasCaroAsync()
        {
            var productoMasCaro = await _context.Products
                .OrderByDescending(p => p.Price) // Ordena por precio de forma descendente
                .Select(p => new ProductoMasCaroDTO { 
                    NombreProducto = p.Name, 
                    Precio = p.Price 
                })
                .FirstOrDefaultAsync(); // Obtiene el primer producto, el más caro

            return productoMasCaro;
        }
        
        // Nuevo método: Obtener todos los pedidos después de una fecha específica
        public async Task<IEnumerable<Order>> ObtenerPedidosDespuésDeFechaAsync(DateTime fecha)
        {
            return await _context.Orders
                .Where(o => o.OrderDate > fecha) // Filtra los pedidos por fecha
                .ToListAsync(); // Devuelve los resultados como una lista
        }
        
        public async Task<decimal> ObtenerPromedioDePrecioAsync()
        {
            // Usamos LINQ para obtener el promedio de los precios de los productos
            return await _context.Products
                .AverageAsync(p => p.Price);  // Calculamos el promedio de los precios
        }
        
        public async Task<IEnumerable<Product>> ObtenerProductosSinDescripcionAsync()
        {
            // Filtramos los productos donde Description sea null o vacío
            return await _context.Products
                .Where(p => string.IsNullOrEmpty(p.Description))  // Filtramos productos sin descripción
                .ToListAsync();  // Convertimos el resultado a una lista
        }
        
        public async Task<object> ObtenerClienteConMayorNumeroDePedidosAsync()
        {
            var clienteConMasPedidos = await _context.Orders
                .GroupBy(o => o.ClientId) // Agrupar los pedidos por ClientId
                .Select(g => new {
                    ClientId = g.Key,  // El ClientId del grupo
                    NumeroDePedidos = g.Count()  // El número de pedidos del cliente
                })
                .OrderByDescending(g => g.NumeroDePedidos)  // Ordenar de mayor a menor por el número de pedidos
                .FirstOrDefaultAsync(); // Tomar el primer cliente (el que tiene más pedidos)

            return clienteConMasPedidos;
        }
        
        public async Task<IEnumerable<object>> ObtenerTodosLosPedidosYDetallesAsync()
        {
            var pedidosYDetalles = await _context.Orderdetails
                .Include(od => od.Product)  // Asegúrate de incluir los detalles del producto
                .Select(od => new {
                    OrderId = od.OrderId,  // Id de la orden
                    ProductName = od.Product.Name,  // Nombre del producto
                    Quantity = od.Quantity  // Cantidad del producto en la orden
                })
                .ToListAsync();  // Convertir los resultados a una lista

            return pedidosYDetalles;
        }
        
        public async Task<IEnumerable<object>> ObtenerProductosVendidosPorClienteAsync(int clientId)
        {
            var productosVendidos = await _context.Orderdetails
                .Where(od => od.Order.ClientId == clientId)  // Filtramos por el ClientId de la orden
                .Include(od => od.Product)  // Incluimos los detalles del producto
                .Select(od => new {
                    ProductName = od.Product.Name  // Proyectamos el nombre del producto
                })
                .ToListAsync();  // Convertimos el resultado a una lista

            return productosVendidos;
        }
        
        public async Task<IEnumerable<object>> ObtenerClientesQueHanCompradoProductoAsync(int productId)
        {
            var clientesQueHanComprado = await _context.Orderdetails
                .Where(od => od.ProductId == productId)  // Filtramos por ProductId
                .Include(od => od.Order)  // Incluimos la información de la orden
                .ThenInclude(o => o.Client)  // Incluimos la información del cliente (suponiendo que existe la relación)
                .Select(od => new {
                    ClientName = od.Order.Client.Name  // Proyectamos el nombre del cliente
                })
                .Distinct()  // Usamos Distinct para eliminar posibles duplicados (en caso de que un cliente haya comprado el mismo producto más de una vez)
                .ToListAsync();

            return clientesQueHanComprado;
        }
    }
}
