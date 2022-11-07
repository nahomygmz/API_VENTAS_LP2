using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdProducto { get; set; }
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }

        public virtual Proveedor? IdProveedorNavigation { get; set; } = null;
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
