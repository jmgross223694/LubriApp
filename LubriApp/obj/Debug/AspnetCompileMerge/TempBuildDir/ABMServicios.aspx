<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMServicios.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMServicios" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h1-abm">ABM - Servicios</h1>

    <br />

    <asp:DropDownList ID="ddlFiltroBuscar" runat="server" AppendDataBoundItems="true" Style="height: 31px; vertical-align: top;">
        <asp:ListItem Value="0">Buscar por...</asp:ListItem>
        <asp:ListItem Value="ID">ID</asp:ListItem>
        <asp:ListItem Value="Patente">Patente</asp:ListItem>
        <asp:ListItem Value="CUIT_DNI">CUIT / DNI</asp:ListItem>
        <asp:ListItem Value="Fecha">Fecha</asp:ListItem>
    </asp:DropDownList>

    <asp:TextBox ID="txtBuscarFiltro" runat="server" PlaceHolder="Texto buscado..." />
    
    <asp:ImageButton ID="imgBtnBuscarFiltro" runat="server" ToolTip="Buscar Servicio" OnClick="imgBtnBuscarFiltro_Click" ImageUrl="~/img/find-logo.png" cssclass="btn-buscar-filtro-abm" />
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:Label Text="Mostrar..." runat="server" Style="font-size: 16px;" />
    <asp:DropDownList ID="ddlMostrar" runat="server" AppendDataBoundItems="true" Height="31" AutoPostBack="True" OnSelectedIndexChanged="ddlMostrar_SelectedIndexChanged" >
        <asp:ListItem Value="Todos">Todos</asp:ListItem>
        <asp:ListItem Value="Hoy">De hoy</asp:ListItem>
        <asp:ListItem Value="Completados">Completados</asp:ListItem>
        <asp:ListItem Value="Futuros">Futuros</asp:ListItem>
        <asp:ListItem Value="Pendientes">Pendientes</asp:ListItem>
    </asp:DropDownList>

    <asp:TextBox ID="txtBorrarServiciosPorPatente" runat="server" tooltip="Ingresar patente" placeholder="Patente..." />
    <asp:Button ID="btnBorrarServiciosPorPatente" runat="server" Text="Borrar Servicios por Patente" onclick="btnBorrarServiciosPorPatente_Click" />

    <br /><br />

    <button ID="btnAgregarServicio" ToolTip="Agregar Servicio" width="80px" Class="btn-agregar-servicio" >Agregar servicio</button>

    <br />

    <asp:Label ID="lblTabla" runat="server" Font-Size="8" >
        <span style="padding: .5rem;">
        Para editar / eliminar un servicio, se debe buscar por ID.
        </span>
    </asp:Label>

    <table BorderStyle="Inset" style="width:60%; border-color: black; background-color: rgb(255 255 255);">

        <tr>
            <td style="padding: .5rem;" align="center">
                <asp:TextBox ID="txtFecha" runat="server" Tooltip="Fecha" placeholder="Fecha" Width="150" Style="text-align: center;" />
                <asp:TextBox ID="txtHora" runat="server" Tooltip="Hora" placeholder="Hora" Width="50" Style="text-align: center;" />
            </td>
            <td style="padding: .5rem;" align="center">
                <asp:TextBox ID="txtPatente" runat="server" Tooltip="Patente" placeholder="Patente" Width="200" Style="text-align: center;" />
            </td>
            <td style="padding: .5rem;" align="center">
                <asp:DropDownList ID="ddlTiposServicio" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt" >
                    <asp:ListItem Value="0">Tipo de servicio</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td rowspan="2" style="padding: .5rem;" align="center">
                <asp:TextBox ID="txtComentarios" runat="server" Tooltip="Comentarios" placeholder="Comentarios" Width="205px" Height="76px" Font-Size="8" TextMode="MultiLine" Style="resize:none; " /> <%-- Multiline --%>
            </td>
            <td style="padding: .5rem;" align="center">
                 <asp:DropDownList ID="ddlClientes" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt" >
                    <asp:ListItem Value="0">Cliente</asp:ListItem> <%-- Ordenado por nombre ascendente --%>
                </asp:DropDownList>
            </td>
            <td style="padding: .5rem;" align="center" >
                <asp:DropDownList ID="ddlEmpleados" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt" >
                    <asp:ListItem Value="0">Empleado</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="padding: .5rem;" align="center" width="300">
                <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Modificar servicio" onclientclick="return confirm('¿Confirma el cambio?');" OnClick="btnUpdate_Click" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" Style="vertical-align: bottom !important;" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar servicio" onclientclick="return confirm('¿Seguro que desea eliminar el Servicio?');" OnClick="btnDelete_Click" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" Style="vertical-align: bottom !important;" />
            </td>
            <td style="padding: .5rem; vertical-align: baseline;" align="center">
                <asp:DropDownList ID="ddlEstado" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt" >
                    <asp:ListItem Value="0">Estado</asp:ListItem>
                    <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                    <asp:ListItem Value="Completado">Completado</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>

    </table>

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

            <table style="width: 80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                <tr>
                    <td style="padding: .5rem;" align="center">
                        <asp:TextBox ID="txtFecha2" runat="server" Type="date" ToolTip="Fecha" placeholder="Fecha" Width="150" Style="text-align: center;" />
                        <asp:TextBox ID="txtHora2" runat="server" ToolTip="Hora" placeholder="Hora" Width="50" Style="text-align: center;" />
                    </td>
                    <td style="padding: .5rem;" align="center">
                        <asp:TextBox ID="txtPatente2" runat="server" ToolTip="Patente" placeholder="Patente" Width="200" Height="30" Font-Size="9pt" Style="text-align: center;" />
                    </td>
                    <td style="padding: .5rem;" align="center">
                        <asp:DropDownList ID="ddlTiposServicio2" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt">
                            <asp:ListItem Value="0">Tipo de servicio</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="padding: .5rem;" align="center">
                        <asp:TextBox ID="txtComentarios2" runat="server" ToolTip="Comentarios" placeholder="Comentarios" Width="205px" Height="76px" Font-Size="8" TextMode="MultiLine" Style="resize: none;" />
                    </td>
                    <td style="padding: .5rem;" align="center">
                        <asp:DropDownList ID="ddlClientes2" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt">
                            <asp:ListItem Value="0">Cliente</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="padding: .5rem;" align="center">
                        <asp:DropDownList ID="ddlEmpleados2" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt">
                            <asp:ListItem Value="0">Empleado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="padding: .5rem;" align="center" width="300">
                        <asp:ImageButton ID="imgBtnCerrarPopup" runat="server" ToolTip="Cancelar" OnClick="imgBtnCerrarPopup_Click" ImageUrl="~/img/cancel-logo.png" CssClass="img-btn-del-abm" Style="vertical-align: middle;" />
                        <asp:Button ID="btnAgregar" runat="server" ToolTip="Confirmar" Text="Confirmar" OnClientClick="return confirm('¿Confirma agregar el servicio?');" OnClick="btnAgregar_Click" CssClass="btn-agregar-servicio" />
                    </td>
                    <td style="padding: .5rem;" align="center" width="300">
                        <asp:DropDownList ID="ddlEstado2" runat="server" AppendDataBoundItems="true" Width="200" Height="30" Font-Size="9pt">
                            <asp:ListItem Value="0">Estado</asp:ListItem>
                            <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                            <asp:ListItem Value="Completado">Completado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

        </table>

    </div>

    </div>

    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-servicios" OnClick="btnExportExcel_Click" />

    <asp:Button ID="btnExportHistoricoServicios" runat="server" Text="Exportar histórico Servicios a Excel" cssclass="btn-export-excel-historico-turnos" OnClick="btnExportHistoricoServicios_Click" />

    <center>
        <asp:GridView ID="dgvHistoricoServicios" visible="False" runat="server" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" CssClass="dgv-abm-prod" DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="dgvHistoricoServicios_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="IdOriginal" HeaderText="ID Tabla Servicios" SortExpression="IdOriginal" />
                <asp:BoundField DataField="FechaRealizacion" HeaderText="Fecha de Realización" SortExpression="FechaRealizacion" />
                <asp:BoundField DataField="IdVehiculo" HeaderText="ID Vehículo" ReadOnly="True" SortExpression="IdVehiculo" />
                <asp:BoundField DataField="Patente" HeaderText="Patente" SortExpression="Patente" />
                <asp:BoundField DataField="IdTipo" HeaderText="ID Tipo de Servicio" SortExpression="IdTipo" />
                <asp:BoundField DataField="TipoServicio" HeaderText="Tipo de Servicio" ReadOnly="True" SortExpression="TipoServicio" />
                <asp:BoundField DataField="Comentarios" HeaderText="Comentarios" SortExpression="Comentarios" />
                <asp:BoundField DataField="IdCliente" HeaderText="ID Cliente" SortExpression="IdCliente" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" ReadOnly="True" SortExpression="Cliente" />
                <asp:BoundField DataField="IdEmpleado" HeaderText="ID Empleado" SortExpression="IdEmpleado" />
                <asp:BoundField DataField="Empleado" HeaderText="Empleado" ReadOnly="True" SortExpression="Empleado" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                <asp:BoundField DataField="FechaModificado" HeaderText="Fecha última modificación" ReadOnly="True" SortExpression="FechaModificado" />
                <asp:BoundField DataField="HoraModificado" HeaderText="Hora última modificación" ReadOnly="True" SortExpression="HoraModificado" />
                
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
        
        <asp:SqlDataSource ID="ExportHistoricoServicios" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportHistoricoServicios] ORDER BY [FechaModificado] DESC, [HoraModificado] DESC, [ID]"></asp:SqlDataSource>
        
    </center>

    <center>
        <asp:GridView ID="dgvServicios" runat="server" AllowSorting="True" OnSorting="dgvServicios_Sorting" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="1" CssClass="dgv-abm-servicios" AllowCustomPaging="True">
            <AlternatingRowStyle BackColor="White" />
            
            <Columns>
                
                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="TiposServicio" HeaderText="Tipo de Servicio" ReadOnly="True" SortExpression="TiposServicio" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" ReadOnly="True" SortExpression="Fecha" />
                <asp:BoundField DataField="Hora" HeaderText="Hora" ReadOnly="True" SortExpression="Hora" />
                <asp:BoundField DataField="Patente" HeaderText="Patente" SortExpression="Patente" />
                <asp:BoundField DataField="CUIT_DNI" HeaderText="CUIT / DNI" SortExpression="CUIT_DNI" />
                <asp:BoundField DataField="Cliente" HeaderText="Cliente" ReadOnly="True" SortExpression="Cliente" />
                <asp:BoundField DataField="Empleado" HeaderText="Empleado" ReadOnly="True" SortExpression="Empleado" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                
            </Columns>
            
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Font-Italic="False" Font-Strikeout="False" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        
        <asp:SqlDataSource ID="ExportServicios" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportServicios] ORDER BY [Fecha] DESC, [Hora] DESC"></asp:SqlDataSource>
        
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
        var btnAbrirPopup = document.getElementById('btnAgregarServicio'),
            overlay = document.getElementById('overlay'),
            popup = document.getElementById('popup'),
            btnCerrarPopup = document.getElementById('imgBtnCerrarPopup');

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
