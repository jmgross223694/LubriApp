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
    public partial class turnos : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();

                string selectDdlTiposServicio = "SELECT * FROM TiposServicio " +
                                                "WHERE Estado = 1 ORDER BY Descripcion ASC";

                ddlTiposServicio.DataSource = sentencia.DSET(selectDdlTiposServicio);
                ddlTiposServicio.DataMember = "datos";
                ddlTiposServicio.DataTextField = "Descripcion";
                ddlTiposServicio.DataValueField = "ID";
                ddlTiposServicio.DataBind();
            }
        }

        public void BindData()
        {
            lblHora.Visible = false;
            ddlHoraTurno.Visible = false;
            ddlHoraTurno.Enabled = true;
            ddlTiposServicio.Visible = false;
            ddlTiposServicio.Enabled = false;
            txtCuitDni.Visible = false;
            txtCuitDni.Text = "";
            btnBuscarCuitDni.Visible = false;
            calendarioTurnos.Enabled = true;
            ddlVehiculos.Visible = false;
            lblCuitDni.Visible = false;
            lblRegistro.Visible = false;
            btnRegistro.Visible = false;
            txtCuitDni.Enabled = true;
            btnBuscarCuitDni.Text = "Siguiente paso";
            btnAgregarVehículo.Visible = false;

            string selectDgvTurnos = "SELECT * FROM ExportTurnos ORDER BY ID ASC";

            dgvTurnos.DataSource = sentencia.DSET(selectDgvTurnos);
            dgvTurnos.DataBind();
        }

        protected void calendarioTurnos_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
            }

            if ( e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }

            if (e.Day.Date.Year < DateTime.Today.Year)
            {
                e.Day.IsSelectable = false;
            }

            if (e.Day.Date.Year == DateTime.Today.Year && e.Day.Date.Month < DateTime.Today.Month)
            {
                e.Day.IsSelectable = false;
            }

            if (e.Day.Date.Year == DateTime.Today.Year && e.Day.Date.Month == DateTime.Today.Month && e.Day.Date.Day <= DateTime.Today.Day)
            {
                e.Day.IsSelectable = false;
            }
        }

        protected void calendarioTurnos_SelectionChanged(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();

            lblHora.Visible = true;
            ddlHoraTurno.Visible = true;
            ddlTiposServicio.Visible = true;
            ddlTiposServicio.Enabled = true;
            lblCuitDni.Visible = true;
            txtCuitDni.Visible = true;
            btnBuscarCuitDni.Visible = true;
            string dia = calendarioTurnos.SelectedDate.DayOfWeek.ToString();
            string selectCantidad;

            if (dia == "Saturday")
            {
                string selectddl = "select * from HorariosSabado";

                ddlHoraTurno.DataSource = sentencia.DSET(selectddl);
                ddlHoraTurno.DataMember = "datos";
                ddlHoraTurno.DataTextField = "Sabado";
                ddlHoraTurno.DataValueField = "ID";
                ddlHoraTurno.DataBind();

                selectCantidad = "select isnull(COUNT(*), 0) as Cantidad from HorariosSabado";
            }
            else
            {
                string selectddl = "select * from HorariosLunesViernes";

                ddlHoraTurno.DataSource = sentencia.DSET(selectddl);
                ddlHoraTurno.DataMember = "datos";
                ddlHoraTurno.DataTextField = "LunesViernes";
                ddlHoraTurno.DataValueField = "ID";
                ddlHoraTurno.DataBind();

                selectCantidad = "select isnull(COUNT(*), 0) as Cantidad from HorariosLunesViernes";
            }

            string diaNum = calendarioTurnos.SelectedDate.Day.ToString();
            string mes = calendarioTurnos.SelectedDate.Month.ToString();
            string año = calendarioTurnos.SelectedDate.Year.ToString();

            string fechaSeleccionada = año + "/" + mes + "/" + diaNum;

            string IdHorarioTurnosCargados = "EXEC SP_TURNOS_SELECCIONADOS '" + fechaSeleccionada + "'";

            try
            {
                datos.SetearConsulta(IdHorarioTurnosCargados);
                datos.EjecutarLectura();

                datos2.SetearConsulta(selectCantidad);
                datos2.EjecutarLectura();

                int cantidad = 0;

                if (datos2.Lector.Read()) { cantidad = Convert.ToInt32(datos2.Lector["Cantidad"]); }

                while (datos.Lector.Read())
                {
                    int IdHorario = Convert.ToInt32(datos.Lector["ID"]);
                    
                    for (int i = 1; i < cantidad+1; i++)
                    {
                        if (i == IdHorario)
                        {
                            string value = i.ToString();

                            ddlHoraTurno.SelectedValue = value;
                            ddlHoraTurno.SelectedItem.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Error al calcular cantidades.')", true);
            }
            finally
            {
                datos.CerrarConexion();
                datos2.CerrarConexion();
            }
        }

        protected void btnBuscarCuitDni_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            try
            {
                if (btnBuscarCuitDni.Text == "Confirmar Turno")
                {
                    AccesoDatos datos4 = new AccesoDatos();
                    try
                    {
                        string cuitDni = txtCuitDni.Text;
                        string campo;

                        if (txtCuitDni.Text.Length > 8)
                        { campo = "RazonSocial"; }
                        else { campo = "ApeNom"; }

                        string selectNombreCliente = "SELECT * FROM Clientes WHERE CUIT_DNI = '" + cuitDni + "'";

                        datos4.SetearConsulta(selectNombreCliente);
                        datos4.EjecutarLectura();

                        string cliente = "";
                        long IDCliente = 0;

                        if (datos4.Lector.Read())
                        {
                            IDCliente = Convert.ToInt64(datos4.Lector["ID"]);
                            cliente = (string)datos4.Lector[campo];
                        }

                        if (ddlVehiculos.SelectedValue != "Seleccione vehículo")
                        {
                            int IdTipoServicio = Convert.ToInt32(ddlTiposServicio.SelectedValue);
                            long IDVehiculo = Convert.ToInt64(ddlVehiculos.SelectedValue);
                            int idHora = Convert.ToInt32(ddlHoraTurno.SelectedValue);
                            string diaFecha = calendarioTurnos.SelectedDate.Day.ToString();
                            if (calendarioTurnos.SelectedDate.Day < 10) { diaFecha = "0" + diaFecha; }
                            string mesFecha = calendarioTurnos.SelectedDate.Month.ToString();
                            if (calendarioTurnos.SelectedDate.Month < 10) { mesFecha = "0" + mesFecha; }
                            int añoFecha = calendarioTurnos.SelectedDate.Year;
                            string fecha = añoFecha + "-" + mesFecha + "-" + diaFecha;
                            string hora = ddlHoraTurno.SelectedItem.ToString();
                            string dia = calendarioTurnos.SelectedDate.DayOfWeek.ToString();
                            if (dia == "Monday") { dia = "Lunes"; }
                            else if (dia == "Tuesday") { dia = "Martes"; }
                            else if (dia == "Wednesday") { dia = "Miércoles"; }
                            else if (dia == "Thursday") { dia = "Jueves"; }
                            else if (dia == "Friday") { dia = "Viernes"; }
                            else if (dia == "Saturday") { dia = "Sábado"; }

                            int resultadoTurnos = ContarResultadosDB_DosVariablesUnaCadena("Turnos", "CONVERT(date,FechaHora)", 
                                fecha, "IdCliente", IDCliente, "IdVehiculo", IDVehiculo);

                            if (resultadoTurnos == 1)
                            {
                                int IdTurnoExistente = 0;
                                string selectTurnoExistente = "SELECT ID FROM Turnos WHERE CONVERT(date,FechaHora) = '" + fecha + 
                                    "' AND IdCliente = " + IDCliente + " AND IdVehiculo = " + IDVehiculo;

                                AccesoDatos datos1 = new AccesoDatos();

                                try
                                {
                                    datos1.SetearConsulta(selectTurnoExistente);
                                    datos1.EjecutarLectura();
                                    if (datos1.Lector.Read())
                                    {
                                        IdTurnoExistente = Convert.ToInt32(datos1.Lector["ID"]);
                                    }
                                }
                                finally
                                {
                                    datos1.CerrarConexion();
                                }

                                if (IdTurnoExistente != 0)
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Usted tiene un turno pendiente para el día " + fecha + ".\\n\\n" +
                                    "Por cualquier duda o modificaciones sobre el mismo, " +
                                    "por favor comuníquese con nosotros a la brevedad, " +
                                    "informándonos el siguiente código de reserva: " + IdTurnoExistente + ".')", true);
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Usted tiene un turno pendiente para el día " + fecha + ".\\n\\n" +
                                    "Por cualquier duda o modificaciones sobre el mismo, " +
                                    "por favor comuníquese con nosotros a la brevedad.')", true);
                                }
                            }
                            else
                            {
                                string insertTurno = "EXEC SP_AGREGAR_TURNO '" + fecha + " " + hora + "', " + idHora + ", '" + dia + "', " + IDCliente + ", " + IDVehiculo + ", " + IdTipoServicio;

                                sentencia.IUD(insertTurno);

                                BindData();

                                AccesoDatos datos5 = new AccesoDatos();

                                string selectTurnoAgregado = "SELECT ID AS ID, (SELECT C.Mail from Clientes C where C.ID = IdCliente) AS MAIL, " +
                                                      "(SELECT V.Patente FROM Vehiculos V WHERE V.ID = IDVehiculo) AS Patente, " +
                                                      "(SELECT LOWER(TS.Descripcion) FROM TiposServicio TS WHERE TS.ID = IdTipoServicio) AS TipoServicio " +
                                                      "FROM Turnos WHERE " +
                                                      "IdCliente = " + IDCliente + " AND " +
                                                      "IdVehiculo = " + IDVehiculo + " AND " +
                                                      "FechaHora = '" + fecha + " " + hora + "'";

                                long IDTurno = 0;
                                string mailDestino = "";
                                string Patente = "";
                                string TipoServicio = "";

                                try
                                {
                                    datos5.SetearConsulta(selectTurnoAgregado);
                                    datos5.EjecutarLectura();

                                    if (datos5.Lector.Read())
                                    {
                                        IDTurno = Convert.ToInt64(datos5.Lector["ID"]);
                                        mailDestino = datos5.Lector["MAIL"].ToString();
                                        Patente = datos5.Lector["Patente"].ToString();
                                        TipoServicio = datos5.Lector["TipoServicio"].ToString();
                                    }
                                }
                                catch
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Error al leer DB')", true);
                                }
                                finally
                                {
                                    datos5.CerrarConexion();
                                }

                                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                "alert('¡Turno confirmado!\\n\\n" +
                                "*Detalles.\\n" +
                                "-Código de reserva: " + IDTurno + "\\n" +
                                "-Día: " + dia + "\\n" +
                                "-Fecha: " + fecha + "\\n" +
                                "-Horario: " + hora + "\\n" +
                                "-Servicio a realizar: " + TipoServicio + "')", true);

                                if (mailDestino != "")
                                {
                                    try
                                    {
                                        string asunto = "RESERVA DE TURNO";

                                        string cuerpo = "Buenas " + cliente + ", ojalá se encuentre muy bien." +
                                            "\n\nLe acercamos los detalles de su reserva." +
                                            "\n\n-Código de reserva: " + IDTurno +
                                            "\n-Vehículo: " + Patente +
                                            "\n-Día: " + dia +
                                            "\n-Fecha: " + fecha +
                                            "\n-Horario: " + hora +
                                            "\n-Servicio a realizar: " + TipoServicio +
                                            "\nPor cualquier duda o modificación, por favor contáctenos, " +
                                            "indicando su código de reserva." +
                                            "\n\nSaludos." +
                                            "Lubricentro Tony";
                                            
                                        EmailService mailNuevo = new EmailService();
                                        mailNuevo.armarCorreo(mailDestino, asunto, cuerpo);
                                        try
                                        {
                                            mailNuevo.enviarEmail();
                                        }
                                        catch
                                        {
                                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                            "alert('No se pudo enviar el mail.')", true);
                                        }
                                    }
                                    catch
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                        "alert('No se pudo crear el objeto mail.')", true);
                                    }
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('No tiene registrado ninguna dirección de mail en el sistema. " +
                                    "Por favor comuníquese con nosotros para poder enviarle los detalles de su turno.')", true);
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No seleccionó ningún vehículo.')", true);
                        }
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Error al intentar reservar el turno. Por favor contáctenos.')", true);
                    }
                    finally
                    {
                        datos4.CerrarConexion();
                    }
                }
                else
                {
                    if (txtCuitDni.Text == "")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe ingresar un CUIT ó DNI por favor.')", true);
                    }
                    if (ddlTiposServicio.SelectedValue == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Debe seleccionar un Servicio a realizar por favor.')", true);
                    }
                    else
                    {
                        lblRegistro.Visible = false;
                        btnRegistro.Visible = false;

                        string CuitDni = txtCuitDni.Text;
                        string campo = "ApeNom";

                        if (txtCuitDni.Text.Length > 8) { campo = "RazonSocial"; }

                        int resultadoCliente;

                        resultadoCliente = ContarResultadosDB_UnaCadena("Clientes", "CUIT_DNI", CuitDni);

                        if (resultadoCliente != 0)
                        {
                            string selectIdCliente = "SELECT * FROM Clientes WHERE CUIT_DNI = '" + CuitDni + "'";
                            long IdCliente;

                            datos2.SetearConsulta(selectIdCliente);
                            datos2.EjecutarLectura();

                            if (datos2.Lector.Read())
                            {
                                IdCliente = Convert.ToInt64(datos2.Lector["ID"]);
                                string clienteEncontrado = (string)datos2.Lector[campo];

                                int resultado2 = 0;

                                resultado2 = ContarResultadosDB_UnaVariable("Vehiculos", "IdCliente", IdCliente);

                                if (resultado2 != 0) //hay al menos un auto cargado
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('Bienvenido nuevamente " + clienteEncontrado + ".\\n\\n" +
                                    "En el próximo paso seleccione por favor el vehículo.\\n\\n" +
                                    "Si no está en la lista, también puede agregar uno nuevo.')", true);

                                    string selectDdlVehiculos = "SELECT * FROM Vehiculos WHERE IdCliente = " + IdCliente;

                                    ddlVehiculos.Items.Clear();
                                    ddlVehiculos.Items.Add("Seleccione vehículo");
                                    ddlVehiculos.DataSource = sentencia.DSET(selectDdlVehiculos);
                                    ddlVehiculos.DataMember = "datos";
                                    ddlVehiculos.DataTextField = "Patente";
                                    ddlVehiculos.DataValueField = "ID";
                                    ddlVehiculos.DataBind();

                                    btnBuscarCuitDni.Text = "Confirmar Turno";
                                    txtCuitDni.Enabled = false;
                                    ddlHoraTurno.Enabled = false;
                                    calendarioTurnos.Enabled = false;

                                    ddlVehiculos.Visible = true;
                                    btnAgregarVehículo.Visible = true;
                                }
                                else
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                                    "alert('No se encontraron vehículos cargados.\\n\\n" +
                                    "Por favor contáctenos a la brevedad para resolver el problema.')", true);
                                }
                            }
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No se encontraron resultados, revise el CUIT / DNI ingresado.\\n\\n" +
                            "Si todavía no esta registrado, debe hacerlo por unica vez.')", true);

                            lblRegistro.Visible = true;
                            btnRegistro.Visible = true;
                        }
                    }
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Error al intentar reservar el turno. Por favor reintente nuevamente en unos minutos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
                datos2.CerrarConexion();
            }
        }

        protected int ContarResultadosDB_UnaVariable(string tabla, string campo, long variable)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB = "SELECT isnull(COUNT(*), 0) Cantidad FROM " + tabla + " WHERE " + campo + " = " + variable;

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
                "alert('Se produjo un error al intentar leer la tabla: " + tabla + " en la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected int ContarResultadosDB_UnaCadena(string tabla, string campo, string cadena)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB = "SELECT isnull(COUNT(*), 0) Cantidad FROM " + tabla + " WHERE " + campo + " = '" + cadena + "' AND Estado = 1";

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
                "alert('Se produjo un error al intentar leer la tabla: " + tabla + " en la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected int ContarResultadosDB_DosVariablesUnaCadena(string tabla, string campo1, string cadena, string campo2, long variable1, string campo3, long variable2)
        {
            AccesoDatos datos = new AccesoDatos();

            int Resultado = 0;

            string selectDB = "SELECT isnull(COUNT(*), 0) Cantidad FROM " + tabla + " WHERE " + campo1 + " = '" + cadena + 
                              "' AND " + campo2 + " = " + variable1 + " AND " + campo3 + " = " + variable2 + " AND Estado = 'Pendiente'";

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
                "alert('Se produjo un error al intentar leer la tabla: " + tabla + " en la base de datos.')", true);
            }
            finally
            {
                datos.CerrarConexion();
            }

            return Resultado;
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Session.Add("btnRegistroClick", "btnRegistroClick");

            Response.Redirect("registroCliente.aspx");
        }

        protected void btnAgregarVehículo_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();

            long idCliente = 0;
            string cliente = "";

            string selectIdCliente = "SELECT * FROM Clientes WHERE CUIT_DNI = '" + txtCuitDni.Text + "'";

            datos.SetearConsulta(selectIdCliente);
            datos.EjecutarLectura();

            if (datos.Lector.Read()) 
            { 
                idCliente = Convert.ToInt64(datos.Lector["ID"]);
                if (txtCuitDni.Text.Length > 8)
                {
                    cliente = (string)datos.Lector["RazonSocial"];
                    Session.Add("RazonSocial", cliente);
                    Session.Add("ApeNom", "vacio");
                }
                else
                {
                    cliente = (string)datos.Lector["ApeNom"];
                    Session.Add("ApeNom", cliente);
                    Session.Add("RazonSocial", "vacio");
                }
            }

            Session.Add("idClienteRegistrado", idCliente);
            Session.Add("CuitDni", "vacio");
            Session.Add("Telefono", "vacio");
            Session.Add("Mail", "vacio");
            Session.Add("TipoCliente", "vacio");

            Response.Redirect("registroVehiculo.aspx");
        }
    }
}