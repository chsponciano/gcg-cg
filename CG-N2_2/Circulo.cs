using System;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        private double _raio;
        public double Raio 
        {
            get => _raio;
        }
        private int _tamanho;
        private int _eixoX;
        public double EixoX 
        {
            get => _eixoX + _taxa + _raio;
        }
        private int _eixoY;
        public double EixoY
        {
            get => _eixoY + _taxa + _raio;
        }
        private int _eixoXOriginal;
        private int _eixoYOriginal;
        private Ponto4D mouseClick;
        private bool _mostrarMeio;
        private int _taxa;
        private Retangulo _retangulo;

        public Circulo(string rotulo, int eixoX, int eixoY, double raio, Retangulo _retangulo, bool mostrarMeio = false, int tamanho = 5, int _taxa=20) : base(rotulo)
        {
            this._eixoX = eixoX;
            this._eixoY = eixoY;
            this._raio = raio;
            this._tamanho = tamanho;
            this._mostrarMeio = mostrarMeio;
            this._taxa = _taxa;
            this._retangulo = _retangulo;
            // this.CarregarPontosCirculo();
        }

        // protected void CarregarPontosCirculo() {
        //     // for (double i = .0; i <= 360.0; i++) 
        //     // {
        //     //     var pontoAtual = Matematica.GerarPtosCirculo(i, this._raio);
        //     //     base.PontosAdicionar(new Ponto4D(pontoAtual.X + this.EixoX, pontoAtual.Y + this.EixoY));
        //     // }
        //     if (this._mostrarMeio)
        //     {
        //         // Console.WriteLine("DesenhaAmarrado: " + this.EixoX + ", " + this.EixoY);
        //         base.PontosAdicionar(new Ponto4D(this.EixoX, this.EixoY));
        //     }

        //     for (double i = .0; i <= 360.0; i+=.5) 
        //     {
        //         var pontoAtual = Matematica.GerarPtosCirculo(i, this._raio);
        //         base.PontosAdicionar(new Ponto4D(pontoAtual.X + this.EixoX, pontoAtual.Y + this.EixoY));
        //     }
        // }

        protected override void DesenharAramado()
        {
            GL.PointSize(this._tamanho);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Black);

            
            if (this._mostrarMeio)
            {
                // Console.WriteLine("DesenhaAmarrado: " + this.EixoX + ", " + this.EixoY);
                GL.Vertex2(this.EixoX, 
                           this.EixoY);
            }

            for (double i = .0; i <= 360.0; i+=.5) 
            {
                var pontoAtual = Matematica.GerarPtosCirculo(i, this._raio);
                GL.Vertex2(pontoAtual.X + this.EixoX, 
                           pontoAtual.Y + this.EixoY);
            }

            GL.End();
        }

        public void Mover(Ponto4D ponto)
        {
            if (!isOutOfBounds(ponto))
            {
                // var xDiff = Math.Abs(ponto.X) - Math.Abs(mouseClick.X);
                // var yDiff = Math.Abs(ponto.Y) - Math.Abs(mouseClick.Y);

                // var x = mouseClick.X < ponto.X ? ponto.X + xDiff : ponto.X - xDiff;
                // var y = mouseClick.Y < ponto.Y ? ponto.Y + yDiff : ponto.Y - yDiff;
                
                // this._eixoX = (int) x;
                // this._eixoY = (int) y;


                this._eixoX = (int) ponto.X + 70;// - this._taxa - (int)this._raio;
                this._eixoY = (int) ponto.Y + 70;// - this._taxa - (int)this._raio;
                this._retangulo.UpdateCor(new Ponto4D(this.EixoX, this.EixoY));
                // Console.WriteLine(this._eixoX + ", " + this._eixoY);
            }
            else
            {
                this._retangulo.SetCor(Color.LightBlue);
            }
        }

        public void SetMouseClick(Ponto4D ponto)
        {
            this.mouseClick = ponto;
        }

        private bool isOutOfBounds(Ponto4D ponto)
        {
            var dist = Math.Pow(this._eixoX, 2) + Math.Pow(this._eixoY, 2);
            var a = Math.Pow(200, 2);
            // Console.WriteLine(dist + " > " + a);
            return dist > a;
            // return false;
        }

        public void ResetLocation() 
        {
            this._eixoX = this._eixoXOriginal;
            this._eixoY = this._eixoYOriginal;
            this._retangulo.SetCor(Color.Red);
        }
        
        public Ponto4D Centro()
        {
            return new Ponto4D(this.EixoX, this.EixoY);
        } 

    }
}