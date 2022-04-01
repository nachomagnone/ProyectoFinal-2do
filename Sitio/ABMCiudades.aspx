<%@ Page Title="" Language="C#" MasterPageFile="~/MP_E.master" AutoEventWireup="true" CodeFile="ABMCiudades.aspx.cs" Inherits="ABMCiudades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style7 {
            text-align: center;
        }
        .auto-style9 {
            text-align: right;
            width: 356px;
        }
        .auto-style11 {
            text-align: center;
            width: 537px;
        }
        .auto-style13 {
            width: 356px;
        }
        .auto-style14 {
            width: 537px;
        }
        .auto-style15 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style7" colspan="3">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="25pt" ForeColor="#009933" Text="ABM Ciudades"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style14">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Codigo de Ciudad:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtCodigo" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style9">Pais:&nbsp;</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtPais" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">Ciudad:</td>
            <td class="auto-style11">
                <asp:TextBox ID="txtCiudad" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style13">&nbsp;</td>
            <td class="auto-style14">&nbsp;</td>
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
                <asp:Button ID="btn" runat="server" OnClick="Button4_Click" Text="Eliminar" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style15" colspan="3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

