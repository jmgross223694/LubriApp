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
    public partial class catalogo : System.Web.UI.Page
    {
        public List<Producto> lista;

        protected void Page_Load(object sender, EventArgs e)
        {
            ProductoDB productoDB = new ProductoDB();
            try
            {
                lista = productoDB.Listar();
                
                Session.Add("listadoProductos", lista);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void buscarProducto(object sender, EventArgs e)
        {
            List<Producto> filtro;
            if (txtFiltro.Text != "")
            {
                filtro = lista.FindAll(Art => Art.Descripción.ToUpper().Contains(txtFiltro.Text.ToUpper()) || Art.MarcaProducto.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()) || Art.TipoProducto.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()) || Art.EAN.ToString().Contains(txtFiltro.Text));
                lista = null;
                lista = filtro;
            }
        }
    }
}