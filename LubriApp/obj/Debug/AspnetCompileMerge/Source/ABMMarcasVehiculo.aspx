<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMMarcasVehiculo.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMMarcasVehiculo" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <h1 class="h1-abm">ABM - Marcas de vehículos</h1>

    <br />

    <div class="stl-buscador-productos">  
    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar Marca" onclick="btnBuscar_Click" ImageUrl="~/img/find-logo.png" cssclass="btn-buscar-filtro-abm"/>
    <asp:textbox ID="txtBuscar" runat="server" ToolTip="Buscador" placeholder="Buscar..." aria-label="Marca" cssclass="txt-campo-filtro-abm-producto"></asp:TextBox>
    
    <button id="btnAgregar" ToolTip="Agregar nueva Marca" class="btnAddNewBrand">Agregar Nueva</button>
    </div>

    <br /><br />

     <asp:TextBox id="txtIdMarca" runat="server" Visible="false" TooTip="ID" placeholder="ID" onkeypress="javascript:return solonumeros(event)" Style="width: 90%; text-align:center; vertical-align: bottom !important;" />
            
    <div class="stl-menu-marcas-vehiculos">               
        
        <asp:TextBox id="txtMarca" runat="server" ToolTip="Descripción" placeholder="Descripción" onkeypress="javascript:return sololetras(event)" Style="width: 90%; vertical-align: middle; margin: 3px; max-width: none;" />
         
        <div class="stl-submit-delete-marcas">
            <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Marca" onclientclick="return confirm('¿Confirma el cambio?');" onclick="btnUpdate_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="vertical-align: bottom !important;" />
         
            <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar Marca" onclientclick="return confirm('¿Seguro que desea eliminar el Tipo de Producto?');" onclick="btnDelete_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: bottom !important;" />
        </div>

    </div>      

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
                        <asp:TextBox id="txtMarca2" runat="server" ToolTip="Descripción" placeholder="Descripción" onkeypress="javascript:return sololetras(event)" />
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

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-marc-veh" OnClick="btnExportExcel_Click" />

    <br /><br /><br />

    <center>
        <asp:GridView ID="dgvMarcasVehiculo" runat="server" AllowSorting="True" OnSorting="dgvMarcasVehiculo_Sorting" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="5" CssClass="dgv-abm-prod" AllowPaging="True" OnPageIndexChanging="dgvMarcasVehiculo_PageIndexChanging">
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
        <asp:SqlDataSource ID="MarcasVehiculo" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [MarcasVehiculo] ORDER BY [ID] ASC"></asp:SqlDataSource>
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
