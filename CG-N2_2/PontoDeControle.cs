using System;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL; 
using System.Drawing;

namespace gcgcg {
    class PontoDeControle : ObjetoAramado
    {
        private Ponto4D _ponto;
        public Ponto4D Ponto{
            get => _ponto;
            set => _ponto = value;
        }
        
        public PontoDeControle(string rotulo, Ponto4D ponto, int tamanho, Color cor) : base(rotulo)
        {
            this._ponto = ponto;
            base.PrimitivaCor = cor;
            base.PrimitivaTamanho = tamanho;
            base.PrimitivaTipo = PrimitiveType.Points;

            base.PontosAdicionar(ponto);
        }

        public void Cor(Color cor)
        {
            base.PrimitivaCor = cor;
        }

        private Color Cor()
        {
            return base.PrimitivaCor;
        }

        public void Mover(double x, double y) 
        {
            this._ponto.X += x;
            this._ponto.Y += y;
        }
    }
}