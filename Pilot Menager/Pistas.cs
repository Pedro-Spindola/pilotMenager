using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot_Menager
{
    internal class Pistas
    {
        private string nomeGP;
        private string nomeCircuito;
        private int numerosDeVoltas;
        private int curvas;
        private int retas;
        private int tempoBase;
        private int semanaDaProva;

        public Pistas(string nomeGP, string nomeCircuito, int numerosDeVoltas, int curvas, int retas, int tempoBase)
        {
            // Atribuindo do atributos da Pista.
            this.nomeGP = nomeGP;
            this.nomeCircuito = nomeCircuito;
            this.numerosDeVoltas = numerosDeVoltas;
            this.curvas = curvas;
            this.retas = retas;
            this.tempoBase = tempoBase;

        }
        public void exibirInformacao()
        {
            Console.WriteLine($"NomeGP: {nomeGP}");
            /*
            Console.WriteLine($"Nome do Circuito: {nomeCircuito}");
            Console.WriteLine($"Numero de Voltas: {numerosDeVoltas} Voltas");
            Console.WriteLine($"Curvas: %{curvas}");
            Console.WriteLine($"Retas: %{retas}");
            Console.WriteLine($"Tempo: {tempoBase}");
            */
        }
        public string NomeGp
        {
            get { return nomeGP; }
            set { nomeGP = value; }
        }
        public string NomeCircuito
        {
            get { return nomeCircuito; }
            set { nomeCircuito = value; }
        }
        public int NumerosDeVoltas
        {
            get { return numerosDeVoltas; }
            set { numerosDeVoltas = value; }
        }
        public int Curvas
        {
            get { return curvas; }
            set { curvas = value; }
        }
        public int Retas
        {
            get { return retas; }
            set { retas = value; }
        }
        public int TempoBase
        {
            get { return tempoBase; }
            set { tempoBase = value; }
        }
        public int SemanaDaProva
        {
            get { return semanaDaProva; }
            set { semanaDaProva = value; }
        }
    }
    class FormatacaoTempo
    {
        /*
        // Formatação de saída do tempo de pista.
        double milissegundos = 67200;
        int segundos = (int)(milissegundos / 1000);
        int minutos = segundos / 60;
        int milissegundosRestantes = (int)(milissegundos % 1000);
        string formatoTempo = string.Format("{0}:{1:D2}.{2:D3}", minutos, segundos % 60, milissegundosRestantes);
        Console.WriteLine(formatoTempo);
        */
    }
}
