﻿using System;
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
    internal partial class TelaClassificacao : Form
    {
        Principal principal;
        Equipes[] equipes;
        Pilotos[] pilotos;
        Pistas[] pistas;
        public TelaClassificacao(Principal princ, Equipes[] equip, Pilotos[] pilot, Pistas[] pist)
        {
            InitializeComponent();
            principal = princ;
            equipes = equip;
            pilotos = pilot;
            pistas = pist;
        }
        private void TelaClassificacao_Load(object sender, EventArgs e)
        {
            CriarDataGridViewClassPilotos(dvgTelaClassificacaoPiloto);
            CriarDataGridViewClassEquipes(dvgTelaClassificacaoEquipe);
            CriarDataGridViewCampeosPiloto(dvgTelaCampeosPiloto);
            CriarDataGridViewCampeosEquipe(dvgTelaCampeosEquipes);

            List<Principal> nomeCategoria = Principal.ObterListaCategoria();

            comboBoxSelectCategoria.DataSource = nomeCategoria;
            comboBoxSelectCategoria.DisplayMember = "categoria";

        }
        private void comboBoxSelectCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            principal.Categoria = comboBoxSelectCategoria.Text;
            AtualizarIndexComboBox();
            AtualizarTabelas(dvgTelaClassificacaoPiloto, dvgTelaClassificacaoEquipe, dvgTelaCampeosPiloto, dvgTelaCampeosEquipes);
        }
        private void AtualizarIndexComboBox()
        {
            if (principal.Categoria == "F1")
            {
                PreencherDataGridViewClassPilotos(0, 10, dvgTelaClassificacaoPiloto);
                PreencherDataGridViewClassEquipes(0, 10, dvgTelaClassificacaoEquipe);
                // Informa a categoria
                PreencherDataGridViewCampeosPilotos(principal.pilotosCampeoesF1, dvgTelaCampeosPiloto);
                PreencherDataGridViewCampeosEquipe(principal.equipesCampeoesF1, dvgTelaCampeosEquipes);
            }
            else if (principal.Categoria == "F2")
            {
                PreencherDataGridViewClassPilotos(10, 20, dvgTelaClassificacaoPiloto);
                PreencherDataGridViewClassEquipes(10, 20, dvgTelaClassificacaoEquipe);
                // Informa a categoria
                PreencherDataGridViewCampeosPilotos(principal.pilotosCampeoesF2, dvgTelaCampeosPiloto);
                PreencherDataGridViewCampeosEquipe(principal.equipesCampeoesF2, dvgTelaCampeosEquipes);
            }
            else if (principal.Categoria == "F3")
            {
                PreencherDataGridViewClassPilotos(20, 30, dvgTelaClassificacaoPiloto);
                PreencherDataGridViewClassEquipes(20, 30, dvgTelaClassificacaoEquipe);
                // Informa a categoria
                PreencherDataGridViewCampeosPilotos(principal.pilotosCampeoesF3, dvgTelaCampeosPiloto);
                PreencherDataGridViewCampeosEquipe(principal.equipesCampeoesF3, dvgTelaCampeosEquipes);
            }
        }
        public void AtualizarTabelas(DataGridView dataGridViewPilotos, DataGridView dataGridViewEquipes, DataGridView dvgTelaCampeosPiloto, DataGridView dvgTelaCampeosEquipes)
        {
            DataTable classEquipes = (DataTable)dataGridViewEquipes.DataSource;
            DataTable classPilotos = (DataTable)dataGridViewPilotos.DataSource;
            DataTable classCampeaoPilotos = (DataTable)dvgTelaCampeosPiloto.DataSource;
            DataTable classCampeaoEquipe = (DataTable)dvgTelaCampeosEquipes.DataSource;

            // Desative a opção de ordenar em todas as colunas
            foreach (DataGridViewColumn column in dataGridViewEquipes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a opção de ordenar em todas as colunas
            foreach (DataGridViewColumn column in dataGridViewPilotos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a opção de ordenar em todas as colunas
            foreach (DataGridViewColumn column in dvgTelaCampeosPiloto.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a opção de ordenar em todas as colunas
            foreach (DataGridViewColumn column in dvgTelaCampeosEquipes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 3 do maior para o menor
            dataGridViewEquipes.Sort(dataGridViewEquipes.Columns[4], ListSortDirection.Descending);

            // Ordene automaticamente a coluna 6 do maior para o menor
            dataGridViewPilotos.Sort(dataGridViewPilotos.Columns[5], ListSortDirection.Descending);

            // Ordene automaticamente a coluna 0 do maior para o menor
            dvgTelaCampeosPiloto.Sort(dvgTelaCampeosPiloto.Columns[0], ListSortDirection.Descending);

            // Ordene automaticamente a coluna 0 do maior para o menor
            dvgTelaCampeosEquipes.Sort(dvgTelaCampeosEquipes.Columns[0], ListSortDirection.Descending);

            for (int i = 0; i < dvgTelaCampeosPiloto.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTelaCampeosPiloto.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTelaCampeosPiloto.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTelaCampeosPiloto.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dvgTelaCampeosEquipes.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTelaCampeosEquipes.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTelaCampeosEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTelaCampeosEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dataGridViewEquipes.Rows.Count; i++)
            {
                dataGridViewEquipes.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dataGridViewEquipes.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dataGridViewEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dataGridViewEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dataGridViewPilotos.Rows.Count; i++)
            {
                dataGridViewPilotos.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dataGridViewPilotos.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dataGridViewPilotos.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dataGridViewPilotos.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            dvgTelaCampeosPiloto.ClearSelection();
            dvgTelaCampeosEquipes.ClearSelection();
            dataGridViewEquipes.ClearSelection();
            dataGridViewPilotos.ClearSelection();
        }
        private void ManipulacaoClassificacaoCampeonatoEquipes(object sender, DataGridViewSortCompareEventArgs e, DataGridView dataGridView)
        {
            // Método de manipulação do evento SortCompare
            if (e.Column.Index == 4) // Ordena pela coluna 4
            {
                int valorCelula1 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[4].Value);
                int valorCelula2 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[4].Value);
                e.SortResult = valorCelula1.CompareTo(valorCelula2);

                if (e.SortResult == 0)
                {
                    // Se o valor da coluna 4 for igual, ordena pela coluna 5
                    int valorCelula3 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[5].Value);
                    int valorCelula4 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[5].Value);
                    e.SortResult = valorCelula3.CompareTo(valorCelula4);

                    if (e.SortResult == 0)
                    {
                        // Se o valor da coluna 5 for igual, ordena pela coluna 6
                        int valorCelula5 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[6].Value);
                        int valorCelula6 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[6].Value);
                        e.SortResult = valorCelula5.CompareTo(valorCelula6);

                        if (e.SortResult == 0)
                        {
                            // Se o valor da coluna 6 for igual, ordena pela coluna 7
                            int valorCelula7 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[7].Value);
                            int valorCelula8 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[7].Value);
                            e.SortResult = valorCelula7.CompareTo(valorCelula8);
                        }
                    }
                }
                e.Handled = true; // Indica que a comparação foi tratada
            }
        }
        private void ManipulacaoClassificacaoCampeonatoPilotos(object sender, DataGridViewSortCompareEventArgs e, DataGridView dataGridView)
        {
            // Método de manipulação do evento SortCompare
            if (e.Column.Index == 5) // Ordena pela coluna 5
            {
                int valorCelula1 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[5].Value);
                int valorCelula2 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[5].Value);
                e.SortResult = valorCelula1.CompareTo(valorCelula2);

                if (e.SortResult == 0)
                {
                    // Se o valor da coluna 5 for igual, ordena pela coluna 6
                    int valorCelula3 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[6].Value);
                    int valorCelula4 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[6].Value);
                    e.SortResult = valorCelula3.CompareTo(valorCelula4);

                    if (e.SortResult == 0)
                    {
                        // Se o valor da coluna 6 for igual, ordena pela coluna 7
                        int valorCelula5 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[7].Value);
                        int valorCelula6 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[7].Value);
                        e.SortResult = valorCelula5.CompareTo(valorCelula6);

                        if (e.SortResult == 0)
                        {
                            // Se o valor da coluna 7 for igual, ordena pela coluna 8
                            int valorCelula7 = Convert.ToInt32(dataGridView.Rows[e.RowIndex1].Cells[8].Value);
                            int valorCelula8 = Convert.ToInt32(dataGridView.Rows[e.RowIndex2].Cells[8].Value);
                            e.SortResult = valorCelula7.CompareTo(valorCelula8);
                        }
                    }
                }
                e.Handled = true; // Indica que a comparação foi tratada
            }
        }
        private void CriarDataGridViewClassPilotos(DataGridView dataGridViewPilotos)
        {
            DataTable classPilotos = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            classPilotos.Columns.Add("#", typeof(int));
            classPilotos.Columns.Add("C1", typeof(string));
            classPilotos.Columns.Add(sedeColumn);
            classPilotos.Columns.Add("Nome", typeof(string));
            classPilotos.Columns.Add("Equipe", typeof(string));
            classPilotos.Columns.Add("P", typeof(int));
            classPilotos.Columns.Add("1º", typeof(int));
            classPilotos.Columns.Add("2º", typeof(int));
            classPilotos.Columns.Add("3º", typeof(int));
            classPilotos.Columns.Add("Path", typeof(string));

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
            cellStyle.Padding = new Padding(7, 7, 7, 7); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dataGridViewPilotos.RowHeadersVisible = false;
            dataGridViewPilotos.Enabled = false;
            dataGridViewPilotos.ScrollBars = ScrollBars.None;
            dataGridViewPilotos.AllowUserToAddRows = false;
            dataGridViewPilotos.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabeçalho
            dataGridViewPilotos.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewPilotos.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewPilotos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPilotos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewPilotos.DataSource = classPilotos;

            // Altura das linhas
            dataGridViewPilotos.RowTemplate.Height = 30;
            // Define a altura do cabeçalho das colunas
            dataGridViewPilotos.ColumnHeadersHeight = 40;

            // Defina a ordem de exibição das colunas com base nos índices
            dataGridViewPilotos.Columns["#"].DisplayIndex = 0;
            dataGridViewPilotos.Columns["C1"].DisplayIndex = 1;
            dataGridViewPilotos.Columns["Nac"].DisplayIndex = 2;
            dataGridViewPilotos.Columns["Nome"].DisplayIndex = 3;
            dataGridViewPilotos.Columns["Equipe"].DisplayIndex = 4;
            dataGridViewPilotos.Columns["P"].DisplayIndex = 5;
            dataGridViewPilotos.Columns["1º"].DisplayIndex = 6;
            dataGridViewPilotos.Columns["2º"].DisplayIndex = 7;
            dataGridViewPilotos.Columns["3º"].DisplayIndex = 8;
            dataGridViewPilotos.Columns["Path"].DisplayIndex = 9;

            dataGridViewPilotos.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewPilotos.Columns["Path"].Visible = false;
            dataGridViewPilotos.Columns["C1"].HeaderText = string.Empty;

            dataGridViewPilotos.Columns[0].Width = 30;
            dataGridViewPilotos.Columns[1].Width = 10;
            dataGridViewPilotos.Columns[2].Width = 40;
            dataGridViewPilotos.Columns[3].Width = 170;
            dataGridViewPilotos.Columns[4].Width = 120;
            dataGridViewPilotos.Columns[5].Width = 40;
            dataGridViewPilotos.Columns[6].Width = 30;
            dataGridViewPilotos.Columns[7].Width = 30;
            dataGridViewPilotos.Columns[8].Width = 30;
        }
        private void PreencherDataGridViewClassPilotos(int equipeMin, int equipeMax, DataGridView dataGridViewPilotos)
        {

            DataTable classPilotos = (DataTable)dataGridViewPilotos.DataSource;


            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeMin; k < equipeMax; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        row["P"] = pilotos[i].PontosCampeonato;
                        row["1º"] = pilotos[i].PrimeiroColocado;
                        row["2º"] = pilotos[i].SegundoColocado;
                        row["3º"] = pilotos[i].TerceiroColocado;
                        row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                        classPilotos.Rows.Add(row);
                    }
                }
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"].ToString();
                row["Nac"] = Image.FromFile(imagePath);
            }
            // Atualize o DataGridView para refletir as mudan�as
            dataGridViewPilotos.DataSource = classPilotos;

            // Limpe a sele��o inicial
            dataGridViewPilotos.ClearSelection();
        }
        private void CriarDataGridViewClassEquipes( DataGridView dataGridViewEquipes)
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
        private void PreencherDataGridViewClassEquipes(int equipeMin, int equipeMax, DataGridView dataGridViewEquipes)
        {
            DataTable classEquipes = (DataTable)dataGridViewEquipes.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classEquipes.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = equipeMin; i < equipeMax; i++)
            {
                DataRow row = classEquipes.NewRow();
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
        private void CriarDataGridViewCampeosPiloto(DataGridView dgv)
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("Ano", typeof(int));
            classEquipes.Columns.Add(sedeColumn);   // Sede vai ser reperesentada por uma imagem.
            classEquipes.Columns.Add("Nome", typeof(string));
            classEquipes.Columns.Add("P", typeof(int)); // P significa pontos, porem deixa somente o P.
            classEquipes.Columns.Add("C1", typeof(string));
            classEquipes.Columns.Add("Equipe", typeof(string));
            classEquipes.Columns.Add("Path", typeof(string));   // Path e o caminho onde está a minha imagem

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Sede";
            imageColumn.Name = "Sede";
            imageColumn.DataPropertyName = "Sede";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dgv.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

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

            dgv.DataSource = classEquipes;

            // Altura das linhas
            dgv.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;

            // Defina a ordem de exibiçao das colunas com base nos índices
            dgv.Columns["Ano"].DisplayIndex = 0;
            dgv.Columns["Sede"].DisplayIndex = 1;
            dgv.Columns["Nome"].DisplayIndex = 2;
            dgv.Columns["P"].DisplayIndex = 3;
            dgv.Columns["C1"].DisplayIndex = 4;
            dgv.Columns["Equipe"].DisplayIndex = 5;
            dgv.Columns["Path"].DisplayIndex = 6;

            dgv.Columns["Path"].Visible = false;
            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 40;
            dgv.Columns[2].Width = 210;
            dgv.Columns[3].Width = 40;
            dgv.Columns[4].Width = 10;
            dgv.Columns[5].Width = 150;
        }
        private void CriarDataGridViewCampeosEquipe(DataGridView dgv)
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("Ano", typeof(string));
            classEquipes.Columns.Add(sedeColumn);
            classEquipes.Columns.Add("C1", typeof(string));
            classEquipes.Columns.Add("Nome", typeof(string));
            classEquipes.Columns.Add("P", typeof(string));
            classEquipes.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Sede";
            imageColumn.Name = "Sede";
            imageColumn.DataPropertyName = "Sede";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dgv.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

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

            dgv.DataSource = classEquipes;

            // Altura das linhas
            dgv.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;


            // Defina a ordem de exibi��o das colunas com base nos �ndices
            dgv.Columns["Ano"].DisplayIndex = 0;
            dgv.Columns["Sede"].DisplayIndex = 1;
            dgv.Columns["C1"].DisplayIndex = 2;
            dgv.Columns["Nome"].DisplayIndex = 3;
            dgv.Columns["P"].DisplayIndex = 4;
            dgv.Columns["Path"].DisplayIndex = 5;

            dgv.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns["Path"].Visible = false;
            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 50;
            dgv.Columns[1].Width = 40;
            dgv.Columns[2].Width = 10;
            dgv.Columns[3].Width = 350;
            dgv.Columns[4].Width = 50;

        }
        private void PreencherDataGridViewCampeosPilotos(List<Historicos.PilotoCampeao> pilotosCampeoesCategoria, DataGridView dgv)
        {
            DataTable classEquipes = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            classEquipes.Rows.Clear();

            // Adiciona cada piloto campeão como uma nova linha no DataGridView
            foreach (var piloto in pilotosCampeoesCategoria)
            {
                // Cria uma nova linha no DataTable
                DataRow row = classEquipes.NewRow();

                row["Path"] = Path.Combine("Paises", piloto.Sede + ".png");
                string path = row["Path"].ToString();

                // Carrega a imagem da sede do piloto
                Image sedeImage = Image.FromFile(path);

                // Adiciona os dados do piloto à linha do DataGridView
                row["C1"] = piloto.C1;
                row["Ano"] = piloto.Ano;
                row["Sede"] = sedeImage;
                row["Nome"] = piloto.Nome;
                row["P"] = piloto.Pontos;
                row["Equipe"] = piloto.Equipe;

                // Adiciona a linha ao DataTable
                classEquipes.Rows.Add(row);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = classEquipes;
        }
        private void PreencherDataGridViewCampeosEquipe(List<Historicos.EquipeCampeao> equipesCampeoesCategoria, DataGridView dgv)
        {
            DataTable classEquipes = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            classEquipes.Rows.Clear();

            // Adiciona cada equipe campeão como uma nova linha no DataGridView
            foreach (var equipe in equipesCampeoesCategoria)
            {
                // Cria uma nova linha no DataTable
                DataRow row = classEquipes.NewRow();

                row["Path"] = Path.Combine("Paises", equipe.Sede + ".png");
                string path = row["Path"].ToString();

                // Carrega a imagem da sede do piloto
                Image sedeImage = Image.FromFile(path);

                // Adiciona os dados do piloto à linha do DataGridView
                row["C1"] = equipe.C1;
                row["Ano"] = equipe.Ano;
                row["Sede"] = sedeImage;
                row["Nome"] = equipe.Nome;
                row["P"] = equipe.Pontos;
                // Adiciona a linha ao DataTable
                classEquipes.Rows.Add(row);

                //classEquipes.Rows.Add(piloto.Ano, sedeImage, piloto.Nome, piloto.Pontos);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = classEquipes;
        }
        private void btnVolta_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
