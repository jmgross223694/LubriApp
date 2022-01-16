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
    public partial class ABMMarcasProducto : System.Web.UI.Page
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
            string selectMarcasProducto = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                          "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                          "WHERE M.ID = I.IdMarca) Asignaciones " +
                                          "FROM MarcasProducto M ORDER BY M.Descripcion ASC";

            dgvMarcasProducto.DataSource = sentencia.DSET(selectMarcasProducto);
            dgvMarcasProducto.DataBind();

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

                    string GuardarMarca = "INSERT INTO MarcasProducto (Descripcion) values('" + Marca + "')";                   

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

            string Consulta = "select isnull(count(*), 0) from MarcasProducto where Descripcion like '" + Marca + "'";

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
                                                 "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                                 "WHERE M.ID = I.IdMarca) Asignaciones " +
                                                 "FROM MarcasProducto M " +
                                                 "WHERE M.Descripcion LIKE '%" + valor + "%'";

                string selectBuscarMarcaCampos = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                                 "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                                 "WHERE M.ID = I.IdMarca) Asignaciones " +
                                                 "FROM MarcasProducto M " +
                                                 "WHERE M.Descripcion = '" + valor + "'";

                datos2.SetearConsulta(selectBuscarMarcaGrilla);
                datos2.EjecutarLectura();

                dgvMarcasProducto.DataSource = sentencia.DSET(selectBuscarMarcaGrilla);
                dgvMarcasProducto.DataBind();

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

                    sentencia.IUD("DELETE FROM MarcasProducto WHERE ID = '" + id + "' AND Descripcion = '" + descripcion + "'");

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Marca eliminada con éxito.')", true);

                    BindData();
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('La marca seleccionada está asignada a uno o varios productos y no se puede eliminar.')", true);

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

                    sentencia.IUD("UPDATE MarcasProducto SET Descripcion = '" + descripcion + "' WHERE ID = '" + id + "'");

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

        protected void dgvMarcasProducto_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT M.ID ID, M.Descripcion Descripcion, " +
                                   "(SELECT isnull(COUNT(I.ID), 0) FROM Inventario I " +
                                   "WHERE M.ID = I.IdMarca) Asignaciones " +
                                   "FROM MarcasProducto M ORDER BY "
                                   + e.SortExpression + " "
                                   + GetSortDirection(e.SortExpression);

            dgvMarcasProducto.DataSource = sentencia.DSET(selectOrdenar);
            dgvMarcasProducto.DataBind();
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
            Response.AddHeader("content-disposition", "attachment;filename = ExportMarcasProducto " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvMarcasProducto.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvMarcasProducto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcasProducto.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}