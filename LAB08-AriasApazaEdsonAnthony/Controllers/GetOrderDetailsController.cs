using LAB08_AriasApazaEdsonAnthony.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_AriasApazaEdsonAnthony.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;  // Agregado para el UnitOfWork

        // Inyectamos tanto el IOrderDetailRepository como el IUnitOfWork
        public GetOrderDetailsController(IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            _orderDetailRepository = orderDetailRepository;
            _unitOfWork = unitOfWork; // Inicializamos el UnitOfWork
        }

        // GET: api/GetOrderDetails/1
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetProductosPorOrden(int orderId)
        {
            var detalles = await _orderDetailRepository.GetProductosPorOrdenAsync(orderId);

            if (!detalles.Any())
                return NotFound($"No se encontraron productos para la orden con ID {orderId}.");

            return Ok(detalles);
        }
        
        // GET: api/GetOrderDetails/totalCantidad/1
        [HttpGet("totalCantidad/{orderId}")]
        public async Task<IActionResult> ObtenerCantidadTotalDeProductos(int orderId)
        {
            // Ahora utilizamos _orderDetailRepository para obtener la cantidad total
            var totalCantidad = await _orderDetailRepository.ObtenerCantidadTotalDeProductosPorOrdenAsync(orderId); 

            if (totalCantidad == 0)
            {
                return NotFound($"No se encontraron productos para la orden con OrderId {orderId}.");
            }

            return Ok(new { TotalCantidad = totalCantidad });
        }
        
        // Método GET para obtener el producto más caro
        [HttpGet("productoMasCaro")]
        public async Task<IActionResult> ObtenerProductoMasCaro()
        {
            var productoMasCaro = await _orderDetailRepository.ObtenerProductoMasCaroAsync();

            if (productoMasCaro == null)
            {
                return NotFound("No se encontró ningún producto.");
            }

            return Ok(productoMasCaro);
        }
    }
}   