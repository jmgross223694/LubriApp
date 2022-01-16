using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using Negocio;
using Dominio;
using System.Data;

namespace TPC_GROSS_LAINO_CHAPARRO
{
    public partial class ABMProductos : System.Web.UI.Page
    {
        AccesoDatos sentencia = new AccesoDatos();
        protected void Page_Load(object sender, EventArgs e)
        {
            validarNivelUsuario();

            if (!IsPostBack)
            {
                CargarDesplegables();

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
            txtBuscar.Text = "";

            txtEan.Text = "";
            txtDescripcion.Text = "";
            ddlTipoProducto.SelectedValue = "0";
            ddlMarcaProducto.SelectedValue = "0";
            ddlProveedor.SelectedValue = "0";
            txtFechaCompra.Text = "";
            txtFechaVencimiento.Text = "";
            txtCosto.Text = "";
            txtPrecioVenta.Text = "";
            txtStock.Text = "";
            ddlEstado.SelectedValue = "0";

            txtEan2.Text = "";
            txtDescripcion2.Text = "";
            ddlTipoProducto2.SelectedValue = "0";
            ddlMarcaProducto2.SelectedValue = "0";
            ddlProveedor2.SelectedValue = "0";
            txtFechaCompra2.Text = "";
            txtFechaVencimiento2.Text = "";
            txtCosto2.Text = "";
            txtPrecioVenta2.Text = "";
            txtStock2.Text = "";
            ddlEstado2.SelectedValue = "0";

            txtEan.Enabled = false;
            txtDescripcion.Enabled = false;
            ddlTipoProducto.Enabled = false;
            ddlMarcaProducto.Enabled = false;
            ddlProveedor.Enabled = false;
            txtFechaCompra.Enabled = false;
            txtFechaVencimiento.Enabled = false;
            txtCosto.Enabled = false;
            txtPrecioVenta.Enabled = false;
            txtStock.Enabled = false;
            ddlEstado.Enabled = false;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            btnUpdateImage.Visible = true;
            btnUpdateImage.Enabled = false;
            fileUploadImgProd.Visible = true;
            fileUploadImgProd.Enabled = false;
            btnExportExcel.Visible = true;
            dgvInventario.Visible = true;

            CargarInventario();
        }

        public void mostrarScriptMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert",
            "alert('" + mensaje + "')", true);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int tamanio = 0;

            try
            {
                tamanio = fileUploadImgAddProd.PostedFile.ContentLength;
            }
            catch
            {
                if (tamanio == 0) { }
            }

            if (txtEan2.Text == "" || txtDescripcion2.Text == "" || tamanio == 0
                || txtFechaCompra2.Text == "" || txtFechaVencimiento2.Text == "" || txtCosto2.Text == ""
                || txtStock2.Text == "" || ddlMarcaProducto2.SelectedIndex == 0 ||
                ddlTipoProducto2.SelectedIndex == 0 || ddlProveedor2.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos o sin seleccionar.')", true);
            }
            else
            {
                string EAN = txtEan2.Text;
                string Descripcion = txtDescripcion2.Text;
                int IdTipoProducto = Convert.ToInt32(ddlTipoProducto2.SelectedValue);
                int IdMarca = Convert.ToInt32(ddlMarcaProducto2.SelectedValue);
                int IdProveedor = Convert.ToInt32(ddlProveedor2.SelectedValue);
                DateTime FechaCompra = Convert.ToDateTime(txtFechaCompra2.Text);
                DateTime FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento2.Text);

