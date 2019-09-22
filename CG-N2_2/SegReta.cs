using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class SegReta : ObjetoAramado
  {
    protected Ponto4D _pontoA;
    protected Ponto4D _pontoB;
    protected int _tamanho;
    protected Color _cor;

    public SegReta(string rotulo, Ponto4D pontoA, Ponto4D pontoB, int tamanho, Color cor) : base(rotulo)
    {
      this._pontoA = pontoA;
      this._pontoB = pontoB;
      this._tamanho = tamanho;
      this._cor = cor;
    }
    
    protected override void DesenharAramado()
    {
      GL.LineWidth(this._tamanho);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(this._cor);

      GL.Vertex3(this._pontoA.X, this._pontoA.Y, 0); 
      GL.Vertex3(this._pontoB.X, this._pontoB.Y, 0);
      
      GL.End();
    }

    public Ponto4D GetPontoA()
    {
      return this._pontoA;
    }

    public Ponto4D GetPontoB()
    {
      return this._pontoB;
    }
  }
}