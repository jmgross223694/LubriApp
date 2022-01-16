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
    public partial class ABMMarcaVehiculo : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

        }
        public void BindData()
        {
            string selectViewTiposProducto = "SELECT * FROM MarcasVehiculo ORDER BY ID ASC";

            dgvMarcasVehiculos.DataSource = sentencia.DSET(selectViewTiposProducto);
            dgvMarcasVehiculos.DataBind();

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMarca.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('El Campo Marca no puede estar vacío.')", true);
                }
                else
                {

                    if (chequeoMarca() == true)
                    {
                        string Marca = txtMarca.Text;

                        string GuardarMarca = "INSERT INTO MarcasVehiculo (Descripcion) values('" + Marca + "')";

                        sentencia.IUD(GuardarMarca);

                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Se ha agregado la Marca.')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('La Marca ya se encuentra ingresada.')", true);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Response.Redirect("ABMMarcaVehiculo.aspx");
            }
            
        }

        protected bool chequeoMarca()
        {
            string Marca = txtMarca.Text;

            string Consulta = "select count(*) from MarcasVehiculo where Descripcion like '" + Marca + "'";

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

        protected void dgvMarcasVehiculos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //int index;

                //index = Convert.ToInt32(e.CommandArgument);

                //GridViewRow selectedRow = dgvMarcasVehiculos.Rows[index];
                ////TableCell Descripcion = selectedRow.Cells[1] ;
                ////string contact = Descripcion.Text;

                //string contact = selectedRow.Cells[2].;
                ////Usuario u = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Funciona pero falta traer los datos del GridView')", true);
            }
        }
    }
}