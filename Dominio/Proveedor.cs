using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proveedor
    {
        public int ID { get; set; }
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public bool Estado { get; set; }
        public Proveedor(string razonSocial)
        {
            RazonSocial = razonSocial;
        }
        public override string ToString()
        {
            return RazonSocial;
        }
    }
}
