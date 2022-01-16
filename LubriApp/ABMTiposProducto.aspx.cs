using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMTiposProducto : System.Web.UI.Page
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
            string selectViewTiposProducto = "SELECT TP.ID ID, TP.Descripcion Descripcion, " +
                                             "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                             "WHERE TP.ID = I.IdTipo) Asignaciones " +
                                             "FROM TiposProducto TP ORDER BY TP.Descripcion ASC";

            dgvTiposProducto.DataSource = sentencia.DSET(selectViewTiposProducto);
            dgvTiposProducto.DataBind();

            txtDescripcionTipoProducto.Enabled = false;
            txtIdTipoProducto.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtDescripcionTipoProductoBuscar.Text = "";
            txtIdTipoProducto.Text = "";
            txtDescripcionTipoProducto.Text = "";
            txtDescripcionTipoProducto2.Text = "";

        }

        protected void btnDeleteTipoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionTipoProducto.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {

                    string ID = txtIdTipoProducto.Text;
                    string Descripcion = txtDescripcionTipoProducto.Text;

                    string DeleteTipoProducto = "DELETE FROM TiposProducto WHERE ID = '" + ID + "'" +
                                                   " AND Descripcion = '" + Descripcion + "'";

                    sentencia.IUD(DeleteTipoProducto);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Tipo de producto eliminado con éxito.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('El tipo de producto seleccionado está asignado a uno o varios productos y no se puede eliminar.')", true);
                
                BindData();
            }
        }

        protected void btnUpdateTipoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionTipoProducto.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string ID = txtIdTipoProducto.Text;
                    string Descripcion = txtDescripcionTipoProducto.Text;

                    string sp_UpdateTipoProducto = "UPDATE TiposProducto SET Descripcion = '" + Descripcion + "' WHERE ID = " + ID;

                    sentencia.IUD(sp_UpdateTipoProducto);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Tipo de producto modificado con éxito.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha modificado el tipo de producto.')", true);
               
                BindData();
            }
        }

        protected void btnAddTipoProducto_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionTipoProducto2.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string Descripcion = txtDescripcionTipoProducto2.Text;

                    string sp_InsertTipoProducto = "EXEC SP_INSERTAR_TIPO_PRODUCTO '" + Descripcion + "'";

                    sentencia.IUD(sp_InsertTipoProducto);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Tipo de producto agregado con éxito.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Error. Ya existe el tipo de producto ingresado.')", true);

                BindData();
            }
        }

        protected void imgBtnBuscarTipoProducto_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            try
            {
                if (txtDescripcionTipoProductoBuscar.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Ingresa alguna palabra para buscar.')", true);

                    BindData();
                }
                else
                {
                    string Valor = txtDescripcionTipoProductoBuscar.Text;

                    string selectDgvTipoProducto = "SELECT TP.ID ID, TP.Descripcion Descripcion, " +
                                                   "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                                   "WHERE TP.ID = I.IdTipo) Asignaciones " +
                                                   "FROM TiposProducto TP " +
                                                   "WHERE TP.Descripcion LIKE '%" + Valor + "%'";

                    datos2.SetearConsulta(selectDgvTipoProducto);
                    datos2.EjecutarLectura();

                    dgvTiposProducto.DataSource = sentencia.DSET(selectDgvTipoProducto);
                    dgvTiposProducto.DataBind();

                    string selectFiltroTipoProducto = "SELECT TP.ID ID, TP.Descripcion Descripcion, " +
                                                      "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                                      "WHERE TP.ID = I.IdTipo) Asignaciones " +
                                                      "FROM TiposProducto TP " +
                                                      "WHERE TP.Descripcion = '" + Valor + "'";

                    datos.SetearConsulta(selectFiltroTipoProducto);
                    datos.EjecutarLectura();

                    if (datos.Lector.Read() == true)
                    {
                        txtIdTipoProducto.Text = datos.Lector["ID"].ToString();
                        txtDescripcionTipoProducto.Text = (string)datos.Lector["Descripcion"];

                        txtDescripcionTipoProducto.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        if (datos2.Lector.Read() == false)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No se encontraron coincidencias.')", true);

                            BindData();
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error en la búsqueda.')", true);

                BindData();
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void dgvTiposProducto_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT TP.ID ID, TP.Descripcion Descripcion, " +
                                   "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                   "WHERE TP.ID = I.IdTipo) Asignaciones " +
                                   "FROM TiposProducto TP ORDER BY "
                                   + e.SortExpression + " "
                                   + GetSortDirection(e.SortExpression);

            dgvTiposProducto.DataSource = sentencia.DSET(selectOrdenar);
            dgvTiposProducto.DataBind();
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportTiposProducto " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvTiposProducto.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvTiposProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTiposProducto.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}