using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoDatos()
        {
            string dataSource = ".\\SQLEXPRESS",
                   database = "LubriApp";

            string LAN = "data source=" + dataSource + "; " +
                         "initial catalog=" + database + "; " +
                         "integrated security=sspi";

            //string HOST_DB = "";

            try
            {
                conexion = new SqlConnection(LAN);
            }
            catch (Exception)
            {
                //conexion = new SqlConnection(HOST_DB);
            }

            comando = new SqlCommand();
        }

        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        internal void EjetutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public DataSet DSET(string consulta)
        {
            DataSet ds = new DataSet();

            if(Conectar())
            {
                try
                {
                    SqlDataAdapter SDA = new SqlDataAdapter(consulta, conexion);
                    SDA.Fill(ds, "datos");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return ds;
        }

        public int IUD(string consulta)
        {
            int res = -1;

            if(Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    res = 0;
                }
                catch (SqlException mise)
                {
                    int error = Convert.ToInt32(mise);
                }
            }

            return res;
        }

        public bool Conectar()
        {
            try
            {
                SqlConnectionStringBuilder db = new SqlConnectionStringBuilder();
                db.DataSource = ".\\SQLEXPRESS";
                db.InitialCatalog = "LubriApp";
                db.IntegratedSecurity = true;
                string datos = db.ToString();
                conexion = new SqlConnection(datos);
                conexion.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int IUDquery (string consulta)
        {
            int res = -1;

            if (Conectar())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.ExecuteNonQuery();
                    return (int)cmd.ExecuteScalar();                  
                }
                catch (SqlException mise)
                {
                    int error = Convert.ToInt32(mise);
                }
            }

            return res;
        }
    }
}
