<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMVehiculos.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMVehiculos" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h1-abm">ABM - Vehículos</h1>

    <br />

    <asp:ImageButton id="imgBtnBuscar" runat="server" ToolTip="Buscar Cliente" onclick="imgBtnBuscar_Click" ImageUrl="~/img/find-logo.png" Style="vertical-align: middle;" cssclass="btn-buscar-filtro-abm" />
    <asp:TextBox ID="txtBuscar" runat="server" ToolTip="Buscador" PlaceHolder="Buscar..." Style="width: 320px; height: 30px !important; vertical-align: middle;" />
    <asp:DropDownList id="ddlFiltroBuscar" runat="server" AppendDataBoundItems="true" Style="height: 30px; vertical-align: middle;">
        <asp:ListItem Value="Patente">Patente</asp:ListItem>
        <asp:ListItem Value="CUITDNI">CUIT-DNI</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="lblBuscar" runat="server" Text="Para modificar o eliminar, se debe buscar por Patente." Style="font-size: 10px; position: relative; top: -25px; left: -387px;" />
    
    <button ID="btnAgregarVehiculo" ToolTip="Agregar Vehículo" width="80px" Class="btn-agregar-vehiculo-abm" >Agregar vehículo</button>
    <br /><br /><br />

    <table id="tblVehiculos" class="tblVehiculos-style" BorderStyle="Inset" BorderWidth="5px">
    
        <tr>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtPatente2" runat="server" tooltip="Patente" placeholder="Patente" width="200px" MaxLength="11" />
            </td>
            <td Style="padding: .5rem;">
                <asp:DropDownList ID="ddlMarcaVehiculo2" runat="server" tooltip="Marca" AppendDataBoundItems="true" width="200px" Style="height: 30px;" >
                    <asp:ListItem Value="0">Marca</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtModelo2" runat="server" tooltip="Modelo" placeholder="Modelo" width="200px" MaxLength="50" />
            </td>
            <td Style="padding: .5rem;">
                <asp:DropDownList ID="ddlAnioFabricacion2" runat="server" tooltip="Año de fabricación" AppendDataBoundItems="true" width="137px" Style="height: 30px; vertical-align: middle;" >
                <asp:ListItem Value="0">Año fabricación</asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox id="cbEstado" runat="server" Text="Activo" />
            </td>
        </tr>

    </table>

    <asp:ImageButton ID="btnModificar" runat="server" ImageUrl="~/img/edit-logo.png" Tooltip="Modificar Vehículo" CssClass="btn-editar-abm-vehiculos" OnClientClick="return confirm('¿Confirma el/los cambio/s?');" OnClick="btnModificar_Click" />

    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/img/del-logo.png" ToolTip="Eliminar Vehículo" CssClass="btn-eliminar-vehiculo" OnClick="btnEliminar_Click" OnClientClick="return confirm('¿Seguro que desea eliminar el vehículo?');" />

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup popup-estilo">

		        <div align="center">
                    <h5 class="ttl-registro">Registro de vehículo</h5>
                </div>
                <div class="form-cliente">
                    <center>
                        <br />
                        <asp:TextBox ID="txtCuitDni" runat="server" tooltip="CUIT / DNI" placeholder="CUIT / DNI" width="200px" MaxLength="11" />
                        <br /><br />
                        <asp:TextBox ID="txtPatente" runat="server" tooltip="Patente" placeholder="Patente" width="200px" MaxLength="7" />
                        <br /><br />
                        <asp:DropDownList ID="ddlMarcaVehiculo" runat="server" tooltip="Marca" AppendDataBoundItems="true" width="200px" >
                        <asp:ListItem Value="0">Marca</asp:ListItem>
                        </asp:DropDownList>
                        <br /><br />
                        <asp:TextBox ID="txtModelo" runat="server" tooltip="Modelo" placeholder="Modelo" width="200px" MaxLength="50" />
                        <br /><br />
                        <asp:DropDownList ID="ddlAnioFabricacion" runat="server" tooltip="Año de fabricación" AppendDataBoundItems="true" width="200px" >
                        <asp:ListItem Value="0">Año de fabricación</asp:ListItem>
                        </asp:DropDownList>
                        <br /><br />
                        <asp:Button ID="btnCancelar" Text="X" runat="server" ToolTip="Cancelar" cssclass="btn-cerrar-popup" onclick="btnCerraPopup_Click" onclientclick="return confirm('¿Seguro que desea cancelar?');"  />
                        <asp:Button ID="btnConfirmar" runat="server" ToolTip="Agregar Vehículo" Text="Agregar Vehículo" width="150px" CssClass="btn-confirmar-turno-1" onclientclick="return confirm('¿Los datos ingresados son correctos?');" onclick="btnConfirmar_Click" />
                    </center>
                </div>

        </div>

    </div>

    <br />

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-vehiculos" OnClick="btnExportExcel_Click" />

    <center>
        <asp:GridView ID="dgvVehiculos" runat="server" AllowSorting="True" OnSorting="dgvVehiculos_Sorting" AutoGenerateColumns="False" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" CssClass="dgv-abm-vehiculos" DataKeyNames="ID" AllowPaging="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Patente" HeaderText="Patente" SortExpression="Patente" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" ReadOnly="True" SortExpression="Marca" />
                <asp:BoundField DataField="Modelo" HeaderText="Modelo" SortExpression="Modelo" />
                <asp:BoundField DataField="Año de fabricación" HeaderText="Año de fabricación" SortExpression="Año de fabricación" />
                <asp:BoundField DataField="Fecha de alta" HeaderText="Fecha de alta" ReadOnly="True" SortExpression="Fecha de alta" />
                <asp:BoundField DataField="CUITDNI" HeaderText="CUIT / DNI" ReadOnly="True" SortExpression="CUITDNI" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" ReadOnly="True" SortExpression="Cliente" />
                <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:SqlDataSource ID="ExportVehiculos2" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportVehiculos] ORDER BY [CUITDNI], [Patente]"></asp:SqlDataSource>
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
        var btnAbrirPopup = document.getElementById('btnAgregarVehiculo'),
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
