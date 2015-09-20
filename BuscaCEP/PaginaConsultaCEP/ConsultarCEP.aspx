<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultarCEP.aspx.cs" Inherits="PaginaConsultaCEP.ConsultarCEP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Busca CEP</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="frmCEP" runat="server" >
    <div id="pesquisa">
        <div id="EnderecoCEP">
        Busca por CEP: 
        <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>

        <asp:Button ID="btnConsultarEndereco_por_CEP" runat="server" Text="Consultar CEP" onClick=  "btnConsultarEnderecoPorCEP_Click"/>
        </div>
        <hr />
        <p>Busca por Logradouro: </p>

        

        <div id ="CEPEndereco">
        Informe Estado: <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox><br />
        Informe Cidade: <asp:TextBox ID="txtCidade" runat="server"></asp:TextBox><br />
        Informe Logradouro: <asp:TextBox ID="txtLogradouro" runat="server"></asp:TextBox>

        <asp:Button ID="btnConsultarCEP_por_Endereco" runat="server" Text="Consultar CEP" onClick= "btnConsultaCEPporEndereco_Click"/>    

        </div>

        <hr />
    </div>
        <asp:Label runat="server" ID="msgErro"></asp:Label>
        <div id="resultado">
            <asp:GridView ID="grdResultado" runat="server"></asp:GridView>
        </div>
    </form>
</body>
</html>
