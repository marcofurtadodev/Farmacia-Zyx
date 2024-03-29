﻿namespace WfPresentationLayer
{
    /// <summary>
    /// Form de messege box que retorna duas datas 
    /// para mehlor visualizacao do usuario na lista de vendas e compras
    /// </summary>
    public partial class MeumsgBoxWithDates : Form
    {
        public MeumsgBoxWithDates()
        {
            InitializeComponent();
        }
        public ParametrosDatas ParametrosDatas { get; set; }
        public static ParametrosDatas ShowMessageWithDates(string mensagem)
        {
            var msgBox = new MeumsgBoxWithDates();
            msgBox.LblMensagem.MaximumSize = new Size(124, 45);
            msgBox.LblMensagem.MinimumSize = new Size(280, 124);
            msgBox.LblMensagem.AutoSize = true;
            msgBox.LblMensagem.Text = mensagem;
            msgBox.BtnOk.Visible = true;
            msgBox.ShowDialog();
            return msgBox.ParametrosDatas;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ParametrosDatas parametros = new()
            {
                Inicio = DateTime.Now,
                Fim = DateTime.Now,
            };
            this.ParametrosDatas = parametros;
            this.Close();
        }
        private void BtnOk_Click_1(object sender, EventArgs e)
        {
            ParametrosDatas parametros = new()
            {
                Inicio = DataInicio.Value,
                Fim = DataFim.Value,
            };
            this.ParametrosDatas = parametros;
            this.Close();
        }
    }
}
