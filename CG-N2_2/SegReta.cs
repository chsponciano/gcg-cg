using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class SegReta : ObjetoAramado
  {
    private Ponto4D _pontoA;
    private Ponto4D _pontoB;
    private int _tamanho;

    public SegReta(string rotulo, Ponto4D pontoA, Ponto4D pontoB, int tamanho) : base(rotulo)
    {
      this._pontoA = pontoA;
      this._pontoB = pontoB;
      this._tamanho = tamanho;
    }
    
    protected override void DesenharAramado()
    {
      GL.LineWidth(this._tamanho);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.LightBlue);

      GL.Vertex3(this._pontoA.X, this._pontoA.Y, 0); 
      GL.Vertex3(this._pontoB.X, this._pontoB.Y, 0);
      
      GL.End();
    }
  }
}