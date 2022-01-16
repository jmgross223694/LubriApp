<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMClientes.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMClientes" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h1-abm">ABM - Clientes</h1>

    <br />

    <asp:ImageButton id="imgBtnBuscar" runat="server" ToolTip="Buscar Cliente" ImageUrl="~/img/find-logo.png" onclick="imgBtnBuscar_Click" Style="vertical-align: middle;" cssclass="btn-buscar-filtro-abm" />
    <asp:TextBox ID="txtBuscar" runat="server" ToolTip="Buscador" PlaceHolder="Buscar..." Style="width: 320px; height: 30px !important; vertical-align: middle;" ></asp:TextBox>

    <asp:Button id="btnAbmVehiculos" runat="server" text="ABM - Vehículos" CssClass="btn-abm-vehiculos" OnClick="btnAbmVehiculos_Click" />

    <button id="btnPopUpAgregarCliente" ToolTip="Agregar nuevo Cliente" class="btnAddNewClient">Agregar Nuevo</button>

    <br /><br />

    <table BorderStyle="Inset" BorderWidth="5px" style="width:60%; border: solid; border-color: black; background-color: rgb(255 255 255);">

        <tr align="center">
            <td></td>
            <td Style="padding: .5rem;">
                <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Cliente" onclientclick="return confirm('¿Confirma las modificaciones?');" onclick="btnUpdate_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="vertical-align: sub;" />
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar Cliente" onclientclick="return confirm('¿Seguro que desea eliminar al cliente?');" onclick="btnDelete_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: sub;" />
            </td>
            <td></td>
        </tr>
        <tr align="center" >
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtCuitDni" runat="server" TextMode="Number" ToolTip="CUIT / DNI" placeholder="CUIT / DNI" Width="200px" MaxLength="11" onkeypress="javascript:return solonumeros(event)" ></asp:TextBox>
            </td>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtRazonSocial" runat="server" ToolTip="Razón Social" placeholder="Razón Social" Width="200px" MaxLength="11" Rows="1" ></asp:TextBox>
            </td>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtApeNom" runat="server" ToolTip="Apellido y Nombre" placeholder="Apellido y Nombre" Width="200px" MaxLength="100" Rows="1" onkeypress="javascript:return sololetras(event)" ></asp:TextBox>
            </td>
        </tr>
                        
        <tr align="center">
            <td Style="padding: .5rem;">
                <asp:DropDownList ID="ddlTiposCliente" runat="server" ToolTip="Tipo de Cliente" Width="200px" Height="30px" AppendDataBoundItems="true" Style="background-color: white; border-color: black;">
                    <asp:ListItem Value="0">Tipo de Cliente</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtFechaAlta" runat="server" ToolTip="Fecha de Alta" placeholder="Fecha de Alta" Width="200px" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>

        <asp:TextBox runat="server" ID="txtID" Visible="false" ></asp:TextBox>

        <tr align="center">
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtMail" runat="server" ToolTip="e-Mail" placeholder="e-Mail" Width="200px"></asp:TextBox>
            </td>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtTelefono" runat="server" ToolTip="Teléfono / Celular" placeholder="Teléfono / Celular" onkeypress="javascript:return solonumeros(event)" Width="200px"></asp:TextBox>
            </td>
            <td Style="padding: .5rem;">
                <asp:TextBox ID="txtCantVehiculos" runat="server" ToolTip="Cantidad de vehículos registrados" placeholder="Vehículos" Width="80px"></asp:TextBox>
                &nbsp;
                <asp:DropDownList ID="ddlEstado" runat="server" ToolTip="Estado" Width="105px" Height="30px" AppendDataBoundItems="true" Style="background-color: white; border-color: black;">
                    <asp:ListItem Value="0">Estado</asp:ListItem>
                    <asp:ListItem Value="1">Activar</asp:ListItem>
                    <asp:ListItem Value="2">Desactivar</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

	</table>

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

		        <table style="width:80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                    <tr align="center" >
                        <td Style="padding: .5rem;">
                            <asp:TextBox ID="txtCuitDni2" runat="server" TextMode="Number" ToolTip="CUIT / DNI" placeholder="CUIT / DNI" Width="200px" MaxLength="11" onkeypress="javascript:return solonumeros(event)" ></asp:TextBox>
                        </td>
                        <td align="center" style="vertical-align: super; padding: .5rem;">
                            <asp:Button ID="btnCerraPopup" Text="X" runat="server" ToolTip="Cancelar" OnClick="btnCerraPopup_Click" cssclass="btn-cerrar-popup" />
                        </td>
                        <td Style="padding: .5rem;">
                            <asp:TextBox ID="txtRazonSocial2" runat="server" ToolTip="Razón Social" placeholder="Razón Social" Width="200px" MaxLength="11" Rows="1" ></asp:TextBox>
                        </td>
                    </tr>
                                    
                    <tr align="center">
                        <td Style="padding: .5rem;">
                            <asp:TextBox ID="txtApeNom2" runat="server" ToolTip="Apellido y Nombre" placeholder="Apellido y Nombre" Width="200px" MaxLength="100" Rows="1" onkeypress="javascript:return sololetras(event)" ></asp:TextBox>
                        </td>
                        <td Style="padding: .5rem;">
                            <asp:TextBox ID="txtMail2" runat="server" ToolTip="e-Mail" placeholder="e-Mail" Width="200px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr align="center">
                        <td Style="padding: .5rem;">
                            <asp:DropDownList ID="ddlTiposCliente2" runat="server" ToolTip="Tipo de Cliente" Width="200px" Height="30px" AppendDataBoundItems="true" Style="background-color: white; border-color: black;">
                                <asp:ListItem Value="0">Tipo de Cliente</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" Style="padding: .5rem;">
                            <asp:Button ID="imgBtnAgregarCliente" Text="Agregar" runat="server" ToolTip="Agregar Cliente" onclientclick="return confirm('¿Confirma que desea agregar al nuevo cliente?');" onclick="imgBtnAgregarCliente_Click" cssclass="img-btn-add-producto" />
                        </td>
                        <td Style="padding: .5rem;">
                            <asp:TextBox ID="txtTelefono2" runat="server" ToolTip="Teléfono / Celular" placeholder="Teléfono / Celular" onkeypress="javascript:return solonumeros(event)" Width="200px"></asp:TextBox>
                        </td>
                    </tr>

		        </table>

                </div>

    </div>

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-clientes" OnClick="btnExportExcel_Click" />

    <center>
        <asp:GridView ID="dgvClientes" runat="server" AllowSorting="True" OnSorting="dgvClientes_Sorting" AutoGenerateColumns="False" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" CssClass="dgv-abm-prod" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="CUITDNI" HeaderText="CUIT / DNI" SortExpression="CUITDNI" />
                <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" />
                <asp:BoundField DataField="ApeNom" HeaderText="Apellido y Nombre" SortExpression="ApeNom" />
                <asp:BoundField DataField="TipoCliente" HeaderText="Tipo de Cliente" SortExpression="TipoCliente" />
                <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" SortExpression="FechaAlta" />
                <asp:BoundField DataField="Mail" HeaderText="Mail" SortExpression="Mail" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                <asp:BoundField DataField="TotalVehiculosRegistrados" HeaderText="Cant. Vehículos" SortExpression="TotalVehiculosRegistrados" />
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
        <asp:SqlDataSource ID="ExportClientesDB" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportClientes]"></asp:SqlDataSource>
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
        var btnAbrirPopup = document.getElementById('btnPopUpAgregarCliente'),
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
