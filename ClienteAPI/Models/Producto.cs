using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClienteAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }

        public Proveedor IdProveedorNavigation { get; set; } = null;
        //public ICollection<Venta> Venta { get; set; }

    }
}