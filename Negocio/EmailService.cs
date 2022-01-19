using System;
using System.Net.Mail;
using System.Net;
using Negocio;

namespace Servicios
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient client;

        public EmailService()
        {
            string user = "vacio", pass = "vacio";

            string selectDatos = "SELECT * FROM CredencialesMail WHERE ID = 1";

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(selectDatos);
                datos.EjecutarLectura();

                if (datos.Lector.Read() == true)
                {
                    user = datos.Lector["Usuario"].ToString();
                    pass = datos.Lector["Clave"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

            if (user != "vacio")
            {
                client = new SmtpClient();
                client.Credentials = new NetworkCredential(user, pass);
                client.EnableSsl = true;
                client.Port = 587;  //Puerto SMTP: 465 (SSL)/587 (TLS)
                client.Host = "smtp.gmail.com";
            }
        }

        public void armarCorreo(string mailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("noresponder@lubricentrotony.com", "Lubricentro Tony");
            email.To.Add(mailDestino);
            email.Subject = asunto;
            //email.IsBodyHtml = true;
            //email.Body = "<h1>Reporte de materias a las que se ha inscripto</h1> <br>Hola, te inscribiste.... bla bla";
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
