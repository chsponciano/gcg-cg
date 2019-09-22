using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;
using System.Collections.Generic;

namespace gcgcg
{
  internal class Spline : SegReta
  {
    public Spline(string rotulo, Ponto4D pontoA, Ponto4D pontoB, int tamanho, Color cor) : base(rotulo, pontoA, pontoB, tamanho, cor)
    {
    }
    
    protected override void DesenharAramado()
    {
      GL.LineWidth(base._tamanho);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(base._cor);

      GL.Vertex3(base._pontoA.X, base._pontoA.Y, 0);
      GL.Vertex3(base._pontoB.X, base._pontoB.Y, 0);

    //   for (var i = 0.0; i < GetPoints().Count; i += 0.5)
    //   {
    //       var a = GetSplinePoint(i);
    //       Console.WriteLine(a.X + ", " + a.Y);
    //   }

      GL.End();
    }

    private Ponto4D SPLINE_Inter(Ponto4D A, Ponto4D B, Ponto4D t, bool desenha, double qtdPontos)
    {
        var R = new Ponto4D();
        // R.X = A.X + (B.X - A.X) * t / qtdPontos;
        // R.Y = A.Y + (B.Y - A.Y) * t / qtdPontos;
        return R;
    }

    private Ponto4D GetSplinePoint(double t)
	{
        var points = GetPoints();
		int p0, p1, p2, p3;
        p1 = (int) t + 1;
        p2 = p1 + 1;
        p3 = p2 + 1;
        p0 = p1 - 1;

		t = t - (int)t;

		double tt = t * t;
		double ttt = tt * t;

		double q1 = -ttt + 2.0f*tt - t;
		double q2 = 3.0f*ttt - 5.0f*tt + 2.0f;
		double q3 = -3.0f*ttt + 4.0f*tt + t;
		double q4 = ttt - tt;

		double tx = 0.5f * (points[p0].X * q1 + points[p1].X * q2 + points[p2].X * q3 + points[p3].X * q4);
		double ty = 0.5f * (points[p0].Y * q1 + points[p1].Y * q2 + points[p2].Y * q3 + points[p3].Y * q4);

		return new Ponto4D(tx, ty);
	}

    private List<Ponto4D> GetPoints() 
    {
        var points = new List<Ponto4D>();
        points.Add(new Ponto4D(10, 41));
        points.Add(new Ponto4D(20, 41));
        points.Add(new Ponto4D(30, 41));
        points.Add(new Ponto4D(40, 41));
        points.Add(new Ponto4D(50, 41));
        points.Add(new Ponto4D(60, 41));
        points.Add(new Ponto4D(70, 41));
        points.Add(new Ponto4D(80, 41));
        points.Add(new Ponto4D(90, 41));
        points.Add(new Ponto4D(100, 41));
        return points;
    }

  }
}