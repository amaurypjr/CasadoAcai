﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Relatorios : System.Web.UI.Page
{
    Criptografia cripto = new Criptografia("ETEP");

    DataTable listaDescripto = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adm"] == null || Session.Equals("Saiu"))
            Response.Redirect("Menu.aspx");

        else
            return;      
    }

    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        if (txtData.Text == "")
            Response.Write("<script>alert('Digite uma data para pesquisar uma venda !')</script>");

        else
        {
            DSRelatorios.SelectParameters["DATA"].DefaultValue = cripto.Encrypt(txtData.Text);

            CarregarRelatorio();
        }
    }

    protected void gvRelatorio_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id;
        string dataKey;

        foreach (GridViewRow linha in gvRelatorio.Rows)
        {
            if (linha.RowState == (DataControlRowState.Alternate | DataControlRowState.Selected))
                linha.RowState = DataControlRowState.Selected;

            if (linha.RowState == DataControlRowState.Selected)
            {
                dataKey = gvRelatorio.DataKeys[linha.RowIndex].Values["id_vda"].ToString();
                id = Convert.ToInt32(dataKey);

                Session["idVda"] = id;
                linha.RowState = DataControlRowState.Normal;
                break;
            }
        }

        Response.Redirect("DetalhesAdm.aspx");
    }

    public void CarregarRelatorio()
    {
        DataTable relatorio = new DataTable();

        relatorio.Columns.Add("id_vda", typeof(int));
        relatorio.Columns.Add("nome_cli", typeof(String));
        relatorio.Columns.Add("nome_prod", typeof(String));
        relatorio.Columns.Add("adicional", typeof(String));
        relatorio.Columns.Add("valor_vda", typeof(double));

        DataView lista = (DataView)DSRelatorios.Select(DataSourceSelectArguments.Empty);

        for (int i = 0; i < lista.Table.Rows.Count; i++)
        {
            DataRow linha = relatorio.NewRow();

            string adicional = cripto.Decrypt(lista.Table.Rows[i]["adicional"].ToString());
            string preco = cripto.Decrypt(lista.Table.Rows[i]["valor_vda"].ToString()).Replace('.',',');

            linha["id_vda"] = lista.Table.Rows[i]["id_vda"].ToString();
            linha["nome_cli"] = cripto.Decrypt(lista.Table.Rows[i]["nome_cli"].ToString());
            linha["nome_prod"] = cripto.Decrypt(lista.Table.Rows[i]["nome_prod"].ToString());

            if (adicional == "")
                linha["adicional"] = "Nenhum";

            else
                linha["adicional"] = adicional;


            linha["valor_vda"] = preco;

            relatorio.Rows.Add(linha);
        }

        gvRelatorio.DataSource = relatorio;
        gvRelatorio.DataBind();
    }
}