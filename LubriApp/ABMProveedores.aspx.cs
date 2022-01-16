using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMProveedores : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario();

            if (!IsPostBack)
            {
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

            txtCuit.Text = "";
            txtRazonSocial.Text = "";
            txtId.Text = "";
            ddlEstado.SelectedValue = "0";

            txtCuit2.Text = "";
            txtRazonSocial2.Text = "";
            txtId.Text = "";

            txtCuit.Enabled = false;
            txtRazonSocial.Enabled = false;
            ddlEstado.Enabled = false;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            dgvProveedores.DataSource = sentencia.DSET("SELECT * FROM ExportProveedores ORDER BY RazonSocial ASC");
            dgvProveedores.DataBind();
        }

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            BindData();
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
                    string valor = txtBuscar.Text;

                    string selectCamposProveedor = "SELECT * FROM ExportProveedores " +
                                                   " WHERE CUIT = '" + valor + "' " +
                                                   " OR RazonSocial = '" + valor + "'";

                    string selectDgvProveedores = "SELECT * FROM ExportProveedores " +
                                                   " WHERE CUIT LIKE '%" + valor + "%' " +
                                                   " OR RazonSocial LIKE '%" + valor + "%'";

                    datos.SetearConsulta(selectCamposProveedor);
                    datos.EjecutarLectura();

                    datos2.SetearConsulta(selectDgvProveedores);
                    datos2.EjecutarLectura();

                    dgvProveedores.DataSource = sentencia.DSET(selectDgvProveedores);
                    dgvProveedores.DataBind();

                    if (datos.Lector.Read() == true)
                    {
                        txtId.Text = datos.Lector["ID"].ToString();
                        txtCuit.Text = (string)datos.Lector["CUIT"];
                        txtRazonSocial.Text = (string)datos.Lector["RazonSocial"];
                        if ((bool)datos.Lector["Estado"] == true) { ddlEstado.SelectedValue = "1"; }
                        else { ddlEstado.SelectedValue = "2"; }

                        txtCuit.Enabled = true;
                        txtRazonSocial.Enabled = true;
                        ddlEstado.Enabled = true;

                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else if (datos2.Lector.Read() == true)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('La búsqueda no arrojó coincidencias exactas.')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('No se ha encontrado ninguna coincidencia.')", true);

                        BindData();
                    }
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error en la búsqueda.')", true);

                BindData();
            }
            finally
            {
                datos.CerrarConexion();
                datos2.CerrarConexion();
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                long ID = Convert.ToInt64(txtId.Text);
                string CUIT = txtCuit.Text;
                string RazonSocial = txtRazonSocial.Text;
                int Estado = 0;
                if (ddlEstado.SelectedValue == "1") { Estado = 1; }

                if (txtCuit.Text == "" || txtRazonSocial.Text == "" || ddlEstado.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar, por favor revise nuevamente.')", true);
                }
                else
                {
                    string updateSupplier = "UPDATE Proveedores SET CUIT = '" + CUIT + "', " + 
                                            "RazonSocial = '" + RazonSocial + "', " + 
                                            "Estado = " + Estado + " WHERE ID = " + ID;

                    sentencia.IUD(updateSupplier);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Proveedor modificado con éxito.')", true);

                    BindData();
                }
            }
            catch (Exception)
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
                string ID = txtId.Text;

                if (txtCuit.Text == "" || txtRazonSocial.Text == "" || ddlEstado.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar, por favor revise nuevamente.')", true);
                }
                else
                {
                    string deleteSupplier = "DELETE FROM Proveedores WHERE ID = '" + ID + "'";

                    sentencia.IUD(deleteSupplier);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Proveedor eliminado con éxito.')", true);

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('No se puede eliminar el proveedor, ya que está asociado a algún producto.')", true);

                BindData();
            }
        }

        protected void dgvProveedores_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT * FROM ExportProveedores ORDER BY " + e.SortExpression + " "
                                    + GetSortDirection(e.SortExpression);

            dgvProveedores.DataSource = sentencia.DSET(selectOrdenar);
            dgvProveedores.DataBind();
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

        protected void imgBtnAgregarProveedor_Click(object sender, EventArgs e)
        {
            try
            {
                string CUIT = txtCuit2.Text;
                string RazonSocial = txtRazonSocial2.Text;

                if (txtCuit2.Text == "" || txtRazonSocial2.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos, por favor revise nuevamente.')", true);
                }
                else
                {
                    string insertSupplier = "EXEC SP_INSERTAR_PROVEEDOR '" + CUIT + "', '" + RazonSocial + "'";

                    sentencia.IUD(insertSupplier);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Proveedor agregado con éxito.')", true);

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se produjo un error al intentar agregar el proveedor.')", true);

                BindData();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportProveedores " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvProveedores.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProveedores.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}