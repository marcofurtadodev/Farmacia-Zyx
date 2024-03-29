﻿using BusinessLogicalLayer.BusinessLL;
using Entities;
using Shared;
using System.ComponentModel;
using WfPresentationLayer.FormCadastros;

namespace WfPresentationLayer.FormsMostrarClientes
{
    /// <summary>
    /// Form Padrão mostrar Laboratorios em uma datagrid 
    /// dentro desse form a algumas Funcoes que ele faz dentro delas 
    /// Cadastro de Laboratorio que abre o form de Laboratorio vazio
    /// alterar Laboratorios que abre o form de cadastro de Laboratorio preenchido onde o mesmo é feito clicando na linha e clicando no botao
    /// mostrar desabilitados mostra na grid todos os Laboratorios desabilitados
    /// deletar deleta ou desabilita os Laboratorio da grid e do banco de dados onde o mesmo é feito clicando na linha e clicando no botao
    /// 
    /// onde todos esse forms sao aberto dentro de um panel padrão
    /// </summary>
    public partial class FormMostarLaboratorio : Form
    {
        Form _objForm5;
        LaboratorioBLL laboratorioBLL = new();
        public FormMostarLaboratorio()
        {
            InitializeComponent();
        }
        List<Laboratorio> Laboratorios = new();
        private void BtnCadastrarlaboratorio_Click(object sender, EventArgs e)
        {
            _objForm5?.Close();
            _objForm5 = new FormCadastrarLaboratorio
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
            };
            PNLLaboratorio.Controls.Add(_objForm5);
            _objForm5.Show();
            _objForm5.BringToFront();
        }
        private void BtnAlterarLaboratorio_Click(object sender, EventArgs e)
        {
            if (this.GridLaboratorio.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = this.GridLaboratorio.SelectedRows[0];
            if (GridLaboratorio.CurrentRow.Cells[0].Value == null)
            {
                MeuMessageBox.Show("Voce nao selecionou nenhuma coluna");
            }
            else
          {
                Laboratorio lab = new();
              lab.ID = Convert.ToInt32(GridLaboratorio.CurrentRow.Cells[0].Value.ToString());
              lab.Razao_Social = Convert.ToString(GridLaboratorio.CurrentRow.Cells[1].Value.ToString());
              lab.Telefone = Convert.ToString(GridLaboratorio.CurrentRow.Cells[2].Value.ToString());
              lab.Nome_Contato = Convert.ToString(GridLaboratorio.CurrentRow.Cells[3].Value.ToString());
              lab.Email = Convert.ToString(GridLaboratorio.CurrentRow.Cells[4].Value.ToString());
              lab.CNPJ = Convert.ToString(GridLaboratorio.CurrentRow.Cells[5].Value.ToString());

                _objForm5?.Close();
              _objForm5 = new FormCadastrarLaboratorio(lab)
              {
                  TopLevel = false,
                  FormBorderStyle = FormBorderStyle.None,
                  Dock = DockStyle.Fill,
              };
                PNLLaboratorio.Controls.Add(_objForm5);
                _objForm5.Show();
                _objForm5.BringToFront();
            }
            

        }
        private void FormMostarLaboratorio_Load(object sender, EventArgs e)
        {
            Laboratorios = laboratorioBLL.GetAll().Dados;
            foreach (Laboratorio laboratorio in Laboratorios)
            {
                if (laboratorio.Ativo == true)
                {
                    SincronizarListaGrid(laboratorio);
                }
            }
        }
        private void BtnDesabilitados_Click(object sender, EventArgs e)
        {
            LimparGrid();
            List<Laboratorio> LaboratoriosOFF = laboratorioBLL.GetAll().Dados;
            foreach (Laboratorio laboratorio in LaboratoriosOFF)
            {
                if (laboratorio.Ativo == false)
                {
                    SincronizarListaGrid(laboratorio);
                }
            }
            if (GridLaboratorio.RowCount == 1)
            {
                MeuMessageBox.Show("Nao há Laboratorios Desabilitados");
                LimparGrid();
            }

        }
        private void BtnDeletarLaboratorio_Click(object sender, EventArgs e)
        {
            if (this.GridLaboratorio.SelectedRows.Count == 0)
            {
                return;
            }
            if (GridLaboratorio.CurrentRow.Cells[0].Value == null)
            {
                MeuMessageBox.Show("Voce nao selecionou nenhuma coluna");
                return;
            }
            DataGridViewRow row = this.GridLaboratorio.SelectedRows[0];
            int IDCLiente = Convert.ToInt32(GridLaboratorio.CurrentRow.Cells[0].Value.ToString());
            string nome = Convert.ToString(GridLaboratorio.CurrentRow.Cells[1].Value.ToString());

            DialogResult r = MeuMessageBox.Show("Deseja Apagar o Laboratorio" + nome + " Da tabela Ou do banco " + "\r\n" + "X Para Voltar", "Escolha", "Deletar Do banco", "deletar da tabela");
            if (r == DialogResult.Yes)
            {
                DialogResult re = MeuMessageBox.Show("Tem Certeza que deseja deletar o Laboratorio " + nome, " Tem Certeza?", "Sim", "Nao");
                if (re == DialogResult.Yes)
                {
                    Response resposta = laboratorioBLL.Delete(IDCLiente);
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
                DialogResult re = MeuMessageBox.Show("Tem Certeza que deseja Apagar o Laboratorio " + nome, " Tem Certeza?", "Sim", "Nao");
                if (re == DialogResult.Yes)
                {
                    Response resposta = laboratorioBLL.Disable(IDCLiente);
                    if (resposta.HasSuccess)
                    {
                        MeuMessageBox.Show(resposta.Message, "Deu Certo", "OK");
                    }
                    else MeuMessageBox.Show(resposta.Message);
                }
                else { }
            }
        }
        private void SincronizarListaGrid(Laboratorio item)
        {
            GridLaboratorio.Rows.Add(item.ID, item.Razao_Social, item.Telefone, item.Nome_Contato, item.Email, item.CNPJ);
            GridLaboratorio.Sort(GridLaboratorio.Columns[0], ListSortDirection.Ascending);

        }

        // Metodos padrões Para melhor visualizacao e entendimento do usuario 
        private void LimparGrid()
        {
            GridLaboratorio.Rows.Clear();
            GridLaboratorio.Refresh();
            GridLaboratorio.DataSource = null;
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LimparGrid();
            List<Laboratorio> LaboratoriosOFF = laboratorioBLL.GetAll().Dados;
            foreach (Laboratorio laboratorio in LaboratoriosOFF)
            {
                if (laboratorio.Ativo == true)
                {
                    SincronizarListaGrid(laboratorio);
                }
            }
        }
    }
}
