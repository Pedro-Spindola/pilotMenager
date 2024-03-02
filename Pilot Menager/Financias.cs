using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilot_Menager
{
    internal class Financias
    {
        private double mySaldoJogador = 1000000;
        private double mySaldoJogadorSemanal = 10000;
        public double MySaldoJogador
        {
            get { return mySaldoJogador; }
            set { mySaldoJogador = value; }
        }
        public double MySaldoJogadorSemanal
        {
            get { return mySaldoJogadorSemanal; }
            set { mySaldoJogadorSemanal = value; }
        }
    }
}
