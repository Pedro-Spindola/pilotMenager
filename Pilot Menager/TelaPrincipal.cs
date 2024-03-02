/*using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Text.Json;*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using System.Diagnostics;

namespace Pilot_Menager
{
    public partial class TelaPrincipal : Form
    {
        Principal principal;
        private Equipes[] equipes = new Equipes[30];    // Criando array com a quantidade de equipes.
        private Pilotos[] pilotos = new Pilotos[60];    // Criando array com a quantidade de pilotos.
        private Pistas[] pistas = new Pistas[3];        // Crinado array com a quantidade de pistas. 
        private Random random = new Random();
        private Financias financias = new Financias();

        public TelaPrincipal(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }
        private void TelaPrincipal_Load(object sender, EventArgs e)
        {
            if (principal.ConfigInicioGame == 1)
            {
                IniciarNovoGame();
            }
            else if (principal.ConfigInicioGame == 2)
            {
                CarregarDadosDosArquivos();
            }

            AtualizarNomes();
            AtualizarFinancias();
            AtualizarDate();

            CriarDataGridViewClassEquipes();
            CriarDataGridViewClassPilotos();
            PreencherDataGridViewClassEquipes();
            PreencherDataGridViewClassPilotos();

            AtualizarTabelas();
        }
        public void IniciarNovoGame()
        {
            // Gerando um array de Pilotos
            for (int i = 0; i < pilotos.Length; i++)
            {
                Pilotos piloto = new Pilotos();
                if (i == 0)
                {
                    piloto.geraPiloto(principal.NomeJogador, principal.SobrenomeJogador, principal.NacionalidadeJogador);
                    pilotos[i] = piloto;
                }
                else
                {
                    piloto.geraPiloto();
                    pilotos[i] = piloto;
                }
            }

            // Gerando as Equipes
            // Equipes F1
            equipes[0] = new Equipes("Red Bull", "#03183B", "#C70101", "#FFFFFF", "Austria", 95, 95, 95, 95, 95, 95, 95, 95, "Honda", "F1");
            equipes[1] = new Equipes("Mercedes", "#C4C4C4", "#09BF81", "#000000", "Alemanha", 85, 85, 85, 85, 85, 85, 85, 85, "Mercedes", "F1");
            equipes[2] = new Equipes("Ferrari", "#FF0000", "#FFFFFF", "#000000", "Itália", 85, 85, 85, 85, 85, 85, 85, 85, "Ferrari", "F1");
            equipes[3] = new Equipes("Williams", "#112685", "#FFFFFF", "#FFFFFF", "Inglaterra", 75, 75, 75, 75, 75, 75, 75, 75, "TAG", "F1");
            equipes[4] = new Equipes("Aston Martin", "#004039", "#FFFFFF", "#FFFFFF", "Inglaterra", 80, 80, 80, 80, 80, 80, 80, 80, "Mercedes", "F1");
            equipes[5] = new Equipes("McLaren", "#FF8D36", "#000000", "#FFFFFF", "Inglaterra", 90, 90, 90, 90, 90, 90, 90, 90, "Honda", "F1");
            equipes[6] = new Equipes("Alpine", "#CE4A8D", "#2075DC", "#000000", "França", 80, 80, 80, 80, 80, 80, 80, 80, "Renault", "F1");
            equipes[7] = new Equipes("Visa Cash", "#0456D9", "#B10407", "#000000", "Itália", 75, 75, 75, 75, 75, 75, 75, 75, "TAG", "F1");
            equipes[8] = new Equipes("Stake Sauber", "#000000", "#0BEE23", "#FFFFFF", "Suíça", 70, 70, 70, 70, 70, 70, 70, 70, "Ferrari", "F1");
            equipes[9] = new Equipes("Haas", "#002420", "#000000", "#FFFFFF", "Estados Unidos", 70, 70, 70, 70, 70, 70, 70, 70, "Ferrari", "F1");

            // Equipes F2
            equipes[10] = new Equipes("MP Motorsport", "#FF883C", "#FF883C", "#FFFFFF", "Holanda", 65, 65, 65, 65, 65, 65, 65, 65, "TAG", "F2");
            equipes[11] = new Equipes("Infinity Audi", "#CCCCCC", "#991F21", "#000000", "Alemanha", 65, 65, 65, 65, 65, 65, 65, 65, "Audi", "F2");
            equipes[12] = new Equipes("Carlin", "#2151B0", "#75FF07", "#000000", "Inglaterra", 60, 60, 60, 60, 60, 60, 60, 60, "Renault", "F2");
            equipes[13] = new Equipes("Jordan", "#FFE120", "#000000", "#FFFFFF", "Inglaterra", 60, 60, 60, 60, 60, 60, 60, 60, "Mercedes", "F2");
            equipes[14] = new Equipes("Prema", "#FF3622", "#FFFFFF", "#000000", "Itália", 55, 55, 55, 55, 55, 55, 55, 55, "TAG", "F2");
            equipes[15] = new Equipes("Hitech", "#808080", "#000000", "#000000", "Inglaterra", 50, 50, 50, 50, 50, 50, 50, 50, "BMW", "F2");
            equipes[16] = new Equipes("DAMS", "#113861", "#48D4FF", "#FFFFFF", "França", 45, 45, 45, 45, 45, 45, 45, 45, "Renault", "F2");
            equipes[17] = new Equipes("Amersfoort", "#000000", "#FF883C", "#FFFFFF", "Holanda", 45, 45, 45, 45, 45, 45, 45, 45, "Ford", "F2");
            equipes[18] = new Equipes("Lamborghini", "#000000", "#FFAC11", "#FFFFFF", "Itália", 40, 40, 40, 40, 40, 40, 40, 40, "Lamborghini", "F2");
            equipes[19] = new Equipes("Trident", "#3706BF", "#FF3024", "#000000", "Itália", 40, 40, 40, 40, 40, 40, 40, 40, "Toyota", "F2");

            // Equipes F3
            equipes[20] = new Equipes("BMW", "#117CFF", "#FFFFFF", "#000000", "Alemanha", 35, 35, 35, 35, 35, 35, 35, 35, "BMW", "F3");
            equipes[21] = new Equipes("Penske Porsche", "#FFFFFF", "#FF3629", "#000000", "Alemanha", 35, 35, 35, 35, 35, 35, 35, 35, "Audi", "F3");
            equipes[22] = new Equipes("Toyota Gazoo", "#C22A1F", "#C22A1F", "#FFFFFF", "Japão", 30, 30, 30, 30, 30, 30, 30, 30, "Ford", "F3");
            equipes[23] = new Equipes("Campos", "#FFB22A", "#EB3326", "#000000", "Espanha", 25, 25, 25, 25, 25, 25, 25, 25, "BMW", "F3");
            equipes[24] = new Equipes("Tower Motorsports", "#FF9A1C", "#3444FF", "#000000", "Canadá", 20, 20, 20, 20, 20, 20, 20, 20, "Ford", "F3");
            equipes[25] = new Equipes("Team WRT", "#55BEFF", "#55BEFF", "#FFFFFF", "Bélgica", 20, 20, 20, 20, 20, 20, 20, 20, "Lamborghini", "F3");
            equipes[26] = new Equipes("Proton", "#9551FF", "#9551FF", "#FFFFFF", "Alemanha", 15, 15, 15, 15, 15, 15, 15, 15, "Toyota", "F3");
            equipes[27] = new Equipes("Kessel", "#FF0081", "#236EFF", "#FFFFFF", "Suíça", 10, 10, 10, 10, 10, 10, 10, 10, "Ford", "F3");
            equipes[28] = new Equipes("Action Express", "#FF6E63", "#CCCCCC", "#000000", "Estados Unidos", 10, 10, 10, 10, 10, 10, 10, 10, "Toyota", "F3");
            equipes[29] = new Equipes("Team Senna", "#2D7D4E", "#FFD91C", "#000000", "Brasil", 10, 10, 10, 10, 10, 10, 10, 10, "Lamborghini", "F3");


            // Atribuir Pilotos as Equipes
            equipes[0].PrimeiroPiloto = pilotos[0];
            pilotos[0].EquipePiloto = equipes[0].NomeEquipe;
            pilotos[0].StatusPiloto = "1º Piloto";
            pilotos[0].ContratoPiloto = "2 anos";
            pilotos[0].Cor1 = equipes[0].Cor1;
            pilotos[0].Cor2 = equipes[0].Cor2;
            pilotos[0].Categoria = equipes[0].Categoria;

            for (int i = 1; i < (equipes.Length * 2); i++)
            {
                int equipeIndex = i / 2; // Equipe 0 para pilotos 0 e 1, equipe 1 para pilotos 2 e 3, etc.

                Equipes equipe = equipes[equipeIndex];
                if (i % 2 == 0)
                {
                    equipe.PrimeiroPiloto = pilotos[i];
                    pilotos[i].EquipePiloto = equipe.NomeEquipe;
                    pilotos[i].StatusPiloto = "1º Piloto";
                    pilotos[i].ContratoPiloto = "2 anos";
                    pilotos[i].Cor1 = equipe.Cor1;
                    pilotos[i].Cor2 = equipe.Cor2;
                    pilotos[i].Categoria = equipe.Categoria;
                }
                else
                {
                    equipe.SegundoPiloto = pilotos[i];
                    pilotos[i].EquipePiloto = equipe.NomeEquipe;
                    pilotos[i].StatusPiloto = "2º Piloto";
                    pilotos[i].ContratoPiloto = "1 anos";
                    pilotos[i].Cor1 = equipe.Cor1;
                    pilotos[i].Cor2 = equipe.Cor2;
                    pilotos[i].Categoria = equipe.Categoria;
                }
            }
            dadosPistas();
            EmbaralharPistas();
            DefinirSemanasPistas();

            principal.ProxGP = pistas[0].NomeGp;
            principal.ProxGPais = pistas[0].NomeCircuito;
            principal.ProxGPSemana = pistas[0].SemanaDaProva;

        }
        public void dadosPistas()
        {
            pistas[0] = new Pistas("Austrália", "Melbourne", 58, 44, 56, 80000);
            pistas[1] = new Pistas("Itália", "Monza", 53, 35, 65, 81000);
            pistas[2] = new Pistas("Brasil", "Interlagos", 71, 42, 58, 70000);
        }
        public void EmbaralharPistas()
        {
            Random random = new Random();

            // Embaralhe as pistas usando o algoritmo de Fisher-Yates
            for (int i = pistas.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                // Trocar as pistas[i] e pistas[j]
                Pistas temp = pistas[i];
                pistas[i] = pistas[j];
                pistas[j] = temp;
            }
        }
        public void DefinirSemanasPistas()
        {
            pistas[0].SemanaDaProva = 3;
            pistas[1].SemanaDaProva = 5;
            pistas[2].SemanaDaProva = 8;
        }
        public void AtualizaStatusProxCorrida(int contador)
        {

            if (contador > 0 && contador <= 3)
            {
                principal.ProxGP = pistas[0].NomeGp;
                principal.ProxGPais = pistas[0].NomeCircuito;
                principal.ProxGPSemana = pistas[0].SemanaDaProva;
            }
            else if (contador > 3 && contador <= 5)
            {
                principal.ProxGP = pistas[1].NomeGp;
                principal.ProxGPais = pistas[1].NomeCircuito;
                principal.ProxGPSemana = pistas[1].SemanaDaProva;
            }
            else if (contador > 5 && contador <= 8)
            {
                principal.ProxGP = pistas[2].NomeGp;
                principal.ProxGPais = pistas[2].NomeCircuito;
                principal.ProxGPSemana = pistas[2].SemanaDaProva;
            }
            else
            {
                principal.ProxGP = "";
                principal.ProxGPais = "";
                principal.ProxGPSemana = 0;
            }

        }
        public void AtualizarFinanciasSemanal()
        {
            financias.MySaldoJogador = financias.MySaldoJogador + financias.MySaldoJogadorSemanal;
        }
        private void AtualizarNomes()
        {
            labelNomeJogador.Text = string.Format("{0} {1}", principal.NomeJogador, principal.SobrenomeJogador);
            labelIdadeJogador.Text = string.Format("Idade: {0:N0}", principal.IdadeJogador.ToString());
            labelHabilidadeJogador.Text = string.Format("Hab: {0:N0}", principal.HabilidadeJogador.ToString());
            pictureBoxNacionalidadePiloto.ImageLocation = Path.Combine("Paises", principal.NacionalidadeJogador + ".png");
        }
        private void AtualizarFinancias()
        {
            labelSaldoNaConta.Text = string.Format("R$ {0:N0}", financias.MySaldoJogador);
            labelSaldoPorSemana.Text = string.Format("R$ {0:N0}", financias.MySaldoJogadorSemanal);
        }
        private void AtualizarDate()
        {
            labelDataTemporada.Text = string.Format("Semana {0:D2} / {1}", principal.ContadorDeSemana, principal.ContadorDeAno);
            labelStatusTemporada.Text = principal.StatusDaTemporada;

            if (principal.StatusDaTemporada == "Fim-Temporada")
            {
                labelGpDoPais.Text = "Fim de Temporada";
                labelNomeGP.Text = "";
                labelSemanaGP.Text = "";
            }
            else
            {
                labelGpDoPais.Text = string.Format("GP do {0:D2}", principal.ProxGP);
                labelNomeGP.Text = principal.ProxGPais;
                labelSemanaGP.Text = string.Format("Semana {0:D2}", principal.ProxGPSemana.ToString());
            }

        }
        public void AtualizarTabelas()
        {
            DataTable classEquipes = (DataTable)dgvClassEquipes.DataSource;
            DataTable classPilotos = (DataTable)dgvClassPilotos.DataSource;

            // Desative a op��o de ordena��o em todas as colunas
            foreach (DataGridViewColumn column in dgvClassEquipes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a op��o de ordena��o em todas as colunas
            foreach (DataGridViewColumn column in dgvClassPilotos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Ordene automaticamente a coluna 3 do maior para o menor
            dgvClassEquipes.Sort(dgvClassEquipes.Columns[3], ListSortDirection.Descending);

            // Ordene automaticamente a coluna 4 do maior para o menor
            dgvClassPilotos.Sort(dgvClassPilotos.Columns[4], ListSortDirection.Descending);

            for (int i = 0; i < dgvClassEquipes.Rows.Count; i++)
            {
                dgvClassEquipes.Rows[i].Cells["#"].Value = i + 1;
            }
            for (int i = 0; i < dgvClassPilotos.Rows.Count; i++)
            {
                dgvClassPilotos.Rows[i].Cells["#"].Value = i + 1;
            }
            dgvClassEquipes.ClearSelection();
            dgvClassPilotos.ClearSelection();
        }
        private void CriarDataGridViewClassEquipes()
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("#", typeof(int));
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
            dgvClassEquipes.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dgvClassEquipes.RowHeadersVisible = false;
            dgvClassEquipes.Enabled = false;
            dgvClassEquipes.ScrollBars = ScrollBars.None;
            dgvClassEquipes.AllowUserToAddRows = false;
            dgvClassEquipes.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dgvClassEquipes.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgvClassEquipes.GridColor = Color.FromArgb(220, 220, 220);
            dgvClassEquipes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassEquipes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvClassEquipes.DataSource = classEquipes;

            // Altura das linhas
            dgvClassEquipes.RowTemplate.Height = 26;
            // Define a altura do cabe�alho das colunas
            dgvClassEquipes.ColumnHeadersHeight = 30;


            // Defina a ordem de exibi��o das colunas com base nos �ndices
            dgvClassEquipes.Columns["#"].DisplayIndex = 0;
            dgvClassEquipes.Columns["Sede"].DisplayIndex = 1;
            dgvClassEquipes.Columns["Nome"].DisplayIndex = 2;
            dgvClassEquipes.Columns["P"].DisplayIndex = 3;
            dgvClassEquipes.Columns["1º"].DisplayIndex = 4;
            dgvClassEquipes.Columns["2º"].DisplayIndex = 5;
            dgvClassEquipes.Columns["3º"].DisplayIndex = 6;
            dgvClassEquipes.Columns["Path"].DisplayIndex = 7;

            dgvClassEquipes.Columns["Path"].Visible = false;

            dgvClassEquipes.Columns[0].Width = 40;
            dgvClassEquipes.Columns[1].Width = 50;
            dgvClassEquipes.Columns[2].Width = 240;
            dgvClassEquipes.Columns[3].Width = 50;
            dgvClassEquipes.Columns[4].Width = 40;
            dgvClassEquipes.Columns[5].Width = 40;
            dgvClassEquipes.Columns[6].Width = 40;

        }
        private void PreencherDataGridViewClassEquipes()
        {
            DataTable classEquipes = (DataTable)dgvClassEquipes.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < equipes.Length; i++)
            {
                DataRow row = classEquipes.NewRow();
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
            dgvClassEquipes.DataSource = classEquipes;

            // Limpe a seleção inicial
            dgvClassEquipes.ClearSelection();
        }
        private void AtualizarDataGridViewClassEquipes()
        {
            // Primeiro, atualize os dados no DataTable
            DataTable classEquipes = (DataTable)dgvClassEquipes.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classEquipes.Rows.Clear();

            // Em seguida, preencha o DataTable com os novos dados
            for (int i = 0; i < equipes.Length; i++)
            {
                DataRow row = classEquipes.NewRow();
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
            // Por fim, atualize o DataSource do DataGridView para refletir as mudan�as
            dgvClassEquipes.DataSource = classEquipes;

            // Limpe a sele��o inicial
            dgvClassEquipes.ClearSelection();
        }
        private void CriarDataGridViewClassPilotos()
        {
            DataTable classPilotos = new DataTable();
            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            classPilotos.Columns.Add("#", typeof(int));
            classPilotos.Columns.Add(sedeColumn);
            classPilotos.Columns.Add("Nome", typeof(string));
            classPilotos.Columns.Add("Equipe", typeof(string));
            classPilotos.Columns.Add("P", typeof(int));
            classPilotos.Columns.Add("1º", typeof(int));
            classPilotos.Columns.Add("2º", typeof(int));
            classPilotos.Columns.Add("3º", typeof(int));
            classPilotos.Columns.Add("Path", typeof(string));
            classPilotos.Columns.Add("Cor1", typeof(string));
            classPilotos.Columns.Add("Cor2", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dgvClassPilotos.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dgvClassPilotos.RowHeadersVisible = false;
            dgvClassPilotos.Enabled = false;
            dgvClassPilotos.ScrollBars = ScrollBars.None;
            dgvClassPilotos.AllowUserToAddRows = false;
            dgvClassPilotos.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dgvClassPilotos.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgvClassPilotos.GridColor = Color.FromArgb(220, 220, 220);
            dgvClassPilotos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassPilotos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvClassPilotos.DataSource = classPilotos;

            // Altura das linhas
            dgvClassPilotos.RowTemplate.Height = 25;
            // Define a altura do cabe�alho das colunas
            dgvClassPilotos.ColumnHeadersHeight = 25;

            // Defina a ordem de exibi��o das colunas com base nos �ndices
            dgvClassPilotos.Columns["#"].DisplayIndex = 0;
            dgvClassPilotos.Columns["Nac"].DisplayIndex = 1;
            dgvClassPilotos.Columns["Nome"].DisplayIndex = 2;
            dgvClassPilotos.Columns["Equipe"].DisplayIndex = 3;
            dgvClassPilotos.Columns["P"].DisplayIndex = 4;
            dgvClassPilotos.Columns["1º"].DisplayIndex = 5;
            dgvClassPilotos.Columns["2º"].DisplayIndex = 6;
            dgvClassPilotos.Columns["3º"].DisplayIndex = 7;
            dgvClassPilotos.Columns["Path"].DisplayIndex = 8;

            dgvClassPilotos.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvClassPilotos.Columns["Path"].Visible = false;
            dgvClassPilotos.Columns["Cor1"].Visible = false;
            dgvClassPilotos.Columns["Cor2"].Visible = false;

            dgvClassPilotos.Columns[0].Width = 40;
            dgvClassPilotos.Columns[1].Width = 50;
            dgvClassPilotos.Columns[2].Width = 170;
            dgvClassPilotos.Columns[3].Width = 110;
            dgvClassPilotos.Columns[4].Width = 40;
            dgvClassPilotos.Columns[5].Width = 30;
            dgvClassPilotos.Columns[6].Width = 30;
            dgvClassPilotos.Columns[7].Width = 30;
        }
        private void PreencherDataGridViewClassPilotos()
        {

            DataTable classPilotos = (DataTable)dgvClassPilotos.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                row["Equipe"] = pilotos[i].EquipePiloto;
                row["P"] = pilotos[i].PontosCampeonato;
                row["1º"] = pilotos[i].PrimeiroColocado;
                row["2º"] = pilotos[i].SegundoColocado;
                row["3º"] = pilotos[i].TerceiroColocado;
                row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                classPilotos.Rows.Add(row);
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"].ToString();
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudan�as
            dgvClassPilotos.DataSource = classPilotos;

            // Limpe a sele��o inicial
            dgvClassPilotos.ClearSelection();
        }
        private void AtualizarDataGridViewClassPilotos()
        {
            DataTable classPilotos = (DataTable)dgvClassPilotos.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                row["Equipe"] = pilotos[i].EquipePiloto;
                row["P"] = pilotos[i].PontosCampeonato;
                row["1º"] = pilotos[i].PrimeiroColocado;
                row["2º"] = pilotos[i].SegundoColocado;
                row["3º"] = pilotos[i].TerceiroColocado;
                row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                classPilotos.Rows.Add(row);
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"].ToString();
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudan�as
            dgvClassPilotos.DataSource = classPilotos;

            // Limpe a sele��o inicial
            dgvClassPilotos.ClearSelection();
        }
        public void SalvarDadosDosArquivo()
        {
            Equipes[] saveEquipe = equipes;
            Pilotos[] savePiloto = pilotos;
            Pistas[] savePista = pistas;
            DadosCompletos dadosCompletos = new DadosCompletos
            {
                Equipes = saveEquipe,
                Pilotos = savePiloto,
                Pistas = savePista,
                NomeJogador = principal.NomeJogador,
                SobrenomeJogador = principal.SobrenomeJogador,
                NacionalidadeJogador = principal.NacionalidadeJogador,
                IdadeJogador = principal.IdadeJogador,
                HabilidadeJogador = principal.HabilidadeJogador,
                ContadorDeSemana = principal.ContadorDeSemana,
                ContadorDeAno = principal.ContadorDeAno,
                StatusDaTemporada = principal.StatusDaTemporada,
                ProxGP = principal.ProxGP,
                ProxGPais = principal.ProxGPais,
                ProxGPSemana = principal.ProxGPSemana,
                EtapaAtual = principal.EtapaAtual,
                MySaldoJogador = financias.MySaldoJogador,
                MySaldoJogadorSemanal = financias.MySaldoJogadorSemanal,
            };

            string json = JsonSerializer.Serialize(dadosCompletos);
            File.WriteAllText("dados_completos.json", json);

            MessageBox.Show($"Dados das equipes e pilotos salvos com sucesso.");
        }
        public void CarregarDadosDosArquivos()
        {
            if (File.Exists("dados_completos.json"))
            {
                string json = File.ReadAllText("dados_completos.json");
                DadosCompletos dadosCompletos = JsonSerializer.Deserialize<DadosCompletos>(json);

                if (dadosCompletos != null)
                {
                    equipes = dadosCompletos.Equipes;
                    pilotos = dadosCompletos.Pilotos;
                    pistas = dadosCompletos.Pistas;
                    principal.NomeJogador = dadosCompletos.NomeJogador;
                    principal.SobrenomeJogador = dadosCompletos.SobrenomeJogador;
                    principal.NacionalidadeJogador = dadosCompletos.NacionalidadeJogador;
                    principal.IdadeJogador = dadosCompletos.IdadeJogador;
                    principal.HabilidadeJogador = dadosCompletos.HabilidadeJogador;
                    principal.ContadorDeSemana = dadosCompletos.ContadorDeSemana;
                    principal.ContadorDeAno = dadosCompletos.ContadorDeAno;
                    principal.StatusDaTemporada = dadosCompletos.StatusDaTemporada;
                    principal.ProxGP = dadosCompletos.ProxGP;
                    principal.ProxGPais = dadosCompletos.ProxGPais;
                    principal.ProxGPSemana = dadosCompletos.ProxGPSemana;
                    principal.EtapaAtual = dadosCompletos.EtapaAtual;
                    financias.MySaldoJogador = dadosCompletos.MySaldoJogador;
                    financias.MySaldoJogadorSemanal = dadosCompletos.MySaldoJogadorSemanal;

                    MessageBox.Show("Dados das equipes e pilotos carregados com sucesso.");
                }
                else
                {
                    MessageBox.Show("Dados ou equipes/pilotos nulos ap�s a desserializa��o.");
                }
            }
            else
            {
                MessageBox.Show("Arquivo 'dados_completos.json' n�o encontrado.");
            }
        }
        private void pictureBoxBtnContinuar_Click(object sender, EventArgs e)
        {
            if (principal.ContadorDeSemana == principal.ProxGPSemana)
            {
                MessageBox.Show("Corrida");
                TelaQualificacao telaQualificacao = new TelaQualificacao(principal, equipes, pilotos, pistas);
                telaQualificacao.ShowDialog();

                AtualizarDataGridViewClassEquipes();
                AtualizarDataGridViewClassPilotos();
                AtualizarTabelas();

                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarFinanciasSemanal();
                AtualizarFinancias();
                AtualizarDate();
                AtualizarNomes();
            }
            else
            {
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarFinanciasSemanal();
                AtualizarFinancias();
                AtualizarDate();
                AtualizarNomes();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); // Isso fecha o formulario atual
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SalvarDadosDosArquivo();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TelaQualificacao telaQualificacao = new TelaQualificacao(principal, equipes, pilotos, pistas);
            telaQualificacao.ShowDialog();

            AtualizarDataGridViewClassEquipes();
            AtualizarDataGridViewClassPilotos();
            AtualizarTabelas();
        }
        private void pictureBoxClassificacao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Classificação");
            TelaClassificacao telaClassificacao = new TelaClassificacao(principal, equipes, pilotos, pistas);
            telaClassificacao.ShowDialog();
        }
    }
    class DadosCompletos
    {
        public Equipes[] Equipes { get; set; }
        public Pilotos[] Pilotos { get; set; }
        public Pistas[] Pistas { get; set; }
        public string NomeJogador { get; set; }
        public string SobrenomeJogador { get; set; }
        public string NacionalidadeJogador { get; set; }
        public int IdadeJogador { get; set; }
        public int HabilidadeJogador { get; set; }
        public int ContadorDeSemana { get; set; }
        public int ContadorDeAno { get; set; }
        public string StatusDaTemporada { get; set; }
        public string ProxGP { get; set; }
        public string ProxGPais { get; set; }
        public int ProxGPSemana { get; set; }
        public int EtapaAtual { get; set; }
        public double MySaldoJogador { get; set; }
        public double MySaldoJogadorSemanal { get; set; }
    }
}