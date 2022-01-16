<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMProveedores.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.ABMProveedores" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1 class="h1-abm">ABM - Proveedores</h1>

    <br />

    <div class="stl-buscador-productos">        
    <asp:ImageButton id="imgBtnBuscar" runat="server" ToolTip="Buscar Proveedor" ImageUrl="~/img/find-logo.png" Style="vertical-align: middle;" onclick="imgBtnBuscar_Click" cssclass="btn-buscar-filtro-abm" />
    <asp:TextBox ID="txtBuscar" runat="server" ToolTip="Buscador" PlaceHolder="Buscar..." cssclass="txt-campo-filtro-abm-producto" TextMode="Search" ></asp:TextBox>

    <button id="btnPopUpAgregarProveedor" ToolTip="Agregar nuevo Proveedor" class="btnAddNewSupplier">Agregar Nuevo</button>
    </div>
    <br/><br />

    <center>
    <div class="stl-buscador-proveedores">
                <asp:TextBox id="txtCuit" runat="server" ToolTip="CUIT" placeholder="CUIT" onkeypress="javascript:return solonumeros(event)" Style="width: 90%; vertical-align: middle; margin: 3px; max-width: none;" />
            
                <asp:TextBox id="txtRazonSocial" runat="server" ToolTip="Razon Social" placeholder="Razon Social" Style="width: 90%; vertical-align: middle; margin: 3px; max-width: none;" />
            
                           
                <asp:DropDownList ID="ddlEstado" runat="server" ToolTip="Estado" Width="90%" Height="30px" AppendDataBoundItems="true" Style="background-color: white; border-color: black; height: 30px; width: 90%; max-width: none; vertical-align: middle; margin: 3px;">
                    <asp:ListItem Value="0">Estado</asp:ListItem>
                    <asp:ListItem Value="1">Activar</asp:ListItem>
                    <asp:ListItem Value="2">Desactivar</asp:ListItem>
                </asp:DropDownList>
               
                <div  Style="width: 90%; vertical-align: middle; margin: 3px;">
                <asp:ImageButton ID="btnUpdate" runat="server" ToolTip="Editar Proveedor" onclientclick="return confirm('¿Confirma los cambios?');" ImageUrl="~/img/edit-logo.png" cssclass="img-btn-edit-abm" onclick="btnUpdate_Click" Style="vertical-align: bottom !important;" />
                
                <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Eliminar Proveedor" onclientclick="return confirm('¿Seguro que desea eliminar el Proveedor?');" ImageUrl="~/img/del-logo.png" cssclass="img-btn-del-abm" onclick="btnDelete_Click" Style="vertical-align: bottom !important;" />
                </div>
    </div>
    </center>

    <asp:TextBox id="txtId" runat="server" TooTip="ID" placeholder="ID" Visible="false" Style="width: 60px; text-align:center; vertical-align: bottom !important;" />

    <div id="overlay" class="overlay" align="center">

        <div id="popup" class="popup">

		    <table style="width:80%; border: inset; border-color: black; background-color: rgb(255 255 255);">

                <tr align="center">
                    <td align="right" style="padding: .5rem;">
                        <asp:Button ID="btnCerraPopup" Text="X" runat="server" ToolTip="Cancelar" onclick="btnCerraPopup_Click" cssclass="btn-cerrar-popup" />
                    </td>
                </tr>
                
                <tr align="center">
                    <td style="padding: .5rem;">
                        <asp:TextBox id="txtCuit2" runat="server" ToolTip="CUIT" placeholder="CUIT" width="200px" onkeypress="javascript:return solonumeros(event)" />
                    </td>
                </tr>
                <tr align="center">
                    <td style="padding: .5rem;">
                         <asp:TextBox id="txtRazonSocial2" runat="server" ToolTip="Razon Social" placeholder="Razon Social" Style="width: 200px; vertical-align: middle;" />
                    </td>
                </tr>
                <tr align="center">
                    <td align="center" style="padding: .5rem;">
                        <asp:Button ID="imgBtnAgregarProveedor" align="center" Text="Agregar" runat="server" ToolTip="Agregar Proveedor" onclientclick="return confirm('¿Confirma que desea agregar el nuevo proveedor?');" onclick="imgBtnAgregarProveedor_Click" cssclass="img-btn-add-producto" />
                    </td>
                </tr>

		    </table>

        </div>

    </div>

    <br />
    
    <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" cssclass="btn-export-excel btn-export-excel-abm-prov" OnClick="btnExportExcel_Click" />

    <br /><br /><br />

    <center Style="padding: .5rem;">
        <asp:GridView ID="dgvProveedores" runat="server" AllowSorting="True" Onsorting="dgvProveedores_Sorting" AutoGenerateColumns="False" align="center" CellPadding="4" ForeColor="#333333" BackColor="Black" BorderColor="Black" BorderStyle="Inset" BorderWidth="5px" CaptionAlign="Bottom" HorizontalAlign="Center" CssClass="dgv-abm-prod table stl-tbl-proveedores" AllowPaging="True" OnPageIndexChanging="dgvProveedores_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="CUIT" HeaderText="CUIT" SortExpression="CUIT" />
                <asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" SortExpression="RazonSocial" />
                <asp:BoundField DataField="Asignaciones" HeaderText="Asignaciones" SortExpression="Asignaciones" />
                <asp:CheckBoxField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
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
        var btnAbrirPopup = document.getElementById('btnPopUpAgregarProveedor'),
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
