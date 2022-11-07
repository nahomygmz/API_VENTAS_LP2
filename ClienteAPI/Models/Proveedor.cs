using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClienteAPI.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        //public ICollection<Producto> Productos { get; set; }
    }
}