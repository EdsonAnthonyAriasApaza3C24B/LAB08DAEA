using LAB08_AriasApazaEdsonAnthony.Models;
using LAB08_AriasApazaEdsonAnthony.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LAB08_AriasApazaEdsonAnthony.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetClientesController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public GetClientesController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: api/GetClientes/buscar/Juan
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<Client>>> BuscarClientesPorNombre(string nombre)
        {
            var clientes = await _clientRepository.FindAsync(c => c.Name.StartsWith(nombre));

            if (!clientes.Any())
            {
                return NotFound($"No se encontraron clientes cuyo nombre comience con '{nombre}'.");
            }

            return Ok(clientes);
        }
    }
}