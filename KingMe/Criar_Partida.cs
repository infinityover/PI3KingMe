﻿using MePresidentaServidor;
using System;
using System.Windows.Forms;

namespace KingMe
{
    public partial class Criar_Partida : Form
    {
        public Criar_Partida()
        {
            InitializeComponent();
        }

        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public Boolean kill { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtPartidas.Text = Jogo.ListarPartidas();
        }

        private void btnCriarPartida_Click(object sender, EventArgs e)
        {
           if(String.IsNullOrEmpty(this.txtNomePartida.Text)|| String.IsNullOrEmpty(this.senhaPartida))
            {
                MessageBox.Show("Verifique os valores digitados");
                return;
            }
           
            this.idPartida = Jogo.CriarPartida(this.txtNomePartida.Text, this.senhaPartida);
            this.txtIdPartida.Text = this.idPartida;
            this.txtIdPartida.Enabled = false;
            this.txtPartidas.Text = Jogo.ListarPartidas();
        }

        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
            this.txtPartidas.Text = Jogo.ListarPartidas();
        }

        private void btnEntrarJogo_Click(object sender, EventArgs e)
        {
            if (this.txtNomeJogador.Text != "" && this.idPartida != String.Empty)
            {
                string Aux1;
                string[] Aux2;
                Aux1 = Jogo.Entrar(Convert.ToInt32(this.idPartida), this.txtNomeJogador.Text, this.senhaPartida);
                Aux2 = Aux1.Split(',');
                this.idJogador = Aux2[0];
                this.senhaJogador = Aux2[1];
                this.Close();
            }
            else
            {
                MessageBox.Show("Digite um Nome e Senha para entrar na partida");
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            this.senhaPartida = this.txtSenha.Text;
        }

        private void Criar_Partida_FormClosed(object sender, FormClosedEventArgs e)
        {
            kill = true;
            this.Close();
        }

        private void txtNomeJogador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
