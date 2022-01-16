<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMTurnos.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMTurnos" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h1-abm">ABM - Turnos</h1>

    <br />

    <asp:DropDownList ID="ddlFiltroBuscar" runat="server" AppendDataBoundItems="true" Style="height: 31px; vertical-align: top;">
        <asp:ListItem Value="0">Buscar por...</asp:ListItem>
        <asp:ListItem Value="ID">ID</asp:ListItem>
        <asp:ListItem Value="Patente">Patente</asp:ListItem>
        <asp:ListItem Value="CUIT_DNI">CUIT / DNI</asp:ListItem>
        <asp:ListItem Value="Fecha">Fecha</asp:ListItem>
    </asp:DropDownList>

    <asp:TextBox ID="txtBuscarFiltro" runat="server" PlaceHolder="Texto buscado..." />
    
    <asp:ImageButton ID="imgBtnBuscarFiltro" runat="server" ToolTip="Buscar Turno" OnClick="imgBtnBuscarFiltro_Click" ImageUrl="~/img/find-logo.png" cssclass="btn-buscar-filtro-abm" />

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    <asp:Label Text="Mostrar turnos..." runat="server" Style="font-size: 16px;" />
    <asp:DropDownList ID="ddlMostrar" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Height="30" OnSelectedIndexChanged="ddlMostrar_SelectedIndexChanged" >
        <asp:ListItem Value="0">Todos</asp:ListItem>
        <asp:ListItem Value="Hoy">De hoy</asp:ListItem>
        <asp:ListItem Value="Completados">Completados</asp:ListItem>
        <asp:ListItem Value="Futuros">Futuros</asp:ListItem>
        <asp:ListItem Value="Pendientes">Pendientes</asp:ListItem>
    </asp:DropDownList>

    <asp:TextBox ID="txtBorrarTurnosPorPatente" runat="server" tooltip="Ingresar patente" placeholder="Patente..." />
    <asp:Button ID="btnBorrarTurnosPorPatente" runat="server" Text="Borrar Turnos por Patente" onclick="btnBorrarTurnosPorPatente_Click" />

    <br /><br />

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-turnos" OnClick="btnExportExcel_Click" />

    <Button ID="btnEditar" style="border-radius: 100px; background-color: transparent; border-color: transparent;" >
        
        <img src="img/edit-logo.png" alt="..." class="img-btn-edit-abm" />
        
    </Button>

    <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Cancelar Turno" onclientclick="return confirm('¿Seguro que desea cancelar el Turno?');" onclick="btnDelete_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: middle;" />
    
    <asp:Button ID="btnCompletarTurno" runat="server" Text="Completar" ToolTip="Completar Turno" onclick="btnCompletarTurno_Click" Style="vertical-align: middle;" />

    <asp:DropDownList ID="ddlEmpleados" runat="server" AppendDataBoundItems="true" Style="height: 31px; vertical-align: middle;" >
        <asp:ListItem Value="0" >Seleccione Empleado...</asp:ListItem>
    </asp:DropDownList>
    
    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

		    <table style="width:80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                <tr align="center">
                    <td></td>
                    <td align="right" style="padding: .5rem;">
                        <asp:Button ID="btnCerraPopup" Text="X" runat="server" ToolTip="Cancelar" cssclass="btn-cerrar-popup" />
                    </td>
                    <td></td>
                </tr>
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtFecha" runat="server" ToolTip="Fecha" placeholder="Fecha" Width="110" />
                        &nbsp;
                        <asp:DropDownList ID="ddlHoraTurno" runat="server" AppendDataBoundItems="true" Width="76" style="box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2); border-radius: 4px;" >
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td style="padding: .5rem;">
                        <asp:DropDownList ID="ddlTiposServicio" runat="server" AppendDataBoundItems="true" Width="200" style="box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2); border-radius: 4px;" >
                            <asp:ListItem Value="0">Servicio a realizar</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtCuitDni" runat="server" ToolTip="CUIT / DNI" placeholder="CUIT / DNI" onkeypress="javascript:return solonumeros(event)" Width="200" />
                    </td>
                    <td></td>
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtPatente" runat="server" ToolTip="Patente" placeholder="Patente" Width="200" />
                    </td>
                </tr>
                <tr align="center" width="400">
                    <td></td>
                    <td style="padding: .5rem;" align="center">
                        <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Turno" onclientclick="return confirm('¿Confirma el cambio?');" onclick="btnUpdate_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="width: 40px;" />
                    </td>
                    <td></td>
                </tr>

		    </table>

        </div>

    </div>

    <br />

    <asp:Button ID="btnExportTurnosGeneral" runat="server" Text="Exportar histórico Turnos a Excel" cssclass="btn-export-excel-historico-turnos" OnClick="btnExportHistoricoExcel_Click" />

    <center>
        <asp:GridView ID="dgvHistoricoTurnos" visible="false" runat="server" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" CssClass="dgv-abm-prod" DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="dgvHistoricoTurnos_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="ID Tabla Turnos" HeaderText="ID Tabla Turnos" SortExpression="ID Tabla Turnos" />
                <asp:BoundField DataField="Dia" HeaderText="Día" SortExpression="Dia" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Hora" HeaderText="Hora" ReadOnly="True" SortExpression="Hora" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="Cliente" />
                <asp:BoundField DataField="Vehiculo" HeaderText="Vehículo" SortExpression="Vehiculo" />
                <asp:BoundField DataField="Servicio" HeaderText="Servicio" SortExpression="Servicio" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="FechaCambio" HeaderText="Fecha último cambio" SortExpression="FechaCambio" />
                <asp:BoundField DataField="HoraCambio" HeaderText="Hora último cambio" SortExpression="HoraCambio" />
                
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
        
        <asp:SqlDataSource ID="ExportTurnosGeneral" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportTurnosGeneral] ORDER BY [Fecha] DESC, [Hora] DESC"></asp:SqlDataSource>
        
    </center>

    <br /><br />

    <center>
        <asp:GridView ID="dgvTurnos" runat="server" AllowSorting="True" OnSorting="dgvTurnos_Sorting" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="2" CssClass="dgv-abm-prod" DataKeyNames="ID">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Dia" HeaderText="Día" SortExpression="Dia" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Hora" HeaderText="Hora" ReadOnly="True" SortExpression="Hora" />
                <asp:BoundField DataField="TipoServicio" HeaderText="Servicio a realizar" ReadOnly="True" SortExpression="TipoServicio" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" ReadOnly="True" SortExpression="Cliente" />
                <asp:BoundField DataField="CUIT_DNI" HeaderText="CUIT / DNI" ReadOnly="True" SortExpression="CUIT_DNI" />
                <asp:BoundField DataField="Patente" HeaderText="Patente" ReadOnly="True" SortExpression="Patente" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" SortExpression="Estado" />
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
        
        <asp:SqlDataSource ID="ExportTurnos" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportTurnos]"></asp:SqlDataSource>
        
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
        var btnAbrirPopup = document.getElementById('btnEditar'),
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
