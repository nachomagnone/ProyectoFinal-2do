<%@ Page Title="" Language="C#" MasterPageFile="~/MP_E.master" AutoEventWireup="true" CodeFile="ListadoPronosticoTiempo.aspx.cs" Inherits="ListadoPronosticoTiempo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .auto-style7 {
            text-align: center;
        }
        .auto-style8 {
            width: 299px;
        }
        .auto-style12 {
            width: 529px;
        }
        .auto-style9 {
            text-align: right;
            width: 299px;
        }
        .auto-style11 {
            text-align: center;
            width: 529px;
        }
        .auto-style13 {
            width: 529px;
            margin-left: 40px;
        }
        .auto-style14 {
            margin-left: 0px;
        }
        .auto-style15 {
            text-align: center;
            width: 299px;
            height: 84px;
        }
        .auto-style16 {
            text-align: center;
            width: 373px;
            height: 84px;
        }
        .auto-style17 {
            height: 84px;
        }
        .auto-style18 {
            text-align: center;
            width: 299px;
            font-size: small;
        }
        .auto-style19 {
            text-align: center;
            width: 299px;
        }
        .auto-style20 {
            text-align: center;
            width: 440px;
        }
        .auto-style21 {
            text-align: center;
            width: 440px;
            height: 84px;
        }
        .auto-style22 {
            text-align: center;
            width: 373px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style7" colspan="4">
                <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Times New Roman" Font-Size="25pt" ForeColor="#009933" Text="Listado de Pronosticos"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">FILTROS:</td>
            <td class="auto-style12" colspan="2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style18">CIUDAD</td>
            <td class="auto-style22">
                FECHA</td>
            <td class="auto-style20">
                RESUMEN TRABAJO POR METEOROLOGO</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style15">
                <asp:DropDownList ID="ddlCiudades" runat="server" Height="16px" Width="131px">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="BtnFiltrarCiudad" runat="server" OnClick="BtnFiltrarCiudad_Click" Text="Filtrar" Width="83px" />
            </td>
            <td class="auto-style16">
                <asp:TextBox ID="TxtFecha" runat="server" TextMode="Date" Width="148px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="BtnFiltrarFecha" runat="server" OnClick="BtnFiltrarFecha_Click" Text="Filtrar" Width="77px" />
            </td>
            <td class="auto-style21">
                <asp:Button ID="BtnFiltroMeteorologo" runat="server" OnClick="BtnFiltroMeteorologo_Click" Text="Filtrar" Width="98px" />
            </td>
            <td class="auto-style17">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style11" colspan="2">
                <asp:GridView ID="grvMeteorologos" runat="server" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="grvMeteorologos_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField HeaderText="Ir a los Pronosticos" SelectText="Ir." ShowSelectButton="True" />
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
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style11" colspan="2">
                <asp:GridView ID="grvPronosticos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" CssClass="auto-style14" OnSelectedIndexChanged="grvPronosticos_SelectedIndexChanged" Width="611px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="codigo_interno" HeaderText="Codigo" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="Ciudad.nombre_ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="usu.nombre_Completo" HeaderText="Meteorologo" />
                        <asp:CommandField HeaderText="Prosnoticos Hora" SelectText="Ver Horas" ShowSelectButton="True" />
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
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style11" colspan="2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style13" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td class="auto-style11" colspan="2">
                <asp:GridView ID="grvPronosticoHora" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" Width="607px">
                    <Columns>
                        <asp:BoundField DataField="Hora_pronostico" HeaderText="Hora" />
                        <asp:BoundField DataField="Tipo_Cielo" HeaderText="Cielo" />
                        <asp:BoundField DataField="Temp_Max" HeaderText="Temperatura Max." />
                        <asp:BoundField DataField="Temp_min" HeaderText="Temperatura Min." />
                        <asp:BoundField DataField="Probabilidad_Lluvias" HeaderText="Prob. LLuvias" />
                        <asp:BoundField DataField="Probabilidad_Tormentas" HeaderText="Prob. Tormentas" />
                        <asp:BoundField DataField="Velocidad_Viento" HeaderText="Velocidad Viento" />
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
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style9">&nbsp;</td>
            <td class="auto-style11" colspan="2">
                <asp:Button ID="btnEliminar" runat="server" Text="Limpiar" Width="81px" OnClick="btnEliminar_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

