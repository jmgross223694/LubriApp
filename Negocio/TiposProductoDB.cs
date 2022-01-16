using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class TiposProductoDB
    {
        public List<TipoProducto> Listar()
        {
            TipoProducto aux;
            List<TipoProducto> lista = new List<TipoProducto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select ID, Descripcion from TiposProducto");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    aux = new TipoProducto((int)datos.Lector["ID"], (string)datos.Lector["Descripcion"]);

                    lista.Add(aux);
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

