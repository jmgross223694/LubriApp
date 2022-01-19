<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="turnos.aspx.cs" Inherits="LubriApp.turnos" Culture="Auto" UICulture="Auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {            
            background-image: url("../img/fondo-2.jpg");
            background-color: #FFFFFF4D !important;
            width: 100%;
            height: 100vh;
            background-size: cover;
            background-position: center;
        }
    </style>
    
    <center>
    <h2 class="ttl-turno">¡Reservá tu turno Online!</h2>
    <table width="50%">
        <tr>
            <td align="center">
                <center><asp:Label ID="lblCalendario" Text="Seleccioná una fecha:" runat="server" Style="font-size: 10px;" /></center>
                <asp:Calendar ID="calendarioTurnos" runat="server" Class="calendario-turnos" BackColor="#FFFFCC" OnSelectionChanged="calendarioTurnos_SelectionChanged" BorderColor="#FFCC66" 
                    BorderWidth="1px" CellPadding="5" CellSpacing="5" DayNameFormat="Shortest" style="box-shadow: 0px 0px 10px 0px; font-weight: 600;" Font-Names="Verdana" 
                    Font-Size="8pt" ForeColor="#663399" ShowGridLines="True" ToolTip="Seleccioná un día" OnDayRender="calendarioTurnos_DayRender" FirstDayOfWeek="Monday">
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                    <OtherMonthDayStyle ForeColor="#CC9966" />
                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" ForeColor="Black" />
                    <SelectorStyle BackColor="#FFCC66" />
                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                    <TodayDayStyle BackColor="#FFCC66" ForeColor="Black" />
                </asp:Calendar>
            </td>
            <td align="center" class="td-tabla-campos-turnos" style="padding-left:0.5rem;">
                <asp:Label ID="lblHora" Text="Horarios disponibles:" runat="server" Style="font-size: 10px;" />
                <br />
                <asp:DropDownList ID="ddlHoraTurno" runat="server" style="width: 150px; box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2); border-radius: 4px; background-color: #F9DFABB3;">
                </asp:DropDownList>
                <br /><br />
                <asp:DropDownList ID="ddlTiposServicio" runat="server" AppendDataBoundItems="true" CssClass="ddl-servicios-turnos" Style="width: 150px; box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2); font-size: 14px; border-radius: 4px; background-color: #F9DFABB3;" >
                    <asp:ListItem Value="0">Servicio a realizar</asp:ListItem>
                </asp:DropDownList>
                <br /><br />

                

                <asp:Label ID="lblCuitDni" Text="Ingrese su CUIT / DNI:" runat="server" Style="font-size: 10px;" />
                <br />
                <asp:TextBox ID="txtCuitDni" runat="server" tooltip="CUIT / DNI" placeholder="CUIT / DNI" 
                    onkeypress="javascript:return solonumeros(event);" CssClass="txt-cuit-dni-turnos" MaxLength="11" />
                <br /><br />

                <asp:DropDownList ID="ddlVehiculos" runat="server" AppendDataBoundItems="true" CssClass="ddl-vehiculos-turnos" >
                    <asp:ListItem Value="0">Vehículos</asp:ListItem>   
                </asp:DropDownList>
                <b>
                    <asp:Button ID="btnAgregarVehículo" runat="server" ToolTip="Agregar Vehículo" 
                        Text="+" onclientclick="return confirm('¿Seguro que desea agregar un nuevo vehículo?')" 
                        onclick="btnAgregarVehículo_Click" cssclass="btn-nuevo-vehiculo" />

                </b>
                <br /><br />
                
            </td>
        </tr>
    </table>
        <br />
        <asp:Button ID="btnBuscarCuitDni" runat="server" Text="Siguiente paso" onclick="btnBuscarCuitDni_Click" cssclass="btn-confirmar-turno-1" style="vertical-align: middle !important;" />
        <br />
        <asp:Label ID="lblRegistro" runat="server" Text="Si es su primera vez, regístrese!" Style="font-size: 10px;" />
        <%if (btnRegistro.Visible == true){ %>
        <br />
        <%} %>
        <asp:Button ID="btnRegistro" runat="server" ToolTip="Registrarse" Text="Resgistrarse" onclick="btnRegistro_Click" cssclass="btn-confirmar-turno-1" style="vertical-align: middle !important;" />
        <%if (btnRegistro.Visible == true){ %>
        <br />
        <%} %>
        <br />
        <asp:Label ID="lblMensaje" Text="-" runat="server" cssclass="lbl-mensaje-turnos" />
    </center>

    <script>
         function solonumeros(e) {
             var key;
             if (window.event) { key = e.keyCode; }
             else if (e.which) { key = e.which; }
             if (key < 48 || key > 57) { return false; }
             return true;
         }
    </script>

</asp:Content>
