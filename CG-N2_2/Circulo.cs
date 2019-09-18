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
                var theta = 2.0 * Math.PI * i / 72.0;
                var x = 100.0 * Math.Sin(theta);
                var y = 100.0 * Math.Cos(theta);
                base.PontosAdicionar(new Ponto4D(x, y));
            }

        }
    }
}