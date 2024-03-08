using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot_Menager
{
    public class Principal
    {
        Historicos historico = new Historicos();

        private string corPrincipal = "";
        private string corSecundaria = "";
        private string corTexto = "Automatico";

        private string nomeJogador = "";
        private string sobrenomeJogador = "";
        private int idadeJogador = 18;              
        private string nacionalidadeJogador = "";
        private int habilidadeJogador = 32;             // Deverá gerar de forma aleatoria.

        private int contadorDeSemana = 01;
        private int contadorDeAno = 2024;
        private string statusDaTemporada = "Pre-Temporada";
        private int totalSemanas = 10;

        private string proxGP = "";
        private string proxGPais = "";
        private int proxGPSemana = 0;
        private int etapaAtual = 0;

        // Variavel sendo utilizada na Tela de Classificação.
        public string categoria = "";

        private int configInicioGame = 0;                   // 1 = Novo Jogo, 2 = Continuar Game
        private int tempoRodada = 200;                       // Tempo que vai passar o gamer em milessegundos.

        private int importaciaCarroTemporada = 50;
        private int importanciaPilotoTemporada = 50;

        private int primeiroLugar = 25;
        private int segundoLugar = 18;
        private int terceiroLugar = 15;
        private int quartoLugar = 12;
        private int quintoLugar = 10;
        private int sextoLugar = 8;
        private int setimoLugar = 6;
        private int oitavoLugar = 4;
        private int nonoLugar = 2;
        private int decimoLugar = 1;
        private int pontoVoltaMaisRapida = 1;
        public void ContinuarTurno()
        {
            if (totalSemanas > contadorDeSemana)
            {
                contadorDeSemana++;
                if (contadorDeSemana > 2 && contadorDeSemana <= 8)
                {
                    statusDaTemporada = "Andamento";
                }
                else if (contadorDeSemana > 8)
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
        public void AdicionarPilotoCampeao(string categoria, int ano, string sede, string nome, int pontos, string equipe)
        {
            if (categoria == "F1")
            {
                pilotosCampeoesF1.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, Equipe = equipe });
            }
            else if (categoria == "F2")
            {
                pilotosCampeoesF2.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, Equipe = equipe });
            }
            else if (categoria == "F3")
            {
                pilotosCampeoesF3.Add(new Historicos.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, Equipe = equipe });
            }
        }

        public List<Historicos.EquipeCampeao> equipesCampeoesF1 = new List<Historicos.EquipeCampeao>();
        public List<Historicos.EquipeCampeao> equipesCampeoesF2 = new List<Historicos.EquipeCampeao>();
        public List<Historicos.EquipeCampeao> equipesCampeoesF3 = new List<Historicos.EquipeCampeao>();

        // Método para adicionar um equipe campeão à lista
        public void AdicionarEquipeCampeao(string categoria, int ano, string sede, string nome, int pontos)
        {
            if (categoria == "F1")
            {
                equipesCampeoesF1.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos });
            }
            else if (categoria == "F2")
            {
                equipesCampeoesF2.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos });
            }
            else if (categoria == "F3")
            {
                equipesCampeoesF3.Add(new Historicos.EquipeCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos });
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
            //public string C1 { get; set; }
            //public string C2 { get; set; }
            public string Equipe { get; set; }
        }
        public class EquipeCampeao
        {
            public int Ano { get; set; }
            public string Sede { get; set; }
            //public string C1 { get; set; }
            //public string C2 { get; set; }
            public string Nome { get; set; }
            public int Pontos { get; set; }
        }
    }
}
