using LAB08_AriasApazaEdsonAnthony.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_AriasApazaEdsonAnthony.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrdersController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public GetOrdersController(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        // GET: api/GetOrders/afterDate?fecha=2025-05-01
        [HttpGet("afterDate")]
        public async Task<IActionResult> ObtenerPedidosDespuésDeFecha([FromQuery] DateTime fecha)
        {
            var pedidos = await _orderDetailRepository.ObtenerPedidosDespuésDeFechaAsync(fecha);

            if (pedidos == null || !pedidos.Any())
            {
                return NotFound($"No se encontraron pedidos después de la fecha {fecha.ToString("yyyy-MM-dd")}");
            }

            return Ok(pedidos);
        }
        
        [HttpGet("promedioPrecio")]
        public async Task<IActionResult> ObtenerPromedioDePrecio()
        {
            // Llamamos al repositorio para obtener el precio promedio
            var promedioPrecio = await _orderDetailRepository.ObtenerPromedioDePrecioAsync();

            // Si no hay productos, devolvemos un NotFound
            if (promedioPrecio == 0)
            {
                return NotFound("No se encontraron productos para calcular el promedio.");
            }

            // Devolvemos el promedio de precio
            return Ok(new { PromedioPrecio = promedioPrecio });
        }
        
        [HttpGet("productosSinDescripcion")]
        public async Task<IActionResult> ObtenerProductosSinDescripcion()
        {
            // Llamamos al repositorio para obtener los productos sin descripción
            var productosSinDescripcion = await _orderDetailRepository.ObtenerProductosSinDescripcionAsync();

            // Si no hay productos sin descripción, devolvemos un NotFound
            if (!productosSinDescripcion.Any())
            {
                return NotFound("No se encontraron productos sin descripción.");
            }

            // Devolvemos la lista de productos sin descripción
            return Ok(productosSinDescripcion);
        }
        
        [HttpGet("clienteConMasPedidos")]
        public async Task<IActionResult> ObtenerClienteConMayorNumeroDePedidos()
        {
            var clienteConMasPedidos = await _orderDetailRepository.ObtenerClienteConMayorNumeroDePedidosAsync();

            if (clienteConMasPedidos == null)
            {
                return NotFound("No se encontró un cliente con pedidos.");
            }

            return Ok(clienteConMasPedidos);
        }
        
        [HttpGet("todosLosPedidosYDetalles")]
        public async Task<IActionResult> ObtenerTodosLosPedidosYDetalles()
        {
            var pedidosYDetalles = await _orderDetailRepository.ObtenerTodosLosPedidosYDetallesAsync();

            if (!pedidosYDetalles.Any())
            {
                return NotFound("No se encontraron pedidos.");
            }

            return Ok(pedidosYDetalles);
        }
        
        [HttpGet("productosVendidos/{clientId}")]
        public async Task<IActionResult> ObtenerProductosVendidosPorCliente(int clientId)
        {
            var productosVendidos = await _orderDetailRepository.ObtenerProductosVendidosPorClienteAsync(clientId);

            if (!productosVendidos.Any())
            {
                return NotFound($"No se encontraron productos vendidos al cliente con ClientId {clientId}.");
            }

            return Ok(productosVendidos);
        }
        
        [HttpGet("clientesQueHanComprado/{productId}")]
        public async Task<IActionResult> ObtenerClientesQueHanCompradoProducto(int productId)
        {
            var clientesQueHanComprado = await _orderDetailRepository.ObtenerClientesQueHanCompradoProductoAsync(productId);

            if (!clientesQueHanComprado.Any())
            {
                return NotFound($"No se encontraron clientes que hayan comprado el producto con ProductId {productId}.");
            }

            return Ok(clientesQueHanComprado);
        }
    }
}