<%@ Page Title="" Language="C#" MasterPageFile="~/MP_E.master" AutoEventWireup="true" CodeFile="ABMEmpleado.aspx.cs" Inherits="ABMEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style7 {
            text-align: center;
        }
        .auto-style8 {
            width: 358px;
        }
        .auto-style9 {
            text-align: right;
            width: 358px;
        }
        .auto-style11 {
            text-align: center;
            width: 529px;
        }
        .auto-style12 {
            width: 529px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style7" colspan="3">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="25pt" ForeColor="#009933" Text="ABM Empleado"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Usuario:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtUsuario" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Password:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtPass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Nombre:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtNombre" runat="server" OnTextChanged="TextBox3_TextChanged" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Horas:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtHoras" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="100px" OnClick="btnAgregar_Click" />
            </td>
            <td class="auto-style11">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Width="100px" OnClick="btnModificar_Click" />
            </td>
            <td>
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Width="100px" OnClick="btnEliminar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

