<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMMarcaVehiculo.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMMarcaVehiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h1-abm-prod">ABM - Marca Vehiculos</h1>

    <br />

    <button id="btn-abrir-popup" class="btnAgregarMarca">Agregar Nueva Marca</button>
    <br />

    <center>
        <asp:GridView ID="dgvMarcasVehiculos" runat="server" onrowcommand="dgvMarcasVehiculos_RowCommand" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="2" CssClass="dgv-abm-prod">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                <asp:ButtonField ButtonType="button" Text="Eliminar" commandname="Select" headertext="Eliminar"/>
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
        <asp:SqlDataSource ID="MarcasVehiculos" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [MarcasVehiculo] ORDER BY [ID] ASC"></asp:SqlDataSource>
    </center>

    <div class="overlay " id="overlay">
        <div class="popup " id="popup ">
            <table style="width: 5px ; border: none; border-color: black; background-color: rgb(255 255 255);">
                <tr aling="center">
                    <td>
                        <asp:TextBox ID="txtMarca" runat="server" placeholder="Marca" aria-label="Marca" CssClass="txtbox-abm-prod-ean"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAgregarMarca" OnClick="btnAceptar_Click" runat="server" Text="Agregar" class="btnAgregarMarca" />
                        <asp:Button ID="btnCancelarrPopup" Text="Cancelar" runat="server" ToolTip="Cancelar" CssClass="btnCancelarPopup" />
                    </td>
                </tr>
            </table>
        </div>
    </div>


    <script>
        var btnAbrirPopup = document.getElementById('btn-abrir-popup'),
            overlay = document.getElementById('overlay'),
            popup = document.getElementById('popup'),
            btnCerrarPopup = document.getElementById('btnCancelarPopup');

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
