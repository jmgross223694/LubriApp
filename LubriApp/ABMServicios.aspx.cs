using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Servicios;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMServicios : System.Web.UI.Page
    {
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
            AccesoDatos sentencia = new AccesoDatos();
            AccesoDatos sentencia2 = new AccesoDatos();
            AccesoDatos sentencia3 = new AccesoDatos();
            AccesoDatos sentencia4 = new AccesoDatos();
            AccesoDatos sentencia5 = new AccesoDatos();
            AccesoDatos sentencia6 = new AccesoDatos();
            AccesoDatos sentencia7 = new AccesoDatos();

            txtBorrarServiciosPorPatente.Visible = false;
            ddlFiltroBuscar.SelectedValue = "0";
            txtBuscarFiltro.Text = "";
            ddlMostrar.SelectedValue = "Todos";

            string selectServicios = "SELECT * FROM ExportServicios ORDER BY Fecha DESC, Hora DESC";
            string selectDdlTiposServicio = "SELECT ID as ID, Descripcion as Descripcion FROM TiposServicio WHERE Estado = 1";
            string selectDdlClientes = "SELECT ID as ID, isnull(ApeNom, RazonSocial) as Cliente FROM Clientes";
            string selectDdlEmpleados = "SELECT ID as ID, ApeNom as Empleado FROM Empleados";

            int resultado = ContarResultadosDB("Todos", "null", "null", "null");

            mostrarCantidadServicios(resultado, "Todos");

            ocultarMostrarCamposModificarEliminar("ocultar");

            borrarContenidoCamposModificarEliminar();

            borrarContenidoCamposPopupAgregar();

            try
            {
                dgvServicios.DataSource = sentencia.DSET(selectServicios);
                dgvServicios.DataBind();

                if (!IsPostBack)
                {
                    ddlTiposServicio.DataSource = sentencia2.DSET(selectDdlTiposServicio);
                    ddlTiposServicio.DataMember = "datos";
                    ddlTiposServicio.DataTextField = "Descripcion";
                    ddlTiposServicio.DataValueField = "ID";
                    ddlTiposServicio.DataBind();

                    ddlClientes.DataSource = sentencia3.DSET(selectDdlClientes);
                    ddlClientes.DataMember = "datos";
                    ddlClientes.DataTextField = "Cliente";
                    ddlClientes.DataValueField = "ID";
                    ddlClientes.DataBind();

                    ddlEmpleados.DataSource = sentencia4.DSET(selectDdlEmpleados);
                    ddlEmpleados.DataMember = "datos";
                    ddlEmpleados.DataTextField = "Empleado";
                    ddlEmpleados.DataValueField = "ID";
                    ddlEmpleados.DataBind();

                    ddlTiposServicio2.DataSource = sentencia5.DSET(selectDdlTiposServicio);
                    ddlTiposServicio2.DataMember = "datos";
                    ddlTiposServicio2.DataTextField = "Descripcion";
                    ddlTiposServicio2.DataValueField = "ID";
                    ddlTiposServicio2.DataBind();

                    ddlClientes2.DataSource = sentencia6.DSET(selectDdlClientes);
                    ddlClientes2.DataMember = "datos";
                    ddlClientes2.DataTextField = "Cliente";
                    ddlClientes2.DataValueField = "ID";
                    ddlClientes2.DataBind();

                    ddlEmpleados2.DataSource = sentencia7.DSET(selectDdlEmpleados);
                    ddlEmpleados2.DataMember = "datos";
                    ddlEmpleados2.DataTextField = "Empleado";
                    ddlEmpleados2.DataValueField = "ID";
                    ddlEmpleados2.DataBind();
                }
            }
            catch
            {
                mostrarScriptMensaje("Error en la base de datos.");
            }

            string selectHistoricoTurnos = "SELECT * FROM ExportHistoricoServicios ORDER BY FechaModificado DESC, HoraModificado DESC, ID ASC";

            dgvHistoricoServicios.DataSource = sentencia.DSET(selectHistoricoTurnos);
            dgvHistoricoServicios.DataBind();
        }

        public void borrarContenidoCamposModificarEliminar()
        {
            txtFecha.Text = "";
            txtHora.Text = "";
            txtPatente.Text = "";
            txtComentarios.Text = "";
            ddlTiposServicio.SelectedValue = "0";
            ddlClientes.SelectedValue = "0";
            ddlEmpleados.SelectedValue = "0";
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            ddlEstado.SelectedValue = "0";
        }

        public void ocultarMostrarCamposModificarEliminar(string accion)
        {
            if (accion == "ocultar")
            {
                txtFecha.Visible = false;
                txtHora.Visible = false;
                txtPatente.Visible = false;
                txtComentarios.Visible = false;
                ddlTiposServicio.Visible = false;
                ddlClientes.Visible = false;
                ddlEmpleados.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                ddlEstado.Visible = false;
            }
            else if (accion == "mostrar")
            {
                txtFecha.Visible = true;
                txtHora.Visible = true;
                txtPatente.Visible = true;
                txtComentarios.Visible = true;
                ddlTiposServicio.Visible = true;
                ddlClientes.Visible = true;
                ddlEmpleados.Visible = true;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                ddlEstado.Visible = true;
            }
        }

        public void ocultarMostrarCamposPopupAgregar(string accion)
        {
            if (accion == "ocultar")
            {
                txtFecha2.Visible = false;
                txtHora2.Visible = false;
                txtPatente2.Visible = false;
                txtComentarios2.Visible = false;
                ddlTiposServicio2.Visible = false;
                ddlClientes2.Visible = false;
                ddlEmpleados2.Visible = false;
                btnAgregar.Visible = false;
            }
            else if (accion == "mostrar")
            {
                txtFecha2.Visible = true;
                txtHora2.Visible = true;
                txtPatente2.Visible = true;
                txtComentarios2.Visible = true;
                ddlTiposServicio2.Visible = true;
                ddlClientes2.Visible = true;
                ddlEmpleados2.Visible = true;
                btnAgregar.Visible = true;
            }
        }

        public void borrarContenidoCamposPopupAgregar()
        {
            txtFecha2.Text = "";
            txtHora2.Text = "";
            txtPatente2.Text = "";
            txtComentarios2.Text = "";
            ddlTiposServicio2.SelectedValue = "0";
            ddlClientes2.SelectedValue = "0";
            ddlEmpleados2.SelectedValue = "0";
        }

        protected void imgBtnBuscarFiltro_Click(object sender, ImageClickEventArgs e)
        {
            string valor = txtBuscarFiltro.Text.ToUpper();
            string campo = ddlFiltroBuscar.SelectedValue.ToString();

            if (campo == "0" && valor == "")
            {
                mostrarScriptMensaje("Filtro de búsqueda no seleccionado y filtro de texto vacío.");

                BindData();
            }
            else if (campo == "0")
            {
                mostrarScriptMensaje("Filtro de búsqueda no seleccionado.");
            }
            else if (valor == "")
            {
                mostrarScriptMensaje("Filtro de texto vacío.");
            }
            else
            {
                string comillas = "y", tabla = "ExportServicios";
                if (campo == "ID") { comillas = "n"; }

                int resultado = ContarResultadosDB(tabla, campo, valor, comillas);

                if (resultado != 0)
                {
                    if (campo == "ID")
                    {
                        mostrarScriptMensaje("Se muestra a continuación, el Servicio con ID = " + valor);

                        string selectResultados = "SELECT * FROM " + tabla + " WHERE " + campo + " = " + valor;
                        
                        AccesoDatos sentencia = new AccesoDatos();
                        AccesoDatos datos = new AccesoDatos();

                        try
                        {
                            dgvServicios.DataSource = sentencia.DSET(selectResultados);
                            dgvServicios.DataBind();

                            datos.SetearConsulta(selectResultados);
                            datos.EjecutarLectura();

                            ocultarMostrarCamposModificarEliminar("mostrar");

                            if (datos.Lector.Read() == true)
                            {
                                string ID = datos.Lector["ID"].ToString();
                                txtFecha.Text = datos.Lector["Fecha"].ToString();
                                txtHora.Text = datos.Lector["Hora"].ToString();
                                txtPatente.Text = datos.Lector["Patente"].ToString();
                                txtComentarios.Text = datos.Lector["Comentarios"].ToString();
                                ddlEstado.SelectedValue = datos.Lector["Estado"].ToString();
                                ddlTiposServicio.SelectedValue = datos.Lector["IdTipo"].ToString();
                                ddlClientes.SelectedValue = datos.Lector["IdCliente"].ToString();
                                ddlEmpleados.SelectedValue = datos.Lector["IdEmpleado"].ToString();

                                Session.Add("IdServicio", ID);
                                Session.Add("Fecha", txtFecha.Text);
                                Session.Add("Hora", txtHora.Text);
                                Session.Add("Patente", txtPatente.Text);
                                Session.Add("Comentarios", txtComentarios.Text);
                                Session.Add("Estado", ddlEstado.SelectedValue);
                                Session.Add("IdTipo", ddlTiposServicio.SelectedValue);
                                Session.Add("IdCliente", ddlClientes.SelectedValue);
                                Session.Add("IdEmpleado", ddlEmpleados.SelectedValue);
                            }
                        }
                        catch
                        {
                            mostrarScriptMensaje("Error en la Base de datos.");
                        }
                        finally
                        {
                            datos.CerrarConexion();
                        }
                    }
                    else
                    {
                        Session.Remove("IdServicio");
                        Session.Remove("Fecha");
                        Session.Remove("Hora");
                        Session.Remove("Patente");
                        Session.Remove("Comentarios");
                        Session.Remove("Estado");
                        Session.Remove("IdTipo");
                        Session.Remove("IdCliente");
                        Session.Remove("IdEmpleado");

                        ocultarMostrarCamposModificarEliminar("ocultar");

                        string texto1 = "encontaron ", texto2 = " servicios";

                        if (resultado == 1) { texto1 = "encontró "; texto2 = " servicio"; }

                        mostrarScriptMensaje("Se " + texto1 + resultado + texto2);

                        string selectResultados = "SELECT * FROM " + tabla + " WHERE " + campo + " LIKE '%" + valor + "%'";

                        AccesoDatos sentencia = new AccesoDatos();

                        try
                        {
                            dgvServicios.DataSource = sentencia.DSET(selectResultados);
                            dgvServicios.DataBind();
                        }
                        catch
                        {
                            mostrarScriptMensaje("Error en la Base de datos.");
                        }
                    }
                }
                else
                {
                    mostrarScriptMensaje("Su búsqueda no produjo ningún resultado.");
                }
            }
        }

        protected void btnBorrarServiciosPorPatente_Click(object sender, EventArgs e)
        {
            if (txtBorrarServiciosPorPatente.Visible == false)
            {
                txtBorrarServiciosPorPatente.Visible = true;
            }
            else
            {
                if (txtBorrarServiciosPorPatente.Text == "")
                {
                    mostrarScriptMensaje("Debe ingresar una patente.");
                }
                else
                {
                    string patente = txtBorrarServiciosPorPatente.Text.ToUpper();
                    int resultadoServicios = ContarResultadosDB("Servicios", "PatenteVehiculo", patente, "y");
                    int resultadoDB = ContarResultadosDB("Vehiculos", "Patente", patente, "y");

                    if (resultadoDB == 0)
                    {
                        mostrarScriptMensaje("La Patente " + patente + ", no existe en el sistema.");
                    }
                    else if (resultadoServicios != 0)
                    {
                        AccesoDatos sentencia = new AccesoDatos();

                        string deleteServicios = "DELETE FROM Servicios WHERE PatenteVehiculo = '" + patente + "'";

                        try
                        {
                            sentencia.IUD(deleteServicios);

                            mostrarScriptMensaje("Los servicios correspondientes a la Patente " + patente + ", se eliminaron correctamente.");

                            BindData();
                        }
                        catch
                        {
                            mostrarScriptMensaje("Se produjo un error al intentar borrar los servicios " +
                                                 "correspondientes a la pantente " + patente + ".");
                        }
                    }
                    else
                    {
                        mostrarScriptMensaje("La Patente " + patente + ", no registra servicios aún.");
                    }
                }
            }
        }

        public void mostrarScriptMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "alert('" + mensaje + "')", true);
        }

        public void mostrarCantidadServicios(int cantidad, string referencia)
        {
            if (cantidad != 0)
            {
                if (cantidad > 1)
                {
                    if (referencia == "Todos")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontraron " + cantidad + " servicios, cargados en el sistema.')", true);
                    }
                    else if (referencia == "Hoy")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontraron " + cantidad + " servicios, cargados hoy al sistema.')", true);
                    }
                    else if (referencia == "Completados")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontraron " + cantidad + " servicios completados, cargados en el sistema.')", true);
                    }
                    else if (referencia == "Pendientes")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontraron " + cantidad + " servicios pendientes, cargados en el sistema.')", true);
                    }
                    else if (referencia == "Futuros")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontraron " + cantidad + " servicios futuros, cargados en el sistema.')", true);
                    }
                }
                else
                {
                    if (referencia == "Todos")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontró " + cantidad + " solo servicio, cargado en el sistema.')", true);
                    }
                    else if (referencia == "Hoy")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontró " + cantidad + " solo servicio, cargado hoy al sistema.')", true);
                    }
                    else if (referencia == "Completados")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontró " + cantidad + " solo servicio completado, cargado en el sistema.')", true);
                    }
                    else if (referencia == "Pendientes")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontró " + cantidad + " solo servicio pendiente, cargado en el sistema.')", true);
                    }
                    else if (referencia == "Futuros")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se encontró " + cantidad + " solo servicio futuro, cargado en el sistema.')", true);
                    }
                }
            }
            else
            {
                if (referencia == "Todos")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Todavía no hay servicios cargados en el sistema.')", true);
                }
                else if (referencia == "Hoy")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Por el momento, hoy no se cargaron servicios en el sistema.')", true);
                }
                else if (referencia == "Completados")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Por el momento, no hay servicios completados cargados en el sistema.')", true);
                }
                else if (referencia == "Pendientes")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Por el momento, no hay servicios pendientes cargados en el sistema.')", true);
                }
                else if (referencia == "Futuros")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Todavía no hay servicios futuros cargados en el sistema.')", true);
                }
            }
        }

        protected void ddlMostrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccesoDatos sentencia = new AccesoDatos();

            string seleccion = ddlMostrar.SelectedValue.ToString();
            int resultado = ContarResultadosDB("Todos", "null", "null", "null");

            string consulta_1 = "SELECT * FROM ExportServicios ";
            string consulta_2 = "WHERE TRANSLATE(Fecha,'-','/')";
            string consulta_3 = "GETDATE()";

            if (resultado != 0)
            {
                switch (seleccion)
                {
                    case ("Todos"):
                        {
                            resultado = ContarResultadosDB(seleccion, "null", "null", "null");
                            string selectTodos = "SELECT * FROM ExportServicios";

                            dgvServicios.DataSource = sentencia.DSET(selectTodos);
                            dgvServicios.DataBind();

                            mostrarCantidadServicios(resultado, seleccion);
                        }
                        break;
                    case ("Hoy"):
                        {
                            resultado = ContarResultadosDB(seleccion, "null", "null", "null");
                            string selectTodos = "SELECT * FROM ExportServicios WHERE CONVERT(date, Fecha, 105) = " +
                                                 "CONVERT(date, GETDATE(), 105) ORDER BY Hora DESC";

                            dgvServicios.DataSource = sentencia.DSET(selectTodos);
                            dgvServicios.DataBind();

                            mostrarCantidadServicios(resultado, seleccion);
                        }
                        break;
                    case ("Completados"):
                        {
                            resultado = ContarResultadosDB(seleccion, "null", "null", "null");
                            string selectTodos = consulta_1  + " WHERE Estado = 'Completado'";

                            dgvServicios.DataSource = sentencia.DSET(selectTodos);
                            dgvServicios.DataBind();

                            mostrarCantidadServicios(resultado, seleccion);
                        }
                        break;
                    case ("Futuros"):
                        {
                            resultado = ContarResultadosDB(seleccion, "null", "null", "null");
                            string selectTodos = consulta_1 + consulta_2 + " > " + consulta_3;

                            dgvServicios.DataSource = sentencia.DSET(selectTodos);
                            dgvServicios.DataBind();

                            mostrarCantidadServicios(resultado, seleccion);
                        }
                        break;
                    case ("Pendientes"):
                        {
                            resultado = ContarResultadosDB(seleccion, "null", "null", "null");
                            string selectTodos = consulta_1 + " WHERE Estado = 'Pendiente'";

                            dgvServicios.DataSource = sentencia.DSET(selectTodos);
                            dgvServicios.DataBind();

                            mostrarCantidadServicios(resultado, seleccion);
                        }
                        break;
                }
            }
            else
            {
                mostrarCantidadServicios(resultado, "Todos");

                BindData();
            }
        }

        protected void dgvServicios_Sorting(object sender, GridViewSortEventArgs e)
        {
            AccesoDatos sentencia = new AccesoDatos();

            ddlFiltroBuscar.SelectedValue = "0";
            txtBuscarFiltro.Text = "";
            ddlMostrar.SelectedValue = "Todos";

            string selectOrdenar = "SELECT * FROM ExportServicios ORDER BY " + e.SortExpression + " "
                                    + GetSortDirection(e.SortExpression);

            dgvServicios.DataSource = sentencia.DSET(selectOrdenar);
            dgvServicios.DataBind();
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

        protected int ContarResultadosDB(string var, string campo, string valor, string comillas)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB;

            if (var == "Todos")
            {
                selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportServicios";
            }
            else if (var == "Hoy" || var == "Completados" || var == "Futuros" || var == "Pendientes")
            {
                string consulta_1 = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportServicios WHERE CONVERT(date,Fecha,105)";
                string consulta_2 = "CONVERT(date,GETDATE(),105)";

                if (var == "Hoy")
                {
                    selectDB = consulta_1 + " = " + consulta_2;
                }
                else if (var == "Completados")
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportServicios WHERE Estado = 'Completado'";
                }
                else if (var == "Futuros")
                {
                    selectDB = consulta_1 + " > " + consulta_2;
                }
                else if (var == "Pendientes")
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportServicios WHERE Estado = 'Pendiente'";
                }
                else
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM ExportTurnos";
                }
            }
            else
            {
                if (comillas == "y")
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM " + var + " WHERE " + campo + " = '" + valor + "'";
                }
                else
                {
                    selectDB = "SELECT isnull(COUNT(*), 0) as Cantidad FROM " + var + " WHERE " + campo + " = " + valor;
                }
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

        protected int ContarResultadosDB_Insert(string campo1, string valor1, string campo2, string valor2, string campo3, string valor3, string campo4, string valor4)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB = "SELECT isnull(COUNT(*), 0) AS Cantidad FROM ExportServicios WHERE " + campo1 + " = TRANSLATE('" + valor1 + "','/','-') AND " + campo2 + " = '" + valor2 + "' AND " + 
                                                                              campo3 + " = '" + valor3 + "' AND " + campo4 + " = '" + valor4 + "'";

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

        protected int ContarResultadosDB_PatenteCliente(string IdCliente, string Patente)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB = "SELECT isnull(COUNT(*), 0) AS Cantidad FROM Vehiculos WHERE IdCliente = " + IdCliente + " AND Patente = '" + Patente + "'";

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

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            string ID = Session["IdServicio"].ToString();
            string Fecha = Session["Fecha"].ToString();
            string Hora =  Session["Hora"].ToString();
            string UsuarioLogueado = Session["usernameLogueado"].ToString();
            string horaActual = DateTime.Now.ToShortTimeString();
            string fechaActual = DateTime.Now.ToShortDateString();

            AccesoDatos sentencia = new AccesoDatos();

            string deleteServicio = "DELETE FROM Servicios WHERE ID = " + ID;

            try
            {
                sentencia.IUD(deleteServicio);

                mostrarScriptMensaje("Servicio eliminado correctamente.");

                try
                {
                    string mailDestino = "pruebalubriapp@gmail.com";
                    string asunto = "ORDEN DE SERVICIO ELIMINADA - " + horaActual + "_" + fechaActual;
                    string cuerpo = "El usuario '" + UsuarioLogueado + "', ha borrado el servicio con ID = " + ID + " pactado para el día " + Fecha + ", a las " + Hora + "hs.";
                    EmailService mailNuevo = new EmailService();
                    mailNuevo.armarCorreo(mailDestino, asunto, cuerpo);
                    try
                    {
                        mailNuevo.enviarEmail();
                    }
                    catch 
                    {
                        mostrarScriptMensaje("Se ha producido un error al intentar enviar el mail.");
                    }
                }
                catch
                {
                    mostrarScriptMensaje("Se ha producido un error al intentar crear el mail.");
                }

                BindData();
            }
            catch
            {
                mostrarScriptMensaje("Error en la base de datos.");
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFecha.Text == "" || txtHora.Text == "" || txtPatente.Text == "" || ddlEstado.SelectedValue == "0"
                || ddlTiposServicio.SelectedValue == "0" || ddlClientes.SelectedValue == "0" || ddlEmpleados.SelectedValue == "0")
            {
                mostrarScriptMensaje("Hay campos vacíos o sin seleccionar. Por favor revise nuevamente.");
            }
            else
            {
                DateTime FechaServicio = Convert.ToDateTime(txtFecha.Text + " " + txtHora.Text);

                if (FechaServicio.Date <= DateTime.Now.Date || ddlEstado.SelectedValue.ToString() != "Completado")
                {
                    string ID = Session["IdServicio"].ToString();
                    string FechaHora = txtFecha.Text + " " + txtHora.Text;
                    string Patente = txtPatente.Text;
                    string Comentarios = txtComentarios.Text;
                    string Estado = ddlEstado.SelectedValue.ToString();
                    string IdTipo = ddlTiposServicio.SelectedValue.ToString();
                    string IdCliente = ddlClientes.SelectedValue.ToString();
                    string IdEmpleado = ddlEmpleados.SelectedValue.ToString();
                    string Servicio = ddlTiposServicio.SelectedItem.ToString();
                    int DiaAviso = FechaServicio.Day, MesAviso = FechaServicio.Month, AñoAviso = FechaServicio.Year + 1;

                    int CantDiasMes = 0;
                    switch (MesAviso - 1)
                    {
                        case 1:
                            CantDiasMes = 31;
                            break;
                        case 2:
                            if (AñoAviso % 4 == 0 && AñoAviso % 100 == 0 && AñoAviso % 400 == 0)
                            {
                                CantDiasMes = 29;
                            }
                            else { CantDiasMes = 28; }
                            break;
                        case 3:
                            CantDiasMes = 31;
                            break;
                        case 4:
                            CantDiasMes = 30;
                            break;
                        case 5:
                            CantDiasMes = 31;
                            break;
                        case 6:
                            CantDiasMes = 30;
                            break;
                        case 7:
                            CantDiasMes = 31;
                            break;
                        case 8:
                            CantDiasMes = 31;
                            break;
                        case 9:
                            CantDiasMes = 30;
                            break;
                        case 10:
                            CantDiasMes = 31;
                            break;
                        case 11:
                            CantDiasMes = 30;
                            break;
                        case 12:
                            CantDiasMes = 31;
                            break;
                    }

                    if (DiaAviso - 7 <= 0) { DiaAviso = CantDiasMes + DiaAviso - 7; MesAviso--; }
                    else { DiaAviso = DiaAviso - 7; }

                    string FechaAvisoCorta = DiaAviso + "/" + MesAviso + "/" + AñoAviso;

                    AccesoDatos sentencia = new AccesoDatos();
                    AccesoDatos sentencia2 = new AccesoDatos();
                    AccesoDatos datos = new AccesoDatos();
                    AccesoDatos datos2 = new AccesoDatos();

                    string crearAvisoServicio = null;
                    if (Estado == "Completado" && Servicio == "Revisión de filtros"
                        || Servicio == "Revisión de aceite y filtros"
                        || Servicio == "Revisión de aceite")
                    {
                        crearAvisoServicio = "INSERT INTO AvisosServicios(IdCliente, IdServicio, IdTipoServicio, Patente, FechaRealizado, FechaAviso)" +
                        "values(" + IdCliente + ", " + ID + ", " + IdTipo + ", '" + Patente + "', '" + FechaHora + "', '" + FechaAvisoCorta + "')";
                    }

                    string updateServicio = "EXEC UPDATE_SERVICIO " + ID + ", '" + FechaHora + "', '" + Patente + "', '" +
                        Comentarios + "', '" + Estado + "', " + IdTipo + ", " + IdCliente + ", " + IdEmpleado;

                    string cantAvisosServicios = "SELECT isnull(COUNT(*), 0) AS Cantidad FROM AvisosServicios WHERE IdServicio = " + ID;
                    int resultado = 0;

                    try
                    {
                        datos2.SetearConsulta(cantAvisosServicios);
                        datos2.EjecutarLectura();
                        if (datos2.Lector.Read() == true)
                        {
                            resultado = Convert.ToInt32(datos2.Lector["Cantidad"]);
                        }
                    }
                    catch
                    {
                        mostrarScriptMensaje("Error al contar los avisos de servicios en la base de datos.");
                    }
                    finally
                    {
                        datos2.CerrarConexion();
                    }

                    int resultadoPatenteCliente = ContarResultadosDB_PatenteCliente(IdCliente, Patente);

                    string selectNombreCliente = "SELECT ISNULL(ApeNom, RazonSocial) AS Cliente FROM Clientes WHERE ID = " + IdCliente;
                    string Cliente = "vacío";

                    try
                    {
                        datos.SetearConsulta(selectNombreCliente);
                        datos.EjecutarLectura();

                        if (datos.Lector.Read() == true)
                        {
                            Cliente = datos.Lector["Cliente"].ToString();
                        }

                        if (resultadoPatenteCliente != 0)
                        {
                            try
                            {
                                sentencia.IUD(updateServicio);

                                if (resultado == 0 && Estado == "Completado"
                                    && Servicio == "Revisión de filtros"
                                    || Servicio == "Revisión de aceite y filtros"
                                    || Servicio == "Revisión de aceite")
                                {
                                    sentencia2.IUD(crearAvisoServicio);
                                }

                                if (Estado == "Completado")
                                {
                                    mostrarScriptMensaje("El servicio se ha marcado como 'Completado'.");
                                }
                                else
                                {
                                    mostrarScriptMensaje("Servicio modificado correctamente.");
                                }

                                BindData();
                            }
                            catch
                            {
                                mostrarScriptMensaje("Error en la base de datos.");
                            }
                        }
                        else
                        {
                            mostrarScriptMensaje("La Patente " + Patente + " no existe en la lista de vehículos, del cliente " + Cliente + ".");
                        }
                    }
                    catch
                    {
                        mostrarScriptMensaje("Error en la base de datos.");
                    }
                    finally
                    {
                        datos.CerrarConexion();
                    }
                }
                else
                {
                    mostrarScriptMensaje("El servicio solo podrá ser completado, " +
                        "cuando la fecha de realización sea igual o inferior a la fecha actual.");
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtFecha2.Text == "" || txtHora.Text == "" || txtPatente2.Text == "" || ddlTiposServicio2.SelectedValue == "0"
                || ddlClientes2.SelectedValue == "0" || ddlEmpleados2.SelectedValue == "0" || ddlEstado2.SelectedValue == "0")
            {
                mostrarScriptMensaje("Hay campos vacíos o sin seleccionar. Por favor revise nuevamente.");
            }
            else
            {
                DateTime FechaHora = Convert.ToDateTime(txtFecha2.Text + ' ' + txtHora2.Text);
                string Patente = txtPatente2.Text;
                string Comentarios = txtComentarios2.Text;
                string Estado = ddlEstado2.SelectedValue.ToString();
                string IdTipo = ddlTiposServicio2.SelectedValue.ToString();
                string IdCliente = ddlClientes2.SelectedValue.ToString();
                string IdEmpleado = ddlEmpleados2.SelectedValue.ToString();

                //No se puede agregar un servicio repitiendo fecha, hora, tipo de servicio y patente.

                int resultado = ContarResultadosDB_Insert("Fecha", FechaHora.ToShortDateString(), "Hora", txtHora2.Text, "IdTipo", IdTipo, "Patente", Patente);

                if (resultado == 0)
                {

                    string insertServicio = "EXEC INSERT_SERVICIO '" + FechaHora + "', '" + Patente + "', '" + Comentarios + "', '" + Estado + "', " +
                                                                    IdTipo + ", " + IdCliente + ", " + IdEmpleado;

                    AccesoDatos sentencia = new AccesoDatos();

                    try
                    {
                        sentencia.IUD(insertServicio);

                        mostrarScriptMensaje("El servicio se ha insertado correctamente.");

                        BindData();
                    }
                    catch
                    {
                        mostrarScriptMensaje("Error en la base de datos.");
                    }
                }
                else
                {
                    mostrarScriptMensaje("Ya existe un servicio para el día " + FechaHora.ToShortDateString() + " a las " + txtHora2.Text +
                    " hs, para la patente " + txtPatente2.Text.ToUpper() + " y el tipo de servicio " + ddlTiposServicio2.SelectedItem.ToString() + ".");
                }
            }
        }

        protected void imgBtnCerrarPopup_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }

        protected void btnExportHistoricoServicios_Click(object sender, EventArgs e)
        {
            dgvHistoricoServicios.Visible = true;

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = Export_Historico_Servicios " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvHistoricoServicios.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();

            dgvHistoricoServicios.Visible = true;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename = ExportServicios " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvServicios.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void dgvHistoricoServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvHistoricoServicios.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}
