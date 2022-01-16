<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="registroCliente.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.registroCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

    <div id="overlay" class="overlay active" align="center">

        <div id="popup" class="popup popup-estilo">

            <div align="center">
                <h5 class="ttl-registro">Registro de cliente</h5>
            </div>
            <div class="form-cliente">
                <center>
                    <br />
                    <asp:Label ID="lblValidacionCuitDni" runat="server" Text="El CUIT/DNI ya existe!" class="stl-lbl-Validar-Cuit" />
                    <br />
                    <asp:DropDownList ID="ddlIdTipo" runat="server" tooltip="Tipo de cliente" AppendDataBoundItems="true" width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlIdTipo_SelectedIndexChanged" >
                    <asp:ListItem Value="0">Tipo de cliente</asp:ListItem>
                    </asp:DropDownList>
                    <br /><br />
                    <asp:TextBox ID="txtApeNom" runat="server" tooltip="Nombre y Apellido" placeholder="Nombre y Apellido" onkeypress="javascript:return sololetras(event)" width="200px" />
                    <br /><br />
                    <asp:TextBox ID="txtRazonSocial" runat="server" tooltip="Razón Social" placeholder="Razón Social" width="200px" />
                    <br /><br />
                    <asp:TextBox ID="txtCuitDni" runat="server" Type="Number" tooltip="DNI / CUIT" placeholder="DNI / CUIT" onkeypress="javascript:return solonumeros(event)" width="200px" MaxLength="11" />
                    <br /><br />
                    <asp:TextBox ID="txtTelefono" runat="server" Type="Number" tooltip="Teléfono" placeholder="Teléfono" onkeypress="javascript:return solonumeros(event);" width="200px" />
                    <br /><br />
                    <asp:TextBox ID="txtMail" runat="server" tooltip="Mail" placeholder="Mail" width="200px" />
                    <br /><br />
                    <asp:Button ID="btnCancelarRegistro" Text="X" runat="server" ToolTip="Cancelar Registro" cssclass="btn-cerrar-popup" onclientclick="return confirm('¿Seguro que desea cancelar el registro?');" onclick="btnCancelarRegistro_Click" />
                    <asp:Button ID="btnConfirmarRegistro" runat="server" Text="Registrarse" width="150px" CssClass="btn-confirmar-turno-1" onclientclick="return confirm('¿Los datos ingresados son correctos?');" onclick="btnConfirmarRegistro_Click" />
                </center>
            </div>

        </div>

    </div>

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

</asp:Content>
