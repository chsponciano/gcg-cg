using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
  class Mundo : GameWindow
  {
    public static Mundo instance = null;

    public Mundo(int width, int height) : base(width, height) { }

    public static Mundo getInstance()
    {
      if (instance == null)
        instance = new Mundo(600,600);
      return instance;
    }

    private Camera camera = new Camera();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private bool moverPto = false;
    private bool mousePressed = false;
    private Circulo circuloA;
    private Circulo circuloB;
    private Retangulo retangulo;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      retangulo = new Retangulo("A", new Ponto4D(-140, 140), new Ponto4D(140, -140), 200, 20);
      circuloB = new Circulo("C", 0, 0, 200, null, false, 2);
      circuloA = new Circulo("B", 0, 0, 50, retangulo, true, 2, 20 + (int) circuloB.Raio - 50);

      objetosLista.Add(circuloA);
      objetosLista.Add(circuloB);
      objetosLista.Add(retangulo);
      GL.ClearColor(Color.Gray);
    }
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);
      GL.MatrixMode(MatrixMode.Modelview);
      Sru3D();

      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }

      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Escape:
          Exit();
          break;
        case Key.M:
          moverPto = !moverPto;
          break;
      }
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        // Console.WriteLine("OnMouseDown");
        this.mousePressed = true;
        circuloA.SetMouseClick(GetMousePosition(e));
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        // Console.WriteLine("OnMouseUp");
        this.mousePressed = false;
        circuloA.ResetLocation();
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        if (this.mousePressed)
        {
            // Console.WriteLine("OnMouseMove");
            this.circuloA.Mover(GetMousePosition(e));
        }
    }

    private Ponto4D GetMousePosition(MouseEventArgs e)
    {
        // Console.WriteLine((e.X - 20) + ", " + -(e.Y - 20));
        // Console.WriteLine((e.X - 20) + ", " + -(e.Y - 580));
        // return new Ponto4D((e.X - 20), (e.Y - 20));

        // return new Ponto4D((e.X - 20), -(e.Y - 580));
        // return new Ponto4D((e.X), (e.Y));
        // return new Ponto4D((e.X - 245), -(e.Y - 345));
        // var a = this.circuloA.Centro();
        return new Ponto4D((e.X - 300), -(e.Y - 300));
    }

    private void Sru3D()
    {
      GL.LineWidth(7);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      Mundo window = new Mundo(600, 600);
      window.Title = "CG-N2_2";
      window.Run(1.0 / 60.0);
    }
  }

}
