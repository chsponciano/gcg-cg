using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected string rotulo;
    private PrimitiveType primitivaTipo = PrimitiveType.Points;
    private float primitivaTamanho = 7;
    private Color primitivaCor = Color.Black;

    public Objeto(string rotulo)
    {
      this.rotulo = rotulo;
    }

    protected PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    protected float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    protected Color PrimitivaCor { get => primitivaCor; set => primitivaCor = value; }

    public void Desenhar()
    {
      DesenharAramado();
    }

    protected abstract void DesenharAramado();
  }
}