using System;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        private double _raio;
        private int _tamanho;
        private int _eixoX;
        private int _eixoY;

        public Circulo(string rotulo, int eixoX, int eixoY, double raio, int tamanho = 5) : base(rotulo)
        {
            this._eixoX = eixoX;
            this._eixoY = eixoY;
            this._raio = raio;
            this._tamanho = tamanho;
        }

        protected override void DesenharAramado()
        {
            GL.PointSize(this._tamanho);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Black);

            double aux = .0;
            for (double i = .0; i <= 72.0; i++) 
            {
                var pontoAtual = Matematica.GerarPtosCirculo(aux, this._raio);
                GL.Vertex2(pontoAtual.X + this._eixoX, pontoAtual.Y + this._eixoY);
                aux += 5;
            }

            GL.End();
        }
        
        public Ponto4D Centro()
        {
            return new Ponto4D(this._eixoX, this._eixoY);
        }
    }
}