using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vehiculo
    {
        public string Patente { get; set; }
        public MarcaVehiculo Marca { get; set; }
        public string Modelo { get; set; }
        public DateTime AnioFabricacion { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
