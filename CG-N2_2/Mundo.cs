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
        Camera camera = new Camera();
        protected List<Objeto> objetosLista = new List<Objeto>();

        private readonly double _MOVER = 10.0;
        private readonly Ponto4D[] _ORIGINAL = {
            new Ponto4D(200, -200),
            new Ponto4D(200, 200),
            new Ponto4D(-200, 200),
            new Ponto4D(-200, -200)
        };

        private Ponto4D _ponto4DA;
        private Ponto4D _ponto4DB;
        private Ponto4D _ponto4DC;
        private Ponto4D _ponto4DD;
        private SegReta _reta1;
        private SegReta _reta2;
        private SegReta _reta3;
        private PontoDeControle _pontoA;
        private PontoDeControle _pontoB;
        private PontoDeControle _pontoC;
        private PontoDeControle _pontoD;
        private PontoDeControle _pontoAtivo;
        private Spline _spline;

        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._ponto4DA = new Ponto4D(-200, -200);
            this._ponto4DB = new Ponto4D(-200, 200);
            this._ponto4DC = new Ponto4D(200, 200);
            this._ponto4DD = new Ponto4D(200, -200);

            this._pontoA = new PontoDeControle("A", this._ponto4DA, 20, Color.Black);
            this._pontoB = new PontoDeControle("B", this._ponto4DB, 20, Color.Black);
            this._pontoC = new PontoDeControle("C", this._ponto4DC, 20, Color.Black);
            this._pontoD = new PontoDeControle("D", this._ponto4DD, 20, Color.Red);

            this._reta1  = new SegReta("A", this._ponto4DA, this._ponto4DB, 100, Color.Cyan);
            this._reta2  = new SegReta("B", this._ponto4DB, this._ponto4DC, 100, Color.Cyan);
            this._reta3  = new SegReta("C", this._ponto4DC, this._ponto4DD, 100, Color.Cyan);

            this._pontoAtivo = _pontoD;
            this._spline = new Spline("Spline", this._ponto4DD, this._ponto4DC, this._ponto4DB, this._ponto4DA, 15, Color.Yellow);

            objetosLista.Add(this._spline);
            objetosLista.Add(this._reta1);
            objetosLista.Add(this._reta2);
            objetosLista.Add(this._reta3);
            objetosLista.Add(this._pontoA);
            objetosLista.Add(this._pontoB);
            objetosLista.Add(this._pontoC);
            objetosLista.Add(this._pontoD);

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
                case Key.Number1:
                    this._pontoAtivo = this._pontoA;
                    this.Selecionar();
                    break;
                case Key.Number2:
                    this._pontoAtivo = this._pontoB;
                    this.Selecionar();
                    break;
                case Key.Number3:
                    this._pontoAtivo = this._pontoC;
                    this.Selecionar();
                    break;
                case Key.Number4:
                    this._pontoAtivo = this._pontoD;
                    this.Selecionar();
                    break;
                case Key.C:
                    this.Mover(0, this._MOVER);
                    break;
                case Key.B:
                    this.Mover(0, -this._MOVER);
                    break;
                case Key.E:
                    this.Mover(-this._MOVER, 0);
                    break;
                case Key.D:
                    this.Mover(this._MOVER, 0);
                    break;
                case Key.Minus:
                    this._spline.Incrementar();
                    break;
                case Key.Plus:
                    this._spline.Decrementar();
                    break;
                case Key.R:
                    this.RestaurarPontosDeControle();
                    break;
            }
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

        private void Selecionar()
        {
            this._pontoA.Cor((this._pontoA == this._pontoAtivo) ? Color.Red : Color.Black);
            this._pontoB.Cor((this._pontoB == this._pontoAtivo) ? Color.Red : Color.Black);
            this._pontoC.Cor((this._pontoC == this._pontoAtivo) ? Color.Red : Color.Black);
            this._pontoD.Cor((this._pontoD == this._pontoAtivo) ? Color.Red : Color.Black);
        }

        private void Mover(double x, double y)
        {
            this._pontoAtivo.Ponto.X += x;
            this._pontoAtivo.Ponto.Y += y;
        }

        private void RestaurarPontosDeControle()
        {
            this._pontoA.Ponto = this._ORIGINAL[3];
            this._pontoB.Ponto = this._ORIGINAL[2];
            this._pontoC.Ponto = this._ORIGINAL[1];
            this._pontoD.Ponto = this._ORIGINAL[0];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = new Mundo(600, 600);
            window.Title = "CG_Template";
            window.Run(1.0 / 60.0);
        }
    }
}