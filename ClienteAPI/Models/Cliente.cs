using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClienteAPI.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        //public ICollection<Factura> Facturas { get; set; }

    }
}