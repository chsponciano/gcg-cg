using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class SegReta : ObjetoAramado
  {
    protected Ponto4D _pontoA;
    public Ponto4D PontoA{
      get => this._pontoA;
      set => this._pontoA = value;
    }
    protected Ponto4D _pontoB;
    public Ponto4D PontoB{
      get => this._pontoB;
      set => this._pontoB = value;
    }

    public SegReta(string rotulo, Ponto4D pontoA, Ponto4D pontoB, int tamanho, Color cor) : base(rotulo)
    {
      this._pontoA = pontoA;
      this._pontoB = pontoB;

      base.PrimitivaCor = cor;
      base.PrimitivaTamanho = tamanho;
      base.PrimitivaTipo = PrimitiveType.Lines;

      this.GerarPontos();
    }
    
    private void GerarPontos()
    {
      base.PontosAdicionar(new Ponto4D(this._pontoA.X, this._pontoA.Y)); 
      base.PontosAdicionar(new Ponto4D(this._pontoB.X, this._pontoB.Y));
    }
  }
}