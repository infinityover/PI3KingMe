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
        public int inGame = 0;
        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public string[,] matrizTabuleiro { get; set; } = new string[30, 30];
        public string posicaoOperario { get; set; }
        public string Rei { get; private set; }
        public bool FimSetup { get; private set; }
        public bool aguardandoJogada { get; private set; }
        public string voto = "S";

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
            }
            else if (Form == "Entrar_Nova")
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
                this.Close();
            }
        }

        public void setarMatriz()
        {
            this.matrizTabuleiro[0, 0] = Convert.ToString(this.pos00.Location.X) + "," + Convert.ToString(this.pos00.Location.Y) + ",false";
            this.matrizTabuleiro[0, 1] = Convert.ToString(this.pos00.Location.X) + "," + Convert.ToString(this.pos00.Location.Y) + ",false";
            this.matrizTabuleiro[0, 2] = Convert.ToString(this.pos02.Location.X) + "," + Convert.ToString(this.pos02.Location.Y) + ",false";
            this.matrizTabuleiro[0, 3] = Convert.ToString(this.pos03.Location.X) + "," + Convert.ToString(this.pos03.Location.Y) + ",false";
            this.matrizTabuleiro[1, 0] = Convert.ToString(this.pos10.Location.X) + "," + Convert.ToString(this.pos10.Location.Y) + ",false";
            this.matrizTabuleiro[1, 1] = Convert.ToString(this.pos11.Location.X) + "," + Convert.ToString(this.pos11.Location.Y) + ",false";
            this.matrizTabuleiro[1, 2] = Convert.ToString(this.pos12.Location.X) + "," + Convert.ToString(this.pos12.Location.Y) + ",false";
            this.matrizTabuleiro[1, 3] = Convert.ToString(this.pos13.Location.X) + "," + Convert.ToString(this.pos13.Location.Y) + ",false";
            this.matrizTabuleiro[2, 0] = Convert.ToString(this.pos20.Location.X) + "," + Convert.ToString(this.pos20.Location.Y) + ",false";
            this.matrizTabuleiro[2, 1] = Convert.ToString(this.pos21.Location.X) + "," + Convert.ToString(this.pos21.Location.Y) + ",false";
            this.matrizTabuleiro[2, 2] = Convert.ToString(this.pos22.Location.X) + "," + Convert.ToString(this.pos22.Location.Y) + ",false";
            this.matrizTabuleiro[2, 3] = Convert.ToString(this.pos23.Location.X) + "," + Convert.ToString(this.pos23.Location.Y) + ",false";
            this.matrizTabuleiro[3, 0] = Convert.ToString(this.pos30.Location.X) + "," + Convert.ToString(this.pos30.Location.Y) + ",false";
            this.matrizTabuleiro[3, 1] = Convert.ToString(this.pos31.Location.X) + "," + Convert.ToString(this.pos31.Location.Y) + ",false";
            this.matrizTabuleiro[3, 2] = Convert.ToString(this.pos32.Location.X) + "," + Convert.ToString(this.pos32.Location.Y) + ",false";
            this.matrizTabuleiro[3, 3] = Convert.ToString(this.pos33.Location.X) + "," + Convert.ToString(this.pos33.Location.Y) + ",false";
            this.matrizTabuleiro[4, 0] = Convert.ToString(this.pos40.Location.X) + "," + Convert.ToString(this.pos40.Location.Y) + ",false";
            this.matrizTabuleiro[4, 1] = Convert.ToString(this.pos41.Location.X) + "," + Convert.ToString(this.pos41.Location.Y) + ",false";
            this.matrizTabuleiro[4, 2] = Convert.ToString(this.pos42.Location.X) + "," + Convert.ToString(this.pos42.Location.Y) + ",false";
            this.matrizTabuleiro[4, 3] = Convert.ToString(this.pos43.Location.X) + "," + Convert.ToString(this.pos43.Location.Y) + ",false";
            this.matrizTabuleiro[5, 0] = Convert.ToString(this.pos50.Location.X) + "," + Convert.ToString(this.pos50.Location.Y) + ",false";
            this.matrizTabuleiro[5, 1] = Convert.ToString(this.pos51.Location.X) + "," + Convert.ToString(this.pos51.Location.Y) + ",false";
            this.matrizTabuleiro[5, 2] = Convert.ToString(this.pos52.Location.X) + "," + Convert.ToString(this.pos52.Location.Y) + ",false";
            this.matrizTabuleiro[5, 3] = Convert.ToString(this.pos53.Location.X) + "," + Convert.ToString(this.pos53.Location.Y) + ",false";

            Rei = "";
        }

        public void criaPersonagens()
        {
            cmbPersonagens.Items.Clear();
            string personagens = Jogo.ListarPersonagens();
            personagens = personagens.Replace("\r", "");
            string[] aux = personagens.Split('\n');

            for (int i = 0; i < aux.Length - 1; i++)
            {
                cmbPersonagens.Items.Add(aux[i].Substring(0, 1));
            }

        }

        public void setaTabuleiro()
        {
            string tabuleiro = Jogo.VerificarVez(Convert.ToInt32(idJogador)); 
            tabuleiro = tabuleiro.Replace("\r", "");
            string[] posicoes = tabuleiro.Split('\n');
            string[] personagem;
            if (posicoes.Length > 1)
            {
                for (int i = 1; i < posicoes.Length - 1; i++)
                {
                    personagem = posicoes[i].Split(',');
                    atualizaTabuleiro(personagem[1], Convert.ToInt32(personagem[0]));
                }
            }
        }

        public void atualizaTabuleiro(string personagem, int nivel)
        {
            string[] aux = { };
            Control persona = null;
            foreach (Control con in this.Controls)
            {
                if (con is PictureBox)
                {
                    if (Convert.ToString(con.Tag) == personagem)
                    {
                        persona = con;
                        break;
                    }
                }
            }

            if (nivel == 10)
            {
                Rei = personagem;
                persona.Location = posRei.Location;
                inGame = 3;
                return;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    aux = this.matrizTabuleiro[Convert.ToInt32(nivel), i].Split(',');
                    if (aux[2] == "false")
                    {
                        persona.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                        this.matrizTabuleiro[Convert.ToInt32(nivel), i] = aux[0] + ',' + aux[1] + ',' + personagem;
                        if (inGame != 2) this.cmbPersonagens.Items.Remove(personagem);
                        break;
                    }                 
                }
            }
        }

        public void verificaInicio()
        {
            string Partidas;
            string[] Lista;
            string[] Game;
            Partidas = Jogo.ListarPartidas();

            Partidas = Partidas.Replace("\r", "");
            Lista = Partidas.Split('\n');

            for (int i = 0; i < Lista.Length - 1; i++)
            {
                Game = Lista[i].Split(',');
                if (Game[1] == idPartida && Game[0] == "J")
                {
                    this.btnIniciar_partida.Visible = false;
                    this.afterInitialize.Visible = true;
                    this.inGame = 1;
                    break;
                }

            }
        }

        public void verificaPosicionar()
        {           
            string verificavez = Jogo.VerificarVez(Convert.ToInt32(this.idJogador));
            string[] tabuleiro;

            if (verificavez.Contains("ERRO")) {  MessageBox.Show(verificavez); return; }

            verificavez = verificavez.Replace("\r", "");
            tabuleiro = verificavez.Split('\n');
            this.jogadorDaVez = tabuleiro[0];

            if (cmbPersonagens.Items.Count == 0)
            {
                this.inGame = 2;
                this.label3.Visible = false;
                this.cmbDestino.Visible = false;
                criaPersonagens();
                return;
            }
            

            if (this.jogadorDaVez.Contains(this.idJogador))
            {
                mensagem.Text = "Sua Vez, Faça uma Jogada";
                mensagem.ForeColor = Color.LimeGreen;
                this.btnConfirmarJogada.Enabled = true;
                if (this.aguardandoJogada == false)
                {
                    setarMatriz();
                    setaTabuleiro();
                    this.aguardandoJogada = true;
                }

                if (this.chkAuto.Checked && inGame == 1) autoMovePersonagem();
            } else
            {
                setarMatriz();
                setaTabuleiro();
                mensagem.Text = "Aguarde sua Vez!";
                mensagem.ForeColor = Color.Red;
                this.btnConfirmarJogada.Enabled = false;
            }
        }

        private void verificaVotar()
        {
            string verificavez = Jogo.VerificarVez(Convert.ToInt32(this.idJogador));
            string[] tabuleiro;

            if (verificavez.Contains("ERRO")) { MessageBox.Show(verificavez); return; }

            verificavez = verificavez.Replace("\r", "");
            tabuleiro = verificavez.Split('\n');
            this.jogadorDaVez = tabuleiro[0];

            if (this.jogadorDaVez.Contains(this.idJogador))
            {
                mensagem.Text = "Sua Vez, Informe seu Voto";
                mensagem.ForeColor = Color.LimeGreen;
                this.btnConfirmarJogada.Enabled = true;
            }
            else
            {
                mensagem.Text = "Aguarde sua Vez de Votar!";
                mensagem.ForeColor = Color.Red;
                this.btnConfirmarJogada.Enabled = false;
            }
        }

        private void autoMovePersonagem()
        {
            if (cmbPersonagens.Items.Count == 0)
            {
                this.inGame = 2;
                this.label3.Visible = false;
                this.cmbDestino.Visible = false;
                criaPersonagens();
                return;
            }
            Random rnd = new Random();
            int rand = rnd.Next(0, cmbPersonagens.Items.Count);
            cmbPersonagens.SelectedIndex = rand;
            rand = rnd.Next(0,cmbDestino.Items.Count);
            cmbDestino.SelectedIndex = rand;
            btnConfirmarJogada_Click(new object(), new EventArgs());
        }        

        public void movimentaPersonagem(string personagem, int nivel)
        {
            string tabuleiro = Jogo.ColocarPersonagem(Convert.ToInt32(this.idJogador), this.senhaJogador, nivel, personagem);

            setarMatriz();
            setaTabuleiro();
            if (tabuleiro.Contains("ERRO")) { MessageBox.Show(tabuleiro);}
            this.aguardandoJogada = false;
            return;
        }

        public void promovePersonagem(string personagem)
        {
            string tabuleiro = Jogo.Promover(Convert.ToInt32(this.idJogador), this.senhaJogador, personagem);
            if (tabuleiro.Contains("ERRO"))
            {
                MessageBox.Show(tabuleiro);
                return;
            }
            setarMatriz();
            setaTabuleiro();
            this.aguardandoJogada = false;
            return;
        }

        public void votarRei(string voto)
        { 
            string retorno = Jogo.Votar(Convert.ToInt32(this.idJogador), this.senhaJogador, voto);

            if (retorno.Contains("ERRO"))
            {
                MessageBox.Show(retorno);
                return;
            }

            inGame = 2;
        }

        private void Tabuleiro_Load(object sender, EventArgs e)
        {
            criaPersonagens();
            setarMatriz();

            cmbDestino.Items.Add("1");
            cmbDestino.Items.Add("2");
            cmbDestino.Items.Add("3");
            cmbDestino.Items.Add("4");

            txtId.Text = this.idJogador;
            txtSenha.Text = this.senhaJogador;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (inGame)
            {
                case 0:
                    //caso ingame = 0, verifica o inicio da partida
                    //aguardando inicio da partida
                    verificaInicio();
                    break;
                case 1:
                    //case ingame = 1, verifica se é a vez do jogador de posicionar o personagem
                    //fase de setup
                    verificaPosicionar();
                    break;
                case 2:
                    //caso ingame = 2, verifica se é a vez do jogador de promover um personagem
                    //fase promoção dos personagens
                    verificaPosicionar();
                    break;
                case 3:
                    //caso ingame = 3, votar o Rei
                    //fase promoção dos personagens
                    verificaVotar();
                    break;
            }
        }

        private void btnIniciar_partida_Click(object sender, EventArgs e)
        {
            jogadorDaVez = Jogo.Iniciar(Convert.ToInt32(this.idPartida), this.senhaPartida);
            if (jogadorDaVez.Contains("ERRO"))
            {
                MessageBox.Show(jogadorDaVez);
                jogadorDaVez = "";
                return;
            }
            this.btnIniciar_partida.Visible = false;
            this.afterInitialize.Visible = true;           
        }

        private void btnConfirmarJogada_Click(object sender, EventArgs e)
        {
            switch (inGame)
            {
                case 0:
                    //caso ingame = 0, verifica o inicio da partida
                    //aguardando inicio da partida
                    verificaInicio();
                    break;
                case 1:
                    //case ingame = 1, verifica se é a vez do jogador de posicionar o personagem
                    //fase de setup
                    movimentaPersonagem(this.cmbPersonagens.Text.Substring(0, 1), Convert.ToInt32(this.cmbDestino.Text));
                    break;
                case 2:
                    //caso ingame = 2, verifica se é a vez do jogador de promover um personagem
                    //fase promoção dos personagens
                    promovePersonagem(this.cmbPersonagens.Text.Substring(0, 1));
                    break;
                case 3:
                    //caso ingame = 2, verifica se é a vez do jogador de promover um personagem
                    //fase promoção dos personagens
                    votarRei(this.voto);
                    break;
            }

        }
    }
}
