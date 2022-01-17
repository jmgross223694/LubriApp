using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Servicios;

namespace LubriApp
{
    public partial class ABMTurnos : System.Web.UI.Page
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
            string selectDdlTiposServicio = "SELECT * FROM TiposServicio ORDER BY Descripcion ASC";

            ddlTiposServicio.DataSource = sentencia.DSET(selectDdlTiposServicio);
            ddlTiposServicio.DataMember = "datos";
            ddlTiposServicio.DataTextField = "Descripcion";
            ddlTiposServicio.DataValueField = "ID";
            ddlTiposServicio.DataBind();

            string selectTurnos = "SELECT * FROM ExportTurnos";
            string selectCantidadTurnos = "SELECT isnull(COUNT(*), 0) AS Cantidad FROM ExportTurnos";
            int resultado = 0;

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta(selectCantidadTurnos);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    resultado = Convert.ToInt32(datos.Lector["Cantidad"]);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Error en la Base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }
            
            if (resultado != 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('En total, al día de hoy, hay " + resultado + " turno/s.')", true);

                dgvTurnos.DataSource = sentencia.DSET(selectTurnos);
                dgvTurnos.DataBind();

                btnExportExcel.Enabled = true;
                btnExportExcel.Visible = true;
                btnDelete.Visible = false;
                btnCompletarTurno.Visible = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Todavía no hay turnos cargados.')", true);

                btnExportExcel.Enabled = false;
                btnExportExcel.Visible = false;
                btnDelete.Visible = false;
                btnCompletarTurno.Visible = false;
            }

            dgvTurnos.Visible = true;
            ddlFiltroBuscar.SelectedValue = "0";
            txtBuscarFiltro.Text = "";

            btnUpdate.Visible = false;
            ddlTiposServicio.Visible = false;
            txtFecha.Visible = false;
            ddlHoraTurno.Visible = false;
            txtCuitDni.Visible = false;
            txtPatente.Visible = false;
            txtBorrarTurnosPorPatente.Visible = false;
            btnBorrarTurnosPorPatente.Text = "Borrar Turnos por Patente";

            string selectHistoricoTurnos = "SELECT * FROM ExportHistoricoTurnos ORDER BY Fecha DESC, Hora DESC";

            dgvHistoricoTurnos.DataSource = sentencia.DSET(selectHistoricoTurnos);
            dgvHistoricoTurnos.DataBind();

            string selectDdlEmpleados = "SELECT * FROM Empleados ORDER BY ApeNom ASC";

            ddlEmpleados.DataSource = sentencia.DSET(selectDdlEmpleados);
            ddlEmpleados.DataMember = "datos";
            ddlEmpleados.DataTextField = "ApeNom";
            ddlEmpleados.DataValueField = "ID";
            ddlEmpleados.DataBind();

            ddlEmpleados.Visible = false;
        }

        protected void btnExportHistoricoExcel_Click(object sender, EventArgs e)
        {
            dgvHistoricoTurnos.Visible = true;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = Export_Historico_Turnos " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvHistoricoTurnos.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();

            dgvHistoricoTurnos.Visible = false;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportTurnos " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvTurnos.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvTurnos_Sorting(object sender, GridViewSortEventArgs e)
        {
            ddlFiltroBuscar.SelectedValue = "0";
            txtBuscarFiltro.Text = "";
            ddlMostrar.SelectedValue = "0";

            string selectOrdenar = "SELECT * FROM ExportTurnos ORDER BY " + e.SortExpression + " "
                                    + GetSortDirection(e.SortExpression);

            dgvTurnos.DataSource = sentencia.DSET(selectOrdenar);
            dgvTurnos.DataBind();
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

        protected void ddlMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = ddlMostrar.SelectedValue.ToString();
            int resultado = ContarResultadosDB(seleccion, "null", "null");

            if (ddlMostrar.SelectedValue != "0")
            {
                txtFecha.Visible = false;
                txtCuitDni.Visible = false;
                txtPatente.Visible = false;
                ddlHoraTurno.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnCompletarTurno.Visible = false;

                string consulta_1 = "SELECT * FROM ExportTurnos WHERE TRANSLATE(Fecha,'-','/')";
                string consulta_2 = "GETDATE()";

                if (seleccion == "Hoy")
                {
                    if (resultado != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Para el día de hoy, hay " + resultado + " turno/s.')", true);

                        string selectTurnosHoy = consulta_1 + " = " + consulta_2;

                        dgvTurnos.DataSource = sentencia.DSET(selectTurnosHoy);
                        dgvTurnos.DataBind();

                        btnExportExcel.Enabled = true;
                        btnExportExcel.Visible = true;
                        dgvTurnos.Visible = true;
                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Todavía no hay turnos para el día de hoy.')", true);

                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                }
                else if (seleccion == "Completados")
                {
                    if (resultado != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Al día de hoy, hay " + resultado + " turno/s completado/s.')", true);

                        string selectTurnosHoy = "SELECT * FROM ExportTurnos WHERE Estado = 'Completado'";

                        dgvTurnos.DataSource = sentencia.DSET(selectTurnosHoy);
                        dgvTurnos.DataBind();

                        btnExportExcel.Enabled = true;
                        btnExportExcel.Visible = true;
                        dgvTurnos.Visible = true;
                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Todavía no hay turnos completados.')", true);

                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                }
                else if (seleccion == "Futuros")
                {
                    if (resultado != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Al día de hoy, hay " + resultado + " turno/s futuro/s.')", true);

                        string selectTurnosHoy = consulta_1 + " > " + consulta_2;

                        dgvTurnos.DataSource = sentencia.DSET(selectTurnosHoy);
                        dgvTurnos.DataBind();

                        btnExportExcel.Enabled = true;
                        btnExportExcel.Visible = true;
                        dgvTurnos.Visible = true;
                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Todavía no hay turnos futuros.')", true);

                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                }
                else //Pendientes
                {
                    if (resultado != 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Al día de hoy, hay " + resultado + " turno/s pendiente/s.')", true);

                        string selectTurnosPendientes = "SELECT * FROM ExportTurnos WHERE Estado = 'Pendiente'";

                        dgvTurnos.DataSource = sentencia.DSET(selectTurnosPendientes);
                        dgvTurnos.DataBind();

                        btnExportExcel.Enabled = true;
                        btnExportExcel.Visible = true;
                        dgvTurnos.Visible = true;
                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('No hay turnos pendientes por el momento.')", true);

                        ddlFiltroBuscar.SelectedValue = "0";
                        txtBuscarFiltro.Text = "";
                    }
                }
            }
            else
            {
                BindData();
            }
        }

        protected int ContarResultadosDB(string cadena, string campo, string variable)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB;

            if (cadena != "null")
            {
                string consulta_1 = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos WHERE CONVERT(date,Fecha,105)";
                string consulta_2 = "CONVERT(date,GETDATE(),105)";

                if (cadena == "Hoy")
                {
                    selectDB = consulta_1 + " = " + consulta_2;
                }
                else if (cadena == "Completados")
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos WHERE Estado = 'Completado'";
                }
                else if (cadena == "Futuros")
                {
                    selectDB = consulta_1 + " > " + consulta_2;
                }
                else if (cadena == "Pendientes")
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos WHERE Estado = 'Pendiente'";
                }
                else
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos";
                }
            }
            else
            {
                selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos WHERE " + campo + " LIKE '%" + variable + "%'";
            }

            try
            {
                datos.SetearConsulta(selectDB);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Resultado = Convert.ToInt32(datos.Lector["Cantidad"]);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se produjo un error al intentar leer la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected int ContarResultadosDB2(string campo, string variable)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB;

            
            selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportClientes WHERE " + campo + " = '" + variable + "'";

            try
            {
                datos.SetearConsulta(selectDB);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Resultado = Convert.ToInt32(datos.Lector["Cantidad"]);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se produjo un error al intentar leer la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected int ContarResultadosDB3(string tabla, string campo1, string variable1, string campo2, string variable2)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB;


            selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM " + tabla + " WHERE " + 
                        campo1 + " = '" + variable1 + "' AND " + campo2 + " = " + variable2;

            try
            {
                datos.SetearConsulta(selectDB);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Resultado = Convert.ToInt32(datos.Lector["Cantidad"]);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se produjo un error al intentar leer la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected int ContarResultadosDB4(string tabla, string campo1, string variable1, string campo2, string variable2, string campo3, string variable3)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB;


            selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM " + tabla + " WHERE " +
                       campo1 + " = '" + variable1 + "' AND " + campo2 + " = '" + variable2 + "' AND " + 
                       campo3 + " <> '" + variable3 + "'";

            try
            {
                datos.SetearConsulta(selectDB);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    Resultado = Convert.ToInt32(datos.Lector["Cantidad"]);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se produjo un error al intentar leer la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected void imgBtnBuscarFiltro_Click(object sender, ImageClickEventArgs e)
        {
            if (txtBuscarFiltro.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Filtro de texto vacío.')", true);
            }
            else
            {
                if (ddlFiltroBuscar.SelectedValue == "0")
                {
                    ddlFiltroBuscar.SelectedValue = "ID";
                }

                int resultado = ContarResultadosDB("null", ddlFiltroBuscar.SelectedItem.ToString(), txtBuscarFiltro.Text);

                if (resultado != 0 && ddlFiltroBuscar.SelectedValue != "ID")
                {
                    txtFecha.Visible = false;
                    txtCuitDni.Visible = false;
                    txtPatente.Visible = false;
                    ddlHoraTurno.Visible = false;
                    btnUpdate.Visible = false;

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('" + resultado + " turno/s coincide/n con la búsqueda.')", true);

                    string filtroBusqueda = ddlFiltroBuscar.SelectedValue.ToString();
                    string filtroTexto = txtBuscarFiltro.Text;

                    string selectFiltro = "SELECT * FROM ExportTurnos WHERE " +
                                          filtroBusqueda + " LIKE '%" + filtroTexto + "%'";

                    dgvTurnos.DataSource = sentencia.DSET(selectFiltro);
                    dgvTurnos.DataBind();

                    btnExportExcel.Enabled = true;
                    btnExportExcel.Visible = true;
                    dgvTurnos.Visible = true;
                    ddlFiltroBuscar.SelectedValue = "0";
                    txtBuscarFiltro.Text = "";
                }
                else if (resultado != 0 && ddlFiltroBuscar.SelectedValue.ToString() == "ID") //EDITAR - ELIMINAR
                {
                    string txtFiltro = txtBuscarFiltro.Text;

                    //ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    //"alert('Se muestra a continuación el turno con ID = " + txtFiltro + "')", true);

                    string selectFiltro = "SELECT * FROM ExportTurnos WHERE " +
                                          "ID = " + txtFiltro;

                    dgvTurnos.DataSource = sentencia.DSET(selectFiltro);
                    dgvTurnos.DataBind();

                    btnExportExcel.Enabled = true;
                    btnExportExcel.Visible = true;
                    dgvTurnos.Visible = true;
                    ddlFiltroBuscar.SelectedValue = "0";
                    txtBuscarFiltro.Text = "";
                    btnDelete.Visible = true;
                    btnCompletarTurno.Visible = true;

                    //CARGAR DDL HORARIOS DENTRO DE POPUP
                    string selectddl = "select * from HorariosLunesViernes";

                    ddlHoraTurno.DataSource = sentencia.DSET(selectddl);
                    ddlHoraTurno.DataMember = "datos";
                    ddlHoraTurno.DataTextField = "LunesViernes";
                    ddlHoraTurno.DataValueField = "ID";
                    ddlHoraTurno.DataBind();

                    //CARGAR FECHA, CUIT/DNI y PATENTE DE TURNO SELECCIONADO EN CAMPOS CORRESPONDIENTES
                    AccesoDatos datos = new AccesoDatos();

                    try
                    {
                        string selectFiltro2 = "SELECT * FROM ExportTurnos WHERE " +
                                               "ID = " + txtFiltro;

                        datos.SetearConsulta(selectFiltro2);
                        datos.EjecutarLectura();

                        if (datos.Lector.Read())
                        {
                            string IDTurno = datos.Lector["ID"].ToString();
                            string DiaSemana = (string)datos.Lector["Dia"];
                            string Cliente = (string)datos.Lector["Cliente"];

                            txtFecha.Text = (string)datos.Lector["Fecha"];
                            txtCuitDni.Text = (string)datos.Lector["CUIT_DNI"];
                            txtPatente.Text = (string)datos.Lector["Patente"];
                            ddlHoraTurno.SelectedValue = datos.Lector["IDHorario"].ToString();
                            ddlTiposServicio.SelectedValue = datos.Lector["IdTipoServicio"].ToString();

                            ddlTiposServicio.Visible = true;
                            txtFecha.Visible = true;
                            txtCuitDni.Visible = true;
                            txtPatente.Visible = true;
                            ddlHoraTurno.Visible = true;
                            btnUpdate.Visible = true;

                            AccesoDatos datos2 = new AccesoDatos();

                            string IdCliente = "NULL";
                            string IdVehiculo = "NULL";
                            string Estado = "NULL";

                            try
                            {
                                datos2.SetearConsulta("SELECT * FROM Turnos WHERE " +
                                                  "ID = " + txtFiltro);
                                datos2.EjecutarLectura();

                                if (datos2.Lector.Read())
                                {
                                    IdCliente = datos2.Lector["IdCliente"].ToString();
                                    IdVehiculo = datos2.Lector["IdVehiculo"].ToString();
                                    Estado = datos2.Lector["Estado"].ToString();
                                }
                                if (Estado == "Completado") { btnCompletarTurno.Visible = false; }
                            }
                            catch
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Error en la base de datos.')", true);
                            }
                            finally
                            {
                                datos2.CerrarConexion();
                            }

                            Session.Add("IdTurno", IDTurno);
                            Session.Add("DiaSemana", DiaSemana);
                            Session.Add("Fecha", txtFecha.Text);
                            Session.Add("Hora", ddlHoraTurno.SelectedItem.ToString());
                            Session.Add("IdHorario", ddlHoraTurno.SelectedItem.ToString());
                            Session.Add("Cliente", Cliente);
                            Session.Add("CuitDni", txtCuitDni.Text);
                            Session.Add("Patente", txtPatente.Text);
                            Session.Add("IdTipoServicio", ddlTiposServicio.SelectedValue.ToString());
                            Session.Add("NombreTipoServicio", ddlTiposServicio.SelectedItem.ToString());
                            Session.Add("IdCliente", IdCliente);
                            Session.Add("IdVehiculo", IdVehiculo);
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha producido un error en la base de datos.')", true);
                    }
                    finally
                    {
                        datos.CerrarConexion();
                    }
                    
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Su búsqueda no produjo ningún resultado.')", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            AccesoDatos sentecia = new AccesoDatos();

            string IdTurno = Session["IdTurno"].ToString();
            string UsuarioLogueado = Session["usernameLogueado"].ToString();
            string Fecha = DateTime.Now.ToShortDateString(), Hora = DateTime.Now.ToShortTimeString();

            try
            {
                sentencia.IUD("DELETE FROM Turnos WHERE ID = " + IdTurno);

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Turno cancelado con éxito.')", true);

                string mailDestino = "pruebalubriapp@gmail.com";
                string asunto1 = "TURNO CANCELADO";
                string cuerpo1 = "El usuario '" + UsuarioLogueado + "', ha cancelado el turno con ID = " + IdTurno + " el día " + Fecha + ", a las " + Hora + "hs.";

                string asunto2 = "LUBRICENTRO TONY HA CANCELADO SU TURNO";
                string cuerpo2 = "Su turno cuyo código de reserva es " + IdTurno + ", ha sido cancelado por el lubricentro, el día " + DateTime.Now.ToShortDateString() + 
                                 ", a las " + DateTime.Now.ToShortTimeString() + "hs.";

                mailInterno(mailDestino, asunto1, cuerpo1);

                mailClientes(mailDestino, asunto2, cuerpo2);

                string script = @"<script type='text/javascript'>

                                        location.href='ABMTurnos.aspx';

                                   </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('No se pudo cancelar el turno.\\n\\n" +
                "Por favor reintente en unos minutos...')", true);

                BindData();
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            //Obtención de fecha ingresada y día de la semana que corresponde.
            DateTime fechaIngresada = Convert.ToDateTime(txtFecha.Text);
            string diaSemana = fechaIngresada.DayOfWeek.ToString();
            if (diaSemana == "Monday") { diaSemana = "Lunes"; }
            else if (diaSemana == "Tuesday") { diaSemana = "Martes"; }
            else if (diaSemana == "Wednesday") { diaSemana = "Miércoles"; }
            else if (diaSemana == "Thursday") { diaSemana = "Jueves"; }
            else if (diaSemana == "Friday") { diaSemana = "Viernes"; }
            else if (diaSemana == "Saturday") { diaSemana = "Sábado"; }
            else if (diaSemana == "Sunday")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('El día seleccionado es Domingo.\\n\\n" +
                "Por favor selecciona otro.')", true);
            }
            if (diaSemana != "Sunday")
            {
                //Datos originales
                string IDTurno = Session["IdTurno"].ToString();
                string DiaSemana1 = Session["DiaSemana"].ToString();
                string Fecha1 = Session["Fecha"].ToString();
                string Hora1 = Session["Hora"].ToString();
                string Cliente1 = Session["Cliente"].ToString();
                string CuitDni1 = Session["CuitDni"].ToString();
                string Patente1 = Session["Patente"].ToString();
                string IdHorario1 = Session["IdHorario"].ToString();
                string IdCliente1 = Session["IdCliente"].ToString();
                string IdVehiculo1 = Session["IdVehiculo"].ToString();
                string IdTipoServicio1 = Session["IdTipoServicio"].ToString();

                int resultadoCliente = ContarResultadosDB2("CUITDNI", txtCuitDni.Text);

                if (resultadoCliente != 0)
                {
                    //Datos modificados / seleccionados
                    string DiaSemana2 = diaSemana;
                    string Fecha2 = txtFecha.Text;
                    string Hora2 = ddlHoraTurno.SelectedItem.ToString();
                    string CuitDni2 = txtCuitDni.Text;
                    string Patente2 = txtPatente.Text;
                    string IdHorario2 = ddlHoraTurno.SelectedValue.ToString();
                    string IdTipoServicio2 = ddlTiposServicio.SelectedValue.ToString();

                    AccesoDatos datos2 = new AccesoDatos();
                    AccesoDatos datos3 = new AccesoDatos();

                    string selectCliente = "SELECT * FROM ExportClientes WHERE CUITDNI = '" +
                                           txtCuitDni.Text + "'";

                    string Cliente2 = "NULL";
                    string IdCliente2 = "NULL";
                    string IdVehiculo2 = "NULL";

                    try
                    {
                        datos2.SetearConsulta(selectCliente);
                        datos2.EjecutarLectura();

                        if (datos2.Lector.Read())
                        {
                            IdCliente2 = datos2.Lector["ID"].ToString();
                            if (txtCuitDni.Text.Length > 8)
                            {
                                Cliente2 = (string)datos2.Lector["RazonSocial"];
                            }
                            else
                            {
                                Cliente2 = (string)datos2.Lector["ApeNom"];
                            }
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Error al leer la DB. Línea 655 ABMTurnos.')", true);
                    }
                    finally
                    {
                        datos2.CerrarConexion();
                    }
                    
                    int resultadoVehiculo = ContarResultadosDB3("Vehiculos", "Patente", Patente2, "IdCliente", IdCliente2);

                    if (resultadoVehiculo != 0)
                    {
                        string selectIdVehiculo = "SELECT ID as ID FROM Vehiculos WHERE " +
                                                  "Patente = '" + Patente2 + "' AND " +
                                                  "IdCliente = " + IdCliente2;

                        try
                        {
                            datos3.SetearConsulta(selectIdVehiculo);
                            datos3.EjecutarLectura();

                            if (datos3.Lector.Read())
                            {
                                IdVehiculo2 = datos3.Lector["ID"].ToString();
                            }
                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Error al leer la base de datos.')", true);
                        }
                        finally
                        {
                            datos3.CerrarConexion();
                        }

                        //EJECUTAR UPDATE EN DB
                        if (DiaSemana2 != DiaSemana1) { DiaSemana1 = DiaSemana2; }
                        if (Fecha2 != Fecha1) { Fecha1 = Fecha2; }
                        if (Hora2 != Hora1) { Hora1 = Hora2; }
                        if (Cliente2 != Cliente1) { IdCliente1 = IdCliente2; }
                        if (Patente2 != Patente1) { IdVehiculo1 = IdVehiculo2; }
                        if (IdHorario2 != IdHorario1) { IdHorario1 = IdHorario2; }
                        if (IdTipoServicio1 != IdTipoServicio2) { IdTipoServicio1 = IdTipoServicio2; }

                        string updateTurno = "UPDATE Turnos SET IdCliente = " + IdCliente1 +
                                             ", IdVehiculo = " + IdVehiculo1 + ", " +
                                             "Dia = '" + DiaSemana1 + "', " +
                                             "FechaHora = '" + Fecha1 + " " + Hora1 + "', " +
                                             "IdHorario = " + IdHorario1 + ", " +
                                             "IdTipoServicio = " + IdTipoServicio1 +
                                             " WHERE ID = " + IDTurno;

                        int resultadoTurnoDuplicado = ContarResultadosDB4("ExportTurnos", "Fecha", Fecha1, "Hora", Hora1, "Cliente", Cliente1);
                        if (resultadoTurnoDuplicado != 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Ya hay un turno para otro cliente el día " + DiaSemana1 + 
                            " " + Fecha1 + " a las " + Hora1 + "hs.')", true);
                        }
                        else
                        {
                            try
                            {
                                sentencia.IUD(updateTurno);

                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Turno modificado con éxito.')", true);

                                string IdTurno = Session["IdTurno"].ToString();
                                string UsuarioLogueado = Session["usernameLogueado"].ToString();
                                string Fecha = DateTime.Now.ToShortDateString(), Hora = DateTime.Now.ToShortTimeString();
                                string mailDestino1 = "pruebalubriapp@gmail.com";
                                string asunto1 = "TURNO MODIFICADO";
                                string cuerpo1 = "El usuario '" + UsuarioLogueado + "', ha modificado el turno con ID = " + 
                                                IdTurno + " el día " + Fecha + ", a las " + Hora + "hs.";

                                AccesoDatos datosMailDestino = new AccesoDatos();

                                string mailDestino2 = "", NombreCliente = "";

                                try
                                {
                                    string selectMailDestino = "SELECT ISNULL(C.ApeNom, C.RazonSocial) AS CLIENTE, C.Mail AS MAIL FROM Clientes C WHERE C.ID = " + IdCliente1;

                                    datosMailDestino.SetearConsulta(selectMailDestino);
                                    datosMailDestino.EjecutarLectura();

                                    if (datosMailDestino.Lector.Read() == true)
                                    {
                                        mailDestino2 = datosMailDestino.Lector["MAIL"].ToString();
                                        NombreCliente = datosMailDestino.Lector["CLIENTE"].ToString();
                                    }
                                }
                                catch
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Error al buscar el mail del cliente " + NombreCliente + ", en la base de datos.')", true);
                                }

                                string asunto2 = "LUBRICENTRO TONY - MODIFICACIÓN DE TURNO";
                                string cuerpo2 = "Su turno ha sido modificado por nosotros. Abajo le detallamos los cambios realizados.\n\n" +
                                "-Datos originales.\n" + 
                                "*Fecha: " + Session["Fecha"].ToString() + " (" + Session["DiaSemana"].ToString() + ")\n" +
                                "*Horario: " + Session["Hora"].ToString() + "hs\n" +
                                "*Patente: " + Session["Patente"].ToString() + "\n" +
                                "*Tipo de servicio: " + Session["NombreTipoServicio"].ToString() + "\n\n" +
                                "-Datos nuevos.\n";

                                if (Session["Fecha"].ToString() != txtFecha.Text)
                                {
                                    string FechaNueva = txtFecha.Text;

                                    cuerpo2 = cuerpo2 + "*Fecha: " + FechaNueva + " (" + diaSemana + ")\n";
                                }
                                else { cuerpo2 = cuerpo2 + "*Fecha: sin modificar\n"; }
                                if (Session["Hora"].ToString() != ddlHoraTurno.SelectedItem.ToString())
                                {
                                    string HoraNueva = ddlHoraTurno.SelectedItem.ToString();

                                    cuerpo2 = cuerpo2 + "*Hora: " + HoraNueva + "hs\n";
                                }
                                else { cuerpo2 = cuerpo2 + "*Hora: sin modificar\n"; }
                                if (Session["NombreTipoServicio"].ToString() != ddlTiposServicio.SelectedItem.ToString())
                                {
                                    string TipoServicioNuevo = ddlTiposServicio.SelectedItem.ToString();

                                    cuerpo2 = cuerpo2 + "*Tipo de servicio: " + TipoServicioNuevo + "\n";
                                }
                                else { cuerpo2 = cuerpo2 + "*Tipo de servicio: sin modificar\n"; }
                                if (Session["Patente"].ToString() != txtPatente.Text)
                                {
                                    string PatenteNueva = txtPatente.Text;

                                    cuerpo2 = cuerpo2 + "*Patente: " + PatenteNueva + "\n";
                                }
                                else { cuerpo2 = cuerpo2 + "*Patente: sin modificar\n"; }

                                cuerpo2 = cuerpo2 + "\nSaludos cordiales\n\nLubricentro Tony.";

                                mailInterno(mailDestino1, asunto1, cuerpo1);

                                DateTime FechaModificada = Convert.ToDateTime(Fecha1);

                                if (FechaModificada >= DateTime.Now)
                                {
                                    mailClientes(mailDestino2, asunto2, cuerpo2);
                                }

                                string script = @"<script type='text/javascript'>

                                            location.href='ABMTurnos.aspx';

                                       </script>";

                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                            }
                            catch
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Se produjo un error al intentar modificar el Turno.\\n\\n" +
                                "Por favor reintente en unos minutos...')", true);
                            }
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('La patente ingresada no existe en la base de datos.')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('El CUIT / DNI ingresado no existe en la base de datos.')", true);
                }
            }
            else 
            {
                if (diaSemana != "Sunday")
                {
                    BindData();
                }
            }
        }

        protected void btnCompletarTurno_Click(object sender, EventArgs e)
        {
            if (btnCompletarTurno.Text == "Completar Turno")
            {
                try
                {
                    if (ddlEmpleados.SelectedValue.ToString() == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Empleado no seleccionado.')", true);
                    }
                    else
                    {
                        string IdTurno = Session["IdTurno"].ToString();
                        DateTime FechaHora = Convert.ToDateTime(Session["Fecha"].ToString() + " " + Session["Hora"].ToString());

                        if (FechaHora < DateTime.Now)
                        {
                            string completarTurno = "UPDATE Turnos SET Estado = 'Completado' " +
                                                "WHERE ID = " + IdTurno;

                            string Patente = Session["Patente"].ToString();
                            string IdTipoServicio = Session["IdTipoServicio"].ToString();
                            string IdCliente = Session["IdCliente"].ToString();
                            string IdEmpleado = ddlEmpleados.SelectedValue.ToString();
                            string Comentarios = "ID Turno = " + IdTurno;
                            string Estado = "Completado";

                            string insertServicio = "INSERT INTO Servicios(PatenteVehiculo, IdTipo, Comentarios, " +
                                                    "IdCliente, IdEmpleado, Estado) " +
                                                    "values('" + Patente + "', " + IdTipoServicio + ", '" +
                                                    Comentarios + "', " + IdCliente + ", " + IdEmpleado + ", '" +
                                                    Estado + "')";

                            AccesoDatos sentencia = new AccesoDatos();
                            try
                            {
                                sentencia.IUD(insertServicio);

                                sentencia.IUD(completarTurno);

                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Turno completado y servicio añadido con éxito.')", true);

                                string script = @"<script type='text/javascript'>

                                            location.href='ABMTurnos.aspx';

                                       </script>";

                                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                            }
                            catch
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('Se produjo un error y no se completó el turno.')", true);
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El turno no podrá ser completado, antes de la fecha de reserva.')", true);
                        }
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('No se pudo completar el turno.\\n\\n" +
                    "Por favor reintente en unos minutos...')", true);

                    BindData();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Debe seleccionar al empleado. Luego hacer click en el botón completar turno.')", true);
                
                ddlEmpleados.Visible = true;

                btnCompletarTurno.Text = "Completar Turno";
            }
        }

        protected void btnBorrarTurnosPorPatente_Click(object sender, EventArgs e)
        {
            if (btnBorrarTurnosPorPatente.Text == "Borrar Turnos por Patente")
            {
                txtBorrarTurnosPorPatente.Visible = true;
                btnBorrarTurnosPorPatente.Text = "Eliminar";
            }
            else if (txtBorrarTurnosPorPatente.Text != "")
            {
                AccesoDatos datos = new AccesoDatos();
                AccesoDatos datos2 = new AccesoDatos();
                AccesoDatos sentencia = new AccesoDatos();

                string patente = txtBorrarTurnosPorPatente.Text;

                string deleteTurnosVehiculo = "DELETE FROM Turnos WHERE " +
                "(SELECT ID FROM Vehiculos V WHERE V.Patente = '" + patente + "') = IdVehiculo";

                string selectCountVehiculos = "SELECT isnull(COUNT(*), 0) as Cantidad FROM Vehiculos V " +
                "WHERE V.Patente = '" + patente + "'";

                string selectCountTurnos = "SELECT isnull(COUNT(*), 0) as Cantidad FROM Turnos T " +
                "WHERE (SELECT ID FROM Vehiculos V WHERE V.Patente = '" + patente + "') = T.IdVehiculo";

                int existeVehiculo = 0, existenTurnos = 0;

                try
                {
                    datos.SetearConsulta(selectCountVehiculos);
                    datos.EjecutarLectura();

                    datos2.SetearConsulta(selectCountTurnos);
                    datos2.EjecutarLectura();

                    if (datos.Lector.Read() == true && datos2.Lector.Read() == true)
                    {
                        existeVehiculo = Convert.ToInt32(datos.Lector["Cantidad"]);
                        existenTurnos = Convert.ToInt32(datos2.Lector["Cantidad"]);
                    }

                    if (existeVehiculo != 0 && existenTurnos !=0)
                    {
                        sentencia.IUD(deleteTurnosVehiculo);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Los turnos del vehículo patente " + patente + ", se han eliminado correctamente.')", true);

                        BindData();

                        string script = @"<script type='text/javascript'>

                                        location.href='ABMTurnos.aspx';

                                   </script>";

                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                    else if (existeVehiculo != 0 && existenTurnos == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Todavía no existen Turnos para la patente " + patente + ".')", true);
                    }
                    else if (existeVehiculo == 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El vehículo patente " + patente + " no existe en la base de datos.')", true);
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('No se puede conectar con la base de datos. Reintente en unos minutos.')", true);
                }
                finally
                {
                    datos.CerrarConexion();
                    datos2.CerrarConexion();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Texto vacío, debe ingresar una patente.')", true);
            }
        }

        public void mailInterno(string mailDestino, string asunto, string cuerpo)
        {
            try
            {
                EmailService mailNuevo = new EmailService();
                mailNuevo.armarCorreo(mailDestino, asunto, cuerpo);
                try
                {
                    mailNuevo.enviarEmail();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha producido un error al intentar enviar el mail.')", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error al intentar crear el objeto mail.')", true);
            }
        }

        public void mailClientes(string mailDestino, string asunto, string cuerpo)
        {
            try
            {
                EmailService mailNuevo = new EmailService();
                mailNuevo.armarCorreo(mailDestino, asunto, cuerpo);
                try
                {
                    mailNuevo.enviarEmail();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha producido un error al intentar enviar el mail.')", true);
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error al intentar crear el objeto mail.')", true);
            }
        }

        protected void dgvHistoricoTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvHistoricoTurnos.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}