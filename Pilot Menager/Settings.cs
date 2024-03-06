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
    public partial class TelaSettings : Form
    {
        Principal principal;
        public TelaSettings(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }
        private void TelaSettings_Load(object sender, EventArgs e)
        {
            MessageBox.Show("telaSettings");
        }
        private void comboBoxSelectCor_SelectedIndexChanged(object sender, EventArgs e)
        {
            principal.CorTexto = comboBoxSelectCor.Text;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
