using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Venta
    {
        public int IdVenta { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Factura? IdFacturaNavigation { get; set; } = null;
        public virtual Producto? IdProductoNavigation { get; set; } = null;
    }
}
