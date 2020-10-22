﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Carrinho : System.Web.UI.Page
{
    Criptografia cripto = new Criptografia("ETEP");

    DataTable listaDescripto = new DataTable();

    string morango, chocolate, caramelo, menta, tuttiFrutti, maracujá;

    protected void Page_Load(object sender, EventArgs e)
    {
                   
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        btnVoltar.Visible = true;

        gvProduto.Visible = false;

        CarregarAdicionais();

        gvAdicional.Visible = true;

        btnContinuar.Text = "Adicionar ao Carrinho";
        btnContinuar.Width = 200;
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        gvAdicional.Visible = false;

        gvProduto.Visible = true;

        btnContinuar.Text = "Continuar";

        btnVoltar.Visible = false;
        btnContinuar.Width = 130;
    }

    protected void imbAcai_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(1);
    }

    protected void imbSacole_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(2);
    }

    protected void imbGeladinho_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(3);
    }

    protected void ImbSorvete_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(4);
    }

    protected void imbPicole_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(5);
    }

    protected void ImbCremosinho_Click(object sender, ImageClickEventArgs e)
    {
        gvProduto.Visible = true;
        CarregarProduto(6);
    }

    private void CarregarAdicionais()
    {
        string[] adicionais = { "Morango", "Chocolate", "Caramelo", "Menta", "Tutti frutti", "Maracujá" };

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

            linha["id_prod"]     = listaProd.Table.Rows[i]["id_prod"].ToString();
            linha["nome_prod"]   = cripto.Decrypt(listaProd.Table.Rows[i]["nome_prod"].ToString());
            linha["id_tipoProd"] = listaProd.Table.Rows[i]["id_tipoProd"].ToString();
            linha["tam_prod"]    = cripto.Decrypt(listaProd.Table.Rows[i]["tam_prod"].ToString());
            linha["preco_prod"]  = cripto.Decrypt(listaProd.Table.Rows[i]["preco_prod"].ToString()).Replace('.', ',');

            listaDescripto.Rows.Add(linha);
        }

        gvProduto.DataSource = listaDescripto;
        gvProduto.DataBind();
    }

    protected void rbEscolhaProd_CheckedChanged(object sender, EventArgs e)
    {
        
    }

    protected void rbEscolhaAdd_CheckedChanged(object sender, EventArgs e)
    {
            
    }
}