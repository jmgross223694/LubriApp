using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMTiposServicio : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario(); //El validar usuario siempre primero!

            if (!IsPostBack)
            {
                BindData();
            }

            txtIdTipoServicio.Enabled = false;
            txtTipoServicio.Enabled = false;
            btnUpdate.Enabled = false;
            btnIO.Enabled = false;
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
            string selectMarcasVehiculo = "SELECT TS.ID ID, TS.Descripcion Descripcion, " +
                                          "(SELECT isnull(COUNT(S.ID), 0) FROM Servicios S " +
                                          "WHERE TS.ID = S.IdTipo) Asignaciones, " +
                                          "Estado as Estado " +
                                          "FROM TiposServicio TS ORDER BY TS.Descripcion ASC";

            dgvTiposServicio.DataSource = sentencia.DSET(selectMarcasVehiculo);
            dgvTiposServicio.DataBind();

            txtBuscar.Text = "";
            txtIdTipoServicio.Text = "";
            txtTipoServicio.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTipoServicio2.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Descripción vacía.')", true);
            }
            else
            {

                if (chequeoTipoServicio() == true)
                {
                    string TipoServicio = txtTipoServicio2.Text;

                    string GuardarTipoServicio = "INSERT INTO TiposServicio (Descripcion) " +
                                                 "values('" + TipoServicio + "')";

                    sentencia.IUD(GuardarTipoServicio);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha agregado el Tipo de Servicio.')", true);

                    BindData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('El Tipo de Servicio ya existe.')", true);

                    BindData();
                }
            }
        }

        protected bool chequeoTipoServicio()
        {
            string TipoServicio = txtTipoServicio2.Text;

            string Consulta = "select isnull(COUNT(*), 0) from TiposServicio where Descripcion = '" + TipoServicio + "'";

            int existe = sentencia.IUDquery(Consulta);

            if (existe >= 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void dgvTiposServicio_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT TS.ID ID, TS.Descripcion Descripcion, " +
                                   "(SELECT isnull(COUNT(S.ID), 0) FROM Servicios S " +
                                   "WHERE TS.ID = S.IdTipo) Asignaciones " +
                                   "FROM TiposServicio TS ORDER BY "
                                   + e.SortExpression + " "
                                   + GetSortDirection(e.SortExpression);

            dgvTiposServicio.DataSource = sentencia.DSET(selectOrdenar);
            dgvTiposServicio.DataBind();
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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();

            if (txtBuscar.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Descripción vacía.')", true);
            }
            else
            {
                string valor = txtBuscar.Text;
                string selectBuscarTipoServicioGrilla = "SELECT TS.ID ID, TS.Descripcion Descripcion, " +
                                                 "(SELECT isnull(COUNT(S.ID), 0) FROM Servicios S " +
                                                 "WHERE TS.ID = S.IdTipo) Asignaciones, " +
                                                 "Estado as Estado " +
                                                 "FROM TiposServicio TS " +
                                                 "WHERE TS.Descripcion LIKE '%" + valor + "%'";

                string selectBuscarTipoServicioCampos = "SELECT TS.ID ID, TS.Descripcion Descripcion, " +
                                                 "(SELECT isnull(COUNT(S.ID), 0) FROM Servicios S " +
                                                 "WHERE TS.ID = S.IdTipo) Asignaciones, " +
                                                 "Estado as Estado " +
                                                 "FROM TiposServicio TS " +
                                                 "WHERE TS.Descripcion = '" + valor + "'";

                datos2.SetearConsulta(selectBuscarTipoServicioGrilla);
                datos2.EjecutarLectura();

                dgvTiposServicio.DataSource = sentencia.DSET(selectBuscarTipoServicioGrilla);
                dgvTiposServicio.DataBind();

                datos.SetearConsulta(selectBuscarTipoServicioCampos);
                datos.EjecutarLectura();

                if (datos.Lector.Read() == true)
                {
                    txtIdTipoServicio.Text = datos.Lector["ID"].ToString();
                    txtTipoServicio.Text = (string)datos.Lector["Descripcion"];

                    txtTipoServicio.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnIO.Enabled = true;
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

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtIdTipoServicio.Text = "";
            txtTipoServicio.Text = "";

            BindData();
        }

        protected void btnIO_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTipoServicio.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string id = txtIdTipoServicio.Text;
                    string descripcion = txtTipoServicio.Text;

                    sentencia.IUD("DELETE FROM TiposServicio WHERE ID = '" + id + "' AND Descripcion = '" + descripcion + "'");

                    AccesoDatos datos = new AccesoDatos();
                    string selectEstado = "SELECT Estado FROM TiposServicio WHERE ID = " + id;
                    datos.SetearConsulta(selectEstado);
                    datos.EjecutarLectura();
                    int Estado = 0;

                    if (datos.Lector.Read() == true)
                    {
                        Estado = Convert.ToInt32(datos.Lector["Estado"]);
                    }

                    if (Estado == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha deshabilitado el tipo de servicio: " + descripcion + ".')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se habilitó el tipo de servicio: " + descripcion + ".')", true);
                    }

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('El Tipo de Servicio seleccionado está asignado a uno o varios Servicios y no se puede eliminar.')", true);

                BindData();
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtTipoServicio.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string id = txtIdTipoServicio.Text;
                    string descripcion = txtTipoServicio.Text;

                    sentencia.IUD("UPDATE TiposServicio SET Descripcion = '" + descripcion + "' WHERE ID = " + id);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Tipo de servicio actualizado correctamente.')", true);

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha modificado la marca.')", true);

                BindData();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportMarcasVehiculo " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvTiposServicio.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvTiposServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTiposServicio.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}