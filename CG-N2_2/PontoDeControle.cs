using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class PontoDeControle : ObjetoAramado
  {
    private Ponto4D _ponto;
    private Ponto4D _pontoOriginal;
    private Color _cor;
    private SegReta _segReta1;
    private SegReta _segReta2;
    private double _area;

    public PontoDeControle(string rotulo, SegReta segReta1, SegReta segReta2, Color cor, double area=10.0) : base(rotulo)
    {
      this._cor = cor;
      this._segReta1 = segReta1;
      this._segReta2 = segReta2;
      this._area = area / 2;
      this._ponto = this.GetPonto();
      this._pontoOriginal = new Ponto4D(this._ponto.X, this._ponto.Y);
    }
    
    protected override void DesenharAramado()
    {
      GL.Begin(PrimitiveType.QuadStrip);
      GL.Color3(this._cor);
      
      GL.Vertex2(this._ponto.X + this._area, this._ponto.Y + this._area);
      GL.Vertex2(this._ponto.X + this._area, this._ponto.Y - this._area);
      GL.Vertex2(this._ponto.X - this._area, this._ponto.Y + this._area);
      GL.Vertex2(this._ponto.X - this._area, this._ponto.Y - this._area);
      
      GL.End();
    }

    private Ponto4D GetPonto() 
    {
        if (areTheSame(this._segReta1.GetPontoA(), this._segReta2.GetPontoA()))
        {
            return this._segReta1.GetPontoA();
        }
        else if (areTheSame(this._segReta1.GetPontoB(), this._segReta2.GetPontoB())) 
        {
            return this._segReta1.GetPontoB();
        }
        else if (areTheSame(this._segReta1.GetPontoA(), this._segReta2.GetPontoB()))
        {
            return this._segReta1.GetPontoA();
        }
        else if (areTheSame(this._segReta1.GetPontoB(), this._segReta2.GetPontoA()))
        {
            return this._segReta1.GetPontoB();
        }
        else 
        {
            return new Ponto4D();
        }
    }

    private void AddToPonto(double x, double y) 
    {
        if (areTheSame(this._segReta1.GetPontoA(), this._segReta2.GetPontoA()))
        {
            this.AddToPontos(this._segReta1.GetPontoA(), this._segReta2.GetPontoA(), x, y);
        }
        else if (areTheSame(this._segReta1.GetPontoB(), this._segReta2.GetPontoB())) 
        {
            this.AddToPontos(this._segReta1.GetPontoB(), this._segReta2.GetPontoB(), x, y);
        }
        else if (areTheSame(this._segReta1.GetPontoA(), this._segReta2.GetPontoB()))
        {
            this.AddToPontos(this._segReta1.GetPontoA(), this._segReta2.GetPontoB(), x, y);
        }
        else if (areTheSame(this._segReta1.GetPontoB(), this._segReta2.GetPontoA()))
        {
            this.AddToPontos(this._segReta1.GetPontoB(), this._segReta2.GetPontoA(), x, y);
        }
    }

    private void AddToPontos(Ponto4D ponto1, Ponto4D ponto2, double x, double y)
    {
        ponto1.X += x;
        ponto1.Y += y;
        ponto2.X += x;
        ponto2.Y += y;
    }

    private bool areTheSame(Ponto4D ponto1, Ponto4D ponto2) 
    {
        return ponto1.X == ponto2.X && ponto1.Y == ponto2.Y;
    }

    public void SetCor(Color cor) 
    {
        this._cor = cor;
    }

    public void Add(double x, double y)
    {
        this.AddToPonto(x, y);
    }

    public void ResetPonto() 
    {
        var xOriginal = this._pontoOriginal.X;
        var yOriginal = this._pontoOriginal.Y;
        var xAtual = this._ponto.X;
        var yAtual = this._ponto.Y;
        this.AddToPonto(xOriginal - xAtual, yOriginal - yAtual);
    }

    private void aux() 
    {
        Console.WriteLine("1: " + this._segReta1.GetPontoA().X + ", " + this._segReta1.GetPontoA().Y);
        Console.WriteLine("2: " + this._segReta1.GetPontoB().X + ", " + this._segReta1.GetPontoB().Y);
        Console.WriteLine("3: " + this._segReta2.GetPontoA().X + ", " + this._segReta2.GetPontoA().Y);
        Console.WriteLine("4: " + this._segReta2.GetPontoB().X + ", " + this._segReta2.GetPontoB().Y);
    }
  }
}