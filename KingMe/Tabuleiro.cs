using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MePresidentaServidor;

namespace KingMe
{
    public partial class Tabuleiro : Form
    {
        Boolean inGame = false;
        public Tabuleiro(string Form)
        {
            InitializeComponent();
            if (Form == "Criar_Partida")
            {
                Criar_Partida formulario = new Criar_Partida();
                formulario.ShowDialog();
                this.idPartida = formulario.idPartida;
                this.senhaPartida = formulario.senhaPartida;
                this.idJogador = formulario.idJogador;
                this.senhaJogador = formulario.senhaJogador;
                this.jogadorDaVez = formulario.jogadorDaVez;
                timer1.Enabled = true;
            } else if (Form == "Entrar_Nova")
            {
                Entrar_Nova formulario2 = new Entrar_Nova();
                formulario2.ShowDialog();
                this.idPartida = formulario2.idPartida;
                this.senhaPartida = formulario2.senhaPartida;
                this.idJogador = formulario2.idJogador;
                this.senhaJogador = formulario2.senhaJogador;
                this.jogadorDaVez = formulario2.jogadorDaVez;
                timer1.Enabled = true;
            }
            if (String.IsNullOrEmpty(this.idPartida))
            {
                this.Visible = false;
            }
        }

        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public string[,] matrizTabuleiro { get; set; } = new string[20,4];
        public string posicaoOperario { get; set; }
        public int movimentaPersonagem(string personagem, int nivel)
        {
            MessageBox.Show(MePresidentaServidor.Jogo.ColocarPersonagem(Convert.ToInt32(this.idPartida), this.senhaJogador, nivel, personagem));
            string[] aux = { };
            Control persona = null;
            foreach (Control con in this.Controls) {
                if (con is PictureBox)
                {
                   if (Convert.ToString(con.Tag) == personagem)
                    {
                        persona = con;
                        break;
                    }
                }
            }
            if (persona is null)
            {
                return -1;
            }
            for (int i = 0; i < 4; i++)
            {
                aux = this.matrizTabuleiro[(Convert.ToInt32(nivel) - 1), i].Split(',');
                if (aux[2] == "false")
                {
                    persona.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    this.matrizTabuleiro[(Convert.ToInt32(nivel) - 1), i] = aux[0] + ',' + aux[0] + ',' + cmbPersonagens.Text;
                    this.cmbPersonagens.Items.Remove(this.cmbPersonagens.Text);
                    return 1;
                }
            }
            return 0;
        }

