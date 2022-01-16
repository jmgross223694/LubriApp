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
    public partial class ABMEmpleado : System.Web.UI.Page
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

        public void BindData()
        {
            txtBuscar.Text = "";

            txtLegajo.Text = "";
            txtCuil.Text = "";
            txtApeNom.Text = "";
            txtFechaAlta.Text = "";
            txtFechaNacimiento.Text = "";
            txtMail.Text = "";
            txtTelefono.Text = "";
            txtServiciosRealizados.Text = "";

            txtLegajo2.Text = "";
            txtCuil2.Text = "";
            txtApeNom2.Text = "";
            txtFechaAlta2.Text = "";
            txtFechaNacimiento2.Text = "";
            txtMail2.Text = "";
            txtTelefono2.Text = "";

            txtLegajo.Enabled = false;
            txtCuil.Enabled = false;
            txtApeNom.Enabled = false;
            txtFechaAlta.Enabled = false;
            txtFechaNacimiento.Enabled = false;
            txtMail.Enabled = false;
            txtTelefono.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            string selectViewEmpleados = "SELECT * FROM ExportEmpleados";

            dgvEmpleados.DataSource = sentencia.DSET(selectViewEmpleados);
            dgvEmpleados.DataBind();
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
                    "alert('No se ha ingresado ningún nombre / apellido.')", true);

                    BindData();
                }
                else
                {
                    string Valor = txtBuscar.Text;

                    string selectDgv = "SELECT * from ExportEmpleados " +
                                       "WHERE ApeNom LIKE '%" + Valor + "%'" + 
                                       " OR Cuil LIKE '%" + Valor + "%'" +
                                       " OR Legajo LIKE '%" + Valor + "%'";

                    string selectCampos = "SELECT * from ExportEmpleados " +
                                          "WHERE ApeNom = '" + Valor + "'" + 
                                          " OR Cuil = '" + Valor + "'" +
                                          " OR Legajo = '" + Valor + "'";

                    datos2.SetearConsulta(selectDgv);
                    datos2.EjecutarLectura();

                    datos.SetearConsulta(selectCampos);
                    datos.EjecutarLectura();

                    dgvEmpleados.DataSource = sentencia.DSET(selectDgv);
                    dgvEmpleados.DataBind();

                    if (datos.Lector.Read() == true)
                    {
                        txtID.Text = datos.Lector["ID"].ToString();
                        txtLegajo.Text = datos.Lector["Legajo"].ToString();
                        txtCuil.Text = datos.Lector["Cuil"].ToString();
                        txtApeNom.Text = datos.Lector["ApeNom"].ToString();
                        txtFechaAlta.Text = datos.Lector["FechaAlta"].ToString();
                        txtFechaNacimiento.Text = datos.Lector["FechaNacimiento"].ToString();
                        txtMail.Text = datos.Lector["Mail"].ToString();
                        txtTelefono.Text = datos.Lector["Telefono"].ToString();
                        txtServiciosRealizados.Text = datos.Lector["ServiciosRealizados"].ToString();

                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;

                        txtLegajo.Enabled = true;
                        txtCuil.Enabled = true;
                        txtApeNom.Enabled = true;
                        txtFechaAlta.Enabled = true;
                        txtFechaNacimiento.Enabled = true;
                        txtMail.Enabled = true;
                        txtTelefono.Enabled = true;
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
                "alert('Se produjo un error en la búsqueda.')", true);

                BindData();
            }
            finally
            {
                datos.CerrarConexion();
                datos2.CerrarConexion();
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

        protected void dgvEmpleados_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT * FROM ExportEmpleados ORDER BY " + e.SortExpression + " " 
                                    + GetSortDirection(e.SortExpression);

            dgvEmpleados.DataSource = sentencia.DSET(selectOrdenar);
            dgvEmpleados.DataBind();
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

        protected void imgBtnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLegajo2.Text == "" || txtCuil2.Text == "" || txtApeNom2.Text == ""
                    || txtFechaAlta2.Text == "" || txtFechaNacimiento2.Text == "" 
                    || txtMail2.Text == "" || txtTelefono2.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos.')", true);
                }
                else
                {
                    string Legajo = txtLegajo2.Text;
                    string Cuil = txtCuil2.Text;
                    string ApeNom = txtApeNom2.Text;
                    DateTime FechaAlta = Convert.ToDateTime(txtFechaAlta2.Text);
                    DateTime FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento2.Text);
                    FechaAlta.ToShortDateString();
                    FechaNacimiento.ToShortDateString();
                    string Mail = txtMail2.Text;
                    string Telefono = txtTelefono2.Text;

                    string sp_InsertEmpleado = "EXEC SP_INSERTAR_EMPLEADO '" + Legajo + "', '" + Cuil + "', '" + ApeNom
                                             + "', '" + FechaAlta + "', '" + FechaNacimiento + "', '" + Mail
                                             + "', '" + Telefono + "'";

                    sentencia.IUD(sp_InsertEmpleado);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha guardado el nuevo empleado.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha guardado el nuevo empleado.')", true);
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
                if (txtLegajo.Text == "" || txtCuil.Text == "" || txtApeNom.Text == ""
                    || txtFechaAlta.Text == "" || txtFechaNacimiento.Text == ""
                    || txtMail.Text == "" || txtTelefono.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos.')", true);
                }
                else
                {
                    string ID = txtID.Text;
                    string Legajo = txtLegajo.Text;
                    string Cuil = txtCuil.Text;
                    string ApeNom = txtApeNom.Text;
                    DateTime FechaAlta = Convert.ToDateTime(txtFechaAlta.Text);
                    DateTime FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
                    FechaAlta.ToShortDateString();
                    FechaNacimiento.ToShortDateString();
                    string Mail = txtMail.Text;
                    string Telefono = txtTelefono.Text;

                    string sp_UpdateEmpleado = "UPDATE Empleados SET Legajo='" + Legajo + "', " +
                                             "Cuil='" + Cuil + "', ApeNom='" + ApeNom
                                             + "', FechaAlta='" + FechaAlta + "', " +
                                             "FechaNacimiento='" + FechaNacimiento + "', Mail='" + Mail
                                             + "', Telefono='" + Telefono + "' WHERE ID = '" + ID + "'";

                    sentencia.IUD(sp_UpdateEmpleado);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha modificado el empleado.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha modificado el empleado.')", true);
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtLegajo.Text == "" || txtCuil.Text == "" || txtApeNom.Text == ""
                    || txtFechaAlta.Text == "" || txtFechaNacimiento.Text == ""
                    || txtMail.Text == "" || txtTelefono.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos.')", true);
                }
                else
                {
                    string ID = txtID.Text;
                    string Legajo = txtLegajo.Text;

                    string sp_DeleteEmpleado = "DELETE FROM Empleados WHERE ID = '" + ID + "' "
                                             + "AND Legajo = '" + Legajo + "'";

                    sentencia.IUD(sp_DeleteEmpleado);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha eliminado el empleado.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('El empleado tiene asociado uno o más servicios y no se puede eliminar.')", true);

                BindData();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportEmpleados " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvEmpleados.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvEmpleados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmpleados.PageIndex = e.NewPageIndex;
            BindData();
        }

    }
}
