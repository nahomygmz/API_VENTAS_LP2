using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Models;

namespace VentasAPI.Controllers
{
    [Route("api/Proveedor")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly VENTASContext _context;

        public ProveedorController(VENTASContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        //Proveedor especifico
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedores(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        //-------------------------------------------------------------------------------

        //POST
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedore(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedores), new { id = proveedor.IdProveedor }, proveedor);
        }

        //-------------------------------------------------------------------------------

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }

        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedore(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NoContent();
                }
            }
            return Ok();
        }

        //---------------------------------------------------------------------

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedore(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
