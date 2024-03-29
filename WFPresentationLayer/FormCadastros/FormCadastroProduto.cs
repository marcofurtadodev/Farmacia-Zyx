﻿using BusinessLogicalLayer.BusinessLL;
using Entities;
using Shared;
using System.Collections.Generic;
using WfPresentationLayer.FormCadastros;

namespace WfPresentationLayer
{

    /// <summary>
    /// Form de cadastro de Produto padrao de todo cadastro 
    /// onde o mesmo pode fazer o uptade se a textbox id for visivel 
    /// e insert se nao for visivel 
    /// Uma sobrecarga que abre com Produto ja preenchido para alteracao 
    /// e sem sobrecarga que abre para cadastro 
    /// 
    /// esse form ainda chama o cadastro de laboratorio
    /// </summary>
    public partial class FormCadastroProduto : Form
    {
        LaboratorioBLL LB = new();
        ProdutoBll produtoBll = new();
        List<Laboratorio> listaAtiva = new();


        public FormCadastroProduto()
        {
            InitializeComponent();
            CmbBoxLaboratorio.ValueMember = "ID";
            CmbBoxLaboratorio.DisplayMember = "Razao_Social";
            List<Laboratorio> Labs = LB.GetAll().Dados;
            foreach (Laboratorio Laboratorio in Labs)
            {
                if (Laboratorio.Ativo == true)
                {
                    listaAtiva.Add(Laboratorio);
                }
            }
            CmbBoxLaboratorio.DataSource = listaAtiva;
        }
        public FormCadastroProduto(Produto p)
        {
            InitializeComponent();
            LblText.Text = "Alterar Produto";
            List<Laboratorio> Labs = LB.GetAll().Dados;
            CmbBoxLaboratorio.ValueMember = "ID";
            CmbBoxLaboratorio.DisplayMember = "Razao_Social";
            foreach (Laboratorio Laboratorio in Labs)
            {
                if (Laboratorio.Ativo == true)
                {
                    listaAtiva.Add(Laboratorio);
                }
            }
            CmbBoxLaboratorio.DataSource = listaAtiva;
            LblText.Text = "Alterar Produto";

            TxtBoxId.Visible = true;
            TxtBoxId.Enabled = false;
            LblIDPRoduto.Visible = true;

            TxtBoxDescrisaoProduto.Text = p.Descricao;
            TxtBoxId.Text = p.ID.ToString();
            TxtBoxNomeProduto.Text = p.Nome;
            TxtBoxPrecoUnitario.Text = p.Valor_Unitario.ToString();
            CmbBoxLaboratorio.SelectedText = p.ID_Laboratorio.Razao_Social;
        }
        private void BtnCadastrarProduto_Click_1(object sender, EventArgs e)
        {
            if(TxtBoxPrecoUnitario.Text == "" || TxtBoxNomeProduto.Text == "Digite Nome Produto" || TxtBoxDescrisaoProduto.Text == "Digite A Descrição")
            {
                MeuMessageBox.Show("Voce nao informou todos os campos");
                return;
            }
            Laboratorio lab = new();
            lab.ID = Convert.ToInt32(CmbBoxLaboratorio.SelectedValue.ToString());

            Produto produto = new();

            produto.Nome = TxtBoxNomeProduto.Text;
            produto.Descricao = TxtBoxDescrisaoProduto.Text;
            produto.ID_Laboratorio = lab;
            produto.Valor_Unitario = double.Parse(TxtBoxPrecoUnitario.Text);
            produto.Ativo = true;
            if (TxtBoxId.Visible == true)
            {
                produto.ID = int.Parse(TxtBoxId.Text);
                Response resposta = produtoBll.Update(produto);
                MeuMessageBox.Show(resposta.Message);
                if (resposta.HasSuccess)
                { this.Close(); }
            }
            else
            {
                Response resposta = produtoBll.Insert(produto);
                MeuMessageBox.Show(resposta.Message);
                if (resposta.HasSuccess)
                {
                    this.Close();
                }
            }
        }
        private void BtnCadastrarLab_Click(object sender, EventArgs e)
        {
            FormCadastrarLaboratorio formCadastrarLaboratorio = new();
            formCadastrarLaboratorio.ShowDialog();
        }


        // Metodos padrões Para melhor visualizacao e entendimento do usuario 
        private void TxtBoxPrecoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                //troca o . pela virgula
                e.KeyChar = ',';

                //Verifica se já existe alguma vírgula na string
                if (TxtBoxPrecoUnitario.Text.Contains(","))
                {
                    e.Handled = true; // Caso exista, aborte 
                }
            }
            //aceita apenas números, tecla backspace.
            else if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }

        }
        private void ImageBtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtBoxNomeProduto_Enter(object sender, EventArgs e)
        {
            if (TxtBoxNomeProduto.Text == "Digite Nome Produto")
            {
                TxtBoxNomeProduto.Text = "";
            }
            else if (TxtBoxNomeProduto.Text == "")
            {
                TxtBoxNomeProduto.Text = "Digite Nome Produto";
            }
        }

        private void TxtBoxDescrisaoProduto_Enter(object sender, EventArgs e)
        {
            if (TxtBoxDescrisaoProduto.Text == "Digite A Descrição")
            {
                TxtBoxDescrisaoProduto.Text = "";
            }
            else if (TxtBoxDescrisaoProduto.Text == "")
            {
                TxtBoxDescrisaoProduto.Text = "Digite A Descrição";
            }
        }
    }
}
