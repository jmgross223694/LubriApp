using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMMarcasVehiculo : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario();

            if (!IsPostBack)
            {
                BindData();
            }

            txtIdMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
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
            string selectMarcasVehiculo = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                          "(SELECT isnull(COUNT(V.ID), 0) FROM Vehiculos V " +
                                          "WHERE M.ID = V.IdMarca) Asignaciones " +
                                          "FROM MarcasVehiculo M ORDER BY M.Descripcion ASC";

            dgvMarcasVehiculo.DataSource = sentencia.DSET(selectMarcasVehiculo);
            dgvMarcasVehiculo.DataBind();

            txtBuscar.Text = "";
            txtIdMarca.Text = "";
            txtMarca.Text = "";
            txtMarca2.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMarca2.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Descripción vacía.')", true);
            }
            else
            {

                if (chequeoMarca() == true)
                {
                    string Marca = txtMarca2.Text;

                    string GuardarMarca = "INSERT INTO MarcasVehiculo (Descripcion) values('" + Marca + "')";                   

                    sentencia.IUD(GuardarMarca);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha agregado la Marca.')" , true);

                    BindData();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('La Marca ya se encuentra ingresada.')", true);

                    BindData();
                }
            }
        }

        protected bool chequeoMarca()
        {
            string Marca = txtMarca2.Text;

            string Consulta = "select isnull(count(*), 0) from MarcasVehiculo where Descripcion = '" + Marca + "'";

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

        protected void dgvMarcasVehiculo_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                   "(SELECT isnull(COUNT(V.ID), 0) FROM Vehiculos V " +
                                   "WHERE M.ID = V.IdMarca) Asignaciones " +
                                   "FROM MarcasVehiculo M ORDER BY "
                                   + e.SortExpression + " "
                                   + GetSortDirection(e.SortExpression);

            dgvMarcasVehiculo.DataSource = sentencia.DSET(selectOrdenar);
            dgvMarcasVehiculo.DataBind();
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
                string selectBuscarMarcaGrilla = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                                 "(SELECT isnull(COUNT(V.ID), 0) FROM Vehiculos V " +
                                                 "WHERE M.ID = V.IdMarca) Asignaciones " +
                                                 "FROM MarcasVehiculo M " +
                                                 "WHERE M.Descripcion LIKE '%" + valor + "%'";

                string selectBuscarMarcaCampos = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                                 "(SELECT isnull(COUNT(V.ID), 0) FROM Vehiculos V " +
                                                 "WHERE M.ID = V.IdMarca) Asignaciones " +
                                                 "FROM MarcasVehiculo M " +
                                                 "WHERE M.Descripcion = '" + valor + "'";

                datos2.SetearConsulta(selectBuscarMarcaGrilla);
                datos2.EjecutarLectura();

                dgvMarcasVehiculo.DataSource = sentencia.DSET(selectBuscarMarcaGrilla);
                dgvMarcasVehiculo.DataBind();

                datos.SetearConsulta(selectBuscarMarcaCampos);
                datos.EjecutarLectura();

                if(datos.Lector.Read() == true)
                {
                    txtIdMarca.Text = datos.Lector["ID"].ToString();
                    txtMarca.Text = (string)datos.Lector["Descripcion"];

                    txtMarca.Enabled = true;
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

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            txtIdMarca.Text = "";
            txtMarca.Text = "";

            BindData();
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtMarca.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string id = txtIdMarca.Text;
                    string descripcion = txtMarca.Text;

                    sentencia.IUD("DELETE FROM MarcasVehiculo WHERE ID = '" + id + "' AND Descripcion = '" + descripcion + "'");

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Marca eliminada con éxito.')", true);

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('La marca seleccionada está asignada a uno o varios vehículos y no se puede eliminar.')", true);

                BindData();
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtMarca.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción vacía.')", true);
                }
                else
                {
                    string id = txtIdMarca.Text;
                    string descripcion = txtMarca.Text;

                    sentencia.IUD("UPDATE MarcasVehiculo SET Descripcion = '" + descripcion + "' WHERE ID = '" + id + "'");

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Descripción de marca actualizada con éxito.')", true);

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
            dgvMarcasVehiculo.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvMarcasVehiculo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcasVehiculo.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}