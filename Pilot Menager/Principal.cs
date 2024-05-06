using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Pilot_Menager
{
    public class Principal
    {
        Historicos historico = new Historicos();
        private Random random = new Random();

        private string corPrincipal = "";
        private string corSecundaria = "";
        private string corTexto = "Automatico";

        private string nomeJogador = "";
        private string sobrenomeJogador = "";
        private int idadeJogador = 18;              
        private string nacionalidadeJogador = "";
        private int habilidadeJogador = 32;
        private int myIndex = 99;

        private int contadorDeSemana = 1;
        private int contadorDeAno = 2024;
        private string statusDaTemporada = "Pre-Temporada";
        private int totalSemanas = 10;  // 52 semanas

        private string proxGP = "";
        private string proxGPais = "";
        private int proxGPSemana = 0;
        private int etapaAtual = 0;
        private int proxGPVoltas = 0;

        // Variavel sendo utilizada na Tela de Classificação.
        public string categoria = "";

        private int configInicioGame = 0;                   // 1 = Novo Jogo, 2 = Continuar Game
        private int tempoRodada = 200;                       // Tempo que vai passar o gamer em milessegundos. 200

        private int importaciaCarroTemporada = 50;
        private int importanciaPilotoTemporada = 50;

        private int primeiroLugar;
        private int segundoLugar;
        private int terceiroLugar;
        private int quartoLugar;
        private int quintoLugar;
        private int sextoLugar;
        private int setimoLugar;
        private int oitavoLugar;
        private int nonoLugar;
        private int decimoLugar;
        private int decimoPrimeiroLugar;
        private int decimoSegundoLugar;
        private int decimoTerceiroLugar;
        private int decimoQuartoLugar;
        private int decimoQuintoLugar;
        private int decimoSextoLugar;
        private int pontoVoltaMaisRapida;

        public void ConfigurarFaixaDePontuacao(String caminhoArquivo)
        {
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                // Lê e atribui os valores das variáveis
                primeiroLugar = int.Parse(sr.ReadLine());
                segundoLugar = int.Parse(sr.ReadLine());
                terceiroLugar = int.Parse(sr.ReadLine());
                quartoLugar = int.Parse(sr.ReadLine());
                quintoLugar = int.Parse(sr.ReadLine());
                sextoLugar = int.Parse(sr.ReadLine());
                setimoLugar = int.Parse(sr.ReadLine());
                oitavoLugar = int.Parse(sr.ReadLine());
                nonoLugar = int.Parse(sr.ReadLine());
                decimoLugar = int.Parse(sr.ReadLine());
                decimoPrimeiroLugar = int.Parse(sr.ReadLine());
                decimoSegundoLugar = int.Parse(sr.ReadLine());
                decimoTerceiroLugar = int.Parse(sr.ReadLine());
                decimoQuartoLugar = int.Parse(sr.ReadLine());
                decimoQuintoLugar = int.Parse(sr.ReadLine());
                decimoSextoLugar = int.Parse(sr.ReadLine());
                pontoVoltaMaisRapida = int.Parse(sr.ReadLine());
            }
        }
        public void ContinuarTurno()
        {
            if (totalSemanas > contadorDeSemana)
            {
                contadorDeSemana++;
                if (contadorDeSemana > 4 && contadorDeSemana <= 9) //(contadorDeSemana > 4 && contadorDeSemana <= 48)
                {
                    statusDaTemporada = "Andamento";
                }
                else if (contadorDeSemana > 9) //(contadorDeSemana > 48)
                {
                    statusDaTemporada = "Fim-Temporada";
                }
            }
            else
            {
                contadorDeAno++;
                contadorDeSemana = 1;
                statusDaTemporada = "Pre-Temporada";
            }
        }
        public string formatarNumero(double tempoTotalMilissegundos)
        {
            if(tempoTotalMilissegundos > 3599999)
            {
                // Convertendo milissegundos para horas, minutos, segundos e milissegundos
                int horas = (int)(tempoTotalMilissegundos / (1000 * 60 * 60)); // Obtém as horas
                int minutos = (int)((tempoTotalMilissegundos / (1000 * 60)) % 60); // Obtém os minutos
                int segundos = (int)((tempoTotalMilissegundos / 1000) % 60); // Obtém os segundos
                int milissegundos = (int)(tempoTotalMilissegundos % 1000); // Obtém os milissegundos

                // Formatando o tempo
                string tempoFormatado = $"{horas}:{minutos:00}:{segundos:00}.{milissegundos:000}";
                return tempoFormatado; ;
            } else
            {
                // Convertendo milissegundos para minutos, segundos e milissegundos
                int minutos = (int)(tempoTotalMilissegundos / (1000 * 60)); // Obtém os minutos
                int segundos = (int)((tempoTotalMilissegundos / 1000) % 60); // Obtém os segundos
                int milissegundos = (int)(tempoTotalMilissegundos % 1000); // Obtém os milissegundos

                // Formatando o tempo
                string tempoFormatado = $"{minutos}:{segundos:00}.{milissegundos:000}";
                return tempoFormatado;
            }
        }
        public string nomeAbreviado(string nome, string sobrenome)
        {
            char primeiraLetra = nome[0];
            string nomeCompleto = $"{primeiraLetra}. {sobrenome}";

            return nomeCompleto;
        }
        public static List<Principal> ObterListaCategoria()
        {
            List<Principal> listSerie = new List<Principal>
            {
            new Principal { categoria = "F1"},
            new Principal { categoria = "F2"},
            new Principal { categoria = "F3"},
            // Adicione outros países aqui
            };
            return listSerie;
        }

        public List<Historicos.PilotoCampeao> pilotosCampeoesF1 = new List<Historicos.PilotoCampeao>();
        public List<Historicos.PilotoCampeao> pilotosCampeoesF2 = new List<Historicos.PilotoCampeao>();
        public List<Historicos.PilotoCampeao> pilotosCampeoesF3 = new List<Historicos.PilotoCampeao>();

        // Método para adicionar um piloto campeão à lista
        public void AdicionarPilotoCampeao(string categoria, int ano, string sede, string nome, int pontos, string cor1, string equipe)
        {
            if (categoria == "F1")
            {
                pilotosCampeoesF1.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
            }
            else if (categoria == "F2")
            {
                pilotosCampeoesF2.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
            }
            else if (categoria == "F3")
            {
                pilotosCampeoesF3.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
            }
        }

        public List<Historicos.EquipeCampeao> equipesCampeoesF1 = new List<Historicos.EquipeCampeao>();
        public List<Historicos.EquipeCampeao> equipesCampeoesF2 = new List<Historicos.EquipeCampeao>();
        public List<Historicos.EquipeCampeao> equipesCampeoesF3 = new List<Historicos.EquipeCampeao>();

        // Método para adicionar um equipe campeão à lista
        public void AdicionarEquipeCampeao(string categoria, int ano, string sede, string cor1, string nome, int pontos)
        {
            if (categoria == "F1")
            {
                equipesCampeoesF1.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
            }
            else if (categoria == "F2")
            {
                equipesCampeoesF2.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
            }
            else if (categoria == "F3")
            {
                equipesCampeoesF3.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
            }
        }

        internal void Transferencia(Pilotos[] pilot, int indice1, int indice2)
        {
            Pilotos temp = pilot[indice1];
            pilot[indice1] = pilot[indice2];
            pilot[indice2] = temp;
            myIndex = indice1;
        }

        List<string> atrinutosList = new List<string>()
        {
            "largada",
            "concentracao",
            "ultrapassagem",
            "experiencia",
            "rapidez",
            "chuva",
            "acertoDoCarro",
            "fisico"
        };
        List<string> atributosListEquipes = new List<string>()
        {
            "aerodinamica",
            "freio",
            "asaDianteira",
            "asaTraseira",
            "cambio",
            "eletrico",
            "direcao",
            "confiabilidade"
        };
        internal void XpTurnoSemanal(Pilotos[] pilotos)
        {
            foreach (Pilotos piloto in pilotos)
            {   
                double newXp = piloto.XpPiloto + piloto.PotencialPiloto;
                piloto.XpPiloto = newXp;

                
                if(piloto.XpPiloto >= 1)
                {
                    if (piloto.IdadePiloto <= piloto.AugePiloto)
                    {
                        // Adicionar pontos para acrescentar.
                        do
                        {
                            if (piloto.XpPiloto >= 1 && piloto.MediaPiloto < 100)
                            {
                                string atributoAleatorio = atrinutosList[random.Next(atrinutosList.Count)];
                                switch (atributoAleatorio)
                                {
                                    case "largada":
                                        if (piloto.Largada < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Largada++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "concentracao":
                                        if (piloto.Concentracao < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Concentracao++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "ultrapassagem":
                                        if (piloto.Ultrapassagem < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Ultrapassagem++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "experiencia":
                                        if (piloto.Experiencia < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Experiencia++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "rapidez":
                                        if (piloto.Rapidez < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Rapidez++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "chuva":
                                        if (piloto.Chuva < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Chuva++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "acertoDoCarro":
                                        if (piloto.AcertoDoCarro < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.AcertoDoCarro++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "fisico":
                                        if (piloto.Fisico < 100)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Fisico++;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (piloto.MediaPiloto == 100)
                            {
                                piloto.XpPiloto--;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        } while (true);
                    }
                    else
                    {
                        do
                        {
                            if (piloto.XpPiloto >= 1 && piloto.MediaPiloto > 1)
                            {
                                string atributoAleatorio = atrinutosList[random.Next(atrinutosList.Count)];
                                switch (atributoAleatorio)
                                {
                                    case "largada":
                                        if (piloto.Largada > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Largada--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "concentracao":
                                        if (piloto.Concentracao > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Concentracao--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "ultrapassagem":
                                        if (piloto.Ultrapassagem > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Ultrapassagem--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "experiencia":
                                        if (piloto.Experiencia > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Experiencia--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "rapidez":
                                        if (piloto.Rapidez > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Rapidez--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "chuva":
                                        if (piloto.Chuva > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Chuva--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "acertoDoCarro":
                                        if (piloto.AcertoDoCarro > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.AcertoDoCarro--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    case "fisico":
                                        if (piloto.Fisico > 0)
                                        {
                                            piloto.XpPiloto--;
                                            piloto.Fisico--;
                                            piloto.AtualizarMedia();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        } while (true);
                    }
                }
            }
        }
        internal void XpEquipeSemanal(Equipes[] equipes)
        {
            foreach (Equipes equipe in equipes)
            {
                int newXp = random.Next(1, 4);   // Vai sortear entre 1 e 5 (1 = -1      2 = 0      3 = 1)

                do
                {
                    if (equipe.MediaEquipe <= 100 && equipe.MediaEquipe >= 10 && newXp != 0)
                    {
                        string atributoAleatorio = atributosListEquipes[random.Next(atributosListEquipes.Count)];
                        switch (atributoAleatorio)
                        {
                            case "aerodinamica":
                                if (equipe.Aerodinamica <= 100 && equipe.Aerodinamica >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Aerodinamica <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Aerodinamica -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Aerodinamica >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Aerodinamica += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "freio":
                                if (equipe.Freio <= 100 && equipe.Freio >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Freio <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Freio -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Freio >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Freio += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "asaDianteira":
                                if (equipe.AsaDianteira <= 100 && equipe.AsaDianteira >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.AsaDianteira <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.AsaDianteira -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.AsaDianteira >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.AsaDianteira += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "asaTraseira":
                                if (equipe.AsaTraseira <= 100 && equipe.AsaTraseira >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.AsaTraseira <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.AsaTraseira -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.AsaTraseira >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.AsaTraseira += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "cambio":
                                if (equipe.Cambio <= 100 && equipe.Cambio >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Cambio <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Cambio -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Cambio >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Cambio += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "eletrico":
                                if (equipe.Eletrico <= 100 && equipe.Eletrico >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Eletrico <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Eletrico -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Eletrico >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Eletrico += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "direcao":
                                if (equipe.Direcao <= 100 && equipe.Direcao >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Direcao <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Direcao -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Direcao >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Direcao += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            case "confiabilidade":
                                if (equipe.Confiabilidade <= 100 && equipe.Confiabilidade >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipe.Confiabilidade <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Confiabilidade -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipe.Confiabilidade >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipe.Confiabilidade += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipe.AtualizarMedia();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else if (equipe.MediaEquipe == 100 || equipe.MediaEquipe == 9)
                    {
                        newXp = 0;
                        break;
                    }
                    else
                    {
                        newXp = 0;
                        break;
                    }
                } while (true);
            }
        }
        
        // Get e Set
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public string CorPrincipal
        {
            get { return corPrincipal; }
            set { corPrincipal = value; }
        }
        public string CorSecundaria
        {
            get { return corSecundaria; }
            set { corSecundaria = value; }
        }
        public string CorTexto
        { 
            get { return corTexto; }
            set { corTexto = value; }
        }
        public string NomeJogador
        {
            get { return nomeJogador; }
            set { nomeJogador = value; }
        }
        public string SobrenomeJogador
        {
            get { return sobrenomeJogador; }
            set { sobrenomeJogador = value; }
        }
        public int IdadeJogador
        {
            get { return idadeJogador; }
            set { idadeJogador = value; }
        }
        public string NacionalidadeJogador
        {
            get { return nacionalidadeJogador; }
            set { nacionalidadeJogador = value; }
        }
        public int MyIndex
        {
            get { return myIndex; }
            set { myIndex = value; }
        }
        public int HabilidadeJogador
        {
            get { return habilidadeJogador;}
            set { habilidadeJogador = value; }
        }
        public int ContadorDeSemana
        {
            get { return contadorDeSemana; }
            set { contadorDeSemana = value; }
        }
        public int ContadorDeAno
        {
            get { return contadorDeAno; }
            set { contadorDeAno = value; }
        }
        public string StatusDaTemporada
        {
            get { return statusDaTemporada; }
            set { statusDaTemporada = value; }
        }
        public int TotalSemanas
        {
            get { return totalSemanas; }
            set { totalSemanas = value; }
        }
        public string ProxGP
        {
            get { return proxGP; }
            set { proxGP = value; }
        }
        public string ProxGPais
        {
            get { return proxGPais; }
            set { proxGPais = value; }
        }
        public int ProxGPSemana
        {
            get { return proxGPSemana; }
            set { proxGPSemana = value; }
        }
        public int ProxGPVoltas
        {
            get { return proxGPVoltas; }
            set { proxGPVoltas = value; }
        }
        public int EtapaAtual
        {
            get { return etapaAtual; }
            set { etapaAtual = value; }
        }
        public int ConfigInicioGame
        {
            get { return configInicioGame; }
            set { configInicioGame = value; }
        }
        public int TempoRodada
        {
            get { return tempoRodada; }
            set { tempoRodada = value; }
        }
        public int ImportanciaCarroTemporada
        {
            get { return importaciaCarroTemporada; }
            set { importaciaCarroTemporada = value; }
        }
        public int ImportanciaPilotoTemporada
        {
            get { return importanciaPilotoTemporada; }
            set { importanciaPilotoTemporada = value; }
        }
        public int PrimeiroLugar
        {
            get { return primeiroLugar; }
            set { primeiroLugar = value; }
        }
        public int SegundoLugar
        {
            get { return segundoLugar; }
            set { segundoLugar = value; }
        }
        public int TerceiroLugar
        {
            get { return terceiroLugar; }
            set { terceiroLugar = value; }
        }
        public int QuartoLugar
        {
            get { return quartoLugar; }
            set { quartoLugar = value; }
        }
        public int QuintoLugar
        {
            get { return quintoLugar; }
            set { quintoLugar = value; }
        }
        public int SextoLugar
        {
            get { return sextoLugar; }
            set { sextoLugar = value; }
        }
        public int SetimoLugar
        {
            get { return setimoLugar; }
            set { setimoLugar = value; }
        }
        public int OitavoLugar
        {
            get { return oitavoLugar; }
            set { oitavoLugar = value; }
        }
        public int NonoLugar
        {
            get { return nonoLugar; }
            set { nonoLugar = value; }
        }
        public int DecimoLugar
        {
            get { return decimoLugar; }
            set { decimoLugar = value; }
        }
        public int DecimoPrimeiroLugar
        {
            get { return decimoPrimeiroLugar; }
            set { decimoPrimeiroLugar = value; }
        }
        public int DecimoSegundoLugar
        {
            get { return decimoSegundoLugar; }
            set { decimoSegundoLugar = value; }
        }
        public int DecimoTerceiroLugar
        {
            get { return decimoTerceiroLugar; }
            set { decimoTerceiroLugar = value; }
        }
        public int DecimoQuartoLugar
        {
            get { return decimoQuartoLugar; }
            set { decimoQuartoLugar = value; }
        }
        public int DecimoQuintoLugar
        {
            get { return decimoQuintoLugar; }
            set { decimoQuintoLugar = value; }
        }
        public int DecimoSextoLugar
        {
            get { return decimoSextoLugar; }
            set { decimoSextoLugar = value; }
        }
        public int PontoVoltaMaisRapida
        {
            get { return pontoVoltaMaisRapida; }
            set { pontoVoltaMaisRapida = value; }
        }

    }
    public class Historicos
    {
        public class PilotoCampeao
        {
            public int Ano { get; set; }
            public string Sede { get; set; }
            public string Nome { get; set; }
            public int Pontos { get; set; }
            public string C1 { get; set; }
            public string Equipe { get; set; }
        }
        public class EquipeCampeao
        {
            public int Ano { get; set; }
            public string Sede { get; set; }
            public string C1 { get; set; }
            public string Nome { get; set; }
            public int Pontos { get; set; }
        }
    }
}
