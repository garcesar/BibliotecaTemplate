<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ABMPublicacionDigital.aspx.cs" Inherits="ABMPublicacionDigital" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body bgcolor="antiquewhite">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <b>Mantenimiento Publicación Digital<br />
            <br />
        </b>
        <table >
            <tr>
                <td style="width: 94px; height: 21px">
                    ISBN:</td>
                <td style="width: 100px; height: 21px">
                    <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox></td>
                <td style="width: 100px; height: 21px">
                    <asp:Button ID="btnBuscar" runat="server" Font-Bold="True" 
                        Text="Buscar" OnClick="btnBuscar_Click" /></td>
            </tr>
            <tr>
                <td style="width: 94px">
                    Título:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 94px">
                    Formato:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtFormato" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 94px">
                    Protegida</td>
                <td align="center" style="width: 100px">
                    <asp:CheckBox ID="chkProtegida" runat="server" /></td>
                <td align="center" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnAgregar" runat="server" Font-Bold="True" OnClick="btnAgregar_Click"
                        Text="Agregar!" />
                    <asp:Button ID="BtnModificar" runat="server" Font-Bold="True" 
                        Text="Modificar" OnClick="BtnModificar_Click" Enabled="False" />
                    <asp:Button ID="btnBaja" runat="server" Font-Bold="True" 
                        Text="Eliminar" OnClick="btnBaja_Click" Enabled="False" /></td>
                <td align="center" colspan="1">
                    <asp:Button ID="btnLimpiar" runat="server" Font-Bold="True" OnClick="btnLimpiar_Click"
                        Text="Limpiar" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblError" runat="server"></asp:Label>&nbsp;<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Default.aspx">Volver</asp:LinkButton>
    
    </div>
    </form>
</body>
</html>
