using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMVehiculos : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario();

            if (!IsPostBack)
            {
                BindData();
                cargarDdlAños();
                CargarMarcasVehiculo();
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

        public void CargarMarcasVehiculo()
        {
            string MarcasVehiculo = "SELECT * FROM MarcasVehiculo ORDER BY Descripcion ASC";
            string MarcasVehiculo2 = "SELECT * FROM MarcasVehiculo ORDER BY Descripcion ASC";

            ddlMarcaVehiculo.DataSource = sentencia.DSET(MarcasVehiculo);
            ddlMarcaVehiculo.DataMember = "datos";
            ddlMarcaVehiculo.DataTextField = "Descripcion";
            ddlMarcaVehiculo.DataValueField = "ID";
            ddlMarcaVehiculo.DataBind();

            ddlMarcaVehiculo2.DataSource = sentencia.DSET(MarcasVehiculo2);
            ddlMarcaVehiculo2.DataMember = "datos";
            ddlMarcaVehiculo2.DataTextField = "Descripcion";
            ddlMarcaVehiculo2.DataValueField = "ID";
            ddlMarcaVehiculo2.DataBind();
        }

        public void BindData()
        {
            string selectVehiculos = "SELECT * FROM ExportVehiculos ORDER BY [Fecha de alta] ASC";

            dgvVehiculos.DataSource = sentencia.DSET(selectVehiculos);
            dgvVehiculos.DataBind();

            txtBuscar.Text = "";
            ddlFiltroBuscar.SelectedValue = "Patente";
            ddlMarcaVehiculo2.Enabled = false;
            ddlAnioFabricacion2.Enabled = false;
            txtPatente2.Enabled = false;
            txtModelo2.Enabled = false;
            cbEstado.Checked = false;
            cbEstado.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            ddlMarcaVehiculo2.SelectedValue = "0";
            ddlAnioFabricacion2.SelectedValue = "0";
            txtPatente2.Text = "";
            txtModelo2.Text = "";

            btnModificar.Visible = false;
            btnModificar.Enabled = false;
            btnEliminar.Visible = false;
            btnEliminar.Enabled = false;
        }

        private void cargarDdlAños()
        {
            List<int> listaAños = new List<int>();

            int añoDesde = 1970;
            int añoHasta = DateTime.Today.Year;

            for (int i = añoDesde; i <= añoHasta; i++)
            {
                listaAños.Add(i);
            }

            int tamaño = añoHasta - añoDesde;

            for (int i = 0; i <= tamaño; i++)
            {
                ddlAnioFabricacion.Items.Add(listaAños[i].ToString());
                ddlAnioFabricacion2.Items.Add(listaAños[i].ToString());
            }
        }
        
        protected void dgvVehiculos_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT * FROM ExportVehiculos ORDER BY " + e.SortExpression + " "
                                    + GetSortDirection(e.SortExpression);

            dgvVehiculos.DataSource = sentencia.DSET(selectOrdenar);
            dgvVehiculos.DataBind();
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
            Response.AddHeader("content-disposition", "attachment;filename = ExportVehiculos " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvVehiculos.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void imgBtnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Filtro de texto vacío.')", true);

                BindData();
            }
            else
            {
                string selectBuscar = "SELECT * FROM ExportVehiculos " +
                                      "WHERE " + ddlFiltroBuscar.SelectedValue.ToString()
                                      + " = '" + txtBuscar.Text +
                                      "' ORDER BY [Fecha de alta] ASC";

                dgvVehiculos.DataSource = sentencia.DSET(selectBuscar);
                dgvVehiculos.DataBind();

                if (ddlFiltroBuscar.SelectedValue.ToString() == "Patente")
                {
                    AccesoDatos datos = new AccesoDatos();

                    try
                    {
                        datos.SetearConsulta(selectBuscar);
                        datos.EjecutarLectura();
                        
                        if (datos.Lector.Read() == true)
                        {
                            long id = Convert.ToInt64(datos.Lector["ID"]);
                            string Patente = datos.Lector["Patente"].ToString();
                            string Marca = datos.Lector["IdMarca"].ToString();
                            string Modelo = datos.Lector["Modelo"].ToString();
                            string AnioFabricacion = datos.Lector["Año de fabricación"].ToString();
                            string Cliente = datos.Lector["Cliente"].ToString();
                            int Estado = Convert.ToInt32(datos.Lector["Estado"]);

                            Session.Add("IdVehiculo", id);
                            Session.Add("PatenteVehiculo", Patente);
                            Session.Add("MarcaVehiculo", Marca);
                            Session.Add("ModeloVehiculo", Modelo);
                            Session.Add("AnioVehiculo", AnioFabricacion);
                            Session.Add("ClienteVehiculo", Cliente);
                            Session.Add("EstadoVehiculo", Estado);

                            if (Estado == 1)
                            {
                                cbEstado.Checked = true;
                            }
                            ddlMarcaVehiculo2.SelectedValue = Marca;
                            ddlAnioFabricacion2.SelectedValue = AnioFabricacion;
                            txtPatente2.Text = Patente;
                            txtModelo2.Text = Modelo;

                            ddlMarcaVehiculo2.Enabled = true;
                            ddlAnioFabricacion2.Enabled = true;
                            txtPatente2.Enabled = true;
                            txtModelo2.Enabled = true;
                            cbEstado.Enabled = true;
                            btnModificar.Visible = true;
                            btnModificar.Enabled = true;
                            btnEliminar.Visible = true;
                            btnEliminar.Enabled = true;
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El vehículo patente " + txtBuscar.Text + " no existe en la base de datos.')", true);
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se produjo un error en la búsqueda.\n\n" +
                        "Por favor reintenta más tarde.')", true);

                        BindData();
                    }
                    finally
                    {
                        datos.CerrarConexion();
                    }
                }
                else
                {
                    ddlMarcaVehiculo2.SelectedValue = "0";
                    ddlAnioFabricacion2.SelectedValue = "0";
                    txtPatente2.Text = "";
                    txtModelo2.Text = "";

                    ddlMarcaVehiculo2.Enabled = false;
                    ddlAnioFabricacion2.Enabled = false;
                    txtPatente2.Enabled = false;
                    txtModelo2.Enabled = false;
                    cbEstado.Checked = false;
                    cbEstado.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
            }
        }

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtCuitDni.Text == "" || txtPatente.Text == "" || ddlMarcaVehiculo.SelectedValue == "0"
                || txtModelo.Text == "" || ddlAnioFabricacion.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos. Debe completar todos.')", true);
            }
            else
            {
                AccesoDatos datos = new AccesoDatos();
                AccesoDatos sentencia = new AccesoDatos();

                try
                {
                    string cuitDni = txtCuitDni.Text, patente = txtPatente.Text;
                    string idMarca = ddlMarcaVehiculo.SelectedValue.ToString();
                    string modelo = txtModelo.Text, año = ddlAnioFabricacion.SelectedValue.ToString();

                    string selectIdCliente = "SELECT C.ID, isnull(RazonSocial, ApeNom) as Cliente FROM " +
                                             "Clientes C WHERE C.CUIT_DNI = '" + cuitDni + "'";

                    datos.SetearConsulta(selectIdCliente);
                    datos.EjecutarLectura();

                    long idCliente = 0;
                    string NombreCliente = "NULL";

                    if (datos.Lector.Read() == true)
                    {
                        idCliente = Convert.ToInt64(datos.Lector["ID"]);
                        NombreCliente = datos.Lector["Cliente"].ToString();

                        string spAgregarVehiculo = "EXEC SP_AGREGAR_VEHICULO '" + patente + "', " + 
                            idMarca + ", '" + modelo + "', " + año + ", " + idCliente;

                        try
                        {
                            sentencia.IUD(spAgregarVehiculo);
                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('La patente " + patente + ", ya existe en la base de datos.')", true);
                        }

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El vehículo patente " + patente + ", para el cliente " + NombreCliente + 
                        ", se ha agregado correctamente.')", true);

                        string script = @"<script type='text/javascript'>

                                        location.href='ABMVehiculos.aspx';

                                   </script>";

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El CUIT/DNI " + cuitDni + " no existe en la base de datos.')", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se produjo un error al intentar agregar el vehículo.\n\n" +
                    "Por favor reintente en unos minutos.')", true);
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
        }

        protected void btnModificar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtPatente2.Text == "" || ddlMarcaVehiculo2.SelectedValue == "0"
                || txtModelo2.Text == "" || ddlAnioFabricacion2.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos. Por favor revise nuevamente.')", true);
            }
            else
            {
                string id = Session["IdVehiculo"].ToString();
                string patente = txtPatente2.Text, idMarca = ddlMarcaVehiculo2.SelectedValue.ToString();
                string modelo = txtModelo2.Text, año = ddlAnioFabricacion2.SelectedValue.ToString();
                bool estado = cbEstado.Checked;

                AccesoDatos sentencia = new AccesoDatos();

                try
                {
                    string updateVehiculo = "EXEC UPDATE_VEHICULO " + id + ", '" + patente + "', " +
                                            idMarca + ", '" + modelo + "', " + año + ", " + estado;

                    sentencia.IUD(updateVehiculo);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('El vehículo se ha modificado correctamente.')", true);

                    BindData();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se produjo un error al intentar modificar el vehículo.')", true);
                }
            }
        }

        protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            string patente = Session["PatenteVehiculo"].ToString();
            string marca = Session["MarcaVehiculo"].ToString();
            string modelo = Session["ModeloVehiculo"].ToString();
            string anioFabricacion = Session["AnioVehiculo"].ToString();
            string cliente = Session["ClienteVehiculo"].ToString();

            AccesoDatos datos2 = new AccesoDatos();

            if (txtPatente2.Text != patente || txtModelo2.Text != modelo || ddlMarcaVehiculo2.SelectedValue != marca
                || ddlAnioFabricacion2.SelectedValue != anioFabricacion)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Los campos no coinciden con la base de datos. Por favor revise nuevamente.')", true);
            }
            else
            {
                string id = Session["IdVehiculo"].ToString();

                string sp_DeleteVehiculo = "DELETE FROM Vehiculos WHERE ID = " + id;

                string selectCountTurnos = "SELECT isnull(COUNT(*), 0) as Cantidad FROM Turnos T WHERE " +
                                           id + " = T.IdVehiculo";

                int CantTurnosVehiculo = 0;

                try
                {
                    datos2.SetearConsulta(selectCountTurnos);
                    datos2.EjecutarLectura();

                    if (datos2.Lector.Read() == true)
                    {
                        CantTurnosVehiculo = Convert.ToInt32(datos2.Lector["Cantidad"]);
                    }

                    datos2.CerrarConexion();

                    if (CantTurnosVehiculo != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El vehículo patente " + patente + ", tiene turnos asociados." +
                        " Primero debe eliminar dichos turnos y luego reintentar.')", true);
                    }
                    else
                    {
                        sentencia.IUD(sp_DeleteVehiculo);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El vehiculo patente " + patente + ", del cliente " +
                        cliente + ", ha sido eliminado correctamente.')", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Error al intentar leer la base de datos. Reintente en unos minutos.')", true);
                }
                finally
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Pruebaaaaaaa')", true);

                    string script = @"<script type='text/javascript'>

                                        location.href='ABMVehiculos.aspx';

                                   </script>";

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
            }
        }
    }
}