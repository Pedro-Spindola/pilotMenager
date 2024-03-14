using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot_Menager
{
    internal class Equipes
    {
        // Métodos
        private string nomeEquipe;
        private string cor1;
        private string cor2;
        private string corT;
        private string sede;
        private string categoria;

        private int posicaoAtualCampeonato = 1;
        private int pontosCampeonato;
        private int primeiroColocado;
        private int segundoColocado;
        private int terceiroColocado;
        private int classificacaoCampeonato;

        private int aerodinamica;
        private int freio;
        private int asaDianteira;
        private int asaTraseira;
        private int cambio;
        private int eletrico;
        private int direcao;
        private int confiabilidade;
        private int mediaEquipe;
        private int valorDoMotor; // Propriedade para o valor do motor da equipe
        private string nameMotor;
        private Pilotos primeiroPiloto;
        private Pilotos segundoPiloto;

        public Equipes()
        {
        }

        // Método para criar uma nova equiepe.
        public Equipes(string nomeEquipe, string cor1, string cor2, string corT, string sede, int aerodinamica, int freio, int asaDianteira, int asaTraseira, int cambio,
        int eletrico, int direcao, int confiabilidade, string nomeDoMotor, string categoria)
        {
            // Informações
            this.nomeEquipe = nomeEquipe;
            this.cor1 = cor1;
            this.cor2 = cor2;
            this.corT = corT;
            this.sede = sede;

            // Atributos do carro
            this.aerodinamica = aerodinamica;
            this.freio = freio;
            this.asaDianteira = asaDianteira;
            this.asaTraseira = asaTraseira;
            this.cambio = cambio;
            this.eletrico = eletrico;
            this.direcao = direcao;
            this.confiabilidade = confiabilidade;

            mediaEquipe = ((aerodinamica + freio + asaDianteira + asaTraseira + cambio + eletrico + direcao + confiabilidade) / 8);

            nameMotor = nomeDoMotor;

            Motor motor = new Motor();
            valorDoMotor = motor.ObterValorDoMotor(nomeDoMotor);

            this.categoria = categoria;

            pontosCampeonato = 0;
            primeiroColocado = 0;
            segundoColocado = 0;
            terceiroColocado = 0;
        }
        // Get Set
        public string NomeEquipe
        {
            get { return nomeEquipe; }
            set { nomeEquipe = value; }
        }
        public string Sede
        {
            get { return sede; }
            set { sede = value; }
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
        public string CorT
        {
            get { return corT; }
            set { corT = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public Pilotos PrimeiroPiloto
        {
            get { return primeiroPiloto; }
            set { primeiroPiloto = value; }
        }
        public Pilotos SegundoPiloto
        {
            get { return segundoPiloto; }
            set { segundoPiloto = value; }
        }
        public int PosicaoAtualCampeonato
        {
            get { return posicaoAtualCampeonato; }
            set { posicaoAtualCampeonato = value; }
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
        public int ClassificacaoCampeonato
        {
            get { return classificacaoCampeonato; }
            set { classificacaoCampeonato = value; }
        }
        public int Aerodinamica
        {
            get { return aerodinamica; }
            set { aerodinamica = value; }
        }
        public int Freio
        {
            get { return freio; }
            set { freio = value; }
        }
        public int AsaDianteira
        {
            get { return asaDianteira; }
            set { asaDianteira = value; }
        }
        public int AsaTraseira
        {
            get { return asaTraseira; }
            set { asaTraseira = value; }
        }
        public int Cambio
        {
            get { return cambio; }
            set { cambio = value; }
        }
        public int Eletrico
        {
            get { return eletrico; }
            set { eletrico = value; }
        }
        public int Direcao
        {
            get { return direcao; }
            set { direcao = value; }
        }
        public int Confiabilidade
        {
            get { return confiabilidade; }
            set { confiabilidade = value; }
        }
        public int MediaEquipe
        {
            get { return mediaEquipe; }
            set { mediaEquipe = value; }
        }
        public int ValorDoMotor
        {
            get { return valorDoMotor; }
            set { valorDoMotor = value; }
        }
        public string NameMotor
        {
            get { return nameMotor; }
            set { nameMotor = value; }
        }
    }

    class Motor // Class dos Motores
    {
        private Dictionary<string, int> valoresDosMotores = new Dictionary<string, int>();

        public Motor()
        {
            // Add os valores inicial dos Motores.
            valoresDosMotores.Add("Honda", 90);
            valoresDosMotores.Add("Ferrari", 90);
            valoresDosMotores.Add("TAG", 85);
            valoresDosMotores.Add("Mercedes", 85);
            valoresDosMotores.Add("Renault", 80);
            valoresDosMotores.Add("BMW", 80);
            valoresDosMotores.Add("Ford", 75);
            valoresDosMotores.Add("Audi", 75);
            valoresDosMotores.Add("Toyota", 70);
            valoresDosMotores.Add("Lamborghini", 70);
            // ...
        }

        public int ObterValorDoMotor(string nomeDoMotor)
        {
            if (valoresDosMotores.ContainsKey(nomeDoMotor))
            {
                return valoresDosMotores[nomeDoMotor];
            }
            else
            {
                return 0; // Motor desconhecido.
            }
        }
    }
}