                if (FechaCompra > DateTime.Now || FechaVencimiento < DateTime.Now)
                {
                    if (FechaCompra > DateTime.Now && FechaVencimiento < DateTime.Now)
                    {
                        mostrarScriptMensaje("La fecha de compra no puede ser mayor a la fecha de hoy.\n\n" +
                            "La fecha de vencimiento no puede ser menor que la fecha de hoy.");
                    }
                    else if (FechaCompra > DateTime.Now)
                    {
                        mostrarScriptMensaje("La fecha de compra no puede ser mayor a la fecha de hoy.");
                    }
                    else
                    {
                        mostrarScriptMensaje("La fecha de vencimiento no puede ser menor que la fecha de hoy.");
                    }
                }
                else
                {

                    FechaCompra.ToShortDateString();
                    FechaVencimiento.ToShortDateString();
                    string Costo = txtCosto2.Text;
                    string PrecioVenta = txtPrecioVenta2.Text;
                    string Stock = txtStock2.Text;
                    int Estado = 1;

                    if (ddlEstado2.SelectedValue == "2") { Estado = 0; }

                    string sp_InsertInventario = "EXEC SP_INSERTAR_PRODUCTO '" + EAN + "', '" + Descripcion + "', '" + IdTipoProducto
                    + "', '" + IdMarca + "', '" + IdProveedor + "', '" + FechaCompra + "', '" + FechaVencimiento + "', '" + Costo + "', '" + PrecioVenta
                    + "', '" + Stock + "', '" + Estado + "'";

                    //Obtener datos de la imagen
                    byte[] imagenOriginal = new byte[tamanio];
                    fileUploadImgAddProd.PostedFile.InputStream.Read(imagenOriginal, 0, tamanio);
                    Bitmap imagenOriginalBinaria = new Bitmap(fileUploadImgAddProd.PostedFile.InputStream);

                    //Crear imagen Thumbnail (redimensionar imagen)
                    System.Drawing.Image imgThumbnail;
                    int tamanioThumbnail = 200;
                    imgThumbnail = RedimensionarImagen(imagenOriginalBinaria, tamanioThumbnail);
                    byte[] bImgThumbnail = new byte[tamanioThumbnail];
                    ImageConverter convertidor = new ImageConverter();
                    bImgThumbnail = (byte[])convertidor.ConvertTo(imgThumbnail, typeof(byte[]));

                    //Actualizar tabla Inventario en DB
                    //string cadenaConexion = "data source=.\\SQLEXPRESS; initial catalog=GROSS_LAINO_CHAPARRO_DB; integrated security=sspi";
                    string cadenaConexion = "data source=workstation id=DBLubriApp.mssql.somee.com;packet size=4096;user id=jmgross22_SQLLogin_1;pwd=efo9qqqnae;data source=DBLubriApp.mssql.somee.com;persist security info=False;initial catalog=DBLubriApp";
                    SqlConnection conexionSql = new SqlConnection(cadenaConexion);
                    SqlCommand comandoSql = new SqlCommand();
                    comandoSql.CommandText = "INSERT INTO ImagenesInventario(Imagen, EAN) VALUES(@Imagen, " + txtEan2.Text + ")";
                    comandoSql.Parameters.Add("@Imagen", SqlDbType.Image).Value = bImgThumbnail;
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.Connection = conexionSql;

                    try
                    {
                        conexionSql.Open();
                        comandoSql.ExecuteNonQuery(); //Cargar imagen
                        sentencia.IUD(sp_InsertInventario); //Cargar producto

                        mostrarScriptMensaje("El EAN: " + txtEan2.Text + " se ha guardado correctamente.");

                        BindData();
                    }
                    catch
                    {
                        mostrarScriptMensaje("Se ha producido un error y no se ha agregado el EAN: " + txtEan2.Text + " al sistema.");
                    }
                    finally
                    {
                        conexionSql.Close();
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            
            if (txtEan.Text == "" || txtDescripcion.Text == "" || ddlTipoProducto.SelectedIndex == 0
                || ddlMarcaProducto.SelectedIndex == 0
                || ddlProveedor.SelectedIndex == 0 || txtFechaCompra.Text == ""
                || txtFechaVencimiento.Text == "" || txtCosto.Text == ""
                || txtStock.Text == "" || ddlEstado.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Hay campos vacíos o sin seleccionar.')", true);
            }
            else
            {
                string ID = txtID.Text;
                string EAN = txtEan.Text;
                string Descripcion = txtDescripcion.Text;
                int IdTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                int IdMarca = Convert.ToInt32(ddlMarcaProducto.SelectedValue);
                int IdProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
                DateTime FechaCompra = Convert.ToDateTime(txtFechaCompra.Text);
                DateTime FechaVencimiento = Convert.ToDateTime(txtFechaVencimiento.Text);
                string Costo = txtCosto.Text;
                string PrecioVenta = txtPrecioVenta.Text;
                int Stock = Convert.ToInt32(txtStock.Text);
                int Estado = 0;
                if (ddlEstado.SelectedValue == "1") { Estado = 1; }

                string sp_UpdateInventario = "EXEC SP_ACTUALIZAR_PRODUCTO " + ID + ", " + EAN + ", '" + Descripcion + "', " + IdTipoProducto
                + ", " + IdMarca + ", " + IdProveedor + ", '" + FechaCompra.ToShortDateString() + "', '" + FechaVencimiento.ToShortDateString() + "', " + Costo + ", " + PrecioVenta
                + ", " + Stock + ", " + Estado;

                try
                {
                    datos.SetearConsulta(sp_UpdateInventario);
                    datos.EjecutarLectura();

                    mostrarScriptMensaje("Se ha actualizado el producto EAN: " + EAN);

                    BindData();
                }
                catch (Exception)
                {
                    mostrarScriptMensaje("Se ha producido un error y no se ha modificado el producto.");
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEan.Text == "" || txtDescripcion.Text == "" || txtFechaCompra.Text == ""
                    || txtFechaVencimiento.Text == "" || txtCosto.Text == ""
                    || txtStock.Text == "" || ddlMarcaProducto.SelectedIndex == 0 ||
                    ddlTipoProducto.SelectedIndex == 0 || ddlProveedor.SelectedIndex == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Hay campos vacíos o sin seleccionar.')", true);
                }
                else
                {
                    string EAN = txtEan.Text;

                    string sp_DeleteInventario = "DELETE FROM Inventario WHERE EAN = " + EAN;

                    sentencia.IUD(sp_DeleteInventario);

                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Se ha eliminado el producto.')", true);

                    BindData();
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Se ha producido un error y no se ha eliminado el producto.')", true);
            }
            finally
            {
                BindData();
            }
        }

        protected void imgBtnBuscarProducto_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datos2 = new AccesoDatos();
            try
            {
                if (txtBuscar.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Ingresa un filtro de búsqueda.')", true);

                    BindData();
                }
                else
                {
                    string Valor = txtBuscar.Text;

                    string columnasSelectCamposProducto = "SELECT ID, EAN, Descripción, Imagen," +
                                                        " Convert(varchar(10), IdTipo) IdTipo, TipoProducto," +
                                                        " Convert(varchar(10), IdMarca) IdMarca, Marca," +
                                                        " Convert(varchar(10), IdProveedor) IdProveedor, Proveedor," +
                                                        " Convert(varchar(10), [Fecha de Compra], 105) FechaCompra, " +
                                                        " Convert(varchar(10), [Fecha de Vencimiento], 105) FechaVencimiento," +
                                                        " Convert(varchar(10), Costo) Costo, " +
                                                        " Convert(varchar(10), PrecioVenta) PrecioVenta," +
                                                        " Convert(varchar(10), Stock) Stock, Convert(varchar(10), Estado) Estado " +
                                                        " FROM ExportInventario";

                    string selectDgvProducto = "SELECT EAN, Descripción, Imagen, TipoProducto, Marca, " +
                                               "Proveedor, [Fecha de Compra], [Fecha de Vencimiento], Costo, " +
                                               "PrecioVenta, Stock, Estado FROM ExportInventario " +
                                               "WHERE EAN LIKE '%" + Valor + "%'" +
                                               " OR Descripción LIKE '%" + Valor + "%'" +
                                               " OR TipoProducto LIKE '%" + Valor + "%'" +
                                               " OR Marca LIKE '%" + Valor + "%'" +
                                               " OR Proveedor LIKE '%" + Valor + "%'";

                    datos2.SetearConsulta(selectDgvProducto);
                    datos2.EjecutarLectura();

                    if (Valor.All(char.IsDigit) == true)
                    {
                        string selectCamposProducto = columnasSelectCamposProducto +
                                                " WHERE EAN = " + Valor;

                        datos.SetearConsulta(selectCamposProducto);
                        datos.EjecutarLectura();
                    }
                    else
                    {
                        string selectCamposProducto = columnasSelectCamposProducto +
                                                " WHERE Descripción = '" + Valor + "'";

                        datos.SetearConsulta(selectCamposProducto);
                        datos.EjecutarLectura();
                    }

                    if (datos.Lector.Read() == true)
                    {
                        txtID.Text = datos.Lector["ID"].ToString();
                        txtEan.Text = datos.Lector["EAN"].ToString();
                        txtDescripcion.Text = (string)datos.Lector["Descripción"];
                        //mostrarImagenPrueba.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])datos.Lector["Imagen"]);
                        ddlTipoProducto.SelectedValue = datos.Lector["IdTipo"].ToString();
                        ddlMarcaProducto.SelectedValue = datos.Lector["IdMarca"].ToString();
                        ddlProveedor.SelectedValue = datos.Lector["IdProveedor"].ToString();
                        txtFechaCompra.Text = datos.Lector["FechaCompra"].ToString();
                        txtFechaVencimiento.Text = datos.Lector["FechaVencimiento"].ToString();
                        txtCosto.Text = datos.Lector["Costo"].ToString();
                        txtPrecioVenta.Text = datos.Lector["PrecioVenta"].ToString();
                        txtStock.Text = datos.Lector["Stock"].ToString();
                        if (datos.Lector["Estado"].ToString() == "1") { ddlEstado.SelectedValue = "1"; }
                        else { ddlEstado.SelectedValue = "2"; }

                        Session.Add("descripcionOriginalProducto", txtDescripcion.Text);

                        dgvInventario.DataSource = sentencia.DSET(selectDgvProducto);
                        dgvInventario.DataBind();

                        btnUpdateImage.Enabled = true;

                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;

                        txtEan.Enabled = false;
                        txtDescripcion.Enabled = true;
                        ddlTipoProducto.Enabled = true;
                        ddlMarcaProducto.Enabled = true;
                        ddlProveedor.Enabled = true;
                        txtFechaCompra.Enabled = true;
                        txtFechaVencimiento.Enabled = true;
                        txtCosto.Enabled = true;
                        txtPrecioVenta.Enabled = true;
                        txtStock.Enabled = true;
                        ddlEstado.Enabled = true;
                    }
                    else
                    {
                        if (datos2.Lector.Read() == false)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                            "alert('No se econtró ninguna coincidencia.')", true);

                            BindData();
                        }
                        else
                        {
                            dgvInventario.DataSource = sentencia.DSET(selectDgvProducto);
                            dgvInventario.DataBind();

                            txtEan.Text = "";
                            txtDescripcion.Text = "";
                            ddlTipoProducto.SelectedValue = "0";
                            ddlMarcaProducto.SelectedValue = "0";
                            ddlProveedor.SelectedValue = "0";
                            txtFechaCompra.Text = "";
                            txtFechaVencimiento.Text = "";
                            txtCosto.Text = "";
                            txtPrecioVenta.Text = "";
                            txtStock.Text = "";
                            ddlEstado.SelectedValue = "0";

                            txtEan.Enabled = false;
                            txtDescripcion.Enabled = false;
                            ddlTipoProducto.Enabled = false;
                            ddlMarcaProducto.Enabled = false;
                            ddlProveedor.Enabled = false;
                            txtFechaCompra.Enabled = false;
                            txtFechaVencimiento.Enabled = false;
                            txtCosto.Enabled = false;
                            txtPrecioVenta.Enabled = false;
                            txtStock.Enabled = false;
                            ddlEstado.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
                mostrarScriptMensaje("Se produjo un error en la búsqueda. Por favor reintente en unos minutos.");
            }
            finally
            {
                datos.CerrarConexion();
                datos2.CerrarConexion();
            }
        }

        protected void btnCerraPopup_Click(object sender, EventArgs e)
        {
            txtEan2.Text = "";
            txtDescripcion2.Text = "";
            ddlTipoProducto2.SelectedValue = "0";
            ddlMarcaProducto2.SelectedValue = "0";
            ddlProveedor2.SelectedValue = "0";
            txtFechaCompra2.Text = "";
            txtFechaVencimiento2.Text = "";
            txtCosto2.Text = "";
            txtPrecioVenta2.Text = "";
            txtStock2.Text = "";
            ddlEstado2.SelectedValue = "0";

            BindData();
        }

        protected void dgvInventario_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();

            string selectOrdenar = "SELECT * FROM ExportInventario ORDER BY " + e.SortExpression + " "
                                    + GetSortDirection(e.SortExpression);

            dgvInventario.DataSource = sentencia.DSET(selectOrdenar);
            dgvInventario.DataBind();
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
            Response.AddHeader("content-disposition", "attachment;filename = ExportInventario " + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            dgvInventario.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        /*protected void btnPreviewImage_Click(object sender, EventArgs e)
        {
            int tamanio = fileUploadImgProd.PostedFile.ContentLength;

            try
            {
                //Obtener datos de la imagen
                byte[] imagenOriginal = new byte[tamanio];
                fileUploadImgProd.PostedFile.InputStream.Read(imagenOriginal, 0, tamanio);
                Bitmap imagenOriginalBinaria = new Bitmap(fileUploadImgProd.PostedFile.InputStream);
                //Crear imagen Thumbnail
                System.Drawing.Image imgThumbnail;
                int tamanioThumbnail = 200;
                imgThumbnail = RedimensionarImagen(imagenOriginalBinaria, tamanioThumbnail);
                byte[] bImgThumbnail = new byte[tamanioThumbnail];
                ImageConverter convertidor = new ImageConverter();
                bImgThumbnail = (byte[])convertidor.ConvertTo(imgThumbnail, typeof(byte[]));
                //Mostrar vista previa
                string imagenDataUrl64 = "data:image/jpg;base64," + Convert.ToBase64String(bImgThumbnail);
                //mostrarImagenPrueba.ImageUrl = imagenDataUrl64;
            }
            catch
            {
                if (tamanio == 0)
                {
                    mostrarScriptMensaje("No se ha seleccionado ninguna imágen.");
                }
            }
        }*/

        public System.Drawing.Image RedimensionarImagen(System.Drawing.Image imagenOriginal, int alto)
        {
            var Radio = (double)alto / imagenOriginal.Height;
            var NuevoAncho = (int)(imagenOriginal.Width * Radio);
            var NuevoAlto = (int)(imagenOriginal.Height * Radio);
            var imagenRedimensionada = new Bitmap(NuevoAncho, NuevoAlto);
            var g = Graphics.FromImage(imagenRedimensionada);
            g.DrawImage(imagenOriginal, 0, 0, NuevoAncho, NuevoAlto);

            return imagenRedimensionada;
        }

        protected void CargarInventario()
        {
            AccesoDatos sentencia = new AccesoDatos();
            AccesoDatos datos = new AccesoDatos();

            string selectViewInventario = "SELECT * FROM ExportInventario ORDER BY EAN ASC";

            try
            {
                datos.SetearConsulta(selectViewInventario);
                datos.EjecutarLectura();

                if (datos.Lector.Read() == true)
                {
                    string bandera = "0";

                    bandera = datos.Lector["Imagen"].ToString();

                    if (bandera != "0")
                    {
                        dgvInventario.DataSource = sentencia.DSET(selectViewInventario);
                        dgvInventario.DataBind();
                    }
                }
            }
            catch
            {
                mostrarScriptMensaje("Se ha producido un error al intentar listar el inventario.");
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        protected void CargarDesplegables()
        {
            string selectTP = "SELECT * FROM TiposProducto order by Descripcion asc";
            string selectMarcas = "SELECT * FROM MarcasProducto order by Descripcion asc";
            string selectProveedores = "SELECT * FROM Proveedores order by RazonSocial asc";

            ddlTipoProducto.DataSource = sentencia.DSET(selectTP);
            ddlTipoProducto.DataMember = "datos";
            ddlTipoProducto.DataTextField = "Descripcion";
            ddlTipoProducto.DataValueField = "ID";
            ddlTipoProducto.DataBind();

            ddlMarcaProducto.DataSource = sentencia.DSET(selectMarcas);
            ddlMarcaProducto.DataMember = "datos";
            ddlMarcaProducto.DataTextField = "Descripcion";
            ddlMarcaProducto.DataValueField = "ID";
            ddlMarcaProducto.DataBind();

            ddlProveedor.DataSource = sentencia.DSET(selectProveedores);
            ddlProveedor.DataMember = "datos";
            ddlProveedor.DataTextField = "RazonSocial";
            ddlProveedor.DataValueField = "ID";
            ddlProveedor.DataBind();

            ddlTipoProducto2.DataSource = sentencia.DSET(selectTP);
            ddlTipoProducto2.DataMember = "datos";
            ddlTipoProducto2.DataTextField = "Descripcion";
            ddlTipoProducto2.DataValueField = "ID";
            ddlTipoProducto2.DataBind();

            ddlMarcaProducto2.DataSource = sentencia.DSET(selectMarcas);
            ddlMarcaProducto2.DataMember = "datos";
            ddlMarcaProducto2.DataTextField = "Descripcion";
            ddlMarcaProducto2.DataValueField = "ID";
            ddlMarcaProducto2.DataBind();

            ddlProveedor2.DataSource = sentencia.DSET(selectProveedores);
            ddlProveedor2.DataMember = "datos";
            ddlProveedor2.DataTextField = "RazonSocial";
            ddlProveedor2.DataValueField = "ID";
            ddlProveedor2.DataBind();
        }

        protected void btnUpdateImage_Click(object sender, EventArgs e)
        {
            if (btnUpdateImage.Text == "Actualizar Imágen")
            {
                fileUploadImgProd.Enabled = true;
                btnUpdateImage.Text = "Confirmar";
            }
            else
            {
                int tamanio = fileUploadImgProd.PostedFile.ContentLength;

                if (tamanio != 0)
                {
                    //Obtener datos de la imagen
                    byte[] imagenOriginal = new byte[tamanio];
                    fileUploadImgProd.PostedFile.InputStream.Read(imagenOriginal, 0, tamanio);
                    Bitmap imagenOriginalBinaria = new Bitmap(fileUploadImgProd.PostedFile.InputStream);

                    //Crear imagen Thumbnail (redimensionar imagen)
                    System.Drawing.Image imgThumbnail;
                    int tamanioThumbnail = 200;
                    imgThumbnail = RedimensionarImagen(imagenOriginalBinaria, tamanioThumbnail);
                    byte[] bImgThumbnail = new byte[tamanioThumbnail];
                    ImageConverter convertidor = new ImageConverter();
                    bImgThumbnail = (byte[])convertidor.ConvertTo(imgThumbnail, typeof(byte[]));

                    //Actualizar tabla Inventario en DB
                    //string cadenaConexion = "data source=.\\SQLEXPRESS; initial catalog=GROSS_LAINO_CHAPARRO_DB; integrated security=sspi";
                    string cadenaConexion = "data source=workstation id=DBLubriApp.mssql.somee.com;packet size=4096;user id=jmgross22_SQLLogin_1;pwd=efo9qqqnae;data source=DBLubriApp.mssql.somee.com;persist security info=False;initial catalog=DBLubriApp";
                    SqlConnection conexionSql = new SqlConnection(cadenaConexion);
                    SqlCommand comandoSql = new SqlCommand();
                    //comandoSql.CommandText = "INSERT INTO ImagenesInventario(Imagen, EAN) VALUES(@Imagen, " + txtEan.Text + ")";
                    comandoSql.CommandText = "UPDATE ImagenesInventario SET Imagen = @Imagen WHERE EAN = " + txtEan.Text;
                    int Cantidad = 1;
                    try
                    {
                        AccesoDatos datos = new AccesoDatos();
                        datos.SetearConsulta("SELECT isnull(COUNT(*), 0) Cantidad FROM ImagenesInventario WHERE EAN = " + txtEan.Text);
                        datos.EjecutarLectura();
                        if (datos.Lector.Read() == true)
                        {
                            Cantidad = Convert.ToInt32(datos.Lector["Cantidad"]);
                        }
                        datos.CerrarConexion();
                    }
                    catch { }

                    if (Cantidad == 0)
                    {
                        comandoSql.CommandText = "INSERT INTO ImagenesInventario(Imagen, EAN) VALUES(@Imagen, " + txtEan.Text + ")";
                    }
                    
                    comandoSql.Parameters.Add("@Imagen", SqlDbType.Image).Value = bImgThumbnail;
                    comandoSql.CommandType = CommandType.Text;
                    comandoSql.Connection = conexionSql;

                    try
                    {
                        conexionSql.Open();
                        comandoSql.ExecuteNonQuery();

                        btnUpdateImage.Text = "Actualizar Imágen";

                        mostrarScriptMensaje("La imágen para el EAN: " + txtEan.Text + " se ha subido correctamente.");

                        BindData();
                    }
                    catch
                    {
                        mostrarScriptMensaje("Error al cargar la imágen para el EAN: " + txtEan.Text + ".");
                    }
                    finally
                    {
                        fileUploadImgProd.Enabled = false;
                        conexionSql.Close();
                    }
                }
                else
                {
                    mostrarScriptMensaje("No se ha seleccionado ninguna imágen.");
                }
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string Ean = txtEan.Text;

            AccesoDatos sentencia = new AccesoDatos();

            string deleteImage = "DELETE FROM ImagenesInventario WHERE EAN = " + Ean;

            try
            {
                sentencia.IUD(deleteImage);

                mostrarScriptMensaje("La imágen del EAN: " + Ean + " se ha borrado correctamente.");

                btnUpdateImage.Enabled = true;
            }
            catch
            {
                mostrarScriptMensaje("Se produjo un error y no se pudo borrar la imágen del producto.");
            }
        }

        protected void dgvInventario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInventario.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}
