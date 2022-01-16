using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMUsuario_aspx : System.Web.UI.Page
    {
        AccesoDatos negocio = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario();            

            string selectTiposUsuario = "SELECT * FROM TiposUsuario";

            if(!IsPostBack)
            {
                ddlTipoUsuario.DataSource = negocio.DSET(selectTiposUsuario);
                ddlTipoUsuario.DataMember = "datos";
                ddlTipoUsuario.DataTextField = "Descripcion";
                ddlTipoUsuario.DataValueField = "ID";
                ddlTipoUsuario.DataBind();

                BindData();
            }
        }
        
        protected void validarNivelUsuario()
        {
            if (!(Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).TipoUsuario == Dominio.TipoUsuario.ADMIN))
            {
                Session.Add("error", "Para ingresar a esta página debes tener nivel de usuario ADMIN.");
                Response.Redirect("Error.aspx", false);
            }
        }

        public void BindData()
        {
            txtUser.Text = "";
            txtPassword.Text = "";
            txtPassword2.Text = "";
            mail.Text = "";
            ddlTipoUsuario.SelectedValue = "0";

            string selectUsuarios = "SELECT * FROM ExportUsuarios";

            dgvUsuarios.DataSource = negocio.DSET(selectUsuarios);
            dgvUsuarios.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtUser.Text == "" || txtPassword.Text == "" || txtPassword2.Text == "" || mail.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos o sin seleccionar.')", true);
            }
            else
            {
                if(txtPassword.Text != txtPassword2.Text)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Las contraseñas no coinciden.')", true);
                }
                else
                {
                    string User = txtUser.Text;
                    string Pass = txtPassword.Text;
                    string Mail = mail.Text;
                    int TipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);

                    string sp_VerificarUsuario = "Select * from Usuarios where Usuario = '" + User + "'";

                    negocio.SetearConsulta(sp_VerificarUsuario);
                    negocio.EjecutarLectura();

                    if (negocio.Lector.Read() == false)
                    {
                        string sp_InsertarUsuario = "Exec SP_INSERTAR_USUARIO '" + User + "', '" + Pass + "', '" + Mail + "', '" + TipoUsuario + "'";

                        negocio.IUD(sp_InsertarUsuario);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha registrado el usuario.')", true);

                        BindData();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El Usuario ya se encuentra registrado.')", true);
                    }
                }
            }
        }
    }
}