using System;
using CG_Biblioteca;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
  internal class Retangulo : ObjetoAramado
  {
    private Ponto4D ptoInfEsq, ptoSupDir, ptoInfDir, ptoSupEsq;
    private int _taxa;
    private double _raio;
    public Retangulo(string rotulo,Ponto4D ptoInfEsq, Ponto4D ptoSupDir, double _raio, int _taxa,  int _tamanho = 2) : base(rotulo)
    {
      this.ptoInfEsq = ptoInfEsq;
      this.ptoSupDir = ptoSupDir;
      this.ptoInfDir = new Ponto4D(ptoSupDir.X,ptoInfEsq.Y);
      this.ptoSupEsq = new Ponto4D(ptoInfEsq.X,ptoSupDir.Y);
      base.PrimitivaTamanho = _tamanho;
      this._taxa = _taxa;
      this._raio = _raio;
      base._cor = Color.Red;
      GerarPtosRetangulo();
    }

    private void GerarPtosRetangulo() {
      base.PontosRemoverTodos();
      for (double i = ptoInfEsq.X; i < ptoInfDir.X; i++) 
      {
        base.PontosAdicionar(new Ponto4D(i + this._raio + this._taxa, ptoSupDir.Y + this._raio + this._taxa));
        base.PontosAdicionar(new Ponto4D(i + this._raio + this._taxa, ptoInfDir.Y + this._raio + this._taxa));
      }
      for (double i = ptoSupEsq.Y; i < ptoInfEsq.Y; i++) 
      {
        base.PontosAdicionar(new Ponto4D(ptoSupEsq.X + this._raio + this._taxa, i + this._raio + this._taxa));
        base.PontosAdicionar(new Ponto4D(ptoSupDir.X + this._raio + this._taxa, i + this._raio + this._taxa));
      }
    }
    
    public void AtualizaCorDeAcordoComALocalizacaoDoRetangulo(Ponto4D ponto)
    {
      if (ponto.X >= ptoSupDir.X  + this._raio + this._taxa
          || ponto.X <= ptoSupEsq.X  + this._raio + this._taxa
          || ponto.Y <= ptoSupDir.Y  + this._raio + this._taxa
          || ponto.Y >= ptoInfDir.Y + this._raio + this._taxa)
      {
        base._cor = Color.Yellow;
      }
      else 
      {
        base._cor = Color.Red;
      }
    }

    public void SetCor(Color cor)
    {
      base._cor = cor;
    }

  }
}