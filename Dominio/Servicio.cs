using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Servicio
    {
        public int ID { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public Vehiculo Vehiculo { get; set; }
        TipoServicio TipoServicio { get; set; }
        public string Comentarios { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaPedidoTurno { get; set; }
        public bool Estado { get; set; }
    }
}
