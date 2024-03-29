﻿using BusinessLogicalLayer.BusinessLL;
using Entities;
using Shared;
using System.ComponentModel;
using WfPresentationLayer.FormCadastros;

namespace WfPresentationLayer.Alteraçoes
{
    /// <summary>
    /// Form Padrão mostrar Fornecedor em uma datagrid 
    /// dentro desse form a algumas Funcoes que ele faz dentro delas 
    /// Cadastro de Fornecedores que abre o form de cliente vazio
    /// alterar Fornecedor que abre o form de cadastro de Fornecedor preenchido onde o mesmo é feito clicando na linha e clicando no botao
    /// mostrar desabilitados mostra na grid todos os Fornecedor desabilitados
    /// deletar deleta ou desabilita os Fornecedor da grid e do banco de dados onde o mesmo é feito clicando na linha e clicando no botao
    /// 
    /// onde dentro do form ele tem uma sobrecarga se ele for aberto para gerar um Fornecedor em tela de Cadastros ou vendas
    /// 
    /// onde todos esse forms sao aberto dentro de um panel padrão
    /// </summary>
    public partial class Alteracao_Fornecedor : Form
    {
        private Form _objForm5;
        FornecedorBll fornecedorBll = new();
        public Fornecedor fornecedorSelecionado { get; set; }
        private bool _isOpenedByAnotherForm;
        public Alteracao_Fornecedor(bool v)
        {
            InitializeComponent();
            this._isOpenedByAnotherForm = v;
        }
        public Alteracao_Fornecedor()
        {
            InitializeComponent();
        }
        private void Alteracao_Fornecedor_Load(object sender, EventArgs e)
        {
            List<Fornecedor> fornecedores = fornecedorBll.GetAll().Dados;
            foreach (Fornecedor Fornecedor in fornecedores)
            {
                if (Fornecedor.Ativo == true)
                {
                    SincronizarListaGrid(Fornecedor);
                }
            }
        }
        private void BtnCadastrarFornecedor_Click(object sender, EventArgs e)
        {
            _objForm5?.Close();
            _objForm5 = new FormCadastroFornecedor
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
            };
            PnlFornecedores.Controls.Add(_objForm5);
            _objForm5.Show();
            _objForm5.BringToFront();

        }
        private void SincronizarListaGrid(Fornecedor item)
        {
         GridFornecedor.Rows.Add(item.ID, item.Razao_Social, item.Telefone, item.Nome_Contato, item.Email, item.CNPJ);
            GridFornecedor.Sort(GridFornecedor.Columns[0], ListSortDirection.Ascending);

        }
        private void BtnAlterarFornecedor_Click(object sender, EventArgs e)
        {
            if (this.GridFornecedor.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = this.GridFornecedor.SelectedRows[0];
            if (GridFornecedor.CurrentRow.Cells[0].Value == null)
            {
                MeuMessageBox.Show("Voce nao selecionou nenhuma coluna");
            }
            else
            {
                Fornecedor forne = new();
                forne.ID = Convert.ToInt32(GridFornecedor.CurrentRow.Cells[0].Value.ToString());
                forne.Razao_Social = Convert.ToString(GridFornecedor.CurrentRow.Cells[1].Value.ToString());
                forne.Telefone = Convert.ToString(GridFornecedor.CurrentRow.Cells[2].Value.ToString());
                forne.Nome_Contato = Convert.ToString(GridFornecedor.CurrentRow.Cells[3].Value.ToString());
                forne.Email = Convert.ToString(GridFornecedor.CurrentRow.Cells[4].Value.ToString());
                forne.CNPJ = Convert.ToString(GridFornecedor.CurrentRow.Cells[5].Value.ToString());

                _objForm5?.Close();
                _objForm5 = new FormCadastroFornecedor(forne)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                };
                PnlFornecedores.Controls.Add(_objForm5);
                _objForm5.Show();
                _objForm5.BringToFront();
            }
        }
        private void BtnDesabilitados_Click(object sender, EventArgs e)
        {
            LimparGrid();

            List<Fornecedor> fornecedoresOFF = fornecedorBll.GetAll().Dados;
            foreach (Fornecedor fornecedor in fornecedoresOFF)
            {
                if (fornecedor.Ativo == false)
                {
                    SincronizarListaGrid(fornecedor);
                }
            }
            if (GridFornecedor.RowCount == 1)
            {
                MeuMessageBox.Show("Nao há Fornecedores Desabilitados");
                LimparGrid();
            }

        }
        private void BtnDeletarFornecedor_Click(object sender, EventArgs e)
        {
            if (this.GridFornecedor.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = this.GridFornecedor.SelectedRows[0];
            int IDCLiente = Convert.ToInt32(GridFornecedor.CurrentRow.Cells[0].Value.ToString());
            string nome = Convert.ToString(GridFornecedor.CurrentRow.Cells[1].Value.ToString());

            DialogResult r = MeuMessageBox.Show("Deseja Apagar o Fornecedor" + nome + " Da tabela Ou do banco " + "\r\n" + "X Para Voltar", "Escolha", "Deletar Do banco", "deletar da tabela");
            if (r == DialogResult.Yes)
            {
                DialogResult re = MeuMessageBox.Show("Tem Certeza que deseja deletar o Fornecedor " + nome, " Tem Certeza?", "Sim", "Nao");
                if (re == DialogResult.Yes)
                {
                    Response resposta = fornecedorBll.Delete(IDCLiente);
                    if (resposta.HasSuccess)
                    {
                        MeuMessageBox.Show(resposta.Message, "Deu Certo", "OK");
                    }
                    else MeuMessageBox.Show(resposta.Message);
                }
                else { }
            }
            else if (r == DialogResult.No)
            {
                DialogResult re = MeuMessageBox.Show("Tem Certeza que deseja Apagar o Fornecedor " + nome, " Tem Certeza?", "Sim", "Nao");
                if (re == DialogResult.Yes)
                {
                    Response resposta = fornecedorBll.Disable(IDCLiente);
                    if (resposta.HasSuccess)
                    {
                        MeuMessageBox.Show(resposta.Message, "Deu Certo", "OK");
                    }
                    else MeuMessageBox.Show(resposta.Message);
                }
                else { }
            }
        }

        // Metodos padrões Para melhor visualizacao e entendimento do usuario 
        private void LimparGrid()
        {
            GridFornecedor.Rows.Clear();
            GridFornecedor.Refresh();
            GridFornecedor.DataSource = null;
        }

        /// <summary>
        /// Metodo Para Trazer dados de cliente para melhor procura do mesmo 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridFornecedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Fornecedor fornecedorselecionado = new();
            fornecedorselecionado.ID = Convert.ToInt32(GridFornecedor.Rows[e.RowIndex].Cells[0].Value);
            SingleResponse<Fornecedor> response = fornecedorBll.GetByID(fornecedorselecionado.ID);
            if (!response.HasSuccess)
            {
                MessageBox.Show(response.Message);
                return;
            }
            if (this._isOpenedByAnotherForm)
            {
                this.fornecedorSelecionado = response.Item;
                this.Close();
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LimparGrid();
            List<Fornecedor> fornecedores = fornecedorBll.GetAll().Dados;
            foreach (Fornecedor fornecedor in fornecedores)
            {
                if (fornecedor.Ativo == true)
                {
                    SincronizarListaGrid(fornecedor);
                }
            }
        }
    }
}
