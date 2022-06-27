﻿namespace WfPresentationLayer
{
    public partial class MeuMessageBox : Form
    {
        public MeuMessageBox()
        {
            InitializeComponent();
            
        }

        public DialogResult Resultado { get; private set; }
        public static DialogResult Show(string mensagem, string Titulo)
        {
            var msgBox = new MeuMessageBox();
            msgBox.LblMensagem.MaximumSize = new Size(124, 45);
            msgBox.LblMensagem.MinimumSize = new Size(280, 124);
            msgBox.LblMensagem.AutoSize = true;
            msgBox.BtnNao.Visible = false;
            msgBox.BtnSim.Visible = false;
            msgBox.LblMensagem.Text = mensagem;
            msgBox.LabelTitulo.Text = Titulo;
            msgBox.ShowDialog();
            return msgBox.Resultado;
        }
        public static DialogResult Show(string mensagem,string Titulo, string textoSim, string textoNao)
        {
            var msgBox = new MeuMessageBox();
            msgBox.LblMensagem.MaximumSize = new Size(124,45);
            msgBox.LblMensagem.MinimumSize = new Size(280, 124);
            msgBox.LblMensagem.AutoSize = true;
            msgBox.LblMensagem.Text = mensagem;
            msgBox.BtnSim.Text = textoSim;
            msgBox.BtnNao.Text = textoNao;
            msgBox.LabelTitulo.Visible = true;
            msgBox.LabelTitulo.Text = Titulo;
            msgBox.BtnOk.Visible = false;
            msgBox.ShowDialog();
            return msgBox.Resultado;
        }
        public static DialogResult Show(string mensagem)
        {
            var msgBox = new MeuMessageBox();
            
            msgBox.LblMensagem.Text = mensagem;
            msgBox.LblMensagem.MaximumSize = new Size(124, 45);
            msgBox.LblMensagem.MinimumSize = new Size(280, 124);
            msgBox.LblMensagem.AutoSize = true;
            msgBox.BtnNao.Visible = false;
            msgBox.BtnSim.Visible = false;
            msgBox.LabelTitulo.Visible = false;
            msgBox.ShowDialog();
            return msgBox.Resultado;
        }
        public static DialogResult Show(string mensagem,string titulo,string BOTAOOK)
        {
            var msgBox = new MeuMessageBox();

            msgBox.LblMensagem.Text = mensagem;
            msgBox.LblMensagem.MaximumSize = new Size(124, 45);
            msgBox.LblMensagem.MinimumSize = new Size(280, 124);
            msgBox.LblMensagem.AutoSize = true;
            msgBox.BtnNao.Visible = false;
            msgBox.BtnOk.Visible = true;
            msgBox.BtnOk.Text = BOTAOOK;
            msgBox.BtnSim.Visible = false;
            msgBox.LabelTitulo.Text = titulo;
            msgBox.ShowDialog();
            return msgBox.Resultado;
        }
        private void BtnOk_Click(object sender, EventArgs e)
        {
          Resultado = DialogResult.OK;
          this.Close();
        }
        private void BtnSim_Click_1(object sender, EventArgs e)
        {
            Resultado = DialogResult.Yes;
            Close();
        }

        private void BtnNao_Click(object sender, EventArgs e)
        {
            Resultado = DialogResult.No;
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
