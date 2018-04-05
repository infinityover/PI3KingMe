using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingMe
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnCriarPartida_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            string nome = "Criar_Partida";
            Tabuleiro form = new Tabuleiro(nome);
            form.ShowDialog();
            this.Close();
        }

        private void btnEntrarPartidaNova_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            string nome = "Entrar_Nova";
            Tabuleiro form = new Tabuleiro(nome);
            try{
                form.ShowDialog();
            }
            catch( Exception EX)
            {
            }
            
            form.Close();
            this.Close();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }
    }
}
