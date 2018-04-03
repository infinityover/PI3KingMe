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
    public partial class Entrar_Nova : Form
    {
        public Entrar_Nova()
        {
            InitializeComponent();
        }

        public string idPartida { get; set; }
        public string senhaPartida { get; set; }
        public string idJogador { get; set; }
        public string senhaJogador { get; set; }
        public string jogadorDaVez { get; set; }
        public Boolean kill { get; set; }


        private void Entrar_Nova_Load(object sender, EventArgs e)
        {
            string Aux1;
            string[] Aux2;
            string[] Aux3;
            string[] Aux4;

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            //Add column header
            listView1.Columns.Add("ID", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("NOME", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("CRIAÇÃO", 78, HorizontalAlignment.Center);

            Aux1 = MePresidentaServidor.Jogo.ListarPartidas();
            Aux1 = Aux1.Replace("\r", "");
            Aux2 = Aux1.Split('\n');
 
            ListViewItem itm;

            for (int i=0; i<Aux2.Length - 1; i++)
            {
                Aux3 = Aux2[i].Split(',');
                Aux4 = Aux3;
                if (Aux3[0] == "A")
                {
                    Aux4[0] = Aux3[1];
                    Aux4[1] = Aux3[3];
                    Aux4[2] = Aux3[2];

                    itm = new ListViewItem(Aux4);
                    listView1.Items.Add(itm);
                }
            }
        }

        private void btnIniciarJogo_Click(object sender, EventArgs e)
        {
            if (this.txtNomeJogador.Text != "")
            {
                string Aux1;
                string[] Aux2;

                for (int i=0; i<listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Checked == true)
                    {
                        this.idPartida = listView1.Items[i].Text;
                    }
                }

                Aux1 = MePresidentaServidor.Jogo.Entrar(Convert.ToInt32(this.idPartida), this.txtNomeJogador.Text, this.senhaPartida);
                Aux2 = Aux1.Split(',');

                this.idJogador = Aux2[0];
                this.senhaJogador = Aux2[1];

                kill = false;
                this.Close();
    }
            else
            {
                MessageBox.Show("Digite um Nome e Senha para entrar na partida");
            }
        }

        private void txtSenhaPartida_TextChanged(object sender, EventArgs e)
        {
            this.senhaPartida = this.txtSenhaPartida.Text;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Entrar_Nova_FormClosed(object sender, FormClosedEventArgs e)
        {
            kill = true;
            this.Close();
        }
    }
}
