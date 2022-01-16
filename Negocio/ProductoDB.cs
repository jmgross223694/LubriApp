using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoDB
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("select * from ExportInventario order by EAN asc");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();

                    aux.EAN = (long)datos.Lector["EAN"];
                    aux.Descripción = (string)datos.Lector["Descripción"];
                    aux.Imagen = Convert.ToBase64String((byte[])datos.Lector["Imagen"]);
                    aux.TipoProducto = new TipoProducto((string)datos.Lector["TipoProducto"]);
                    aux.MarcaProducto = new MarcaProducto((string)datos.Lector["Marca"]);
                    aux.Proveedor = new Proveedor((string)datos.Lector["Proveedor"]);
                    aux.Costo = Math.Truncate((decimal)datos.Lector["Costo"] * 100) / 100;
                    aux.PrecioVenta = Math.Truncate((decimal)datos.Lector["PrecioVenta"] * 100) / 100;
                    aux.Stock = (int)datos.Lector["Stock"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    aux.MarcaProducto.Estado = (bool)datos.Lector["EstadoMarca"];
                    aux.Proveedor.Estado = (bool)datos.Lector["EstadoProveedor"];

                    if (aux.Estado == true && aux.MarcaProducto.Estado == true && aux.Proveedor.Estado == true)
                    {
                        lista.Add(aux);
                    }
                }
            }
            catch
            {
                
            }
            finally
            {
                datos.CerrarConexion();
            }
            return lista;
        }
    }
}
