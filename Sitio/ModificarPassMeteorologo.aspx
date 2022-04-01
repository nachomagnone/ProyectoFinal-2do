<%@ Page Title="" Language="C#" MasterPageFile="~/MP_M.master" AutoEventWireup="true" CodeFile="ModificarPassMeteorologo.aspx.cs" Inherits="ModificarPassMeteorologo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style6 {
            font-family: "Times New Roman", Times, serif;
            font-size: 25pt;
            color: #009933;
            text-align: center;
        }
        .auto-style7 {
            width: 354px;
        }
        .auto-style8 {
            width: 505px;
        }
        .auto-style9 {
            width: 505px;
            text-align: center;
        }
        .auto-style10 {
            width: 354px;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style6" colspan="3"><strong><em>Modificar Pass de Meteorologo</em></strong></td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style8">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style10">Meteorologo</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtUsulog" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style10">Password</td>
            <td class="auto-style9">
                <asp:TextBox ID="txtPass" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">&nbsp;</td>
            <td class="auto-style9">
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" Width="100px" OnClick="btnModificar_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

