using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Factura
    {
        public Factura()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; } = null;
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
