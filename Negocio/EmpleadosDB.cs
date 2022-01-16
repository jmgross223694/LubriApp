using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class EmpleadosDB
    {
        public static void ActualizarEmpleados()
        {

        }

        public List<Empleado> Listar()
        {
            List<Empleado> lista = new List<Empleado>();
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos servicio = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select * from ExportEmpleados");
                datos.EjecutarLectura();


                //FALTA CREAR EN NEGOCIO LA CLASE LISTAR QUE ES LA QUE TRAE LA LISTA DE SERVICIOS.
                //servicio.SetearConsulta("select * from ExportServicios");
                //servicio.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Empleado aux = new Empleado();

                    aux.Legajo = (string) datos.Lector["Legajo"];
                    aux.CuilCuit = (string)datos.Lector["Cuil"];
                    aux.Name = (string)datos.Lector["Apellido y Nombre"];
                    aux.FechaAlta = ((DateTime)datos.Lector["Fecha de Alta"]).ToShortDateString();
                    aux.FechaNacimiento = ((DateTime)datos.Lector["Fecha de Nacimiento"]).ToShortDateString();
                    aux.Mail = (string)datos.Lector["Mail"];
                    aux.Celular = (string)datos.Lector["Telefono"];                   
                    //aux.servicios = (List<Servicio>)servicio.Listar();
                    aux.Estado = (bool)datos.Lector["Estado"];

                    if (aux.Estado == true)
                    {
                        lista.Add(aux);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
