using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Models;

namespace VentasAPI.Controllers
{
    [Route("api/Venta")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VENTASContext _context;

        public VentaController(VENTASContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }

        //Venta especifica
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVentas(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }
            return venta;
        }

        //---------------------------------------------------------------------

        //POST
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVentas), new { id = venta.IdVenta }, venta);
        }

        //---------------------------------------------------------------------
        private bool VentaExists(int id)
        {
            return _context.Facturas.Any(e => e.IdFactura == id);
        }
        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
