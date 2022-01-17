using System;
using System.Web;
using System.Web.UI;

namespace LubriApp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pagina = HttpContext.Current.Request.Url.AbsoluteUri;

            if (pagina == "https://localhost:44347/index"
            || pagina == "https://localhost:44347/catalogoProductos"
            || pagina == "https://localhost:44347/login"
            || pagina == "https://localhost:44347/registroCliente"
            || pagina == "https://localhost:44347/turnos"
            || pagina == "https://localhost:44347/contacto"
            || pagina == "https://localhost:44347/registroVehiculo"
            || pagina == "http://www.lubriapp.somee.com/index"
            || pagina == "http://www.lubriapp.somee.com/catalogoProductos"
            || pagina == "http://www.lubriapp.somee.com/login"
            || pagina == "http://www.lubriapp.somee.com/registroCliente"
            || pagina == "http://www.lubriapp.somee.com/turnos"
            || pagina == "http://www.lubriapp.somee.com/contacto"
            || pagina == "http://www.lubriapp.somee.com/registroVehiculo"
            || pagina == "https://www.lubriapp.somee.com/index"
            || pagina == "https://www.lubriapp.somee.com/catalogoProductos"
            || pagina == "https://www.lubriapp.somee.com/login"
            || pagina == "https://www.lubriapp.somee.com/registroCliente"
            || pagina == "https://www.lubriapp.somee.com/turnos"
            || pagina == "https://www.lubriapp.somee.com/contacto"
            || pagina == "https://www.lubriapp.somee.com/registroVehiculo")
            {
                if (!(Session["usuario"] == null))
                {
                    lblWebInterna.Visible = true;
                }
                else
                {
                    btnIniciarSesion.CssClass = "btn-invisible";
                    lblWebInterna.Visible = false;
                }
            }
            validarConexiónUsuario();
            cerrarConexiónUsuario();
        }


        protected void validarConexiónUsuario()
        {
            if (!(Session["usuario"] == null))
            {
                btnIniciarSesion.CssClass = "btn-invisible";
            }
        }

        protected void cerrarConexiónUsuario()
        {
            if (Session["usuario"] == null)
            {
                btnCerrarSesion2.CssClass = "btn-invisible";
            }
        }

        protected void btnCerrarSesion2_Click(object sender, EventArgs e)
        {
            Logout();
            Response.Redirect("index.aspx", false);
        }

        public void Logout()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        }
    }
}