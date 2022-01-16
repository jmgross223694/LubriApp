<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMTiposProducto.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMTiposProducto" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1 class="h1-abm">ABM - Tipos de Producto</h1>

    <br />

    <asp:ImageButton id="imgBtnBuscarTipoProducto" runat="server" ToolTip="Buscar" OnClick="imgBtnBuscarTipoProducto_Click" ImageUrl="~/img/find-logo.png" cssclass="btn-buscar-filtro-abm" />
    <asp:TextBox ID="txtDescripcionTipoProductoBuscar" runat="server" ToolTip="Buscador" PlaceHolder="Buscar..." onkeypress="javascript:return sololetras(event)" cssclass="txt-campo-filtro-abm-tipos-producto"></asp:TextBox>

    <button id="btnAgregarTipoProducto" ToolTip="Agregar Tipo de Producto" class="btnAddNewProductType">Agregar Nuevo</button>
    
    <br /><br />

    <table>

        <tr>
            <td style="padding: .5rem;">
                <asp:TextBox id="txtDescripcionTipoProducto" runat="server" ToolTip="Descripción" placeholder="Descripción" onkeypress="javascript:return sololetras(event)" Style="width: 200px; vertical-align: middle;" />
            </td>
            <td style="padding: .5rem;">
                <asp:TextBox id="txtIdTipoProducto" runat="server" TooTip="ID" Visible="false" placeholder="ID" Style="width: 60px; text-align:center; vertical-align: bottom !important;" />
                <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Tipo de Producto" onclientclick="return confirm('¿Confirma el cambio?');" OnClick="btnUpdateTipoProducto_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="vertical-align: bottom !important;" />
                &nbsp;&nbsp;
                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar Tipo de Producto" onclientclick="return confirm('¿Seguro que desea eliminar el Tipo de Producto?');" OnClick="btnDeleteTipoProducto_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: bottom !important;" />
            </td>
        </tr>

    </table>

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

		    <table style="width:80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                <tr align="center">
                    <td align="right" style="padding: .5rem;">
                        <asp:Button ID="btnCerraPopup" Text="X" runat="server" ToolTip="Cancelar" cssclass="btn-cerrar-popup" onclick="btnCerraPopup_Click" />
                    </td>
                </tr>
                
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtDescripcionTipoProducto2" runat="server" ToolTip="Descripción" placeholder="Descripción" onkeypress="javascript:return sololetras(event)" />
                    </td>
                </tr>
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:Button ID="imgBtnAgregarTipoProducto2" Text="Agregar" runat="server" ToolTip="Agregar Producto" onclientclick="return confirm('¿Confirma que desea agregar el nuevo producto?');" onclick="btnAddTipoProducto_Click" cssclass="img-btn-add-producto" />
                    </td>
                </tr>

		    </table>

        </div>

    </div>

    <br />

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-tiposprod" OnClick="btnExportExcel_Click" />

    <br /><br /><br />

    <center style="padding: .5rem;">
        <asp:GridView ID="dgvTiposProducto" runat="server" AllowSorting="True" OnSorting="dgvTiposProducto_Sorting" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="5" CssClass="dgv-abm-prod" AllowPaging="True" OnPageIndexChanging="dgvTiposProducto_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                <asp:BoundField DataField="Asignaciones" HeaderText="Asignaciones" SortExpression="Asignaciones" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerSettings Position="TopAndBottom" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:SqlDataSource ID="ExportTiposProducto" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportTiposProducto] ORDER BY [ID] ASC"></asp:SqlDataSource>
    </center>

    <script>
        function solonumeros(e) {
            var key;
            if (window.event) { key = e.keyCode; }
            else if (e.which) { key = e.which; }
            if (key < 48 || key > 57) { return false; }
            return true;
        }

        function sololetras(e) {
            var key;
            if (window.event) { key = e.keyCode; }
            else if (e.which) { key = e.which; }
            if (key >= 48 && key <= 57) { return false; }
            return true;
        }
    </script>

    <script>
        var btnAbrirPopup = document.getElementById('btnAgregarTipoProducto'),
            overlay = document.getElementById('overlay'),
            popup = document.getElementById('popup'),
            btnCerrarPopup = document.getElementById('btn-cerrar-popup');

        btnAbrirPopup.addEventListener('click', function (e) {
            e.preventDefault();
            overlay.classList.add('active');
            popup.classList.add('active');
        });

        btnCerrarPopup.addEventListener('click', function (e) {
            e.preventDefault();
            overlay.classList.remove('active');
            popup.classList.remove('active');
        });
    </script>

</asp:Content>
