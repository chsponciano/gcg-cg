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
        private static readonly double QUANTITY = 10.0;
        private Camera camera = new Camera();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private bool moverPto = false;
        private Spline spline;
        private PontoDeControle pontoDeControleSelecionado;
        private PontoDeControle pontoDeControle1;
        private PontoDeControle pontoDeControle2;        
        private PontoDeControle pontoDeControle3;        
        private PontoDeControle pontoDeControle4;        

        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BuildSegReta();
            BuildQuadradoComPontosDeControle();

            GL.ClearColor(Color.Gray);
        }

        private void BuildSegReta() 
        {
            var reta1 = new SegReta("A", new Ponto4D(0.0, 0.0), new Ponto4D(0.0, 200.0), 7, Color.Red);
            var reta2 = new SegReta("B", new Ponto4D(0.0, 0.0), new Ponto4D(200.0, 0.0), 7, Color.Green);
            this.objetosLista.Add(reta1);
            this.objetosLista.Add(reta2);
        }

        private void BuildQuadradoComPontosDeControle() 
        {
            var reta3 = new SegReta("C", new Ponto4D(-100.0, 100.0), new Ponto4D( 100.0,  100.0), 2, Color.LightBlue);
            var reta4 = new SegReta("D", new Ponto4D( 100.0, 100.0), new Ponto4D( 100.0, -100.0), 2, Color.LightBlue);
            var reta5 = new SegReta("E", new Ponto4D(-100.0, 100.0), new Ponto4D(-100.0, -100.0), 2, Color.LightBlue);

            this.spline = new Spline("F", new Ponto4D(100.0, -100.0), new Ponto4D(-100.0, -100.0), 2, Color.Yellow);

            this.pontoDeControle1 = new PontoDeControle("G", reta3, reta5, Color.Black);
            this.pontoDeControle2 = new PontoDeControle("H", reta3, reta4, Color.Black);
            this.pontoDeControle3 = new PontoDeControle("I", reta5, spline, Color.Black);
            this.pontoDeControle4 = new PontoDeControle("J", reta4, spline, Color.Black);

            this.objetosLista.Add(pontoDeControle1);
            this.objetosLista.Add(pontoDeControle2);
            this.objetosLista.Add(pontoDeControle3);
            this.objetosLista.Add(pontoDeControle4);
            this.objetosLista.Add(reta3);
            this.objetosLista.Add(reta4);
            this.objetosLista.Add(reta5);
            this.objetosLista.Add(spline);
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
                case Key.Keypad1:
                case Key.Number1:
                    this.Selecionar(this.pontoDeControle1);
                    break;
                case Key.Keypad2:
                case Key.Number2:
                    this.Selecionar(this.pontoDeControle2);
                    break;
                case Key.Keypad3:
                case Key.Number3:
                    this.Selecionar(this.pontoDeControle3);
                    break;
                case Key.Keypad4:
                case Key.Number4:
                    this.Selecionar(this.pontoDeControle4);
                    break;
                case Key.C:
                    this.MoverParaCima();
                    break;
                case Key.E:
                    this.MoverParaEsquerda();
                    break;
                case Key.B:
                    this.MoverParaBaixo();
                    break;
                case Key.D:
                    this.MoverParaDireita();
                    break;
                case Key.KeypadPlus:
                    this.AdicionarPontosSpline();
                    break;
                case Key.KeypadSubtract:
                    this.DiminuirPontosSpline();
                    break;
                case Key.R:
                    this.RestaurarPontosDeControle();
                    break;
            }
        }

        public void AdicionarPontosSpline()
        {
            Console.WriteLine("AdicionarPontosSpline");
        }

        public void DiminuirPontosSpline()
        {
            Console.WriteLine("DiminuirPontosSpline");
        }

        public void RestaurarPontosDeControle()
        {
            this.pontoDeControle1.ResetPonto();
            this.pontoDeControle2.ResetPonto();
            this.pontoDeControle3.ResetPonto();
            this.pontoDeControle4.ResetPonto();
        }

        private void MoverParaCima()
        {
            if (this.pontoDeControleSelecionado != null)
            {
                this.pontoDeControleSelecionado.Add(0.0, QUANTITY);
            }
        }

        private void MoverParaBaixo()
        {
            if (this.pontoDeControleSelecionado != null)
            {
                this.pontoDeControleSelecionado.Add(0.0, -QUANTITY);
            }
        }

        private void MoverParaDireita()
        {
            if (this.pontoDeControleSelecionado != null)
            {
                this.pontoDeControleSelecionado.Add(QUANTITY, 0.0);
            }
        }

        private void MoverParaEsquerda() 
        {
            if (this.pontoDeControleSelecionado != null)
            {
                this.pontoDeControleSelecionado.Add(-QUANTITY, 0.0);
            }
        }

        private void Selecionar(PontoDeControle pontoDeControle)
        {
            if (this.pontoDeControleSelecionado != null)
            {
                this.pontoDeControleSelecionado.SetCor(Color.Black);    
            }
            this.pontoDeControleSelecionado = pontoDeControle;
            this.pontoDeControleSelecionado.SetCor(Color.Red);
        }

        private void Sru3D()
        {
            // GL.LineWidth(7);
            // GL.Begin(PrimitiveType.Lines);
            // GL.Color3(Color.Red);
            // GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
            // GL.Color3(Color.Green);
            // GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
            // GL.Color3(Color.Blue);
            // GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            // GL.End();
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