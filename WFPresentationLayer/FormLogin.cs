﻿using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfPresentationLayer
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void BtnLogar_Click(object sender, EventArgs e)
        {
            MetodoLogin metodoLogin = new MetodoLogin(TxtBoxLogin.Text, TxtBoxSenha.Text);
            
            if (metodoLogin.IsLoggedIn(metodoLogin))
            {
                LabelResposta.Text = "Bem vindo";
                MenuGeralAdmin menuGeralAdmin = new MenuGeralAdmin();
                
                    menuGeralAdmin.ShowDialog();
                
            }else { LabelResposta.Text = "Email ou Senha Invalidos"; }

               
        }

        private void TxtBoxLogin_TextChanged(object sender, EventArgs e)
        {
            if(TxtBoxLogin.Text == "Email" )
            {
                TxtBoxLogin.Text = "";
                
            }
            else if(TxtBoxLogin.Text == "")
            {
                TxtBoxLogin.Text = "Email"; 
            }
        }

        private void TxtBoxSenha_TextChanged(object sender, EventArgs e)
        {
            if (TxtBoxSenha.Text == "Senha")
            {
                TxtBoxSenha.Text = "";
            }
            else if (TxtBoxSenha.Text == "")
            {
                TxtBoxSenha.Text = "Senha";
            }
            

        }

        
    }
}
