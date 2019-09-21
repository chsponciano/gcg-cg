using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
    internal class Ponto : ObjetoAramado
    {
        public PrimitiveType forma;
        private Ponto4D _pontoA;
        private Ponto4D _pontoB;
        private Ponto4D _pontoC;
        private Ponto4D _pontoD;

        public Ponto(string rotulo, PrimitiveType forma) : base(rotulo)
        {
            this.forma = forma;
            this._pontoA = new Ponto4D(){ X = -200, Y = 200};
            this._pontoB = new Ponto4D(){ X = 200, Y = 200};
            this._pontoC = new Ponto4D(){ X = 200, Y = -200};
            this._pontoD = new Ponto4D(){ X = -200, Y = -200};
        }

        protected override void DesenharAramado()
        {
            GL.PointSize(5);
            GL.Begin(this.forma);
            
            GL.Color3(Color.Cyan);
            GL.Vertex2(this._pontoA.X, this._pontoA.Y);

            GL.Color3(Color.Magenta);
            GL.Vertex2(this._pontoB.X, this._pontoB.Y);

            GL.Color3(Color.Black);
            GL.Vertex2(this._pontoC.X, this._pontoC.Y);  

            GL.Color3(Color.Yellow);
            GL.Vertex2(this._pontoD.X, this._pontoD.Y);
            
            GL.End();
        }
    }
}