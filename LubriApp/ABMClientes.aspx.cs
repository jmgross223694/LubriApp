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
    public partial class ABMClientes : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {

            validarNivelUsuario();

            string selectTiposCliente = "SELECT * FROM TiposCliente";

            if (!IsPostBack)
            {
                ddlTiposCliente.DataSource = sentencia.DSET(selectTiposCliente);
                ddlTiposCliente.DataMember = "datos";
                ddlTiposCliente.DataTextField = "Descripcion";
                ddlTiposCliente.DataValueField = "ID";
                ddlTiposCliente.DataBind();

                ddlTiposCliente2.DataSource = sentencia.DSET(selectTiposCliente);
                ddlTiposCliente2.DataMember = "datos";
                ddlTiposCliente2.DataTextField = "Descripcion";
                ddlTiposCliente2.DataValueField = "ID";
                ddlTiposCliente2.DataBind();

                BindData();
            }
        }

        protected void validarNivelUsuario()
        {
            if (!(Session["usuario"] != null))
            {
                Session.Add("error", "Para ingresar a esta página debes estar logueado.");
                Response.Redirect("Error.aspx", false);
            }
        }

        public void BindData()
        {
            txtBuscar.Text = "";

            txtID.Text = "";
            txtCuitDni.Text = "";
            txtRazonSocial.Text = "";
            txtApeNom.Text = "";
            txtFechaAlta.Text = "";
            txtMail.Text = "";
            txtTelefono.Text = "";
            ddlTiposCliente.SelectedValue = "0";
            txtCantVehiculos.Text = "";
            ddlEstado.SelectedValue = "0";

            txtCuitDni2.Text = "";
            txtRazonSocial2.Text = "";
            txtApeNom2.Text = "";
            txtMail2.Text = "";
            txtTelefono2.Text = "";
            ddlTiposCliente2.SelectedValue = "0";

            txtID.Enabled = false;
            txtCuitDni.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtApeNom.Enabled = false;
            txtFechaAlta.Enabled = false;
            txtMail.Enabled = false;
            txtTelefono.Enabled = false;
            ddlTiposCliente.Enabled = false;
            txtCantVehiculos.Enabled = false;
            ddlEstado.Enabled = false;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            string selectViewClientes = "SELECT * FROM ExportClientes WHERE Estado = 1";

            dgvClientes.DataSource = sentencia.DSET(selectViewClientes);
            dgvClientes.DataBind();
        }

        protected void imgBtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            try
            {
                if (txtBuscar.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Filtro de texto vacío.')", true);

                    BindData();
                }
                else
                {
                    string Valor = txtBuscar.Text;

                    string selectDgv = "SELECT * from ExportClientes " +
                                       "WHERE ApeNom LIKE '%" + Valor + "%'" +
                                       " OR RazonSocial LIKE '%" + Valor + "%'" +
                                       " OR CUITDNI LIKE '%" + Valor + "%'" +
                                       " OR TipoCliente LIKE '%" + Valor + "%'" +
                                       " OR Mail LIKE '%" + Valor + "%'" +
                                       " OR Telefono LIKE '%" + Valor + "%'";

                    string selectCampos = "SELECT * from ExportClientes " +
                                       "WHERE ApeNom = '" + Valor + "'" +
                                       " OR RazonSocial = '" + Valor + "'" +
                                       " OR CUITDNI = '" + Valor + "'" +
                                       " OR Mail = '" + Valor + "'" +
                                       " OR Telefono = '" + Valor + "'";

                    datos2.SetearConsulta(selectDgv);
                    datos2.EjecutarLectura();

                    datos.SetearConsulta(selectCampos);
                    datos.EjecutarLectura();

                    dgvClientes.DataSource = sentencia.DSET(selectDgv);
                    dgvClientes.DataBind();

                    if (datos.Lector.Read() == true)
                    {
                        txtID.Text = datos.Lector["ID"].ToString();
                        txtCuitDni.Text = datos.Lector["CUITDNI"].ToString();
                        txtRazonSocial.Text = datos.Lector["RazonSocial"].ToString();
                        if (txtRazonSocial.Text == "-") { txtRazonSocial.Text = ""; }
                        txtApeNom.Text = datos.Lector["ApeNom"].ToString();
                        if (txtApeNom.Text == "-") { txtApeNom.Text = ""; }
                        txtFechaAlta.Text = datos.Lector["FechaAlta"].ToString();
                        txtMail.Text = datos.Lector["Mail"].ToString();
                        txtTelefono.Text = datos.Lector["Telefono"].ToString();
                        txtCantVehiculos.Text = datos.Lector["TotalVehiculosRegistrados"].ToString();
                        ddlTiposCliente.SelectedValue = datos.Lector["IdTipo"].ToString();
                        if ((bool)datos.Lector["Estado"] == true) { ddlEstado.SelectedValue = "1"; }
                        else { ddlEstado.SelectedValue = "2"; }

                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;

                        txtCuitDni.Enabled = true;
                        txtRazonSocial.Enabled = true;
                        txtApeNom.Enabled = true;
                        txtFechaAlta.Enabled = true;
                        txtMail.Enabled = true;
                        txtTelefono.Enabled = true;
                        ddlEstado.Enabled = true;
                        ddlTiposCliente.Enabled = true;
                    }
                    else
                    {
                        if (datos2.Lector.Read() == false)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No se encontraron coincidencias.')", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('La búsqueda no arrojó coincidencias exactas.')", true);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        protected void dgvClientes_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT * FROM ExportClientes ORDER BY " + e.SortExpression + " " 
                                    + GetSortDirection(e.SortExpression);

            dgvClientes.DataSource = sentencia.DSET(selectOrdenar);
            dgvClientes.DataBind();
        }

        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";

            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void imgBtnAgregarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuitDni2.Text == "" || txtRazonSocial2.Text == "" && txtApeNom2.Text == ""
                    || txtTelefono2.Text == "" || txtMail2.Text == "" || ddlTiposCliente2.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar. Por favor revise nuevamente.')", true);
                }
                else
                {
                    int IdTipo = Convert.ToInt32(ddlTiposCliente2.SelectedValue);
                    string CUIT_DNI = txtCuitDni2.Text;
                    string RazonSocial = txtRazonSocial2.Text;
                    string ApeNom = txtApeNom2.Text;
                    string Mail = txtMail2.Text;
                    string Telefono = txtTelefono2.Text;

                    if (RazonSocial == "")
                    {
                        string sp_InsertCliente = "EXEC SP_AGREGAR_CLIENTE_DNI " + IdTipo + ", '" + CUIT_DNI + "', '"
                                                + ApeNom + "', '" + Mail + "', '" + Telefono + "'";

                        sentencia.IUD(sp_InsertCliente);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha guardado el nuevo cliente.')", true);

                        BindData();
                    }
                    if (ApeNom == "")
                    {
                        string sp_InsertCliente = "EXEC SP_AGREGAR_CLIENTE_CUIT " + IdTipo + ", '" + CUIT_DNI + "', '" + RazonSocial
                                                + "', '" + Mail + "', '" + Telefono + "'";

                        sentencia.IUD(sp_InsertCliente);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha guardado el nuevo cliente.')", true);

                        BindData();
                    }
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha agregado el nuevo cliente.')", true);

                BindData();
            }
        }

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtCuitDni.Text == "" || txtRazonSocial.Text == "" && txtApeNom.Text == ""
                    || txtFechaAlta.Text == "" || txtMail.Text == "" 
                    || ddlTiposCliente.SelectedIndex == 0 || ddlEstado.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar. Por favor revise nuevamente.')", true);
                }
                else
                {
                    string ID = txtID.Text;
                    string CuitDni = txtCuitDni.Text;
                    string RazonSocial = txtRazonSocial.Text;
                    string ApeNom = txtApeNom.Text;
                    DateTime FechaAlta = Convert.ToDateTime(txtFechaAlta.Text);
                    FechaAlta.ToShortDateString();
                    string Mail = txtMail.Text;
                    string Telefono = txtTelefono.Text;
                    int TipoCliente = Convert.ToInt32(ddlTiposCliente.SelectedValue);
                    int Estado = 1;
                    if (ddlEstado.SelectedValue == "2") { Estado = 0; }


                    string sp_UpdateCliente = "EXEC SP_ACTUALIZAR_CLIENTE " + ID + ", " + TipoCliente + ", '" + CuitDni + "', '" + RazonSocial
                                              + "', '" + ApeNom + "', '" + FechaAlta + "', '" + Mail + "', '" + Telefono
                                              + "', " + Estado;

                    sentencia.IUD(sp_UpdateCliente);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Cliente modificado con éxito.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error en la edición.')", true);

                BindData();
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtCuitDni.Text == "" || txtRazonSocial.Text == "" && txtApeNom.Text == ""
                    || txtFechaAlta.Text == "" || txtMail.Text == "" 
                    || ddlTiposCliente.SelectedIndex == 0 || ddlEstado.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar. Por favor revise nuevamente.')", true);
                }
                else
                {
                    string ID = txtID.Text;

                    string sp_DeleteCliente = "DELETE FROM Clientes WHERE ID = " + ID;

                    sentencia.IUD(sp_DeleteCliente);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Cliente eliminado con éxito.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error al intentar eliminar al cliente.')", true);

                BindData();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportClientes " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvClientes.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnAbmVehiculos_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMVehiculos.aspx");
        }

    }
}
