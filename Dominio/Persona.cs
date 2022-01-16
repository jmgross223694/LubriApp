using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        public string Name { get; set; }
        public string FechaAlta { get; set; }
        public string Mail { get; set; }
        public string Celular { get; set; }
        public string CuilCuit { get; set; }
        public Persona() { }
        public Persona(string apenom)
        {
            Name = apenom;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
