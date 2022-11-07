using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClienteAPI.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public Factura IdFacturaNavigation { get; set; } = null;
        public Producto IdProductoNavigation { get; set; } = null;
    }
}