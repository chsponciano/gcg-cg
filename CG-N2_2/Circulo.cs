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
        private Ponto4D mouseClick;
        private bool _mostrarMeio;
        private int _taxa;
        private Retangulo _retangulo;

        public Circulo(string rotulo, int eixoX, int eixoY, double raio, Retangulo _retangulo, bool mostrarMeio = false, int tamanho = 5, int _taxa=20) : base(rotulo)
        {
            this._eixoX = eixoX;
            this._eixoY = eixoY;
            this._raio = raio;
            this._mostrarMeio = mostrarMeio;
            base.PrimitivaTamanho = tamanho;
            this._taxa = _taxa;
            this._retangulo = _retangulo;
            this.CarregarPontos();
            
        }

        private void CarregarPontos() 
        {
            base.PontosRemoverTodos();
            if (this._mostrarMeio)
            {
                base.PontosAdicionar(new Ponto4D(this.EixoX, this.EixoY));
            }

            for (double i = .0; i <= 360.0; i+=.5) 
            {
                var pontoAtual = Matematica.GerarPtosCirculo(i, this._raio);
                base.PontosAdicionar(new Ponto4D(pontoAtual.X + this.EixoX, pontoAtual.Y + this.EixoY));
            }
        }

        public void Mover(Ponto4D ponto)
        {
            if (!isOutOfBounds(ponto))
            {
                // 70 = taxa do circulo de fora + raio do cirulo de fora = 20 + 50
                this._eixoX = (int) ponto.X + 70;
                this._eixoY = (int) ponto.Y + 70;
                this._retangulo.AtualizaCorDeAcordoComALocalizacaoDoRetangulo(new Ponto4D(this.EixoX, this.EixoY));
            }
            else
            {
                this._retangulo.SetCor(Color.LightBlue);
            }
            this.CarregarPontos();
        }

        public void SetMouseClick(Ponto4D ponto)
        {
            this.mouseClick = ponto;
        }

        private bool isOutOfBounds(Ponto4D ponto)
        {
            var dist = Math.Pow(this._eixoX, 2) + Math.Pow(this._eixoY, 2);
            var a = Math.Pow(200, 2);
            return dist > a;
        }

        public void ResetLocation() 
        {
            this._eixoX = 0;
            this._eixoY = 0;
            this._retangulo.SetCor(Color.Red);
            this.CarregarPontos();
        }
        
        public Ponto4D Centro()
        {
            return new Ponto4D(this.EixoX, this.EixoY);
        } 

    }
}