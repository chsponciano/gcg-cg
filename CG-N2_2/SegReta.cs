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
    private double _raio;
    private double _angulo;

    public double Raio
    {
        get => _raio;
        set => _raio = value;
    }
    public double Angulo
    {
        get => _angulo;
        set => _angulo = value;
    }


    public SegReta(string rotulo, int raio, int angulo, int tamanho) : base(rotulo)
    {
        this._raio = raio;
        this._angulo = angulo;
        this._pontoA = new Ponto4D(){X = 0, Y = 0};
        this._pontoB = Matematica.GerarPtosCirculo(this._angulo, this._raio);
        this._tamanho = tamanho;
    }

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
      GL.Color3(Color.Black);

      GL.Vertex3(this._pontoA.X, this._pontoA.Y, 0); 
      GL.Vertex3(this._pontoB.X, this._pontoB.Y, 0);
      
      GL.End();
    }

    public void AlterarEixoX(int valor)
    {
        this._pontoA.X = this._pontoA.X + valor;
        this._pontoB.X = this._pontoB.X + valor;
    }

    public void GerarPonto(){
        var aux = Matematica.GerarPtosCirculo(this._angulo, this._raio);
        aux.X += this._pontoA.X;
        aux.Y += this._pontoA.Y;
        
        this._pontoB = new Ponto4D(){X = aux.X, Y = aux.Y};
    }
  }
}