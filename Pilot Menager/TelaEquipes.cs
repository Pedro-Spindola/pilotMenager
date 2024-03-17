using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pilot_Menager
{
    internal partial class TelaEquipes : Form
    {
        Principal principal;
        Equipes[] equipes;
        Pilotos[] pilotos;
        public TelaEquipes(Principal princ, Equipes[] equip, Pilotos[] pilot)
        {
            InitializeComponent();
            principal = princ;
            equipes = equip;
            pilotos = pilot;
        }
        private void TelaEquipes_Load(object sender, EventArgs e)
        {
            CriarDataGridViewClassEquipes(dvgTelaEquipesExibirTodasEquipes);
            CriarDataGridViewHistoricoDoPiloto(dvgTelaEquipesExibirTodasEquipes);
            PreencherDataGridViewClassEquipes(dvgTelaEquipesExibirTodasEquipes);

            // Manipular o evento CellDoubleClick
            dvgTelaEquipesExibirTodasEquipes.CellDoubleClick += dvgTelaEquipesExibirTodasEquipes_CellContentClick;
        }
        private void dvgTelaEquipesExibirTodasEquipes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique duplo foi feito em uma célula válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int i = Convert.ToInt32(dvgTelaEquipesExibirTodasEquipes.Rows[e.RowIndex].Cells["Index"].Value);
                TpNomeEquipe.Text = equipes[i].NomeEquipe;
                TpPaisEquipe.Text = equipes[i].Sede;

                TpAerodinamica.Text = equipes[i].Aerodinamica.ToString();
                TpFreio.Text = equipes[i].Freio.ToString();
                TpAsaDianteira.Text = equipes[i].AsaDianteira.ToString();
                TpAsaTraseira.Text = equipes[i].AsaTraseira.ToString();
                TpCambio.Text = equipes[i].Cambio.ToString();
                TpEletrico.Text = equipes[i].Eletrico.ToString();
                TpDirecao.Text = equipes[i].Direcao.ToString();
                TpConfiabilidade.Text = equipes[i].Confiabilidade.ToString();

                TpSalarioPiloto1.Text = string.Format("R$ {0:N2}", equipes[i].PrimeiroPilotoSalario);
                TpContratoPiloto1.Text = string.Format("{0} Anos", equipes[i].PrimeiroPilotoContrato);

                TpSalarioPiloto1.Text = string.Format("R$ {0:N2}", equipes[i].SegundoPilotoSalario);
                TpContratoPiloto1.Text = string.Format("{0} Anos", equipes[i].SegundoPilotoContrato);

                TpMotor.Text = equipes[i].NameMotor;

                PreencherDataGridViewHistoricoPilotos(equipes[i].equipeTemporadas, dvgTelaEquipesExibirTodasEquipes);
                AtualizarTabelas(dvgTelaEquipesExibirTodasEquipes);

                Color corPrincipal;
                Color corSecundaria;
                corPrincipal = ColorTranslator.FromHtml(equipes[i].Cor1);
                corSecundaria = ColorTranslator.FromHtml(equipes[i].Cor2);

                TpLabelCor1A.BackColor = corPrincipal;
                TpLabelCor1B.BackColor = corSecundaria;
                TpLabelCor3A.BackColor = corPrincipal;
                TpLabelCor3B.BackColor = corSecundaria;
            }
        }
        private void CriarDataGridViewClassEquipes(DataGridView dataGridViewEquipes)
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("#", typeof(int));
            classEquipes.Columns.Add("C1", typeof(string));
            classEquipes.Columns.Add(sedeColumn);
            classEquipes.Columns.Add("Nome", typeof(string));
            classEquipes.Columns.Add("P", typeof(int));
            classEquipes.Columns.Add("1º", typeof(int));
            classEquipes.Columns.Add("2º", typeof(int));
            classEquipes.Columns.Add("3º", typeof(int));
            classEquipes.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Sede";
            imageColumn.Name = "Sede";
            imageColumn.DataPropertyName = "Sede";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dataGridViewEquipes.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dataGridViewEquipes.RowHeadersVisible = false;
            dataGridViewEquipes.Enabled = false;
            dataGridViewEquipes.ScrollBars = ScrollBars.None;
            dataGridViewEquipes.AllowUserToAddRows = false;
            dataGridViewEquipes.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dataGridViewEquipes.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewEquipes.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewEquipes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEquipes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewEquipes.DataSource = classEquipes;

            // Altura das linhas
            dataGridViewEquipes.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dataGridViewEquipes.ColumnHeadersHeight = 30;


            // Defina a ordem de exibi��o das colunas com base nos �ndices
            dataGridViewEquipes.Columns["#"].DisplayIndex = 0;
            dataGridViewEquipes.Columns["C1"].DisplayIndex = 1;
            dataGridViewEquipes.Columns["Sede"].DisplayIndex = 2;
            dataGridViewEquipes.Columns["Nome"].DisplayIndex = 3;
            dataGridViewEquipes.Columns["P"].DisplayIndex = 4;
            dataGridViewEquipes.Columns["1º"].DisplayIndex = 5;
            dataGridViewEquipes.Columns["2º"].DisplayIndex = 6;
            dataGridViewEquipes.Columns["3º"].DisplayIndex = 7;
            dataGridViewEquipes.Columns["Path"].DisplayIndex = 8;

            dataGridViewEquipes.Columns["Path"].Visible = false;
            dataGridViewEquipes.Columns["C1"].HeaderText = string.Empty;

            dataGridViewEquipes.Columns[0].Width = 30;
            dataGridViewEquipes.Columns[1].Width = 10;
            dataGridViewEquipes.Columns[2].Width = 40;
            dataGridViewEquipes.Columns[3].Width = 250;
            dataGridViewEquipes.Columns[4].Width = 50;
            dataGridViewEquipes.Columns[5].Width = 40;
            dataGridViewEquipes.Columns[6].Width = 40;
            dataGridViewEquipes.Columns[7].Width = 40;

        }
        private void PreencherDataGridViewClassEquipes(DataGridView dataGridViewEquipes)
        {
            DataTable classEquipes = (DataTable)dataGridViewEquipes.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classEquipes.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < equipes.Length; i++)
            {
                DataRow row = classEquipes.NewRow();
                row["#"] = equipes[i].PosicaoAtualCampeonato;
                row["C1"] = equipes[i].Cor1;
                row["Nome"] = equipes[i].NomeEquipe;
                row["P"] = equipes[i].PontosCampeonato;
                row["1º"] = equipes[i].PrimeiroColocado;
                row["2º"] = equipes[i].SegundoColocado;
                row["3º"] = equipes[i].TerceiroColocado;
                row["Path"] = Path.Combine("Paises", equipes[i].Sede + ".png");
                classEquipes.Rows.Add(row);
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classEquipes.Rows)
            {
                string imagePath = row["Path"].ToString();
                if (!string.IsNullOrEmpty(imagePath)) // Verifica se o caminho do arquivo n�o est� vazio
                {
                    row["Sede"] = Image.FromFile(imagePath);
                }
            }
            // Atualize o DataGridView para refletir as mudan�as
            dataGridViewEquipes.DataSource = classEquipes;

            // Limpe a seleção inicial
            dataGridViewEquipes.ClearSelection();

        }
        private void CriarDataGridViewHistoricoDoPiloto(DataGridView dgv)
        {
            DataTable histoticoEquipe = new DataTable();

            histoticoEquipe.Columns.Add("#", typeof(int));
            histoticoEquipe.Columns.Add("Ano", typeof(int));
            histoticoEquipe.Columns.Add("Motor", typeof(string));
            histoticoEquipe.Columns.Add("C1", typeof(string));
            histoticoEquipe.Columns.Add("Equipe", typeof(string));
            histoticoEquipe.Columns.Add("P", typeof(string));
            histoticoEquipe.Columns.Add("Serie", typeof(string));

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

            dgv.DataSource = histoticoEquipe;

            // Altura das linhas
            dgv.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;

            // Defina a ordem de exibiçao das colunas com base nos índices
            dgv.Columns["#"].DisplayIndex = 0;
            dgv.Columns["Ano"].DisplayIndex = 1;
            dgv.Columns["Motor"].DisplayIndex = 2;
            dgv.Columns["C1"].DisplayIndex = 3;
            dgv.Columns["Equipe"].DisplayIndex = 4;
            dgv.Columns["P"].DisplayIndex = 5;
            dgv.Columns["Serie"].DisplayIndex = 6;

            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 30;
            dgv.Columns[1].Width = 40;
            dgv.Columns[2].Width = 70;
            dgv.Columns[3].Width = 10;
            dgv.Columns[4].Width = 120;
            dgv.Columns[5].Width = 50;
            dgv.Columns[6].Width = 70;
        }
        private void PreencherDataGridViewHistoricoPilotos(List<Equipes.EquipeTemporadas> equipeTemporadas, DataGridView dgv)
        {
            DataTable histoticoEquipe = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            histoticoEquipe.Rows.Clear();

            // Adiciona cada piloto campeão como uma nova linha no DataGridView
            foreach (var piloto in equipeTemporadas)
            {
                // Cria uma nova linha no DataTable
                DataRow row = histoticoEquipe.NewRow();


                // Adiciona os dados do piloto à linha do DataGridView
                row["#"] = piloto.Position;
                row["Ano"] = piloto.Ano;
                row["Motor"] = piloto.Motor;
                row["C1"] = piloto.C1;
                row["Equipe"] = piloto.Equipe;
                row["P"] = piloto.Pontos;
                row["Serie"] = piloto.CategoriaAtual;

                // Adiciona a linha ao DataTable
                histoticoEquipe.Rows.Add(row);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = histoticoEquipe;
        }
        public void AtualizarTabelas(DataGridView dgv)
        {
            DataTable histoticoEquipe = (DataTable)dgv.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Ordene automaticamente a coluna 4 do maior para o menor
            dgv.Sort(dgv.Columns[1], ListSortDirection.Descending);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgv.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgv.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgv.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            dgv.ClearSelection();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
