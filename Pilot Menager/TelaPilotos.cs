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
                TpHabPiloto.Text = pilotos[i].MediaPiloto.ToString();

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
        private void CriarDataGridViewHistoricoDoPiloto(DataGridView dgv)
        {
            DataTable histoticoPiloto = new DataTable();

            histoticoPiloto.Columns.Add("#", typeof(int));
            histoticoPiloto.Columns.Add("Ano", typeof(int));
            histoticoPiloto.Columns.Add("C1", typeof(string));
            histoticoPiloto.Columns.Add("Equipe", typeof(string));
            histoticoPiloto.Columns.Add("P", typeof(string));
            histoticoPiloto.Columns.Add("Serie", typeof(string));

            // Configurando Layout
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ScrollBars = ScrollBars.Vertical;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.GridColor = Color.FromArgb(220, 220, 220);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.DataSource = histoticoPiloto;

            // Altura das linhas
            dgv.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;

            // Defina a ordem de exibiçao das colunas com base nos índices
            dgv.Columns["#"].DisplayIndex = 0;
            dgv.Columns["Ano"].DisplayIndex = 1;
            dgv.Columns["C1"].DisplayIndex = 2;
            dgv.Columns["Equipe"].DisplayIndex = 3;
            dgv.Columns["P"].DisplayIndex = 4;
            dgv.Columns["Serie"].DisplayIndex = 5;

            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 35;
            dgv.Columns[1].Width = 45;
            dgv.Columns[2].Width = 10;
            dgv.Columns[3].Width = 140;
            dgv.Columns[4].Width = 60;
            dgv.Columns[5].Width = 100;
        }
        /*
        private void PreencherDataGridViewHistoricoPilotos(List<Pilotos.PilotoTemporadas> pilotosTemporadas, DataGridView dgv)
        {
            DataTable histoticoPiloto = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            histoticoPiloto.Rows.Clear();

            // Adiciona cada piloto campeão como uma nova linha no DataGridView
            foreach (var piloto in pilotosTemporadas)
            {
                // Cria uma nova linha no DataTable
                DataRow row = histoticoPiloto.NewRow();

                row["Path"] = Path.Combine("Paises", piloto.Sede + ".png");
                string path = row["Path"].ToString();

                // Carrega a imagem da sede do piloto
                Image sedeImage = Image.FromFile(path);

                // Adiciona os dados do piloto à linha do DataGridView
                row["#"] = piloto.;
                row["Ano"] = piloto.Ano;
                row["C1"] = sedeImage;
                row["Equipe"] = piloto.Nome;
                row["P"] = piloto.Pontos;
                row["Serie"] = piloto.Equipe;

                // Adiciona a linha ao DataTable
                histoticoPiloto.Rows.Add(row);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = histoticoPiloto;
        }*/
        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
