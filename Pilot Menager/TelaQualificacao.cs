﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Metrics;

namespace Pilot_Menager
{
    internal partial class TelaQualificacao : Form
    {
        Principal principal;
        Equipes[] equipes;
        Pilotos[] pilotos;
        Pistas[] pistas;
        string categoria = "";
        string listcategoria = "";
        Random random = new Random();
        private int btnclick = 0;
        private int numberVoltasF = 0, numberVoltasT = 0;
        private int myIndex;
        Color corPrincipal;
        Color corSecundaria;
        Color corTexto;
        public TelaQualificacao(Principal princ, Equipes[] equip, Pilotos[] pilot, Pistas[] pist)
        {
            InitializeComponent();
            principal = princ;
            equipes = equip;
            pilotos = pilot;
            pistas = pist;
        }
        private void TelaQualificacao_Load(object sender, EventArgs e)
        {
            corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
            corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
            myIndex = principal.MyIndex;

            AtualizarNomes();

            if (pilotos[myIndex].Categoria == "F1")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else if (pilotos[myIndex].Categoria == "F2")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(10, 20, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(10, 20, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else if (pilotos[myIndex].Categoria == "F3")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(20, 30, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(20, 30, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else
            {
                /*
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
                */
                // Obtém a lista de categorias
                List<Principal> categorias = Principal.ObterListaCategoria();
                string fCategoria = pilotos[myIndex].Categoria;

                // Itera sobre cada categoria na lista
                foreach (var categoria in categorias)
                {
                    if (categoria.categoria != fCategoria && categoria.categoria == "F1")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(0, 10, "F1");
                    }
                    else if (categoria.categoria != fCategoria && categoria.categoria == "F2")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(10, 20, "F2");
                    }
                    else if (categoria.categoria != fCategoria && categoria.categoria == "F3")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(20, 30, "F3");
                    }
                }
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].TempoDeVoltaQualificacao = 0;
                    pilotos[j].TempoCorrida = 0;
                    pilotos[j].TempoDeVoltaCorrida = 0;
                    pilotos[j].QualificacaoParaCorrida = 0;
                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = 0;
                    pilotos[j].PosicaoNaVoltaMaisRapida = 0;
                    pilotos[j].PosicaoNaCorrida = 0;
                    pilotos[j].DiferancaAnt = 0;
                    pilotos[j].DiferancaPri = 0;
                }
                MetodoParaQualificarEquipes(0, 10);
                MetodoParaQualificarEquipes(10, 20);
                MetodoParaQualificarEquipes(20, 30);
                MetodoParaQualificarPilotos("F1");
                MetodoParaQualificarPilotos("F2");
                MetodoParaQualificarPilotos("F3");
                this.Close();
            }
            labelTreinoCorrida.Text = "Treino";
        }
        private void AtualizarNomes()
        {
            labelNomeGp.Text = string.Format("GP do {0:D2}", principal.ProxGP);
            labelNomePais.Text = principal.ProxGPais;
            labelSemanaGP.Text = string.Format("Semana {0:D2} / {1}", principal.ProxGPSemana, principal.ContadorDeAno);
            pictureBoxPaisGP.ImageLocation = Path.Combine("Paises", principal.ProxGP + ".png");
            lbQualificacaoClima.Text = string.Format("Clima: ");
            numberVoltasT = principal.ProxGPVoltas;
            lbQualificacaoVoltas.Text = string.Format("Voltas: " + numberVoltasF + " / " + numberVoltasT);

            if (principal.CorTexto == "Branco")
            {
                panel1.ForeColor = Color.White;
            }
            else if (principal.CorTexto == "Preto")
            {
                panel1.ForeColor = Color.Black;
            }
            else if (principal.CorTexto == "Automatico")
            {
                // Calcula o brilho da cor (luminosidade)
                double brilho = (corPrincipal.R * 0.299 + corPrincipal.G * 0.587 + corPrincipal.B * 0.114) / 255;

                if (brilho < 0.4)
                {
                    pictureBoxBtnContinuarQualificacao.Image = Properties.Resources.menu_continuar_w;
                    panel1.ForeColor = Color.White;
                }
                else
                {
                    pictureBoxBtnContinuarQualificacao.Image = Properties.Resources.menu_continuar_b;
                    panel1.ForeColor = Color.Black;
                }
            }
            panel1.BackColor = corPrincipal;
            panel2.BackColor = corSecundaria;
            panel3.BackColor = corSecundaria;
        }
        private void contadorDeVoltas()
        {
            lbQualificacaoVoltas.Text = string.Format("Voltas: " + numberVoltasF + " / " + numberVoltasT);
        }
        private void AtualizarTabelasQualificacaoVoltas(DataGridView dvgTableQualificacaoF1)
        {
            DataTable qualificacaoEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 5 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[6], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void CriarDataGridViewQualificacaoEquipesF1(DataGridView dvgTableQualificacaoF1)
        {
            DataTable qualificacaoEquipesF1 = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            qualificacaoEquipesF1.Columns.Add("C1", typeof(string));
            qualificacaoEquipesF1.Columns.Add("#", typeof(int));
            qualificacaoEquipesF1.Columns.Add(sedeColumn);
            qualificacaoEquipesF1.Columns.Add("Nome", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Equipe", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Melhor Tempo", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Segundos", typeof(int));
            qualificacaoEquipesF1.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dvgTableQualificacaoF1.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dvgTableQualificacaoF1.RowHeadersVisible = false;
            dvgTableQualificacaoF1.Enabled = false;
            dvgTableQualificacaoF1.ScrollBars = ScrollBars.None;
            dvgTableQualificacaoF1.AllowUserToAddRows = false;
            dvgTableQualificacaoF1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabeçalho
            dvgTableQualificacaoF1.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.GridColor = Color.FromArgb(220, 220, 220);
            dvgTableQualificacaoF1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
            dvgTableQualificacaoF1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgTableQualificacaoF1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dvgTableQualificacaoF1.DataSource = qualificacaoEquipesF1;

            // Altura das linhas
            dvgTableQualificacaoF1.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dvgTableQualificacaoF1.ColumnHeadersHeight = 30;

            // Defina a ordem de exibição das colunas com base nos índices
            dvgTableQualificacaoF1.Columns["C1"].DisplayIndex = 0;
            dvgTableQualificacaoF1.Columns["#"].DisplayIndex = 1;
            dvgTableQualificacaoF1.Columns["Nac"].DisplayIndex = 2;
            dvgTableQualificacaoF1.Columns["Nome"].DisplayIndex = 3;
            dvgTableQualificacaoF1.Columns["Equipe"].DisplayIndex = 4;
            dvgTableQualificacaoF1.Columns["Melhor Tempo"].DisplayIndex = 5;
            dvgTableQualificacaoF1.Columns["Segundos"].DisplayIndex = 6;
            dvgTableQualificacaoF1.Columns["Path"].DisplayIndex = 7;

            dvgTableQualificacaoF1.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dvgTableQualificacaoF1.Columns["Path"].Visible = false;
            dvgTableQualificacaoF1.Columns["Segundos"].Visible = false;
            dvgTableQualificacaoF1.Columns["C1"].HeaderText = string.Empty;

            dvgTableQualificacaoF1.Columns[0].Width = 10;
            dvgTableQualificacaoF1.Columns[1].Width = 40;
            dvgTableQualificacaoF1.Columns[2].Width = 50;
            dvgTableQualificacaoF1.Columns[3].Width = 330;
            dvgTableQualificacaoF1.Columns[4].Width = 240;
            dvgTableQualificacaoF1.Columns[5].Width = 170;
        }
        private void PreencherDataGridViewQualificacaoEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.formatarNumero(pilotos[i].TempoDeVoltaQualificacao);
                        row["Melhor Tempo"] = tempo;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
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
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarDataGridViewQualificacaoEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.formatarNumero(pilotos[i].TempoDeVoltaQualificacao);
                        row["Melhor Tempo"] = tempo;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
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
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        //Divisa das funções Treino e Corrida
        private void AtualizarTabelasCorridaInicio(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 10 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[9], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarTabelasCorridaVoltas(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 11 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[10], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void CriarDataGridViewCorridaEquipesF1(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            CorridaEquipesF1.Columns.Add("C1", typeof(string));
            CorridaEquipesF1.Columns.Add("#", typeof(int));
            CorridaEquipesF1.Columns.Add(sedeColumn);
            CorridaEquipesF1.Columns.Add("Nome", typeof(string));
            CorridaEquipesF1.Columns.Add("Equipe", typeof(string));
            CorridaEquipesF1.Columns.Add("Ult. Volta", typeof(string));
            CorridaEquipesF1.Columns.Add("Dif. Ant.", typeof(string));
            CorridaEquipesF1.Columns.Add("Dif. Pri.", typeof(string));
            CorridaEquipesF1.Columns.Add("Segundos", typeof(int));
            CorridaEquipesF1.Columns.Add("Qualific", typeof(int));
            CorridaEquipesF1.Columns.Add("TempoTotal", typeof(int));
            CorridaEquipesF1.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dvgTableQualificacaoF1.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dvgTableQualificacaoF1.RowHeadersVisible = false;
            dvgTableQualificacaoF1.Enabled = false;
            dvgTableQualificacaoF1.ScrollBars = ScrollBars.None;
            dvgTableQualificacaoF1.AllowUserToAddRows = false;
            dvgTableQualificacaoF1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabeçalho
            dvgTableQualificacaoF1.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.GridColor = Color.FromArgb(220, 220, 220);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
            dvgTableQualificacaoF1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgTableQualificacaoF1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dvgTableQualificacaoF1.DataSource = CorridaEquipesF1;

            // Altura das linhas
            dvgTableQualificacaoF1.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dvgTableQualificacaoF1.ColumnHeadersHeight = 30;

            // Defina a ordem de exibição das colunas com base nos índices
            dvgTableQualificacaoF1.Columns["C1"].DisplayIndex = 0;
            dvgTableQualificacaoF1.Columns["#"].DisplayIndex = 1;
            dvgTableQualificacaoF1.Columns["Nac"].DisplayIndex = 2;
            dvgTableQualificacaoF1.Columns["Nome"].DisplayIndex = 3;
            dvgTableQualificacaoF1.Columns["Equipe"].DisplayIndex = 4;
            dvgTableQualificacaoF1.Columns["Ult. Volta"].DisplayIndex = 5;
            dvgTableQualificacaoF1.Columns["Dif. Ant."].DisplayIndex = 6;
            dvgTableQualificacaoF1.Columns["Dif. Pri."].DisplayIndex = 7;
            dvgTableQualificacaoF1.Columns["Segundos"].DisplayIndex = 8;
            dvgTableQualificacaoF1.Columns["Qualific"].DisplayIndex = 9;
            dvgTableQualificacaoF1.Columns["TempoTotal"].DisplayIndex = 10;
            dvgTableQualificacaoF1.Columns["Path"].DisplayIndex = 11;


            dvgTableQualificacaoF1.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dvgTableQualificacaoF1.Columns["Path"].Visible = false;
            dvgTableQualificacaoF1.Columns["Segundos"].Visible = false;
            dvgTableQualificacaoF1.Columns["Qualific"].Visible = false;
            dvgTableQualificacaoF1.Columns["TempoTotal"].Visible = false;

            dvgTableQualificacaoF1.Columns[0].Width = 10;
            dvgTableQualificacaoF1.Columns[1].Width = 40;
            dvgTableQualificacaoF1.Columns[2].Width = 50;
            dvgTableQualificacaoF1.Columns[3].Width = 270;
            dvgTableQualificacaoF1.Columns[4].Width = 170;
            dvgTableQualificacaoF1.Columns[5].Width = 100;
            dvgTableQualificacaoF1.Columns[6].Width = 100;
            dvgTableQualificacaoF1.Columns[7].Width = 100;
        }
        private void PreencherDataGridViewCorridaEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.formatarNumero(pilotos[i].TempoDeVoltaCorrida);
                        row["Ult. Volta"] = tempo;
                        string difAnt = principal.formatarNumero(pilotos[i].DiferancaAnt);
                        row["Dif. Ant."] = difAnt;
                        string difPri = principal.formatarNumero(pilotos[i].DiferancaPri);
                        row["Dif. Pri."] = difPri;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Qualific"] = pilotos[i].QualificacaoParaCorrida;
                        row["TempoTotal"] = pilotos[i].TempoCorrida;
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
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarDataGridViewCorridaEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.formatarNumero(pilotos[i].TempoDeVoltaCorrida);
                        row["Ult. Volta"] = tempo;
                        string difAnt = principal.formatarNumero(pilotos[i].DiferancaAnt);
                        row["Dif. Ant."] = difAnt;
                        string difPri = principal.formatarNumero(pilotos[i].DiferancaPri);
                        row["Dif. Pri."] = difPri;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Qualific"] = pilotos[i].QualificacaoParaCorrida;
                        row["TempoTotal"] = pilotos[i].TempoCorrida;
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
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private async void pictureBoxBtnContinuarQualificacao_Click(object sender, EventArgs e)
        {
            // Desabilitar o btn
            pictureBoxBtnContinuarQualificacao.Enabled = false;
            string fCategoria = pilotos[myIndex].Categoria;

            if (pilotos[myIndex].Categoria == "F1")
            {
                await FuncaoParaRealizarSemanaDeCorrida(0, 10, fCategoria);
            }
            else if (pilotos[myIndex].Categoria == "F2")
            {
                await FuncaoParaRealizarSemanaDeCorrida(10, 20, fCategoria);
            }
            else if (pilotos[myIndex].Categoria == "F3")
            {
                await FuncaoParaRealizarSemanaDeCorrida(20, 30, fCategoria);
            }
            // Reabilitar o btn
            pictureBoxBtnContinuarQualificacao.Enabled = true;
        }
        private async Task FuncaoParaRealizarSemanaDeCorrida(int equipeF1Min, int equipeF1Max, string fCategoria)
        {
            btnclick++;
            if (btnclick == 1)
            {
                progressBarQualificacao.Maximum = 10;
                // Vai executar as voltas do meu treino de qualificação.
                // Nesse caso meu treino está tendo 10 voltas
                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                                equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                                pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase);
                                // Está ordenando a volta mais rapida do piloto.
                                if (pilotos[j].TempoDeVoltaQualificacao > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaQualificacao == 0)
                                {
                                    pilotos[j].TempoDeVoltaQualificacao = tempoDaVoltaAtual;
                                }
                            }
                        }
                    }
                    AtualizarDataGridViewQualificacaoEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                    AtualizarTabelasQualificacaoVoltas(dvgTableF1);
                    await Task.Delay(principal.TempoRodada);
                    progressBarQualificacao.Value = i;
                }
                // Vai executar a ordem de qualificação dos pilotos.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                            {
                                if (pilotos[i].TempoDeVoltaQualificacao > pilotos[j].TempoDeVoltaQualificacao)
                                {
                                    pilotos[i].QualificacaoParaCorrida++;
                                }
                            }
                        }
                    }
                }
            }
            else if (btnclick == 2)
            {
                AtualizarNomes();
                CriarDataGridViewCorridaEquipesF1(dvgTableF1);
                PreencherDataGridViewCorridaEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                AtualizarTabelasCorridaInicio(dvgTableF1);

                labelTreinoCorrida.Text = "Corrida";
                progressBarQualificacao.Value = 0;
            }
            else if (btnclick == 3)
            {
                progressBarQualificacao.Maximum = numberVoltasT;
                // Vai executar as voltas da cominha corrida.
                for (int i = 1; i <= numberVoltasT; i++)
                {
                    // Distribui as vantagem da classificação
                    if (i == 1)
                    {
                        for (int j = 0; j < pilotos.Length; j++)
                        {
                            for (int k = equipeF1Min; k < equipeF1Max; k++)
                            {
                                if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    pilotos[j].TempoCorrida = (pilotos[j].QualificacaoParaCorrida * 100);
                                }
                            }
                        }
                    }

                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                                equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                                pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase);
                                pilotos[j].TempoCorrida = pilotos[j].TempoCorrida + tempoDaVoltaAtual;
                                pilotos[j].TempoDeVoltaCorrida = tempoDaVoltaAtual;
                                // Está ordenando a volta mais rapida do piloto.
                                if (pilotos[j].TempoDeVoltaMaisRapidaCorrida > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaMaisRapidaCorrida == 0)
                                {
                                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = tempoDaVoltaAtual;
                                }
                            }
                        }
                    }
                    // Calcular a posição na corrida.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        pilotos[j].PosicaoNaCorrida = 0;
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[j].TempoCorrida > pilotos[k].TempoCorrida)
                                    {
                                        pilotos[j].PosicaoNaCorrida++;
                                    }
                                }
                            }
                        }
                    }
                    // Calcula a diferença de tempo do piloto para o primeiro.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[k].PosicaoNaCorrida == 0)
                                    {
                                        pilotos[j].DiferancaPri = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                    }
                                }
                            }
                        }
                    }
                    // Calcula a diferença de tempo do piloto para a anterior.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[j].PosicaoNaCorrida == (pilotos[k].PosicaoNaCorrida + 1))
                                    {
                                        pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                    }
                                    else if (pilotos[j].PosicaoNaCorrida == 0)
                                    {
                                        pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida;
                                    }
                                }
                            }
                        }
                    }
                    AtualizarDataGridViewCorridaEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                    AtualizarTabelasCorridaVoltas(dvgTableF1);
                    await Task.Delay(principal.TempoRodada);
                    progressBarQualificacao.Value = i;
                    numberVoltasF = i;
                    contadorDeVoltas();
                }
                // Vai executar a ordem das voltas mais rapida dos pilotos.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                            {
                                if (pilotos[i].TempoDeVoltaMaisRapidaCorrida > pilotos[j].TempoDeVoltaMaisRapidaCorrida)
                                {
                                    pilotos[i].PosicaoNaVoltaMaisRapida++;
                                }
                            }
                        }
                    }
                }
                // Pontuando o piloto com a volta mais rapida.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                        {
                            if (pilotos[i].PosicaoNaVoltaMaisRapida == 0)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PontoVoltaMaisRapida;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PontoVoltaMaisRapida;
                            }
                        }
                    }
                }
                // Pontuação da Corrida para os Pilotos e Equipes
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                        {
                            if (pilotos[i].PosicaoNaCorrida == 0)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PrimeiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PrimeiroLugar;
                                pilotos[i].PrimeiroColocado++;
                                equipes[k].PrimeiroColocado++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 1)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SegundoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SegundoLugar;
                                pilotos[i].SegundoColocado++;
                                equipes[k].SegundoColocado++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 2)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.TerceiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.TerceiroLugar;
                                pilotos[i].TerceiroColocado++;
                                equipes[k].TerceiroColocado++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 3)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuartoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuartoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 4)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuintoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuintoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 5)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SextoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SextoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 6)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SetimoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SetimoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 7)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.OitavoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.OitavoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 8)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.NonoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.NonoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 9)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 10)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoPrimeiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoPrimeiroLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 11)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSegundoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSegundoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 12)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoTerceiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoTerceiroLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 13)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuartoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuartoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 14)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuintoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuintoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 15)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSextoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSextoLugar;
                            }
                        }
                    }
                }
                // Atualizar o atributos classificação dos pilotos *Fazer
                // Atualizar o atributos classificação das equipes *Fazer
            }
            else if (btnclick == 4)
            {
                // Obtém a lista de categorias
                List<Principal> categorias = Principal.ObterListaCategoria();

                // Itera sobre cada categoria na lista
                foreach (var categoria in categorias)
                {
                    if (categoria.categoria != fCategoria && categoria.categoria == "F1")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(0, 10, "F1");
                    }
                    else if (categoria.categoria != fCategoria && categoria.categoria == "F2")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(10, 20, "F2");
                    }
                    else if (categoria.categoria != fCategoria && categoria.categoria == "F3")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(20, 30, "F3");
                    }
                }
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].TempoDeVoltaQualificacao = 0;
                    pilotos[j].TempoCorrida = 0;
                    pilotos[j].TempoDeVoltaCorrida = 0;
                    pilotos[j].QualificacaoParaCorrida = 0;
                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = 0;
                    pilotos[j].PosicaoNaVoltaMaisRapida = 0;
                    pilotos[j].PosicaoNaCorrida = 0;
                    pilotos[j].DiferancaAnt = 0;
                    pilotos[j].DiferancaPri = 0;
                }
                MetodoParaQualificarEquipes(0, 10);
                MetodoParaQualificarEquipes(10, 20);
                MetodoParaQualificarEquipes(20, 30);
                MetodoParaQualificarPilotos("F1");
                MetodoParaQualificarPilotos("F2");
                MetodoParaQualificarPilotos("F3");
                this.Close();
            }
        }
        private void MetodoParaQualificarEquipes(int equipeMin, int equipeMax)
        {
            for (int i = equipeMin; i < equipeMax; i++)
            {
                equipes[i].PosicaoAtualCampeonato = 1;
                for (int j = equipeMin; j < equipeMax; j++)
                {
                    if (i != j)
                    {
                        if (equipes[i].PontosCampeonato <= equipes[j].PontosCampeonato)
                        {
                            if (equipes[i].PontosCampeonato == equipes[j].PontosCampeonato)
                            {
                                if (equipes[i].PrimeiroColocado == equipes[j].PrimeiroColocado)
                                {
                                    if (equipes[i].SegundoColocado == equipes[j].SegundoColocado)
                                    {
                                        if (equipes[i].TerceiroColocado == equipes[j].TerceiroColocado)
                                        {
                                            if (i > j)
                                            {
                                                equipes[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (equipes[i].TerceiroColocado < equipes[j].TerceiroColocado)
                                        {
                                            equipes[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (equipes[i].SegundoColocado < equipes[j].SegundoColocado)
                                    {
                                        equipes[i].PosicaoAtualCampeonato++;
                                    }
                                }
                                else if (equipes[i].PrimeiroColocado < equipes[j].PrimeiroColocado)
                                {
                                    equipes[i].PosicaoAtualCampeonato++;
                                }
                            }
                            else if (equipes[i].PontosCampeonato < equipes[j].PontosCampeonato)
                            {
                                equipes[i].PosicaoAtualCampeonato++;
                            }
                        }
                    }
                }
            }
        }
        private void MetodoParaQualificarPilotos(string fCategoria)
        {
            for (int i = 0; i < pilotos.Length; i++)
            {
                if (pilotos[i].Categoria == fCategoria)
                {
                    pilotos[i].PosicaoAtualCampeonato = 1;
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        if (pilotos[j].Categoria == fCategoria)
                        {
                            if (i != j)
                            {
                                if (pilotos[i].PontosCampeonato <= pilotos[j].PontosCampeonato)
                                {
                                    if (pilotos[i].PontosCampeonato == pilotos[j].PontosCampeonato)
                                    {
                                        if (pilotos[i].PrimeiroColocado == pilotos[j].PrimeiroColocado)
                                        {
                                            if (pilotos[i].SegundoColocado == pilotos[j].SegundoColocado)
                                            {
                                                if (pilotos[i].TerceiroColocado == pilotos[j].TerceiroColocado)
                                                {
                                                    if (i > j)
                                                    {
                                                        pilotos[i].PosicaoAtualCampeonato++;
                                                    }
                                                }
                                                else if (pilotos[i].TerceiroColocado < pilotos[j].TerceiroColocado)
                                                {
                                                    pilotos[i].PosicaoAtualCampeonato++;
                                                }
                                            }
                                            else if (pilotos[i].SegundoColocado < pilotos[j].SegundoColocado)
                                            {
                                                pilotos[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (pilotos[i].PrimeiroColocado < pilotos[j].PrimeiroColocado)
                                        {
                                            pilotos[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (pilotos[i].PontosCampeonato < pilotos[j].PontosCampeonato)
                                    {
                                        pilotos[i].PosicaoAtualCampeonato++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void FuncaoParaRealizarSemanaDeCorridaCPU(int equipeF1Min, int equipeF1Max, string fCategoria)
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                        {
                            int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                            equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                            pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase);
                            // Está ordenando a volta mais rapida do piloto.
                            if (pilotos[j].TempoDeVoltaQualificacao > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaQualificacao == 0)
                            {
                                pilotos[j].TempoDeVoltaQualificacao = tempoDaVoltaAtual;
                            }
                        }
                    }
                }
            }
            // Vai executar a ordem de qualificação dos pilotos.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                        {
                            if (pilotos[i].TempoDeVoltaQualificacao > pilotos[j].TempoDeVoltaQualificacao)
                            {
                                pilotos[i].QualificacaoParaCorrida++;
                            }
                        }
                    }
                }
            }
            // Vai executar as voltas da cominha corrida.
            for (int i = 1; i <= pistas[principal.EtapaAtual].NumerosDeVoltas; i++)
            {
                // Distribui as vantagem da classificação
                if (i == 1)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                pilotos[j].TempoCorrida = i * 100;
                            }
                        }
                    }
                }

                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                        {
                            int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                            equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                            pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase);
                            pilotos[j].TempoCorrida = pilotos[j].TempoCorrida + tempoDaVoltaAtual;
                            pilotos[j].TempoDeVoltaCorrida = tempoDaVoltaAtual;
                            // Está ordenando a volta mais rapida do piloto.
                            if (pilotos[j].TempoDeVoltaMaisRapidaCorrida > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaMaisRapidaCorrida == 0)
                            {
                                pilotos[j].TempoDeVoltaMaisRapidaCorrida = tempoDaVoltaAtual;
                            }
                        }
                    }
                }
                // Calcular a posição na corrida.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].PosicaoNaCorrida = 0;
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[j].TempoCorrida > pilotos[k].TempoCorrida)
                                {
                                    pilotos[j].PosicaoNaCorrida++;
                                }
                            }
                        }
                    }
                }
                // Calcula a diferença de tempo do piloto para o primeiro.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[k].PosicaoNaCorrida == 0)
                                {
                                    pilotos[j].DiferancaPri = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                }
                            }
                        }
                    }
                }
                // Calcula a diferença de tempo do piloto para a anterior.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[j].PosicaoNaCorrida == (pilotos[k].PosicaoNaCorrida + 1))
                                {
                                    pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                }
                                else if (pilotos[j].PosicaoNaCorrida == 0)
                                {
                                    pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida;
                                }
                            }
                        }
                    }
                }
            }
            // Vai executar a ordem das voltas mais rapida dos pilotos.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                        {
                            if (pilotos[i].TempoDeVoltaMaisRapidaCorrida > pilotos[j].TempoDeVoltaMaisRapidaCorrida)
                            {
                                pilotos[i].PosicaoNaVoltaMaisRapida++;
                            }
                        }
                    }
                }
            }
            // Pontuando o piloto com a volta mais rapida.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        if (pilotos[i].PosicaoNaVoltaMaisRapida == 0)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PontoVoltaMaisRapida;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PontoVoltaMaisRapida;
                        }
                    }
                }
            }
            // Pontuação da Corrida para os Pilotos e Equipes
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        if (pilotos[i].PosicaoNaCorrida == 0)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PrimeiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PrimeiroLugar;
                            pilotos[i].PrimeiroColocado++;
                            equipes[k].PrimeiroColocado++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 1)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SegundoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SegundoLugar;
                            pilotos[i].SegundoColocado++;
                            equipes[k].SegundoColocado++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 2)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.TerceiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.TerceiroLugar;
                            pilotos[i].TerceiroColocado++;
                            equipes[k].TerceiroColocado++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 3)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuartoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuartoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 4)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuintoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuintoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 5)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SextoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SextoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 6)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SetimoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SetimoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 7)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.OitavoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.OitavoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 8)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.NonoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.NonoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 9)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 10)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoPrimeiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoPrimeiroLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 11)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSegundoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSegundoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 12)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoTerceiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoTerceiroLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 13)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuartoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuartoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 14)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuintoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuintoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 15)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSextoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSextoLugar;
                        }
                    }
                }
            }
            // Atualizar o atributos classificação dos pilotos *Fazer
            // Atualizar o atributos classificação das equipes *Fazer
        }
        private int AlgoritmoParaVoltas(int motor, int aerodinamica, int freio, int asaDianteira, int asaTraseira, int cambio,
        int eletrico, int direcao, int confiabilidade, int largada, int concentracao, int ultrapassagem, int experiencia, int rapidez,
        int chuva, int acertoCarro, int fisico, int importanciPiloto, int importanciaCarro, int curvas, int retas, int tempoBase)
        {
            Random r = new Random();

            int t = 5000; //valorPadraoTempo
            int c = 1; //valorPadraoCarro
            int p = 1; //valorPadraoPiloto
            /*
            int medCarro = (r.Next(c, aerodinamica + 1) + r.Next(c, freio + 1) + r.Next(c, asaDianteira + 1) + r.Next(c, asaTraseira + 1) + r.Next(c, cambio + 1) + r.Next(c, eletrico + 1) + r.Next(c, direcao + 1) + r.Next(c, confiabilidade + 1)) / 8;
            int medPiloto = (r.Next(p, largada + 1) + r.Next(p, concentracao + 1) + r.Next(p, ultrapassagem + 1) + r.Next(p, experiencia + 1) + r.Next(p, rapidez + 1) + r.Next(p, chuva + 1) + r.Next(p, acertoCarro + 1) + r.Next(p, fisico + 1)) / 8;*/
            int medCarro = ((aerodinamica + freio + asaDianteira + asaTraseira + cambio + eletrico + direcao + confiabilidade) / 8);
            int medPiloto = (largada + concentracao + ultrapassagem + experiencia + rapidez + chuva + acertoCarro + fisico) / 8;
            int medCarroVel = (r.Next(c, aerodinamica + 1) + r.Next(c, asaDianteira + 1) + r.Next(c, asaTraseira + 1) + r.Next(c, freio + 1)) / 4;
            int medCarroQual = (r.Next(c, cambio + 1) + r.Next(c, confiabilidade + 1) + r.Next(c, direcao + 1) + r.Next(c, eletrico + 1)) / 4;
            int medPilotVel = (r.Next(p, ultrapassagem + 1) + r.Next(p, experiencia + 1) + r.Next(p, rapidez + 1)) / 3;
            int medPilotFis = (r.Next(p, concentracao + 1) + r.Next(p, acertoCarro + 1) + r.Next(p, fisico + 1)) / 3;

            int atributo01 = ((motor + medCarro) + (r.Next(c, medCarro)));
            int atributo02 = (medCarroVel + medPiloto + medPilotVel) * ((retas / 100) + 1);
            int atributo03 = (medCarroQual + medPiloto + medPilotFis) * ((curvas / 100) + 1);
            int atributo04 = (medCarroVel + medPilotVel);
            int atributo05 = (medCarroQual + medPilotFis);
            int atributo06 = ((medPiloto + motor) + (r.Next(c, medPiloto)));
            int atributo07 = (medPilotVel + medPilotFis) * ((importanciPiloto / 100) + 1);
            int atributo08 = (medCarroVel + medCarroQual) * ((importanciaCarro / 100) + 1);
            int atributo09 = (medPilotVel + medPilotFis) * ((retas / 100) + 1);
            int atributo10 = (medCarroVel + medCarroQual) * ((curvas / 100) + 1);

            int somaDosAtributos = (((atributo01 + atributo02 + atributo03 + atributo04 + atributo05 + atributo06 + atributo07 + atributo08 + atributo09 + atributo10) * 7) + r.Next(0, 10));

            int volta = ((tempoBase + t) - somaDosAtributos);

            return volta;
        }
    }
}
