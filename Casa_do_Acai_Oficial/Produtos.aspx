﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Produtos.aspx.cs" Inherits="Carrinho" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" href="Style/Style.css">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"
    integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
<title>CasaDoAç@í</title>

<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"
    integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj"
    crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.3/js/bootstrap.bundle.min.js"
    integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx"
    crossorigin="anonymous"></script>
    
    <style type="text/css">
        p {
            font-family: 'CHICKEN Pie';
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">        
        <div class="header">
            <nav class="navbar navbar-expand-lg">
                <a class="navbar-brand" href="Menu.aspx" style="margin-left: 17%;" id="a">CasaDoAç@í</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse">
                    <ul class="navbar-nav" id="textoNav">
                        <li class="nav-item">
                            <a class="nav-link" id="btn1" href="Menu.aspx" onclick="mudarCor('btn1')">INÍCIO <span
                                    class="sr-only">(current)</span></a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="btn2" href="QuemNosSomos.aspx" onclick="mudarCor('btn2')">QUEM SOMOS</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="btn3" href="Contato.aspx" onclick="mudarCor('btn3')">CONTATO</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="btn4" href="Produtos.aspx" onclick="mudarCor('btn4')">CADÁPIO</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="btn5" href="Login.aspx" onclick="mudarCor('btn5')">LOGIN</a>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
        
        <p>Escolha um ou mais produtos !</p>
        <asp:ImageButton ID="imbAcai" runat="server" Height="125px" ImageUrl="~/Imagens/Acai.png" Width="135px" OnClick="imbAcai_Click" />
        <asp:ImageButton ID="imbSacole" runat="server" Height="125px" ImageUrl="~/Imagens/Sacole.png" Width="135px" OnClick="imbSacole_Click" />
        <asp:ImageButton ID="imbGeladinho" runat="server" Height="125px" ImageUrl="~/Imagens/Geladinho.png" Width="135px" OnClick="imbGeladinho_Click" />
        <br />
        <br />
        <asp:ImageButton ID="ImbSorvete" runat="server" Height="125px" ImageUrl="~/Imagens/Sorvete.png" Width="135px" OnClick="ImbSorvete_Click" />
        <asp:ImageButton ID="imbPicole" runat="server" Height="125px" ImageUrl="~/Imagens/Picole.png" Width="135px" OnClick="imbPicole_Click" />
        <asp:ImageButton ID="ImbCremosinho" runat="server" Height="125px" ImageUrl="~/Imagens/Cremosinho.png" Width="135px" OnClick="ImbCremosinho_Click" />
        <br />
        <br />
        <div ID="teste1">
            <asp:GridView ID="gvProduto" runat="server" AutoGenerateColumns="False" DataKeyNames="id_prod" Font-Names="CHICKEN Pie" Font-Size="16pt">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbEscolhaProd" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="nome_prod" HeaderText="Nome" SortExpression="nome_prod" />
                    <asp:BoundField DataField="preco_prod" DataFormatString="{0:c}" HeaderText="Preço" SortExpression="preco_prod" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="White" />
                <HeaderStyle BackColor="#990099" Font-Bold="False" ForeColor="White" />
            </asp:GridView>
            <asp:GridView ID="gvAdicional" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="CHICKEN Pie" Font-Size="16pt" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:RadioButton ID="rbEscolhaAdd" runat="server" AutoPostBack="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="#990099" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="Gray" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <br />
        </div>
        <br />
        <asp:Button ID="btnVoltar" runat="server" text="Voltar" Width="130px" OnClick="btnVoltar_Click" BackColor="#990099" Font-Names="CHICKEN Pie" Font-Size="16px" ForeColor="White" Visible="False"/>
        <asp:Button ID="btnContinuar" runat="server" text="Continuar" Width="130px" OnClick="btnContinuar_Click" BackColor="#990099" Font-Names="CHICKEN Pie" Font-Size="16px" ForeColor="White"/>
        <asp:Button ID="btnAdicionar" runat="server" text="Adicionar" Width="130px" OnClick="btnAdicionar_Click" BackColor="#990099" Font-Names="CHICKEN Pie" Font-Size="16px" ForeColor="White" Visible="False"/>
        <br />
        <asp:Label ID="lblIds" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblAdd" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblQtdIds" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <div ID="teste2">
        </div>
        <br />
        <asp:SqlDataSource ID="DSTipoProduto" runat="server" ConnectionString="<%$ ConnectionStrings:casadoacaiConnectionString %>" ProviderName="<%$ ConnectionStrings:casadoacaiConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tipo_prod"></asp:SqlDataSource>
        &nbsp;<asp:SqlDataSource ID="DSProduto" runat="server" ConnectionString="<%$ ConnectionStrings:casadoacaiConnectionString %>" InsertCommand="INSERT INTO produto(id_prod, nome_prod, id_tipoProd, tam_prod) VALUES (@IDPROD, @NOME, @TIPO, @TAMANHO)" ProviderName="<%$ ConnectionStrings:casadoacaiConnectionString.ProviderName %>" SelectCommand="SELECT * FROM produto">
            <InsertParameters>
                <asp:Parameter Name="IDPROD" />
                <asp:Parameter Name="NOME" />
                <asp:Parameter Name="TIPO" />
                <asp:Parameter Name="TAMANHO" />
            </InsertParameters>
        </asp:SqlDataSource>
        
    </form>    
</body>
</html>
