using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class registroVehiculo : System.Web.UI.Page
    {
        AccesoDatos datos = new AccesoDatos();
        AccesoDatos datos2 = new AccesoDatos();
        AccesoDatos datos3 = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["btnRegistroClick"] != null)
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                Session.Add("error", "Ha sido redirigido a esta página por error.");

                Response.Redirect("Error.aspx");
            }
        }

        private void BindData()
        {
            string selectMarcasVehiculo = "SELECT * FROM MarcasVehiculo ORDER BY Descripcion ASC";

            cargarDdlAños();

            ddlMarcaVehiculo.DataSource = datos3.DSET(selectMarcasVehiculo);
            ddlMarcaVehiculo.DataMember = "datos";
            ddlMarcaVehiculo.DataTextField = "Descripcion";
            ddlMarcaVehiculo.DataValueField = "ID";
            ddlMarcaVehiculo.DataBind();
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
            }
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("turnos.aspx");
        }

        protected void btnConfirmarRegistro_Click(object sender, EventArgs e)
        {
            if (txtPatente.Text == "" || ddlMarcaVehiculo.SelectedValue == "0"
                || txtModelo.Text == "" || ddlAnioFabricacion.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos o sin seleccionar, por favor revíselos.')", true);
            }
            else
            {
                try
                {
                    //Campos Cliente
                    string IdCliente = "";
                    string cuitDni = Session["CuitDni"].ToString();
                    string apeNom = Session["ApeNom"].ToString();
                    string razonSocial = Session["RazonSocial"].ToString();
                    string telefono = Session["Telefono"].ToString();
                    string mail = Session["Mail"].ToString();
                    string tipoCliente = Session["TipoCliente"].ToString();

                    if (cuitDni == "vacio")
                    {
                        int cantVehiculos = 0;
                        string patente = txtPatente.Text;
                        string validarPatente = "SELECT ISNULL(COUNT(*), 0) Cantidad FROM Vehiculos WHERE Patente = '" + patente + "'";
                        AccesoDatos datos4 = new AccesoDatos();

                        try
                        {
                            datos4.SetearConsulta(validarPatente);
                            datos4.EjecutarLectura();

                            if (datos4.Lector.Read() == true)
                            {
                                cantVehiculos = Convert.ToInt32(datos4.Lector["Cantidad"]);
                            }
                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Se ha producido un error en la Base de datos.')", true);
                        }
                        finally
                        {
                            datos4.CerrarConexion();
                        }
                        

                        if (cantVehiculos == 0)
                        {
                            IdCliente = Session["idClienteRegistrado"].ToString();
                            string IdMarca = ddlMarcaVehiculo.SelectedValue.ToString();
                            string modelo = txtModelo.Text;
                            string añoFabricacion = ddlAnioFabricacion.SelectedItem.ToString();

                            string SP_AgregarVehiculo = "EXEC SP_AGREGAR_VEHICULO '" + patente + "', " + IdMarca + ", '" +
                                                        modelo + "', " + añoFabricacion + ", " + IdCliente;

                            datos.IUD(SP_AgregarVehiculo);

                            string apeNom_razonSocial = apeNom; //CON ESTA VARIABLE DETERMINO EN EL SIGUIENTE 'IF' SI ES APE-NOM O RAZON-SOCIAL.

                            if (apeNom == "vacio") { apeNom_razonSocial = razonSocial; }

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El vehículo patente: " + patente + ", para el cliente " +
                             apeNom_razonSocial + ", se ha registrado correctamente.')", true);

                            string script = @"<script type='text/javascript'>

                                            location.href='turnos.aspx';

                                       </script>";

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El vehículo patente: " + patente + ", ya existe en el sistema.')", true);
                        }
                    }
                    else
                    {
                        int cantVehiculos = 0;
                        string patente = txtPatente.Text;
                        string validarPatente = "SELECT ISNULL(COUNT(*), 0) Cantidad FROM Vehiculos WHERE Patente = '" + patente + "'";
                        AccesoDatos datos5 = new AccesoDatos();

                        try
                        {
                            datos5.SetearConsulta(validarPatente);
                            datos5.EjecutarLectura();

                            if (datos5.Lector.Read() == true)
                            {
                                cantVehiculos = Convert.ToInt32(datos5.Lector["Cantidad"]);
                            }
                        }
                        catch
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('Se ha producido un error en la Base de datos.')", true);
                        }
                        finally
                        {
                            datos5.CerrarConexion();
                        }

                        if (cantVehiculos == 0)
                        {
                            //Campos Vehículo
                            string IdMarca = ddlMarcaVehiculo.SelectedValue.ToString();
                            string modelo = txtModelo.Text;
                            string añoFabricacion = ddlAnioFabricacion.SelectedItem.ToString();

                            string variableSP; //EN LA BASE HAY 2 SP, UNO SI ES DNI Y OTRO SI ES CUIT. LO DETERMINO EN EL SIGUIENTE 'IF'.
                            string apeNom_razonSocial; //CON ESTA VARIABLE DETERMINO EN EL SIGUIENTE 'IF' SI ES APE-NOM O RAZON-SOCIAL.

                            if (apeNom == "") { variableSP = "CUIT"; apeNom_razonSocial = razonSocial; }
                            else { variableSP = "DNI"; apeNom_razonSocial = apeNom; }


                            //Primero registrar el cliente
                            string SP_RegistrarCliente = "EXEC SP_AGREGAR_CLIENTE_" + variableSP + " " + tipoCliente +
                                                         ", '" + cuitDni + "', '" + apeNom_razonSocial + "', '" +
                                                         mail + "', '" + telefono + "'";

                            datos.IUD(SP_RegistrarCliente);

                            //Luego obtener el ID del cliente
                            string selectIdCliente = "SELECT ID as ID FROM Clientes WHERE CUIT_DNI = '" + cuitDni + "'";

                            datos2.SetearConsulta(selectIdCliente);
                            datos2.EjecutarLectura();

                            if (datos2.Lector.Read()) { IdCliente = datos2.Lector["ID"].ToString(); }

                            datos2.CerrarConexion();

                            //Luego con el ID obtenido, registrar el vehiculo
                            string SP_AgregarVehiculo = "EXEC SP_AGREGAR_VEHICULO '" + patente + "', " + IdMarca + ", '" +
                                                        modelo + "', " + añoFabricacion + ", " + IdCliente;

                            datos.IUD(SP_AgregarVehiculo);

                            txtPatente.Text = "";
                            ddlMarcaVehiculo.SelectedValue = "0";
                            txtModelo.Text = "";
                            ddlAnioFabricacion.SelectedValue = "0";

                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El cliente  " + apeNom_razonSocial + " y el vehículo patente: " + patente +
                            ", se han registrado correctamente.')", true);


                            string script = @"<script type='text/javascript'>

                                            location.href='turnos.aspx';

                                       </script>";

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('El vehículo patente: " + patente + ", ya existe en el sistema.')", true);
                        }
                    }
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha producido un error en el registro.\\n\\n" +
                    "Por favor reintente en unos minutos.')", true);
                }
            }
        }
    }
}