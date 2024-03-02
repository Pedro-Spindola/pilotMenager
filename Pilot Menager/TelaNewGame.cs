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
    public partial class TelaNewGame : Form
    {
        Principal principal;
        public TelaNewGame(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }
        private void TelaNewGame_Load(object sender, EventArgs e)
        {
            List<Pais> nomesPais = Pais.ObterListaDePaises();

            listEscolheNacionalidade.DataSource = nomesPais;
            listEscolheNacionalidade.DisplayMember = "Nome";
        }
        private void inputNomePiloto_TextChanged(object sender, EventArgs e)
        {
            principal.NomeJogador = inputNomePiloto.Text;
        }
        private void inputSobrenomePiloto_TextChanged(object sender, EventArgs e)
        {
            principal.SobrenomeJogador = inputSobrenomePiloto.Text;
        }

        private void listEscolheNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            principal.NacionalidadeJogador = listEscolheNacionalidade.Text;
        }
        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(principal.NomeJogador))
            {
                if (!string.IsNullOrEmpty(principal.SobrenomeJogador))
                {
                    TelaPrincipal telaprincipal = new TelaPrincipal(principal);
                    this.Hide();
                    telaprincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sobrenome do jogador não pode ser em branco.");
                }
            }
            else
            {
                MessageBox.Show("Nome do jogador não pode ser em branco.");
                MessageBox.Show(principal.NomeJogador);
            }
        }
    }
}
