using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClienteAPI.Models
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }

        public Cliente IdClienteNavigation { get; set; } = null;
        //public ICollection<Venta> Venta { get; set; }
    }
}