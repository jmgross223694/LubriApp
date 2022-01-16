<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMUsuario.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMUsuario_aspx" %>
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
        <div>               
            <div class="form-group stl-frm-log">
                <label for="exampleInputEmail1">Nuevo Usuario</label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Nuevo Usuario" />
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="*****" type="password" />       
                <label for="exampleInputPassword1">Repita su Contraseña</label>
                <asp:TextBox ID="txtPassword2" runat="server" CssClass="form-control" placeholder="*****" type="password" />
                <small id="emailHelp" class="form-text text-muted">Su contraseña no será compartida con nadie.</small>
            </div>
            <div>
                <label for="exampleInputEmail1">Mail</label>
                <asp:TextBox ID="mail" runat="server" CssClass="form-control" placeholder="Mail" />
            </div>
            <label for="exampleInputPassword1">Tipo de Usuario</label>
            <div>
            <asp:DropDownList ID="ddlTipoUsuario" runat="server" AppendDataBoundItems="true" ToolTip="Tipo de Producto">
                <asp:ListItem Value="0">Tipo Usuario</asp:ListItem>
            </asp:DropDownList>
            </div>
            <br />
            <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrar_Click" CssClass="btn btn-primary" Text="Registrar" />
                         
        </div>
    </center>

    <br /><br /><br />

    <Button ID="btnAbrirPopUp" class="btn-ver-todos-usuarios" >Ver todos</Button>

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

            <div id="divBtnCerrar" align="right">
                <Button ID="btnCerrarPopUp" >X</Button>
            </div>

            <br /><br />

            <div id="divDgvUsuarios">

	            <center style="padding: .5rem;">

                    <asp:GridView ID="dgvUsuarios" runat="server" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" AutoGenerateColumns="False" PageSize="2" CssClass="dgv-abm-prod" DataKeyNames="ID" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="TipoUser" HeaderText="Jerarquía" ReadOnly="True" SortExpression="TipoUser" />
                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                            <asp:BoundField DataField="Pass" HeaderText="Clave" SortExpression="Pass" />
                            <asp:BoundField DataField="Mail" HeaderText="Mail" SortExpression="Mail" />
                            <asp:BoundField DataField="FechaAlta" HeaderText="Fecha de Alta" ReadOnly="True" SortExpression="FechaAlta" />
                            <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
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
                    
                    <asp:SqlDataSource ID="ExportUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:GROSS_LAINO_CHAPARRO_DBConnectionString %>" SelectCommand="SELECT * FROM [ExportUsuarios] ORDER BY [ID]"></asp:SqlDataSource>
                
                </center>
            
            </div>

        </div>

    </div>

    <script>
        var btnAbrirPopup = document.getElementById('btnAbrirPopUp'),
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
