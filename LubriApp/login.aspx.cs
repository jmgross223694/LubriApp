using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            UsuarioDB negocio = new UsuarioDB();

            try
            {
                usuario = new Usuario(txtUser.Text, txtPassword.Text, false);
                
                if (negocio.Loguear(usuario))
                {
                    txtUser.Text = "";
                    txtPassword.Text = "";
                    Session.Add("usernameLogueado", usuario.User);
                    Session.Add("usuario", usuario);
                    Response.Redirect("WebInterna.aspx", false);
                }
                else
                {
                    Session.Add("error","User o pass incorrectos.");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}