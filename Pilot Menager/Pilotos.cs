using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot_Menager
{
    internal class Pilotos
    {
        // Métodos

        private string nacionalidadePiloto = "";
        private string nomePiloto = "";
        private string sobrenomePiloto = "";
        private string statusPiloto = "Disponivel";
        private double salarioPiloto = 0;
        private int contratoPiloto = 0;
        private string equipePiloto = "";
        private string categoria = "";
        private int posicaoAtualCampeonato;
        private int idadePiloto;
        private int augePiloto;
        private int visibilidadePiloto;
        private int aposentadoriaPiloto;
        private double xpPiloto;
        private double potencialPiloto;
        private string cor1;
        private string cor2;
        private Random random = new Random();
        private int largada;
        private int concentracao;
        private int ultrapassagem;
        private int experiencia;
        private int rapidez;
        private int chuva;
        private int acertoDoCarro;
        private int fisico;
        private int mediaPiloto;
        private int pontosCampeonato;
        private int primeiroColocado;
        private int segundoColocado;
        private int terceiroColocado;
        private double RandomNacionalidade;
        // Dados para treino e corrida.
        private int tempoDeVoltaQualificacao;
        private int tempoDeVoltaCorrida;
        private int tempoDeVoltaMaisRapidaCorrida;
        private int posicaoNaVoltaMaisRapida;
        private int qualificacaoParaCorrida;
        private int posicaoNaCorrida;
        private int diferancaAnt;
        private int tempoCorrida;
        private int resultadoCorrida;
        private int diferancaPri;

        // --- para criar um novo piloto.
        public Pilotos() { }

        public Pilotos(string nome, string sobrenome, string nacionalidad, int idade, int auge, int aposentadoria, int largad, int concent, int ultrapassag, int experience, int rapid, int chuv, int acerto, int fisic)
        {
            PaisPilotos paisPilotos = new PaisPilotos();
            string nacionalidade = nacionalidad;

            nacionalidadePiloto = nacionalidad;
            nomePiloto = nome;
            sobrenomePiloto = sobrenome;
            xpPiloto = 0;
            potencialPiloto = random.Next(60, 81);
            potencialPiloto = (potencialPiloto / 100);

            // Definir de forma aleatória a idade do piloto (18 até 21)
            idadePiloto = idade;

            // Definir de forma aleatória o auge do piloto (30 até 36)
            augePiloto = auge;

            // Definir de forma aleatória a aposentadoria do piloto (36 até 41)
            aposentadoriaPiloto = aposentadoria;

            // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            visibilidadePiloto = random.Next(0, 51);

            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 30)
            largada = largad;
            concentracao = concent;
            ultrapassagem = ultrapassag;
            experiencia = experience;
            rapidez = rapid;
            chuva = chuv;
            acertoDoCarro = acerto;
            fisico = fisic;

            mediaPiloto = ((largada + concentracao + ultrapassagem + experiencia + rapidez + chuva + acertoDoCarro + fisico) / 8);
        }
        public void geraPiloto()
        {
            PaisPilotos paisPilotos = new PaisPilotos();
            string nacionalidade;
            string nomeArquivo;
            string sobrenomeArquivo;
            RandomNacionalidade = 7;
            //RandomNacionalidade = random.Next(0,19);

            // Seleciona uma nacionalidade aleatória
            if (RandomNacionalidade <= 9)
            {
                nacionalidade = paisPilotos.NacionalidadesTop1[random.Next(paisPilotos.NacionalidadesTop1.Count)];
                nacionalidadePiloto = nacionalidade;
            }
            else if (RandomNacionalidade <= 14)
            {
                nacionalidade = paisPilotos.NacionalidadesTop2[random.Next(paisPilotos.NacionalidadesTop2.Count)];
                nacionalidadePiloto = nacionalidade;
            }
            else if (RandomNacionalidade <= 17)
            {
                nacionalidade = paisPilotos.NacionalidadesTop2[random.Next(paisPilotos.NacionalidadesTop2.Count)];
                nacionalidadePiloto = nacionalidade;
            }
            else
            {
                nacionalidade = paisPilotos.NacionalidadesTop3[random.Next(paisPilotos.NacionalidadesTop3.Count)];
                nacionalidadePiloto = nacionalidade;
            }

            // Construa o caminho completo para o arquivo de nomes do piloto
            nomeArquivo = Path.Combine("NomesPilotos", "Piloto_" + nacionalidade + ".txt");
            // Construa o caminho completo para o arquivo do sobrenome do piloto
            sobrenomeArquivo = Path.Combine("SobrenomesPilotos", "Piloto_" + nacionalidade + ".txt");

            // Verifique se o arquivo existe antes de lê-lo
            if (File.Exists(nomeArquivo))
            {
                string[] nomes = File.ReadAllLines(nomeArquivo);

                // Seleciona um nome aleatório a partir dos nomes lidos
                string nomeAleatorio = nomes[random.Next(nomes.Length)];

                // Configure os campos da classe com os valores selecionados
                nomePiloto = nomeAleatorio;
            }
            else
            {
                // Lida com o caso em que o arquivo não foi encontrado
                Console.WriteLine("Arquivo de nomes não encontrado para a nacionalidade: " + nacionalidade);
            }
            // Verifique se o arquivo existe antes de lê-lo
            if (File.Exists(sobrenomeArquivo))
            {
                string[] ssnomes = File.ReadAllLines(sobrenomeArquivo);

                // Seleciona um nome aleatório a partir dos nomes lidos
                string sobrenomeAleatorio = ssnomes[random.Next(ssnomes.Length)];

                // Configure os campos da classe com os valores selecionados
                sobrenomePiloto = sobrenomeAleatorio;
            }
            else
            {
                // Lida com o caso em que o arquivo não foi encontrado
                Console.WriteLine("Arquivo de nomes não encontrado para a nacionalidade: " + nacionalidade);
            }

            xpPiloto = 0;
            potencialPiloto = random.Next(60, 81);
            potencialPiloto = (potencialPiloto / 100);

            // Definir de forma aleatória a idade do piloto (18 até 21)
            idadePiloto = random.Next(18, 22);

            // Definir de forma aleatória o auge do piloto (30 até 36)
            augePiloto = random.Next(30, 37);

            // Definir de forma aleatória a aposentadoria do piloto (36 até 41)
            aposentadoriaPiloto = random.Next(36, 42);

            // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            visibilidadePiloto = random.Next(0, 51);

            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 40)
            largada = random.Next(10, 40);
            concentracao = random.Next(10, 40);
            ultrapassagem = random.Next(10, 40);
            experiencia = random.Next(10, 40);
            rapidez = random.Next(10, 40);
            chuva = random.Next(10, 40);
            acertoDoCarro = random.Next(10, 40);
            fisico = random.Next(10, 40);

            mediaPiloto = ((largada + concentracao + ultrapassagem + experiencia + rapidez + chuva + acertoDoCarro + fisico) / 8);

        }
        // Criar o piloto do Jogador
        public void geraPiloto(string nn, string sn, string nac)
        {
            PaisPilotos paisPilotos = new PaisPilotos();
            string nacionalidade = nac;

            nacionalidadePiloto = nac;
            nomePiloto = nn;
            sobrenomePiloto = sn;
            xpPiloto = 0;
            potencialPiloto = random.Next(60, 81);
            potencialPiloto = (potencialPiloto / 100);

            // Definir de forma aleatória a idade do piloto (18 até 20)
            idadePiloto = random.Next(18, 21);

            // Definir de forma aleatória o auge do piloto (30 até 34)
            augePiloto = random.Next(30, 35);

            // Definir de forma aleatória a aposentadoria do piloto (36 até 42)
            aposentadoriaPiloto = random.Next(36, 43);

            // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            visibilidadePiloto = random.Next(0, 51);

            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 30)
            /*
            largada = random.Next(10, 30);
            concentracao = random.Next(10, 30);
            ultrapassagem = random.Next(10, 30);
            experiencia = random.Next(10, 30);
            rapidez = random.Next(10, 30);
            chuva = random.Next(10, 30);
            acertoDoCarro = random.Next(10, 30);
            fisico = random.Next(10, 30);
            */
            largada = 30;
            concentracao = 27;
            ultrapassagem = 28;
            experiencia = 20;
            rapidez = 28;
            chuva = 30;
            acertoDoCarro = 24;
            fisico = 22;

            mediaPiloto = ((largada + concentracao + ultrapassagem + experiencia + rapidez + chuva + acertoDoCarro + fisico) / 8);
        }
        public List<Pilotos.PilotoTemporadas> pilotosTemporadas = new List<Pilotos.PilotoTemporadas>();

        // Método para adicionar um piloto campeão à lista
        public void AdicionarHistoricosPiloto(int position, int ano, string cor1, string equipe, int pontos, string catAtual)
        {
            pilotosTemporadas.Add(new Pilotos.PilotoTemporadas { Position = position,  Ano = ano, C1 = cor1, Equipe = equipe, Pontos = pontos, CategoriaAtual = catAtual });
        }
        public class PilotoTemporadas
        {
            public int Position { get; set; }
            public int Ano { get; set; }
            public string C1 { get; set; }
            public string Equipe { get; set; }
            public int Pontos { get; set; }
            public string CategoriaAtual { get; set; }
        }
        // Get Set
        public string NacionalidadePiloto
        {
            get { return nacionalidadePiloto; }
            set { nacionalidadePiloto = value; }
        }
        public string NomePiloto
        {
            get { return nomePiloto; }
            set { nomePiloto = value; }
        }
        public string SobrenomePiloto
        {
            get { return sobrenomePiloto; }
            set { sobrenomePiloto = value; }
        }
        public double SalarioPiloto
        {
            get { return salarioPiloto; }
            set { salarioPiloto = value; }
        }
        public string StatusPiloto
        {
            get { return statusPiloto; }
            set { statusPiloto = value; }
        }
        public int ContratoPiloto
        {
            get { return contratoPiloto; }
            set { contratoPiloto = value; }
        }
        public string EquipePiloto
        {
            get { return equipePiloto; }
            set { equipePiloto = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public int PosicaoAtualCampeonato
        {
            get { return posicaoAtualCampeonato; }
            set { posicaoAtualCampeonato = value; }
        }
        public int IdadePiloto
        {
            get { return idadePiloto; }
            set { idadePiloto = value; }
        }
        public double PotencialPiloto
        {
            get { return potencialPiloto; }
            set { potencialPiloto = value; }
        }
        public double XpPiloto
        {
            get { return xpPiloto; }
            set { xpPiloto = value; }
        }
        public int AugePiloto
        {
            get { return augePiloto; }
            set { augePiloto = value; }
        }
        public int AposentadoriaPiloto
        {
            get { return aposentadoriaPiloto; }
            set { aposentadoriaPiloto = value; }
        }
        public int VisibilidadePiloto
        {
            get { return visibilidadePiloto; }
            set { visibilidadePiloto = value; }
        }
        public string Cor1
        {
            get { return cor1; }
            set { cor1 = value; }
        }
        public string Cor2
        {
            get { return cor2; }
            set { cor2 = value; }
        }
        public int Largada
        {
            get { return largada; }
            set { largada = value; }
        }
        public int Concentracao
        {
            get { return concentracao; }
            set { concentracao = value; }
        }
        public int Ultrapassagem
        {
            get { return ultrapassagem; }
            set { ultrapassagem = value; }
        }
        public int Experiencia
        {
            get { return experiencia; }
            set { experiencia = value; }
        }
        public int Rapidez
        {
            get { return rapidez; }
            set { rapidez = value; }
        }
        public int Chuva
        {
            get { return chuva; }
            set { chuva = value; }
        }
        public int AcertoDoCarro
        {
            get { return acertoDoCarro; }
            set { acertoDoCarro = value; }
        }
        public int Fisico
        {
            get { return fisico; }
            set { fisico = value; }
        }
        public int MediaPiloto
        {
            get { return mediaPiloto; }
            set { mediaPiloto = value; }
        }
        public int PontosCampeonato
        {
            get { return pontosCampeonato; }
            set { pontosCampeonato = value; }
        }
        public int PrimeiroColocado
        {
            get { return primeiroColocado; }
            set { primeiroColocado = value; }
        }
        public int SegundoColocado
        {
            get { return segundoColocado; }
            set { segundoColocado = value; }
        }
        public int TerceiroColocado
        {
            get { return terceiroColocado; }
            set { terceiroColocado = value; }
        }
        public int TempoDeVoltaQualificacao
        {
            get { return tempoDeVoltaQualificacao; }
            set { tempoDeVoltaQualificacao = value; }
        }
        public int TempoDeVoltaCorrida
        {
            get { return tempoDeVoltaCorrida; }
            set { tempoDeVoltaCorrida = value; }
        }
        public int TempoDeVoltaMaisRapidaCorrida
        {
            get { return tempoDeVoltaMaisRapidaCorrida; }
            set { tempoDeVoltaMaisRapidaCorrida = value; }
        }
        public int PosicaoNaVoltaMaisRapida
        {
            get { return posicaoNaVoltaMaisRapida; }
            set { posicaoNaVoltaMaisRapida = value; }
        }
        public int QualificacaoParaCorrida
        {
            get { return qualificacaoParaCorrida; }
            set { qualificacaoParaCorrida = value; }
        }
        public int PosicaoNaCorrida
        {
            get { return posicaoNaCorrida; }
            set { posicaoNaCorrida = value; }
        }
        public int TempoCorrida
        {
            get { return tempoCorrida; }
            set { tempoCorrida = value; }
        }
        public int ResultadoCorrida
        {
            get { return resultadoCorrida; }
            set { resultadoCorrida = value; }
        }
        public int DiferancaAnt
        {
            get { return diferancaAnt; }
            set { diferancaAnt = value; }
        }
        public int DiferancaPri
        {
            get { return diferancaPri; }
            set { diferancaPri = value; }
        }
    }
    public class PaisPilotos
    {
        public List<string> NacionalidadesTop1 { get; } = new List<string>
        {
            "Brasil",
            "Alemanha",
            "Austrália",
            "Bélgica",
            "Canadá",
            "Dinamarca",
            "Espanha",
            "Estados Unidos",
            "França",
            "Inglaterra",
            "Itália",
            "Japão",
            "México",
            "Noruega",
            "Holanda",
            // Adicione outras nacionalidades conforme necessário
        };
        public List<string> NacionalidadesTop2 { get; } = new List<string>
        {
            "Brasil",
            "Alemanha",
            "Austrália",
            "Bélgica",
            "Canadá",
            "Dinamarca",
            "Espanha",
            "Estados Unidos",
            "França",
            "Inglaterra",
            "Itália",
            "Japão",
            "México",
            "Noruega",
            "Holanda",
            "Arábia Saudita",
            "Argentina",
            "Catar",
            "China",
            "Emirados Árabes Unidos",
            "Africa do Sul",
            "Israel",
            "Mônaco",
            "Nova Zelândia",
            "Polônia",
            "Portugal",
            "Rússia",
            "Suécia",
            "Suíça",
            "Austria",
            // Adicione outras nacionalidades conforme necessário
        };
        public List<string> NacionalidadesTop3 { get; } = new List<string>
        {
            "Brasil",
            "Alemanha",
            "Austrália",
            "Bélgica",
            "Canadá",
            "Dinamarca",
            "Espanha",
            "Estados Unidos",
            "França",
            "Inglaterra",
            "Itália",
            "Japão",
            "México",
            "Noruega",
            "Holanda",
            "Arábia Saudita",
            "Argentina",
            "Catar",
            "China",
            "Emirados Árabes Unidos",
            "Africa do Sul",
            "Israel",
            "Mônaco",
            "Nova Zelândia",
            "Polônia",
            "Portugal",
            "Rússia",
            "Suécia",
            "Suíça",
            "Austria",
            "Albânia",
            "Argélia",
            "Armênia",
            "Bahrein",
            "Chile",
            "Colômbia",
            "Egito",
            "Filândia",
            "Grécia",
            "Índia",
            "Irlanda",
            "Islândia",
            "Marrocos",
            "Turquia",
            // Adicione outras nacionalidades conforme necessário
        };
        public List<string> NacionalidadesTop4 { get; } = new List<string>
        {
            "Brasil",
            "Alemanha",
            "Austrália",
            "Bélgica",
            "Canadá",
            "Dinamarca",
            "Espanha",
            "Estados Unidos",
            "França",
            "Inglaterra",
            "Itália",
            "Japão",
            "México",
            "Noruega",
            "Holanda",
            "Arábia Saudita",
            "Argentina",
            "Catar",
            "China",
            "Emirados Árabes Unidos",
            "Africa do Sul",
            "Israel",
            "Mônaco",
            "Nova Zelândia",
            "Polônia",
            "Portugal",
            "Rússia",
            "Suécia",
            "Suíça",
            "Austria",
            "Albânia",
            "Argélia",
            "Armênia",
            "Bahrein",
            "Chile",
            "Colômbia",
            "Egito",
            "Filândia",
            "Grécia",
            "Índia",
            "Irlanda",
            "Islândia",
            "Marrocos",
            "Turquia",
            "Afeganistão",
            "Andorra",
            "Angola",
            "Azerbaijão",
            "Bangladesh",
            "Bósnia - Herzegovina",
            "Camarões",
            "Congo",
            "Corei do Sul",
            "Costa do Marfim",
            "Equador",
            "Eslováquia",
            "Eslovênia",
            "Estônia",
            "Hungria",
            "Nigéria",
            "Paraguai",
            "Peru",
            "República Tcheca",
            "Sérvia",
            "Singapura",
            "Tailândia",
            "Tunísia",
            "Ucrânia",
            "Uruguai",
            "Bahamas",
            "Barbados",
            "Bielorrússia",
            "Bulgária",
            "Costa Rica",
            "Cuba",
            // Adicione outras nacionalidades conforme necessário
        };
    }
}
