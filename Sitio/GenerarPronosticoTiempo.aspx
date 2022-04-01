<%@ Page Title="" Language="C#" MasterPageFile="~/MP_M.master" AutoEventWireup="true" CodeFile="GenerarPronosticoTiempo.aspx.cs" Inherits="GenerarPronosticoTiempo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style6 {
            width: 100%;
            height: 405px;
        }
        .auto-style12 {
            height: 56px;
            width: 425px;
        }
        .auto-style13 {
            height: 22px;
            font-family: "Times New Roman";
            font-style: italic;
            font-weight: bold;
            font-size: 25pt;
            color: #009933;
            text-align: center;
        }
        .auto-style16 {
            height: 56px;
            text-align: center;
        }
        .auto-style19 {
            width: 323px;
            height: 8px;
            text-align: right;
        }
        .auto-style20 {
            width: 341px;
            height: 8px;
            text-align: right;
        }
        .auto-style24 {
            width: 257px;
            height: 32px;
        }
        .auto-style25 {
            width: 341px;
            height: 32px;
        }
        .auto-style26 {
            height: 32px;
            width: 425px;
        }
        .auto-style29 {
            width: 285px;
            height: 31px;
            text-align: center;
        }
        .auto-style30 {
            width: 323px;
            height: 32px;
        }
        .auto-style31 {
            width: 323px;
            height: 56px;
        }
        .auto-style34 {
            width: 257px;
            height: 8px;
            text-align: center;
        }
        .auto-style35 {
            height: 8px;
            width: 425px;
        }
        .auto-style36 {
            height: 56px;
            text-align: center;
            font-family: "times New Roman", Times, serif;
            font-size: x-large;
            color: #009933;
        }
        .auto-style38 {
            width: 257px;
            height: 27px;
            text-align: center;
        }
        .auto-style39 {
            width: 323px;
            height: 27px;
            text-align: right;
        }
        .auto-style40 {
            height: 27px;
            text-align: center;
        }
        .auto-style41 {
            height: 27px;
            width: 425px;
        }
        .auto-style42 {
            width: 323px;
            height: 31px;
            text-align: right;
        }
        .auto-style44 {
            width: 207px;
            height: 31px;
            text-align: center;
        }
        .auto-style45 {
            width: 257px;
            height: 31px;
            text-align: center;
        }
        .auto-style50 {
            width: 323px;
            height: 27px;
        }
        .auto-style52 {
            width: 341px;
            height: 27px;
        }
        .auto-style53 {
            width: 257px;
            height: 22px;
            text-align: center;
        }
        .auto-style54 {
            width: 323px;
            height: 22px;
        }
        .auto-style55 {
            width: 341px;
            height: 22px;
        }
        .auto-style56 {
            height: 22px;
            width: 425px;
        }
        .auto-style57 {
            height: 31px;
            width: 425px;
        }
        .auto-style58 {
            width: 323px;
            height: 29px;
        }
        .auto-style59 {
            height: 29px;
            text-align: center;
        }
        .auto-style60 {
            height: 29px;
            width: 425px;
        }
        .auto-style61 {
            height: 32px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" class="auto-style6">
        <tr>
            <td class="auto-style13" colspan="5">Alta de Pronosticos</td>
        </tr>
        <tr>
            <td class="auto-style30"></td>
            <td class="auto-style24"></td>
            <td class="auto-style25" colspan="2"></td>
            <td class="auto-style26"></td>
        </tr>
        <tr>
            <td class="auto-style19">Ciudad</td>
            <td class="auto-style34">
                <asp:DropDownList ID="ddlCiudades" runat="server" Height="51px" Width="195px">
                </asp:DropDownList>
                <br />
            </td>
            <td class="auto-style20" colspan="2">Fecha</td>
            <td class="auto-style35"><asp:TextBox ID="txtFecha" runat="server" Height="16px" TextMode="Date" Width="187px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style19">&nbsp;</td>
            <td class="auto-style34">&nbsp;</td>
            <td class="auto-style20" colspan="2">&nbsp;</td>
            <td class="auto-style35">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style36" colspan="5"><strong><em>Pronosticos por Hora</em></strong></td>
        </tr>
        <tr>
            <td class="auto-style39">Hora&nbsp;</td>
            <td class="auto-style38">
                <asp:TextBox ID="txbHora" runat="server" Height="16px" TextMode="Number" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style40" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Tipo de cielo<br />
            </td>
            <td class="auto-style41">
                <asp:DropDownList ID="ddlCielo" runat="server" Height="16px" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style42">Temperatura</td>
            <td class="auto-style45">Minima</td>
            <td class="auto-style44">
                <asp:TextBox ID="txbTempMin" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style29">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Maxima<br />
            </td>
            <td class="auto-style57">
                <asp:TextBox ID="txbTempMax" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style54"></td>
            <td class="auto-style53">Prbabilidad de tormenta</td>
            <td class="auto-style55" colspan="2">
                <asp:TextBox ID="txbTormenta" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style56"></td>
        </tr>
        <tr>
            <td class="auto-style50"></td>
            <td class="auto-style38">Probabilidad de lluvia</td>
            <td class="auto-style52" colspan="2">
                <asp:TextBox ID="txbLluvia" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style41"></td>
        </tr>
        <tr>
            <td class="auto-style50"></td>
            <td class="auto-style38">Velocidad de viento</td>
            <td class="auto-style52" colspan="2">
                <asp:TextBox ID="txbViento" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style41"></td>
        </tr>
        <tr>
            <td class="auto-style50">&nbsp;</td>
            <td class="auto-style38">&nbsp;</td>
            <td class="auto-style52" colspan="2">
                &nbsp;</td>
            <td class="auto-style41">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style61" colspan="5">
                <asp:Button ID="btnAltaPHora" runat="server" OnClick="btnAltaPHora_Click" Text="Añadir Pronostico Hora" Width="194px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style31">&nbsp;</td>
            <td class="auto-style16" colspan="3">
                <asp:GridView ID="grvPronosticosHora" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Hora_pronostico" HeaderText="Hora" />
                        <asp:BoundField DataField="Tipo_Cielo" HeaderText="Tipo de Cielo" />
                        <asp:BoundField DataField="Temp_min" HeaderText="Temp. Min" />
                        <asp:BoundField DataField="Temp_Max" HeaderText="Temp. Max" />
                        <asp:BoundField DataField="probabilidad_Lluvias" HeaderText="Prob. Lluvias" />
                        <asp:BoundField DataField="Probabilidad_Tormentas" HeaderText="Prob. Tormentas" />
                        <asp:BoundField DataField="Velocidad_viento" HeaderText="Velocidad  del Viento" />
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
            <td class="auto-style12">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style59" colspan="5">
                <asp:Button ID="btnAltaPronostico" runat="server" Height="26px" OnClick="btnAltaPronostico_Click" Text="Crear Pronostico" Width="100px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style58">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td class="auto-style59" colspan="3">
                &nbsp;</td>
            <td class="auto-style60">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

