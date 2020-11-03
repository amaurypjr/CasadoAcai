﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Cardapio_Logado : System.Web.UI.Page
{
    Criptografia cripto = new Criptografia("ETEP");

    DataTable listaDescripto = new DataTable();

    string[] adicionais = { "Morango", "Chocolate", "Caramelo", "Menta", "Tutti frutti", "Maracujá" };

    private bool ImbAcaiClicado = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["logado"].ToString() != "Ok")
        {
            Response.Redirect("Cardapio_NaoLogado.aspx");
        }
    }

    protected void imbAcai_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(1);
        gvProduto.Visible = true;
        ImbAcaiClicado = true;
        AcaiClicado();
        gvAdicional.Visible = false;
    }

    protected void imbSacole_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(2);
        gvProduto.Visible = true;
        ImbAcaiClicado = false;
        AcaiClicado();
        EscolherOutroProd();
    }

    protected void imbGeladinho_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(3);
        gvProduto.Visible = true;
        ImbAcaiClicado = false;
        AcaiClicado();
        EscolherOutroProd();
    }

    protected void ImbSorvete_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(4);
        gvProduto.Visible = true;
        ImbAcaiClicado = false;
        AcaiClicado();
        EscolherOutroProd();
    }

    protected void imbPicole_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(5);
        gvProduto.Visible = true;
        ImbAcaiClicado = false;
        AcaiClicado();
        EscolherOutroProd();
    }

    protected void ImbCremosinho_Click(object sender, ImageClickEventArgs e)
    {
        CarregarProduto(6);
        gvProduto.Visible = true;
        ImbAcaiClicado = false;
        AcaiClicado();
        EscolherOutroProd();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        btnVoltar.Visible = true;
        gvProduto.Visible = false;
        AcaiClicado();
        gvAdicional.Visible = true;
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        gvAdicional.Visible = false;
        gvProduto.Visible = true;
        btnVoltar.Visible = false;
        btnAdicionar.Visible = false;
        btnContinuar.Visible = true;
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        DataView listaProd;
        listaProd = (DataView)DSProduto.Select(DataSourceSelectArguments.Empty);
        lblIds.Text = "";
        lblAdd.Text = "";
        int qtdIds = 0;

        foreach (GridViewRow linha in gvProduto.Rows)
        {
            RadioButton rbEscolhaProd;

            rbEscolhaProd = (RadioButton)linha.FindControl("rbEscolhaProd");

            if (rbEscolhaProd.Checked == true)
            {
                qtdIds++;

                int linhaSelecionada = linha.DataItemIndex;

                Session["idProd"] = Convert.ToString(listaProd.Table.Rows[linhaSelecionada]["id_prod"]);
                Session["qtdIds"] = qtdIds;

                lblIds.Text += Session["idProd"].ToString();

                lblQtdIds.Text = qtdIds.ToString();
            }
        }

        if (gvAdicional.Visible == false)
            return;
        else
        {
            foreach (GridViewRow linha in gvAdicional.Rows)
            {
                RadioButton rbEscolhaAdd;

                rbEscolhaAdd = (RadioButton)linha.FindControl("rbEscolhaAdd");

                if (rbEscolhaAdd.Checked == true)
                {
                    string AddSelecionado = adicionais[linha.DataItemIndex].ToString();

                    Session["Adicional"] = AddSelecionado;

                    lblAdd.Text += Session["Adicional"].ToString() + " ";
                }
            }
        }

        Response.Write("<script>if ('" + qtdIds + "' == 1) alert('" + qtdIds + " produto foi adicionado ao Carrinho'); " +
            " else alert('" + qtdIds + " produtos foi adicionado ao Carrinho');</script>");
    }

    private void CarregarAdicionais()
    {
        DataTable listaAdd = new DataTable();
        listaAdd.Columns.Add("Adicional");

        for (int i = 0; i < adicionais.GetLength(0); i++)
        {
            listaAdd.Rows.Add();
            listaAdd.Rows[i]["Adicional"] = adicionais[i].ToString();
        }

        gvAdicional.DataSource = listaAdd;
        gvAdicional.DataBind();
    }

    private void CarregarProduto(int tipoProd)
    {
        listaDescripto.Columns.Add("id_prod", typeof(int));
        listaDescripto.Columns.Add("nome_prod", typeof(string));
        listaDescripto.Columns.Add("id_tipoProd", typeof(int));
        listaDescripto.Columns.Add("tam_prod", typeof(string));
        listaDescripto.Columns.Add("preco_prod", typeof(double));

        DataView listaProd;

        listaProd = (DataView)DSProduto.Select(DataSourceSelectArguments.Empty);

        listaDescripto.DefaultView.RowFilter = "id_tipoProd = '" + tipoProd + "'";

        for (int i = 0; i < listaProd.Table.Rows.Count; i++)
        {
            DataRow linha = listaDescripto.NewRow();

            linha["id_prod"] = listaProd.Table.Rows[i]["id_prod"].ToString();
            linha["nome_prod"] = cripto.Decrypt(listaProd.Table.Rows[i]["nome_prod"].ToString());
            linha["id_tipoProd"] = listaProd.Table.Rows[i]["id_tipoProd"].ToString();
            linha["tam_prod"] = cripto.Decrypt(listaProd.Table.Rows[i]["tam_prod"].ToString());
            linha["preco_prod"] = cripto.Decrypt(listaProd.Table.Rows[i]["preco_prod"].ToString()).Replace('.', ',');

            listaDescripto.Rows.Add(linha);
        }

        gvProduto.DataSource = listaDescripto;
        gvProduto.DataBind();
    }

    private void AcaiClicado()
    {
        if (ImbAcaiClicado == true)
        {
            CarregarAdicionais();

            btnContinuar.Visible = true;

            btnAdicionar.Visible = false;
        }
        else
        {
            btnContinuar.Visible = false;

            btnAdicionar.Visible = true;
        }
    }

    private void EscolherOutroProd()
    {
        if (gvAdicional.Visible == true)
        {
            gvAdicional.Visible = false;

            ImbAcaiClicado = false;

            btnVoltar.Visible = false;

            AcaiClicado();
        }
    }
}