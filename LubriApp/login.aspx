<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.login" %>
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
                <label for="exampleInputEmail1">Usuario</label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Usuario" />
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="*****" type="password" />                
                <small id="emailHelp" class="form-text text-muted">Su contraseña no será compartida con nadie.</small>
            </div>
            <div class="form-group form-check">
                <input type="checkbox" class="form-check-input" id="exampleCheck1">
                <label class="form-check-label" for="exampleCheck1">Recordar Usuario</label>
            </div>
            
            <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-primary" OnClick="btnIngresar_Click" Text="Iniciar Sesión" />
                            
        </div>
    </center>

</asp:Content>
