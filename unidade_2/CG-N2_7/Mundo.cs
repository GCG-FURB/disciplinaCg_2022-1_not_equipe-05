/**
  Autor: Dalton Solano dos Reis
**/

#define CG_Gizmo
// #define CG_Privado

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using OpenTK.Input;
using CG_Biblioteca;
using CG_N2;

namespace gcgcg
{
    class Mundo : GameWindow
    {
        private static Mundo instanciaMundo = null;

        private Mundo(int width, int height) : base(width, height) { }

        public static Mundo GetInstance(int width, int height)
        {
            if (instanciaMundo == null)
                instanciaMundo = new Mundo(width, height);
            return instanciaMundo;
        }

        private SegReta SrPalito;
        private int RaioSrPalito = 100;
        private int AnguloSrPalito = 45;

        private CameraOrtho camera = new CameraOrtho();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private char objetoId = '@';
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private bool mouseMoverPto = false;
        private Retangulo obj_Retangulo;
        private int indexListaTipos = 0;
        private Retangulo retanguloQueMuda;

        private Ponto pontoSpline1;
        private Ponto pontoSpline2;
        private Ponto pontoSpline3;
        private Ponto pontoSpline4;
        private Ponto pontoSplineSelecionado;

        private Spline spline;

        public static Ponto4D pontoCentral = new Ponto4D(0, 0);
        public static Ponto4D pontoDireitoSuperior = Matematica.GerarPtosCirculo(45, 300);
        public static Ponto4D pontoEsquerdoSuperior = Matematica.GerarPtosCirculo(135, 300);
        public static Ponto4D pontoEsquerdoInferior = Matematica.GerarPtosCirculo(225, 300);
        public static Ponto4D pontoDireitoInferior = Matematica.GerarPtosCirculo(315, 300);
        public static Cor corRetangulo = new Cor(252, 15, 192);
        public static bool pegouCirculo = false;

        private List<PrimitiveType> listaTipos = new List<PrimitiveType>()
        {
            PrimitiveType.Points,
            PrimitiveType.Lines,
            PrimitiveType.LineLoop,
            PrimitiveType.LineStrip,
            PrimitiveType.Triangles,
            PrimitiveType.TriangleStrip,
            PrimitiveType.TriangleFan,
            PrimitiveType.Quads,
            PrimitiveType.QuadStrip,
            PrimitiveType.Polygon
        };
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            camera.xmin = -300; camera.xmax = 300; camera.ymin = -300; camera.ymax = 300;
            //camera.xmin = -400; camera.xmax = 400; camera.ymin = -400; camera.ymax = 400;

            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");

            //DesenharCirculo(new Ponto4D(0, 0), 100, Color.Yellow, 5);

            //DesenharTrianguloECirculos();

            //DesenharRetanguloMudaFormaPrimitiva();

            //DesenharSenhorPalito();

            //DesenharSpline();

            DesenharBbox();



#if CG_Privado
      objetoId = Utilitario.charProximo(objetoId);
      obj_SegReta = new Privado_SegReta(objetoId, null, new Ponto4D(50, 150), new Ponto4D(150, 250));
      obj_SegReta.ObjetoCor.CorR = 255; obj_SegReta.ObjetoCor.CorG = 255; obj_SegReta.ObjetoCor.CorB = 0;
      objetosLista.Add(obj_SegReta);
      objetoSelecionado = obj_SegReta;

