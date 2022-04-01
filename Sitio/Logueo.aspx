<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 400px;
            text-align: right;
        }
        .auto-style2 {
            width: 442px;
            text-align: center;
        }
        .auto-style3 {
            text-align: center;
            background-color: #009933;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                    <td class="auto-style3" colspan="3">
                        <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="50pt" ForeColor="White" Text="Logueo"></asp:Label>
                </td>
                </tr>
            <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            <tr>
                <div style="margin-left: 40px">
                    <td class="auto-style1">
                        <asp:Label ID="lblUsuLg" runat="server" Text="Usuario:"></asp:Label>
                </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtUsu" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnIngresar" runat="server" OnClick="btnIngresar_Click" Text="Ingresar" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblPass" runat="server" Text="Password:"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPass" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>
    
    </div>
    </form>
</body>
</html>
