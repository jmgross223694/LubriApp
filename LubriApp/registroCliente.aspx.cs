using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class registroCliente : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["btnRegistroClick"] != null)
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                Session.Add("error", "Ha sido redirigido a esta página por error.");

                Response.Redirect("Error.aspx");
            }
        }

        private void BindData()
        {
            string selectTiposCliente = "SELECT * FROM TiposCliente ORDER BY Descripcion ASC";

            ddlIdTipo.DataSource = sentencia.DSET(selectTiposCliente);
            ddlIdTipo.DataMember = "datos";
            ddlIdTipo.DataTextField = "Descripcion";
            ddlIdTipo.DataValueField = "ID";
            ddlIdTipo.DataBind();

            txtApeNom.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtCuitDni.Enabled = false;
            txtTelefono.Enabled = false;
            txtMail.Enabled = false;
            btnConfirmarRegistro.Enabled = false;

            lblValidacionCuitDni.CssClass = "stl-lbl-Validar-Cuit";
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("turnos.aspx");
        }

        protected void btnConfirmarRegistro_Click(object sender, EventArgs e)
        {
            if (ddlIdTipo.SelectedValue == "1" || ddlIdTipo.SelectedValue == "3" || ddlIdTipo.SelectedValue == "4")
            {
                if (txtCuitDni.Text.Length <= 8)
                {
                    Session.Add("error", "El tipo de cliente no coincide con la longitud del CUIT/DNI ingresado.");
                    Response.Redirect("error.aspx");
                }
            }
            if (ddlIdTipo.SelectedValue == "2" && txtCuitDni.Text.Length > 8)
            {
                Session.Add("error", "El tipo de cliente no coincide con la longitud del CUIT/DNI ingresado.");
                Response.Redirect("error.aspx");
            }
            if (txtMail.Text == "" || txtTelefono.Text == "" || ddlIdTipo.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos ó sin seleccionar.\\n\\n" +
                "Por favor revíselos y reintente.')", true);
            }

            if (txtCuitDni.Text.Length < 7)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('El CUIT ó DNI es inválido.\\n\\n" +
                "Por favor revíselo y reintente.')", true);
            }
            else if (txtCuitDni.Text.Length > 8 && txtRazonSocial.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Razón Social sin completar. Por favor ingrésela.')", true);
            }
            else if (txtCuitDni.Text.Length <= 8 && txtApeNom.Text == "" && ddlIdTipo.SelectedValue == "2" )
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Nombre y Apellido sin completar. Por favor ingréselos.')", true);
            }
            else
            {
                if (txtCuitDni.Text.Length > 8) { txtApeNom.Text = ""; }
                else { txtRazonSocial.Text = ""; }

                int cantidadClientes = 0;
                AccesoDatos datos = new AccesoDatos();
                string validarCliente = "SELECT ISNULL(COUNT(*), 0) Cantidad FROM Clientes WHERE CUIT_DNI = '" + txtCuitDni.Text + "'";
                try
                {
                    datos.SetearConsulta(validarCliente);
                    datos.EjecutarLectura();

                    if (datos.Lector.Read() == true)
                    {
                        cantidadClientes = Convert.ToInt32(datos.Lector["Cantidad"]);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha producido un error en la BD. Por favor reintente en unos minutos.')", true);
                }
                finally
                {
                    datos.CerrarConexion();
                }

                if (cantidadClientes == 0)
                {
                    Session.Add("CuitDni", txtCuitDni.Text);
                    Session.Add("ApeNom", txtApeNom.Text);
                    Session.Add("RazonSocial", txtRazonSocial.Text);
                    Session.Add("Telefono", txtTelefono.Text);
                    Session.Add("Mail", txtMail.Text);
                    Session.Add("TipoCliente", ddlIdTipo.SelectedValue.ToString());

                    Response.Redirect("registroVehiculo.aspx");
                }
                else
                {
                    lblValidacionCuitDni.CssClass = "stl-alerta-Cuit-Registro-Cliente";
                }
            }
        }

        protected void ddlIdTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoCliente = ddlIdTipo.SelectedItem.ToString();

            if (ddlIdTipo.SelectedValue != "0")
            {
                txtCuitDni.Enabled = true;
                txtTelefono.Enabled = true;
                txtMail.Enabled = true;
                btnConfirmarRegistro.Enabled = true;
            }

            if (tipoCliente == "Particular")
            { 
                txtApeNom.Enabled = true;
                txtRazonSocial.Enabled = false;
                txtCuitDni.Attributes.Add("placeholder", "DNI");
                txtCuitDni.MaxLength = 8;
            }
            else if (ddlIdTipo.SelectedValue != "0")
            { 
                txtApeNom.Enabled = false;
                txtRazonSocial.Enabled = true;
                txtCuitDni.Attributes.Add("placeholder", "CUIT");
                txtCuitDni.MaxLength = 11;
            }
        }
    }
}