<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registroVehiculo.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.registroVehiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

    <div id="overlay" class="overlay active" align="center">

        <div id="popup" class="popup popup-estilo">

            <div align="center">
                <h5 class="ttl-registro">Registro de vehículo</h5>
            </div>
            <div class="form-cliente">
                <center>
                    <br />
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
                    <asp:Button ID="btnCancelarRegistro" Text="X" runat="server" ToolTip="Cancelar Registro" cssclass="btn-cerrar-popup" onclientclick="return confirm('Si cancela ahora, NO se realizará el registro ¿Seguro que desea cancelar?');" OnClick="btnCancelarRegistro_Click" />
                    <asp:Button ID="btnConfirmarRegistro" runat="server" ToolTip="Agregar Vehículo" Text="Agregar Vehículo" width="150px" CssClass="btn-confirmar-turno-1" onclientclick="return confirm('¿Los datos ingresados son correctos?');" onclick="btnConfirmarRegistro_Click" />
                </center>
            </div>

        </div>

    </div>

</asp:Content>
