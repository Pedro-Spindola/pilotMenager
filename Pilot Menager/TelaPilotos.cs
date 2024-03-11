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
    internal partial class TelaPilotos : Form
    {
        Principal principal;
        Equipes[] equipes;
        Pilotos[] pilotos;
        Pistas[] pistas;
        public TelaPilotos(Principal princ, Equipes[] equip, Pilotos[] pilot, Pistas[] pist)
        {
            InitializeComponent();
            principal = princ;
            equipes = equip;
            pilotos = pilot;
            pistas = pist;

            // Manipular o evento CellDoubleClick
            dvgTelaPilotoExibirTodosPilotos.CellDoubleClick += DataGridViewPilotos_CellDoubleClick;
        }

        private void TelaPilotos_Load(object sender, EventArgs e)
        {
            CriarDataGridViewClassPilotos(dvgTelaPilotoExibirTodosPilotos);
            PreencherDataGridViewClassPilotos(dvgTelaPilotoExibirTodosPilotos);
        }


        private void DataGridViewPilotos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            // Verifica se o clique duplo foi feito em uma célula válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int i = Convert.ToInt32(dvgTelaPilotoExibirTodosPilotos.Rows[e.RowIndex].Cells["Index"].Value);
                TpNomeCompletoPiloto.Text = pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto;
                TpIdadePiloto.Text = pilotos[i].IdadePiloto.ToString() + " Anos";
                TpPaisPiloto.Text = pilotos[i].NacionalidadePiloto;

                TpLargada.Text = pilotos[i].Largada.ToString();
                TpConcentracao.Text = pilotos[i].Concentracao.ToString();
                TpUltrapassagem.Text = pilotos[i].Ultrapassagem.ToString();
                TpExperiencia.Text = pilotos[i].Experiencia.ToString();
                TpRapidez.Text = pilotos[i].Rapidez.ToString();
                TpChuva.Text = pilotos[i].Chuva.ToString();
                TpAcertoDoCarro.Text = pilotos[i].AcertoDoCarro.ToString();
                TpFisico.Text = pilotos[i].Fisico.ToString();
            }
        }
        private void CriarDataGridViewClassPilotos(DataGridView dataGridViewPilotos)
        {
            DataTable classPilotos = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            classPilotos.Columns.Add("#", typeof(int));
            classPilotos.Columns.Add(sedeColumn);
            classPilotos.Columns.Add("Nome", typeof(string));
            classPilotos.Columns.Add("Idade", typeof(string));
            classPilotos.Columns.Add("Equipe", typeof(string));
            classPilotos.Columns.Add("Contrato", typeof(string));
            classPilotos.Columns.Add("Path", typeof(string));
            classPilotos.Columns.Add("Index", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dataGridViewPilotos.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dataGridViewPilotos.RowHeadersVisible = false;
            dataGridViewPilotos.AllowUserToAddRows = false;
            dataGridViewPilotos.AllowUserToDeleteRows = false;
            dataGridViewPilotos.AllowUserToOrderColumns = false;
            dataGridViewPilotos.AllowUserToResizeColumns = false;
            dataGridViewPilotos.AllowUserToResizeColumns = false;
            dataGridViewPilotos.AllowUserToResizeRows = false;
            dataGridViewPilotos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewPilotos.ScrollBars = ScrollBars.Vertical;
            dataGridViewPilotos.AllowUserToAddRows = false;
            dataGridViewPilotos.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dataGridViewPilotos.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewPilotos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dataGridViewPilotos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewPilotos.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewPilotos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPilotos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewPilotos.DataSource = classPilotos;

            // Altura das linhas
            dataGridViewPilotos.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dataGridViewPilotos.ColumnHeadersHeight = 30;

            // Defina a ordem de exibição das colunas com base nos índices
            dataGridViewPilotos.Columns["#"].DisplayIndex = 0;
            dataGridViewPilotos.Columns["Nac"].DisplayIndex = 1;
            dataGridViewPilotos.Columns["Nome"].DisplayIndex = 2;
            dataGridViewPilotos.Columns["Idade"].DisplayIndex = 3;
            dataGridViewPilotos.Columns["Equipe"].DisplayIndex = 4;
            dataGridViewPilotos.Columns["Contrato"].DisplayIndex = 5;
            dataGridViewPilotos.Columns["Path"].DisplayIndex = 6;
            dataGridViewPilotos.Columns["Index"].DisplayIndex = 7;

            dataGridViewPilotos.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewPilotos.Columns["Path"].Visible = false;

            dataGridViewPilotos.Columns[0].Width = 30;
            dataGridViewPilotos.Columns[1].Width = 40;
            dataGridViewPilotos.Columns[2].Width = 140;
            dataGridViewPilotos.Columns[3].Width = 80;
            dataGridViewPilotos.Columns[4].Width = 100;
            dataGridViewPilotos.Columns[5].Width = 100;
        }
        private void PreencherDataGridViewClassPilotos(DataGridView dataGridViewPilotos)
        {

            DataTable classPilotos = (DataTable)dataGridViewPilotos.DataSource;


            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                row["#"] = i + 1;
                row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                row["Idade"] = pilotos[i].IdadePiloto + " Anos";
                row["Equipe"] = pilotos[i].EquipePiloto;
                row["Contrato"] = pilotos[i].ContratoPiloto;
                row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");
                row["Index"] = i;

                classPilotos.Rows.Add(row);
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"].ToString();
                row["Nac"] = Image.FromFile(imagePath);
            }
            // Atualize o DataGridView para refletir as mudan�as
            dataGridViewPilotos.DataSource = classPilotos;

            // Limpe a seleção inicial
            dataGridViewPilotos.ClearSelection();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