    private void Tabuleiro_Load(object sender, EventArgs e)
        {
            cmbPersonagens.Items.Add("A");
            cmbPersonagens.Items.Add("B");
            cmbPersonagens.Items.Add("C");
            cmbPersonagens.Items.Add("D");
            cmbPersonagens.Items.Add("E");
            cmbPersonagens.Items.Add("F");
            cmbPersonagens.Items.Add("G");
            cmbPersonagens.Items.Add("H");
            cmbPersonagens.Items.Add("I");
            cmbPersonagens.Items.Add("J");
            cmbPersonagens.Items.Add("K");
            cmbPersonagens.Items.Add("L");
            cmbPersonagens.Items.Add("M");

            cmbDestino.Items.Add("1");
            cmbDestino.Items.Add("2");
            cmbDestino.Items.Add("3");
            cmbDestino.Items.Add("4");

            this.matrizTabuleiro[0,0] = Convert.ToString(this.pos10.Location.X) + "," + Convert.ToString(this.pos10.Location.Y) + ",false";
            this.matrizTabuleiro[0,1] = Convert.ToString(this.pos11.Location.X) + "," + Convert.ToString(this.pos11.Location.Y) + ",false";
            this.matrizTabuleiro[0,2] = Convert.ToString(this.pos12.Location.X) + "," + Convert.ToString(this.pos12.Location.Y) + ",false";
            this.matrizTabuleiro[0,3] = Convert.ToString(this.pos13.Location.X) + "," + Convert.ToString(this.pos13.Location.Y) + ",false";
            this.matrizTabuleiro[1,0] = Convert.ToString(this.pos20.Location.X) + "," + Convert.ToString(this.pos20.Location.Y) + ",false";
            this.matrizTabuleiro[1,1] = Convert.ToString(this.pos21.Location.X) + "," + Convert.ToString(this.pos21.Location.Y) + ",false";
            this.matrizTabuleiro[1,2] = Convert.ToString(this.pos22.Location.X) + "," + Convert.ToString(this.pos22.Location.Y) + ",false";
            this.matrizTabuleiro[1,3] = Convert.ToString(this.pos23.Location.X) + "," + Convert.ToString(this.pos23.Location.Y) + ",false";
            this.matrizTabuleiro[2,0] = Convert.ToString(this.pos30.Location.X) + "," + Convert.ToString(this.pos30.Location.Y) + ",false";
            this.matrizTabuleiro[2,1] = Convert.ToString(this.pos31.Location.X) + "," + Convert.ToString(this.pos31.Location.Y) + ",false";
            this.matrizTabuleiro[2,2] = Convert.ToString(this.pos32.Location.X) + "," + Convert.ToString(this.pos32.Location.Y) + ",false";
            this.matrizTabuleiro[2,3] = Convert.ToString(this.pos33.Location.X) + "," + Convert.ToString(this.pos33.Location.Y) + ",false";
            this.matrizTabuleiro[3,0] = Convert.ToString(this.pos40.Location.X) + "," + Convert.ToString(this.pos40.Location.Y) + ",false";
            this.matrizTabuleiro[3,1] = Convert.ToString(this.pos41.Location.X) + "," + Convert.ToString(this.pos41.Location.Y) + ",false";
            this.matrizTabuleiro[3,2] = Convert.ToString(this.pos42.Location.X) + "," + Convert.ToString(this.pos42.Location.Y) + ",false";
            this.matrizTabuleiro[3,3] = Convert.ToString(this.pos43.Location.X) + "," + Convert.ToString(this.pos43.Location.Y) + ",false";
            this.matrizTabuleiro[4,0] = Convert.ToString(this.pos50.Location.X) + "," + Convert.ToString(this.pos50.Location.Y) + ",false";
            this.matrizTabuleiro[4,1] = Convert.ToString(this.pos51.Location.X) + "," + Convert.ToString(this.pos51.Location.Y) + ",false";
            this.matrizTabuleiro[4,2] = Convert.ToString(this.pos52.Location.X) + "," + Convert.ToString(this.pos52.Location.Y) + ",false";
            this.matrizTabuleiro[4,3] = Convert.ToString(this.pos53.Location.X) + "," + Convert.ToString(this.pos53.Location.Y) + ",false";

            txtId.Text = this.idJogador;
            txtSenha.Text = this.senhaJogador;
        }

        private void btnConfirmarJogada_Click(object sender, EventArgs e)
        {
            movimentaPersonagem(this.cmbPersonagens.Text,Convert.ToInt32(this.cmbDestino.Text));
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.inGame == false)
            { 
                string Aux1;
                string[] Aux2;
                string[] Aux3;
                Aux1 = Jogo.ListarPartidas();
            
                Aux1 = Aux1.Replace("\r", "");
                Aux2 = Aux1.Split('\n');

                for (int i = 0; i< Aux2.Length; i++)
                {
                    Aux3 = Aux2[i].Split(',');
                    if (Aux3[1] == idPartida)
                    {
                        MessageBox.Show("Começou a partida");
                        this.inGame = true;
                        break;
                    }
                }
            } else
            {
                MessageBox.Show(Jogo.VerificarVez(Convert.ToInt32(this.idJogador)));

                if (this.jogadorDaVez == this.idJogador)
                {
                    mensagem.Text = "Sua Vez, Faça uma Jogada" + this.jogadorDaVez;
                    mensagem.ForeColor = Color.LimeGreen;
                }
                else
                {
                    mensagem.Text = "Aguarde sua Vez!" + this.jogadorDaVez;
                    mensagem.ForeColor = Color.Red;
                }
            }
            
        }
    }
}
