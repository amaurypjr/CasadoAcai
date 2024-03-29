﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Carrinho : System.Web.UI.Page
{
    Criptografia cripto = new Criptografia("ETEP");

    DataTable listaDescripto = new DataTable();

    DataView itens;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["logado"] == null || Session["logado"].Equals("Saiu"))
            Response.Redirect("Menu.aspx");

        else
        {
            lH1.Text = GerarH1();
            CarregarCarrinho();
        }
    }
    public string GerarH1()
    {
        StringBuilder sbH1 = new StringBuilder();

        if (Session["ultVenda"] == null)
        {
            sbH1.AppendLine("<h1 style='text-align: center;'>SEU CARRINHO ESTÁ VAZIO !</h1>");
            return sbH1.ToString();
        }

        else
        {
            sbH1.AppendLine("<h1 style='text-align: center;'>SEU CARRINHO</h1>");
            return sbH1.ToString();
        }
    }

    public void DesabilitarCampos()
    {
        gvCarrinho.Visible = false;
        txtTotalVenda.Text = "";
        ddlForma.ClearSelection();
        ddlForma.Enabled = false;
        btnFinalizar.Enabled = false;
    }

    private void CarregarCarrinho()
    {
        if (Session["ultVenda"] != null)
        {
            double totalCompra = 0;

            DataTable carrinho = new DataTable();

            carrinho.Columns.Add("imagem", (typeof(String)));
            carrinho.Columns.Add("nome_prod", typeof(String));
            carrinho.Columns.Add("adicional", typeof(String));
            carrinho.Columns.Add("qtd_it", typeof(int));
            carrinho.Columns.Add("preco_prod", typeof(double));
            carrinho.Columns.Add("total_ped", (typeof(double)));

            DataView listaCarrinho;
            listaCarrinho = (DataView)DSCarrinho.Select(DataSourceSelectArguments.Empty);

            for (int i = 0; i < listaCarrinho.Table.Rows.Count; i++)
            {
                string tipoProd = cripto.Decrypt(listaCarrinho.Table.Rows[i]["nome_tipo"].ToString());

                DataRow rLinha = carrinho.NewRow();

                if (tipoProd == "Açaí")
                    rLinha["imagem"] = "~/Imagens/Acai.png";

                if (tipoProd == "Sacolé")
                    rLinha["imagem"] = "~/Imagens/Sacole.png";

                if (tipoProd == "Geladinho")
                    rLinha["imagem"] = "~/Imagens/Geladinho.png";

                if (tipoProd == "Sorvete")
                    rLinha["imagem"] = "~/Imagens/Sorvete.png";

                if (tipoProd == "Picolé")
                    rLinha["imagem"] = "~/Imagens/Picole.png";

                if (tipoProd == "Cremosinho")
                    rLinha["imagem"] = "~/Imagens/Cremosinho.png";

                string add = cripto.Decrypt(listaCarrinho.Table.Rows[i]["adicional"].ToString());

                rLinha["nome_prod"] = cripto.Decrypt(listaCarrinho.Table.Rows[i]["nome_prod"].ToString());

                if (add == "")
                    rLinha["adicional"] = "Nenhum";

                else
                    rLinha["adicional"] = add;

                rLinha["qtd_it"] = cripto.Decrypt(listaCarrinho.Table.Rows[i]["qtd_it"].ToString());
                rLinha["preco_prod"] = cripto.Decrypt(listaCarrinho.Table.Rows[i]["preco_prod"].ToString()).Replace('.', ',');
                rLinha["total_ped"] = cripto.Decrypt(listaCarrinho.Table.Rows[i]["total_ped"].ToString());

                totalCompra += Convert.ToDouble(rLinha["total_ped"]);

                carrinho.Rows.Add(rLinha);
            }

            gvCarrinho.DataSource = carrinho;
            gvCarrinho.DataBind();

            txtTotalVenda.Text = totalCompra.ToString("C");
        }

        else { DesabilitarCampos(); }
    }

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        char[] rs = { 'R', '$' };

        string preco = txtTotalVenda.Text.TrimStart(rs).Replace(',', '.');

        DSCarrinho.UpdateParameters["TOTAL"].DefaultValue = cripto.Encrypt(preco);
        DSCarrinho.Update();

        Session["novaCompra"] = "Sim";

        Session["ultVenda"] = null;

        lH1.Text = GerarH1();

        DesabilitarCampos();

        Response.Write("<script>alert('Compra realizada com Sucesso !');</script>");
    }
}