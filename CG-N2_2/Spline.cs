using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{

    internal class Spline : SegReta
    {
        private Ponto4D _pontoA;
        private Ponto4D _pontoB;
        private Ponto4D _pontoC;
        private Ponto4D _pontoD;
        private double _CURVATURA = .0625;

        public Spline(string rotulo, Ponto4D pontoA, Ponto4D pontoB, Ponto4D pontoC, Ponto4D pontoD, int tamanho, Color cor) : base(rotulo, pontoD, pontoA, tamanho, cor)
        {
            this._pontoA = pontoA;
            this._pontoB = pontoB;
            this._pontoC = pontoC;
            this._pontoD = pontoD;
        }

        public void RemoverPonto()
        {
            if (this._CURVATURA < 1)
            {
                this._CURVATURA *= 2;
            }
        }

        public void AdicionarPonto()
        {
            if (this._CURVATURA >= .0625)
            {
                this._CURVATURA /= 2;
            }
        }

        protected override void DesenharAramado()
        {
            GL.LineWidth(base._tamanho);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(base._cor);

            for (double idx = .0; idx <= 1.0; idx += this._CURVATURA)
            {

                var distABX = this._pontoA.X + (this._pontoB.X - this._pontoA.X) * idx;
                var distABY = this._pontoA.Y + (this._pontoB.Y - this._pontoA.Y) * idx;

                var distBCX = this._pontoB.X + (this._pontoC.X - this._pontoB.X) * idx;
                var distBCY = this._pontoB.Y + (this._pontoC.Y - this._pontoB.Y) * idx;

                var distCDX = this._pontoC.X + (this._pontoD.X - this._pontoC.X) * idx;
                var distCDY = this._pontoC.Y + (this._pontoD.Y - this._pontoC.Y) * idx;

                var distABBCX = distABX + (distBCX - distABX) * idx;
                var distABBCY = distABY + (distBCY - distABY) * idx;

                var distBCCDX = distBCX + (distCDX - distBCX) * idx;
                var distBCCDY = distBCY + (distCDY - distBCY) * idx;

                var pontoX = distABBCX + (distBCCDX - distABBCX) * idx;
                var pontoY = distABBCY + (distBCCDY - distABBCY) * idx;

                GL.Vertex2(pontoX, pontoY);
            }

            GL.End();
        }
    }
}