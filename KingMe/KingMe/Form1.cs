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
    public partial class frmLOBBY : Form
    {
        public frmLOBBY()
        {
            InitializeComponent();
        }

        private void lstPARTIDAS_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show(MePresidentaServidor.Jogo.ListarPartidas());
            //var listViewItem;
            //listView1.Items.Add(listViewItem);
        }

        private void lstPARTIDAS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
