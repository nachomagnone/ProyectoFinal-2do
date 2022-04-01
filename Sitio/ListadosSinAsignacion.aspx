<%@ Page Title="" Language="C#" MasterPageFile="~/MP_E.master" AutoEventWireup="true" CodeFile="ListadosSinAsignacion.aspx.cs" Inherits="ListadosSinAsignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .auto-style7 {
            text-align: center;
        }
        .auto-style8 {
            width: 287px;
        }
        .auto-style12 {
            width: 529px;
        }
        .auto-style9 {
            text-align: center;
            width: 287px;
        }
        .auto-style11 {
            text-align: center;
            width: 529px;
        }
        .auto-style13 {
            width: 529px;
            margin-left: 40px;
            height: 168px;
        }
        .auto-style15 {
            text-align: center;
            width: 287px;
            height: 168px;
        }
        .auto-style16 {
            height: 168px;
        }
        .auto-style17 {
            text-align: center;
            width: 529px;
            height: 19px;
        }
        .auto-style19 {
            height: 19px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style7" colspan="3">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="25pt" ForeColor="#009933" Text="Listados sin Asignacion"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">Filtrar Ciudad por Año:</td>
            <td class="auto-style17">
                Filtrar
                Meteorologo por Año:</td>
            <td class="auto-style19"></td>
        </tr>
        <tr>
            <td class="auto-style9">
                <asp:TextBox ID="txtCiudad" runat="server"  Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style11">
                &nbsp;<asp:TextBox ID="txtMeteorologo" runat="server" Width="200px" TextMode="DateTime"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">
                <asp:Button ID="btnfiltrarCiudad" runat="server" OnClick="btnfiltrarCiudad_Click" Text="Filtrar" Width="90px" />
            </td>
            <td class="auto-style11">
                <asp:Button ID="btnFiltrarMete" runat="server" Text="Filtrar" Width="100px" OnClick="btnFiltrarMete_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style15">
                <asp:GridView ID="grvCiudad" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Height="182px" Width="469px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Codigo_Ciudad" HeaderText="Codigo" />
                        <asp:BoundField DataField="Nombre_Pais" HeaderText="Pais" />
                        <asp:BoundField DataField="Nombre_Ciudad" HeaderText="Ciudad" />
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </td>
            <td class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="auto-style7">
                    <asp:GridView ID="grvMeteorologo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Height="188px" Width="435px">
                        <Columns>
                            <asp:BoundField DataField="UsuLog" HeaderText="Usuario" />
                            <asp:BoundField DataField="Nombre_Completo" HeaderText="Nombre Completo" />
                            <asp:BoundField DataField="Mail" HeaderText="Mail" />
                            <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </div>
            </td>
            <td class="auto-style16"></td>
        </tr>
        <tr>
            <td class="auto-style7" colspan="2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style11">&nbsp;</td>
            <td>
                <asp:Button ID="btnEliminar" runat="server" Text="Limpiar" Width="100px" OnClick="btnEliminar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

