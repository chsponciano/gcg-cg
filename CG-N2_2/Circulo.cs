using System;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        public Circulo(string rotulo) : base(rotulo)
        {
            for (double ponto = .0; ponto <= 72.0; ponto++) 
            {
                var _anguloAtual = this.CalcularAngulo(ponto);
                base.PontosAdicionar(new Ponto4D(this.CalcularEixoX(_anguloAtual), this.CalcularEixoY(_anguloAtual)));
            }
        }

        private double CalcularAngulo(double ponto) => 2.0 * Math.PI * ponto / 72.0;

        private double CalcularEixoX(double angulo) => 100 * Math.Sin(angulo);

        private double CalcularEixoY(double angulo) => 100 * Math.Cos(angulo);
    }
}