      objetoId = Utilitario.charProximo(objetoId);
      obj_Circulo = new Privado_Circulo(objetoId, null, new Ponto4D(100, 300), 50);
      obj_Circulo.ObjetoCor.CorR = 0; obj_Circulo.ObjetoCor.CorG = 255; obj_Circulo.ObjetoCor.CorB = 255;
      objetosLista.Add(obj_Circulo);
      objetoSelecionado = obj_Circulo;
#endif
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
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
            GL.LoadIdentity();
#if CG_Gizmo
            Sru3D();
#endif
            for (var i = 0; i < objetosLista.Count; i++)
                objetosLista[i].Desenhar();
            if (bBoxDesenhar && (objetoSelecionado != null))
                objetoSelecionado.BBox.Desenhar();
            this.SwapBuffers();
        }
        private void DesenharCirculo(Ponto4D pontoCentral, int raio, Color cor, int tamanho, int pontos = 72, PrimitiveType primitivo = PrimitiveType.Points)
        {
            Circulo circulo = new Circulo(Convert.ToChar("C"), null, pontoCentral, raio, pontos, primitivo);
            circulo.ObjetoCor.CorR = cor.R; circulo.ObjetoCor.CorG = cor.G; circulo.ObjetoCor.CorB = cor.B;
            circulo.PrimitivaTamanho = tamanho;
            objetosLista.Add(circulo);
        }
        private void DesenharTrianguloECirculos()
        {
            Ponto4D ponto1 = new Ponto4D(0, 100, 0);
            Ponto4D ponto2 = new Ponto4D(-100, -100, 0);
            Ponto4D ponto3 = new Ponto4D(100, -100, 0);

            DesenharSegReta(ponto1, ponto2, Color.Cyan);
            DesenharSegReta(ponto2, ponto3, Color.Cyan);
            DesenharSegReta(ponto3, ponto1, Color.Cyan);

            DesenharCirculo(ponto1, 100, Color.Black, 5);
            DesenharCirculo(ponto2, 100, Color.Black, 5);
            DesenharCirculo(ponto3, 100, Color.Black, 5);
        }
        private void DesenharSegReta(Ponto4D ponto1, Ponto4D ponto2, Color cor)
        {
            SegReta segReta1 = new SegReta(Convert.ToChar("C"), null, ponto1, ponto2);
            segReta1.PrimitivaTamanho = 5;
            segReta1.ObjetoCor.CorR = cor.R; segReta1.ObjetoCor.CorG = cor.G; segReta1.ObjetoCor.CorB = cor.B;
            objetosLista.Add(segReta1);
        }
        private void DesenharRetanguloMudaFormaPrimitiva()
        {
            obj_Retangulo = new Retangulo(Convert.ToChar("C"), null, new Ponto4D(200, 200, 0), new Ponto4D(-200, -200, 0));
            obj_Retangulo.DefinirCorPonto(0, Color.MediumPurple);
            obj_Retangulo.DefinirCorPonto(1, Color.Cyan);
            obj_Retangulo.DefinirCorPonto(2, Color.Yellow);
            obj_Retangulo.DefinirCorPonto(3, Color.Black);

            obj_Retangulo.PrimitivaTamanho = 5;
            obj_Retangulo.PrimitivaTipo = listaTipos[0];
            objetosLista.Add(obj_Retangulo);
            objetoSelecionado = obj_Retangulo;
        }
        private void DesenharSenhorPalito()
        {
            Ponto4D pontoCentral = new Ponto4D(0, 0, 0);
            Ponto4D pontoFinal = PontoFinalBaseadoNoAngulo(pontoCentral, AnguloSrPalito, RaioSrPalito);
            SrPalito = new SegReta(Convert.ToChar("C"), null, pontoCentral, pontoFinal);
            SrPalito.ObjetoCor.CorR = 0; SrPalito.ObjetoCor.CorG = 0; SrPalito.ObjetoCor.CorB = 0;
            SrPalito.PrimitivaTamanho = 5;
            objetosLista.Add(SrPalito);
        }
        private void DesenharSpline()
        {
            Ponto4D ponto1 = new Ponto4D(-200, -200);
            Ponto4D ponto2 = new Ponto4D(-200, 200);
            Ponto4D ponto3 = new Ponto4D(200, 200);
            Ponto4D ponto4 = new Ponto4D(200, -200);

            pontoSpline1 = new Ponto(Convert.ToChar("D"), null, ponto1);
            pontoSpline2 = new Ponto(Convert.ToChar("E"), null, ponto2);
            pontoSpline3 = new Ponto(Convert.ToChar("F"), null, ponto3);
            pontoSpline4 = new Ponto(Convert.ToChar("G"), null, ponto4);
            objetosLista.Add(pontoSpline1);
            objetosLista.Add(pontoSpline2);
            objetosLista.Add(pontoSpline3);
            objetosLista.Add(pontoSpline4);

            SegReta segReta1 = new SegReta(Convert.ToChar("A"), null, ponto1, ponto2);
            segReta1.PrimitivaTamanho = 5;
            segReta1.DefinirCorPonto(0, Color.Black);
            segReta1.ObjetoCor.CorR = 224; segReta1.ObjetoCor.CorG = 255; segReta1.ObjetoCor.CorB = 255;
            objetosLista.Add(segReta1);

            SegReta segReta2 = new SegReta(Convert.ToChar("B"), null, ponto2, ponto3);
            segReta2.PrimitivaTamanho = 5;
            segReta2.ObjetoCor.CorR = 224; segReta2.ObjetoCor.CorG = 255; segReta2.ObjetoCor.CorB = 255;
            objetosLista.Add(segReta2);

            SegReta segReta3 = new SegReta(Convert.ToChar("C"), null, ponto3, ponto4);
            segReta3.PrimitivaTamanho = 5;
            segReta3.ObjetoCor.CorR = 224; segReta3.ObjetoCor.CorG = 255; segReta3.ObjetoCor.CorB = 255;
            objetosLista.Add(segReta3);

            spline = new Spline(Convert.ToChar("D"), null, ponto1, ponto2, ponto3, ponto4, 10);
            spline.PrimitivaTamanho = 5;
            spline.ObjetoCor.CorR = 255; spline.ObjetoCor.CorG = 255; spline.ObjetoCor.CorB = 0;
            objetosLista.Add(spline);

        }
        private void DesenharBbox()
        {

            camera.xmax = 600;
            camera.ymax = 600;
            camera.xmin = -600;
            camera.ymin = -600;

            //Não podemos utilizar o mesmo ponto central

            Retangulo retanguloMenor = new Retangulo(Convert.ToChar("M"), null, pontoEsquerdoInferior, pontoDireitoSuperior);
            retanguloMenor.ObjetoCor = corRetangulo;
            objetosLista.Add(retanguloMenor);

            Ponto pontoCentral = new Ponto(Convert.ToChar("B"), null, Mundo.pontoCentral);
            pontoCentral.PrimitivaTamanho = 5;
            pontoCentral.ObjetoCor = new Cor(1, 1, 1);
            objetosLista.Add(pontoCentral);

            DesenharCirculo(new Ponto4D(0, 0), 300, Color.Black, 2, 720, PrimitiveType.LineLoop);
            DesenharCirculo(Mundo.pontoCentral, 50, Color.Black, 2, primitivo: PrimitiveType.LineLoop);

            //DesenharCirculo(new Ponto4D(0, 0), 300, new Color(0, 0, 0, 0), 1, 720);
        }
        private Ponto4D PontoFinalBaseadoNoAngulo(Ponto4D ponto, int angulo, int raio)
        {
            Ponto4D pontoFinal = Matematica.GerarPtosCirculo(angulo, raio);
            return new Ponto4D(pontoFinal.X + ponto.X, pontoFinal.Y + ponto.Y, 0);
        }
        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.O)
            {
                camera.xmin -= 100;
                camera.xmax += 100;
                camera.ymin -= 100;
                camera.ymax += 100;
            }
            else if (e.Key == Key.I)
            {
                if ((camera.xmin + 100) != 0)
                {
                    camera.xmin += 100;
                    camera.xmax -= 100;
                    camera.ymin += 100;
                    camera.ymax -= 100;
                }
            }
            else if (e.Key == Key.E)
            {
                if (pontoSplineSelecionado != null)
                {
                    pontoSplineSelecionado.ponto.X--;
                    return;
                }
                camera.xmin += 20;
                camera.xmax += 20;
            }
            else if (e.Key == Key.D)
            {
                if (pontoSplineSelecionado != null)
                {
                    pontoSplineSelecionado.ponto.X++;
                    return;
                }
                camera.xmin -= 20;
                camera.xmax -= 20;
            }
            else if (e.Key == Key.C)
            {
                if (pontoSplineSelecionado != null)
                {
                    pontoSplineSelecionado.ponto.Y++;
                    return;
                }
                camera.ymin -= 20;
                camera.ymax -= 20;
            }
            else if (e.Key == Key.B)
            {
                if (pontoSplineSelecionado != null)
                {
                    pontoSplineSelecionado.ponto.Y--;
                    return;
                }
                camera.ymin += 20;
                camera.ymax += 20;
            }
            else if (e.Key == Key.V)
                mouseMoverPto = !mouseMoverPto;
            else if (e.Key == Key.Space)
            {
                if (obj_Retangulo == null)
                    return;

                if (indexListaTipos < listaTipos.Count - 1)
                {
                    indexListaTipos++;
                }
                else
                {
                    indexListaTipos = 0;
                }

                PrimitiveType tipo = listaTipos[indexListaTipos];

                obj_Retangulo.PrimitivaTipo = tipo;
            }
            else if (e.Key == Key.Q)
            {
                if (SrPalito == null)
                    return;

                SrPalito.Ponto1.X--;
                SrPalito.Ponto2.X--;
            }
            else if (e.Key == Key.W)
            {
                if (SrPalito == null)
                    return;

                SrPalito.Ponto1.X++;
                SrPalito.Ponto2.X++;
            }
            else if (e.Key == Key.A)
            {
                if (SrPalito == null)
                    return;

                RaioSrPalito--;
                Ponto4D pontoFinal = PontoFinalBaseadoNoAngulo(SrPalito.Ponto1, AnguloSrPalito, RaioSrPalito);
                SrPalito.Ponto2.X = pontoFinal.X;
                SrPalito.Ponto2.Y = pontoFinal.Y;
            }
            else if (e.Key == Key.S)
            {
                if (SrPalito == null)
                    return;

                RaioSrPalito++;
                Ponto4D pontoFinal = PontoFinalBaseadoNoAngulo(SrPalito.Ponto1, AnguloSrPalito, RaioSrPalito);
                SrPalito.Ponto2.X = pontoFinal.X;
                SrPalito.Ponto2.Y = pontoFinal.Y;
            }
            else if (e.Key == Key.Z)
            {
                if (SrPalito == null)
                    return;

                AnguloSrPalito--;
                Ponto4D pontoFinal = PontoFinalBaseadoNoAngulo(SrPalito.Ponto1, AnguloSrPalito, RaioSrPalito);
                SrPalito.Ponto2.X = pontoFinal.X;
                SrPalito.Ponto2.Y = pontoFinal.Y;
            }
            else if (e.Key == Key.X)
            {
                if (SrPalito == null)
                    return;

                AnguloSrPalito++;
                Ponto4D pontoFinal = PontoFinalBaseadoNoAngulo(SrPalito.Ponto1, AnguloSrPalito, RaioSrPalito);
                SrPalito.Ponto2.X = pontoFinal.X;
                SrPalito.Ponto2.Y = pontoFinal.Y;
            }
            else if (e.Key == Key.KeypadPlus && spline != null)
                spline.quantidadePontos++;
            else if (e.Key == Key.KeypadSubtract && spline != null)
            {
                if (spline.quantidadePontos > 1)
                    spline.quantidadePontos--;
            }
            else if (e.Key == Key.R && spline != null)
                spline.quantidadePontos = 10;
            else if (e.Key == Key.E)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (int i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            else if (e.Key == Key.Number1 || e.Key == Key.Keypad1)
                SelecionarPonto(pontoSpline1);
            else if (e.Key == Key.Number2 || e.Key == Key.Keypad2)
                SelecionarPonto(pontoSpline2);
            else if (e.Key == Key.Number3 || e.Key == Key.Keypad3)
                SelecionarPonto(pontoSpline3);
            else if (e.Key == Key.Number4 || e.Key == Key.Keypad4)
                SelecionarPonto(pontoSpline4);
            else if (e.Key == Key.Plus)
            {
                if (spline.quantidadePontos <= 100)
                    spline.quantidadePontos++;
            }
            else if (e.Key == Key.Minus)
            {
                if (spline.quantidadePontos > 0)
                {
                    var proximoValor = spline.quantidadePontos - 1;
                    if (proximoValor > 0)
                    {
                        spline.quantidadePontos = proximoValor;
                    }
                }

            }
            //TODO: falta atualizar a BBox do objeto
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

        private void SelecionarPonto(Ponto ponto)
        {
            if (spline == null)
            {
                return;
            }

            pontoSpline1.cor = Color.Black;
            pontoSpline2.cor = Color.Black;
            pontoSpline3.cor = Color.Black;
            pontoSpline4.cor = Color.Black;
            ponto.cor = Color.Red;
            pontoSplineSelecionado = ponto;
        }
        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!e.Mouse.IsButtonDown(MouseButton.Left) || !pegouCirculo)
            {
                Mundo.pontoCentral.X = 0;
                Mundo.pontoCentral.Y = 0;

                return;
            }

            int x = (e.X - 300) * 2;
            int y = (e.Y - 300) * -2;

            if (Mundo.pontoDireitoInferior.X > x && Mundo.pontoDireitoInferior.Y < y &&
                Mundo.pontoEsquerdoSuperior.X < x && Mundo.pontoEsquerdoSuperior.Y > y)
            {
                Mundo.pontoCentral.X = x;
                Mundo.pontoCentral.Y = y;

                corRetangulo.CorR = 252;
                corRetangulo.CorG = 15;
                corRetangulo.CorB = 192;
            }
            else
            {
                corRetangulo.CorR = 25;
                corRetangulo.CorG = 140;
                corRetangulo.CorB = 255;

                // método euclidiano
                // d = (x2 - x1)² + (y2 - y1)²
                var d = (x * x) + (y * y);
                var raioQuadrado = 300 * 300;

                if (d < raioQuadrado)
                {
                    Mundo.pontoCentral.X = x;
                    Mundo.pontoCentral.Y = y;
                }
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            Mundo.pegouCirculo = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.IsPressed)
            {
                int x = (e.X - 300) * 2;
                int y = (e.Y - 300) * -2;

                Mundo.pegouCirculo = false;

                if (x > (pontoCentral.X - 50) && x < (pontoCentral.X + 50) &&
                    y < (pontoCentral.Y + 50) && y > (pontoCentral.Y - 50)
                    )
                {
                    Mundo.pegouCirculo = true;
                }
            }
            base.OnMouseDown(e);
        }
#if CG_Gizmo
        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            // GL.Color3(1.0f,0.0f,0.0f);
            GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
            GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
            // GL.Color3(0.0f,1.0f,0.0f);
            GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
            // GL.Color3(0.0f,0.0f,1.0f);
            GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            GL.End();
        }
#endif
    }
    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = Mundo.GetInstance(600, 600);
            window.Title = "CG_N2";
            window.Run(1.0 / 60.0);
        }
    }
}
