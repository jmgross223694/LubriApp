using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Servicios;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AvisosServicios();
        }

        public void mostrarScriptMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "alert('" + mensaje + "')", true);
        }

        public void AvisosServicios()
        {
            string selectDatosAvisos = "SELECT * FROM Export_AvisosServicios";
            
            string selectCountAvisosServicios = "SELECT isnull(COUNT(*), 0) AS Cantidad FROM AvisosServicios " +
            "WHERE FechaAviso = CONVERT(varchar,getdate(),105)";
            
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            EmailService mail = new EmailService();
            int cantidadAvisosServicios = 0;

            try
            {
                datos2.SetearConsulta(selectCountAvisosServicios);
                datos2.EjecutarLectura();

                if (datos2.Lector.Read() == true)
                {
                    cantidadAvisosServicios = Convert.ToInt32(datos2.Lector["Cantidad"]);
                }
            }
            catch
            {
                //mostrarScriptMensaje("Se produjo un error al leer la tabla AvisosServicios");
            }
            finally
            {
                datos2.CerrarConexion();
            }

            if (cantidadAvisosServicios > 0)
            {
                try
                {
                    datos.SetearConsulta(selectDatosAvisos);
                    datos.EjecutarLectura();

                    string asunto = "RECORDATORIO DE REVISIÓN VEHICULAR";

                    while (datos.Lector.Read() == true)
                    {
                        string IdAvisoServicio = datos.Lector["ID"].ToString();
                        string mailDestino = datos.Lector["Mail"].ToString();
                        string FechaRealizado = datos.Lector["FechaRealizado"].ToString();
                        string Cliente = datos.Lector["Cliente"].ToString();
                        string TipoServicio = datos.Lector["TipoServicio"].ToString();
                        string Patente = datos.Lector["Patente"].ToString();
                        int Enviado = Convert.ToInt32(datos.Lector["Enviado"]);

                        string linkTurnos = "https://localhost:44347/turnos.aspx";

                        string cuerpo = "Hola " + Cliente + ", esperamos que se encuentre muy bien.\n" +
                        "Desde Lubricentro Tony le queremos recordar que en 1 semana se cumplirá un " +
                        "año desde que realizó con nosotros, el servicio detallado a continuación.\n\n" +
                        "Detalles:\n" +
                        "- Fecha de realización: " + FechaRealizado +
                        "\n- Tipo de servicio: " + TipoServicio +
                        "\n- Patente de vehículo: " + Patente + "\n\n" +
                        "Le queríamos recomendar obtener un turno a la brevedad para una nueva revisión, " +
                        "para ello haga click en el siguiente link:\n\n" +
                        linkTurnos +
                        ".\n\nCualquier consulta no dude en comunicarse con nosotros.\n\n" +
                        "Saludos cordiales.";

                        mail.armarCorreo(mailDestino, asunto, cuerpo);

                        try
                        {
                            if (Enviado == 0)
                            {
                                mail.enviarEmail();

                                string marcarMailEnviado = "UPDATE AvisosServicios SET MailEnviado=1 WHERE ID = " + IdAvisoServicio;

                                AccesoDatos sentencia = new AccesoDatos();

                                try
                                {
                                    sentencia.IUD(marcarMailEnviado);
                                }
                                catch (Exception)
                                {
                                    mostrarScriptMensaje("Se produjo un error al intentar actualizar la " +
                                    "columna mail enviado en la tabla AvisosServicios.");
                                }
                            }
                        }
                        catch
                        {
                            mostrarScriptMensaje("Se produjo un error al intentar enviar un mail.");
                        }
                    }
                }
                catch
                {
                    mostrarScriptMensaje("Se produjo un error al leer la tabla AvisosServicios");
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
        }
    }
}