<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMMarcasProducto.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMMarcasProducto" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h1-abm">ABM - Marcas de productos</h1>

    <br />

    <div class="stl-buscador-productos">        
        <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar Marca" onclick="btnBuscar_Click" ImageUrl="~/img/find-logo.png" cssclass="btn-buscar-filtro-abm"/>
        <asp:textbox ID="txtBuscar" runat="server" ToolTip="Buscador" placeholder="Buscar..." aria-label="Marca" cssclass="txt-campo-filtro-abm-producto"></asp:TextBox>
        
        <button id="btnAgregar" ToolTip="Agregar nueva Marca" class="btnAddNewBrand" style="vertical-align: middle !important;">Agregar Nueva</button>
    </div>
    <br /><br />

    <table>

        <tr>
            <td>
                <asp:TextBox id="txtIdMarca" runat="server" Visible="false" TooTip="ID" placeholder="ID" onkeypress="javascript:return solonumeros(event)" />
            </td>
            <td>
                <asp:TextBox id="txtMarca" runat="server" ToolTip="Descripción" placeholder="Descripción" onkeypress="javascript:return sololetras(event)" Style="width: 200px; vertical-align: middle;" />
            </td>
            <td align="center">
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Marca" onclientclick="return confirm('¿Confirma el cambio?');" onclick="btnUpdate_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="vertical-align: bottom !important;" />
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar Marca" onclientclick="return confirm('¿Seguro que desea eliminar el Tipo de Producto?');" onclick="btnDelete_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: bottom !important;" />
            </td>
        </tr>

    </table>

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

		    <table style="width:80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                <tr align="center">
                    <td align="center" style="padding: .5rem;">
                        <asp:Button ID="btnCerraPopup" Text="X" runat="server" ToolTip="Cancelar" cssclass="btn-cerrar-popup" onclick="btnCerraPopup_Click" />
                    </td>
                </tr>
                
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtMarca2" runat="server" ToolTip="Descripción" placeholder="Descripción" Width="200px" onkeypress="javascript:return sololetras(event)" />
                    </td>
                </tr>
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:Button ID="imgBtnAgregarMarca" Text="Agregar" runat="server" ToolTip="Agregar Marca" onclientclick="return confirm('¿Confirma que desea agregar el nuevo producto?');" onclick="btnAgregar_Click" cssclass="img-btn-add-producto" />
                    </td>
                </tr>

		    </table>

        </div>

    </div>

    <br />

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-marc-prod" OnClick="btnExportExcel_Click" />

    <br /><br /><br />

    <center Style="padding: .15rem;">
        <asp:GridView ID="dgvMarcasProducto" runat="server" AllowSorting="True" OnSorting="dgvMarcasProducto_Sorting" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="5" CssClass="dgv-abm-prod" AllowPaging="True" OnPageIndexChanging="dgvMarcasProducto_PageIndexChanging">
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
        <asp:SqlDataSource ID="MarcasProducto" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [MarcasProducto] ORDER BY [ID] ASC"></asp:SqlDataSource>
    </center>

    <script>
        var btnAbrirPopup = document.getElementById('btnAgregar'),
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
