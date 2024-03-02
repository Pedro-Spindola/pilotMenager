using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pilot_Menager
{
    public partial class TelaInicial : Form
    {
        Principal principal = new Principal();
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            TelaPrincipal telaprincipal = new TelaPrincipal(principal);
            this.Hide();
            principal.ConfigInicioGame = 2;
            telaprincipal.ShowDialog();
            this.Close();
        }

        private void buttonNovoJogo_Click(object sender, EventArgs e)
        {
            TelaNewGame telaNewGame = new TelaNewGame(principal);
            this.Hide();
            principal.ConfigInicioGame = 1;
            telaNewGame.ShowDialog();
            this.Close();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {

        }
    }
}
