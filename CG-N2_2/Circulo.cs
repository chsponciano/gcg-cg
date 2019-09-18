using System;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        public Circulo(string rotulo) : base(rotulo)
        {
            this.criarPontos();
        }

        private void criarPontos()
        {
            base.PontosRemoverTodos();
            for (double i = .0; i <= 72.0; i++)
            {
                double theta = 2.0 * Math.PI * i / 72.0;
                base.PontosAdicionar(new Ponto4D(100.0 * Math.Sin(theta), 100.0 * Math.Cos(theta)));
            }

        }
    }
}