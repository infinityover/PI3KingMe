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

       //public class posicao
       // {
       //     public int pos_x, pos_y;
       //     private bool ocupado;
       //     private PictureBox imagem;
       //
       // }

        public Tabuleiro(string Form)
        {
            InitializeComponent();
            if (Form == "Criar_Partida")
            {
                Criar_Partida formulario = new Criar_Partida();
                formulario.ShowDialog();
                if (formulario.idJogador == "") this.Close();
                this.idPartida = formulario.idPartida;
                this.senhaPartida = formulario.senhaPartida;
                this.idJogador = formulario.idJogador;
                this.senhaJogador = formulario.senhaJogador;
                this.jogadorDaVez = formulario.jogadorDaVez;
            } else if (Form == "Entrar_Nova")
            {
                Entrar_Nova formulario2 = new Entrar_Nova();
                formulario2.ShowDialog();
                if (formulario2.idJogador== null) this.DestroyHandle();
                this.idPartida = formulario2.idPartida;
                this.senhaPartida = formulario2.senhaPartida;
                this.idJogador = formulario2.idJogador;
                this.senhaJogador = formulario2.senhaJogador;
                this.jogadorDaVez = formulario2.jogadorDaVez;
            }
        }

        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public string[,] matrizTabuleiro { get; set; } = new string[20,4];
        public string posicaoOperario { get; set; }

        //private posicao[,] matriz = new posicao[5,4];
        //
        //public posicao[,] GetMatriz()
        //{
        //    return GetMatriz();
        //}
        //
        //public void SetMatriz(posicao[,] value)
        //{
        //    SetMatriz(value);
        //}

        private void Tabuleiro_Load(object sender, EventArgs e)
        {
            //GetMatriz()[0, 0].pos_x = 10;
            //MessageBox.Show(GetMatriz()[0, 0].pos_x.ToString());
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

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnVerificarVez_Click(object sender, EventArgs e)
        {
            this.jogadorDaVez = MePresidentaServidor.Jogo.VerificarVez(Convert.ToInt32(this.idPartida));
            if (this.jogadorDaVez == this.idJogador)
            {
                mensagem.Text = "Sua Vez, Faça uma Jogada";
                mensagem.ForeColor = Color.LimeGreen;
            } else
            {
                mensagem.Text = "Aguarde sua Vez!";
                mensagem.ForeColor = Color.Red;

            }
        }

        private void btnConfirmarJogada_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(MePresidentaServidor.Jogo.ColocarPersonagem(Convert.ToInt32(this.idPartida), this.senhaJogador, Convert.ToInt32(this.cmbDestino.Text), this.cmbPersonagens.Text));
            string[] aux;
            for (int i=0; i<4; i++)
            {
                
                aux = this.matrizTabuleiro[(Convert.ToInt32(this.cmbDestino.Text) - 1),i].Split(',');

                if (this.cmbPersonagens.Text == "A")
                {                    
                    if (aux[2] == "false")
                    {
                        this.A.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "B")
                {
                    if (aux[2] == "false")
                    {
                        this.B.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "C")
                {
                    if (aux[2] == "false")
                    {
                        this.C.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "D")
                {
                    if (aux[2] == "false")
                    {
                        this.D.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "E")
                {
                    if (aux[2] == "false")
                    {
                        this.E.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "F")
                {
                    if (aux[2] == "false")
                    {
                        this.F.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "G")
                {
                    if (aux[2] == "false")
                    {
                        this.G.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "H")
                {
                    if (aux[2] == "false")
                    {
                        this.H.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "I")
                {
                    if (aux[2] == "false")
                    {
                        this.I.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "J")
                {
                    if (aux[2] == "false")
                    {
                        this.J.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "K")
                {
                    if (aux[2] == "false")
                    {
                        this.K.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "L")
                {
                    if (aux[2] == "false")
                    {
                        this.L.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }
                else if (this.cmbPersonagens.Text == "M")
                {
                    if (aux[2] == "false")
                    {
                        this.M.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                    }
                }

            }

            



        }
    }
}
