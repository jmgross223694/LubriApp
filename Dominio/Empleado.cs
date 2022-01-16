using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado : Persona
    {
        public string Legajo { get; set; }

        public List <Servicio> servicios;

        public string FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}
