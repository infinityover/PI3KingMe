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
        public string[] inGame = null;
        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public string[,] matrizTabuleiro { get; set; } = new string[6, 4];
        public string posicaoOperario { get; set; }
        public string Rei { get; private set; }
        public bool FimSetup { get; private set; }
        public string voto = "";
        public bool aguardarJogada = false;
        public string status_jogo = "";

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

        public void posicoesDefault()
        {
            this.A.Location = new Point(371, 16);
            this.B.Location = new Point(371, 72);
            this.C.Location = new Point(371, 128);
            this.D.Location = new Point(371, 184);
            this.E.Location = new Point(424, 16);
            this.G.Location = new Point(424, 128);
            this.F.Location = new Point(424, 72);
            this.P.Location = new Point(530, 16);
            this.I.Location = new Point(424, 184);
            this.O.Location = new Point(477, 184);
            this.N.Location = new Point(477, 128);
            this.L.Location = new Point(477, 16);
            this.M.Location = new Point(477, 72);
        }

        public void setarCmbPersonagens()
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

        public void setarTabuleiro()
        {
            string tabuleiro = Jogo.VerificarVez(Convert.ToInt32(idJogador));
            tabuleiro = tabuleiro.Replace("\r", "");
            string[] posicoes = tabuleiro.Split('\n');
            string[] personagem;
            string[] aux = { };
            Control persona = null;

            if (posicoes.Length > 2)
            {
                for (int i = 1; i < posicoes.Length - 1; i++)
                {
                    personagem = posicoes[i].Split(',');
                    
                    foreach (Control con in this.Controls)
                    {
                        if (con is PictureBox)
                        {
                            if (Convert.ToString(con.Tag) == personagem[1])
                            {
                                persona = con;
                                break;
                            }
                        }
                    }

                    if (Convert.ToInt32(personagem[0]) == 10)
                    {
                        Rei = personagem[1];
                        persona.Location = posRei.Location;
                        return;
                    }
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            aux = this.matrizTabuleiro[Convert.ToInt32(Convert.ToInt32(personagem[0])), j].Split(',');
                            if (aux[2] == "false")
                            {
                                persona.Location = new Point(Convert.ToInt32(aux[0]), Convert.ToInt32(aux[1]));
                                this.matrizTabuleiro[Convert.ToInt32(Convert.ToInt32(personagem[0])), j] = aux[0] + ',' + aux[1] + ',' + personagem[1];
                                if (inGame[1] == "S") this.cmbPersonagens.Items.Remove(personagem[1]);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void aguardandoInicioDaPartida()
        {
            this.btnIniciar_partida.Visible = true;
            this.afterInitialize.Visible = false;
        }

        public void controlsProperts()
        {
            switch (inGame[0])
            {
                case "A":
                    afterInitialize.Visible = false;
                    btnIniciar_partida.Visible = true;
                    statusVez.Visible = false;
                    break;
                case "J":
                    switch (inGame[1])
                    {
                        case "S":
                            afterInitialize.Visible = true;
                            lblSetor.Visible = true;
                            cmbSetor.Visible = true;
                            lblPersonagem.Visible = true;
                            cmbPersonagens.Visible = true;
                            btnIniciar_partida.Visible = false;
                            statusVez.Visible = true;
                            break;
                        case "J":
                            afterInitialize.Visible = true;
                            lblSetor.Visible = false;
                            cmbSetor.Visible = false;
                            lblPersonagem.Visible = true;
                            cmbPersonagens.Visible = true;
                            btnIniciar_partida.Visible = false;
                            statusVez.Visible = true;
                            break;
                        case "V":
                            afterInitialize.Visible = true;
                            lblSetor.Visible = false;
                            cmbSetor.Visible = false;
                            lblPersonagem.Visible = false;
                            cmbPersonagens.Visible = false;
                            btnIniciar_partida.Visible = false;
                            statusVez.Visible = true;
                            break;
                    }
                    break;
                case "E":
                    afterInitialize.Visible = false;
                    btnIniciar_partida.Visible = false;
                    statusVez.Visible = true;
                    break;
            } 
        }

        public void faseDeSetup()
        {
            controlsProperts();
            string verificavez = Jogo.VerificarVez(Convert.ToInt32(this.idJogador));
            string[] tabuleiro;

            verificavez = verificavez.Replace("\r", "");
            tabuleiro = verificavez.Split('\n');
            jogadorDaVez = tabuleiro[0];

            if (inGame[1] != status_jogo)
            {
                status_jogo = inGame[1];
                this.posicoesDefault();
            }

            if (jogadorDaVez.Contains(idJogador))
            {
                statusVez.Text = "Sua Vez, Coloque um Personagem";
                statusVez.ForeColor = Color.LimeGreen;
                btnConfirmarJogada.Enabled = true;
                if (aguardarJogada == false)
                {
                    setarMatriz();
                    setarTabuleiro();
                    aguardarJogada = true;
                }

                if (chkAuto.Checked) autoMovePersonagemSetup();
            } else
            {
                setarMatriz();
                setarTabuleiro();
                statusVez.Text = "Aguarde sua Vez!";
                statusVez.ForeColor = Color.Red;
                btnConfirmarJogada.Enabled = false;
            }
        }

        public void faseDePromocao()
        {
            controlsProperts();
            setarCmbPersonagens();
            string verificavez = Jogo.VerificarVez(Convert.ToInt32(this.idJogador));
            string[] tabuleiro;

            verificavez = verificavez.Replace("\r", "");
            tabuleiro = verificavez.Split('\n');
            jogadorDaVez = tabuleiro[0];

            if ("V" == status_jogo)
            {
                status_jogo = inGame[1];
                this.posicoesDefault();
            }

            if (jogadorDaVez.Contains(idJogador))
            {
                statusVez.Text = "Sua Vez, Promova um Personagem";
                statusVez.ForeColor = Color.LimeGreen;
                btnConfirmarJogada.Enabled = true;
                if (aguardarJogada == false)
                {
                    setarMatriz();
                    setarTabuleiro();
                    aguardarJogada = true;
                }

                if (chkAuto.Checked) autoMovePersonagemPromover();
            }
            else
            {
                setarMatriz();
                setarTabuleiro();
                statusVez.Text = "Aguarde sua Vez!";
                statusVez.ForeColor = Color.Red;
                btnConfirmarJogada.Enabled = false;
            }
        }

        private void faseDeVotacao()
        {
            controlsProperts();
            string verificavez = Jogo.VerificarVez(Convert.ToInt32(this.idJogador));
            string[] tabuleiro;

            setarMatriz();
            setarTabuleiro();

            verificavez = verificavez.Replace("\r", "");
            tabuleiro = verificavez.Split('\n');
            jogadorDaVez = tabuleiro[0];

            if (jogadorDaVez.Contains(this.idJogador))
            {
                statusVez.Text = "Sua Vez, Informe seu Voto";
                statusVez.ForeColor = Color.LimeGreen;
                btnConfirmarJogada.Enabled = true;
                if (chkAuto.Checked) autoVotar();
                return;
            }
            else
            {
                statusVez.Text = "Aguarde sua vez!";
                statusVez.ForeColor = Color.Red;
                btnConfirmarJogada.Enabled = false;
            }
        }

        private void autoMovePersonagemSetup()
        {
            if (cmbPersonagens.Items.Count == 0) { return; }
            Random rnd = new Random();
            int rand = rnd.Next(0, cmbPersonagens.Items.Count);
            cmbPersonagens.SelectedIndex = rand;
            rand = rnd.Next(0,cmbSetor.Items.Count);
            cmbSetor.SelectedIndex = rand;
            btnConfirmarJogada_Click(new object(), new EventArgs());
        }    
        
        private void autoMovePersonagemPromover()
        {
            if (cmbPersonagens.Items.Count == 0) { return; }
            Random rnd = new Random();
            int rand = rnd.Next(0, cmbPersonagens.Items.Count);
            cmbPersonagens.SelectedIndex = rand;
            btnConfirmarJogada_Click(new object(), new EventArgs());
        }

        private void autoVotar()
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, 3);
            if (rand == 0) { rdbNao.Select(); }
            else { rdbSim.Select(); }
            btnConfirmarJogada_Click(new object(), new EventArgs());
        }

        public void movimentaPersonagem(string personagem, int setor)
        {
            string teste = Jogo.VerificarVez(Convert.ToInt32(idJogador));
            string tabuleiro = Jogo.ColocarPersonagem(Convert.ToInt32(idJogador), senhaJogador, setor, personagem);

            setarMatriz();
            setarTabuleiro();
            if (tabuleiro.Contains("ERRO"))
            {
                //this.autoMovePersonagemSetup();
                return;
            }
            aguardarJogada = false;
            return;
        }

        public void promovePersonagem(string personagem)
        {
            string tabuleiro = Jogo.Promover(Convert.ToInt32(idJogador), senhaJogador, personagem);
            if (tabuleiro.Contains("ERRO"))
            {
                //autoMovePersonagemPromover();
                return;
            }
            setarMatriz();
            setarTabuleiro();
            aguardarJogada = false;
            return;
        }

        public void votarRei()
        {
            if (voto == "")
            {
                MessageBox.Show("Escolha uma opção de voto!");
                return;
            }

            string retorno = Jogo.Votar(Convert.ToInt32(idJogador), senhaJogador, voto);

            if (retorno.Contains("ERRO"))
            {
                MessageBox.Show(retorno);
                return;
            }
        }

        private void fimDeJogo()
        {
            controlsProperts();
            statusVez.Text = "Fim de Jogo";
            MessageBox.Show(Jogo.ListarJogadores(Convert.ToInt32(idPartida)));
            this.Close();
        }

        private void Tabuleiro_Load(object sender, EventArgs e)
        {
            setarCmbPersonagens();
            setarMatriz();
            cmbSetor.Items.Add("1");
            cmbSetor.Items.Add("2");
            cmbSetor.Items.Add("3");
            cmbSetor.Items.Add("4");

            cmbTempo.SelectedIndex = 1;
            txtId.Text = this.idJogador;
            txtSenha.Text = this.senhaJogador;
            lblVersion.Text = "Version: "+Jogo.versao;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.txtcartas.Text = Jogo.ListarCartas(Convert.ToInt32(this.idJogador), this.senhaJogador);
            this.txtjogadores.Text = Jogo.ListarJogadores(Convert.ToInt32( this.idPartida));
            string votos = Jogo.ExibirUltimaVotacao(Convert.ToInt32(this.idJogador), this.senhaJogador);
            if (!votos.Contains("ERRO")) txtVotos.Text = votos;
            this.timer1.Enabled = false;
            String Temp = Jogo.VerificarStatus(Convert.ToInt32(idJogador));
            inGame = Temp.Split(',');

            switch (inGame[0])
            {
                case "A": 
                    aguardandoInicioDaPartida();
                    break;
                case "J":
                    emJogo();
                    break;
                case "E":
                    fimDeJogo();
                    break;
            }

            this.timer1.Enabled = true;
        }

        private void emJogo()
        {
            switch (inGame[1])
            {
                case "S":
                    faseDeSetup();
                    break;
                case "J":
                    //status_jogo = inGame[1];
                    faseDePromocao();
                    break;
                case "V":
                    status_jogo = inGame[1];
                    faseDeVotacao();
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
            if (inGame[0] == "E" && inGame[0] == "A") { return; } 
            switch (inGame[1])
            {
                case "S":
                    movimentaPersonagem(this.cmbPersonagens.Text.Substring(0, 1), Convert.ToInt32(this.cmbSetor.Text));
                    break;
                case "J":
                    promovePersonagem(this.cmbPersonagens.Text.Substring(0, 1));
                    break;
                case "V":
                    votarRei();
                    break;
            }

        }

        private void rdbSim_CheckedChanged(object sender, EventArgs e)
        {
            this.voto = "S";
        }

        private void rdbNao_CheckedChanged(object sender, EventArgs e)
        {
            this.voto = "N";
        }

        private void cmbTempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.timer1.Interval = Convert.ToInt32(cmbTempo.Text) * 1000;
        }
    }
}
