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
using CG_N3;

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



        private CameraOrtho camera = new CameraOrtho();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private ObjetoGeometria objetoSelecionado = null;
        private char objetoId = '@';
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private Poligono objetoNovo = null;

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
                camera.xmin += 20;
                camera.xmax += 20;
            }
            else if (e.Key == Key.D)
            {
                camera.xmin -= 20;
                camera.xmax -= 20;
            }
            else if (e.Key == Key.C)
            {
                camera.ymin -= 20;
                camera.ymax -= 20;
            }
            else if (e.Key == Key.B)
            {
                camera.ymin += 20;
                camera.ymax += 20;
            }
           
            else if (e.Key == Key.E)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (int i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            //TODO: falta atualizar a BBox do objeto
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

       
        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            

            int x = (e.X - 300) * 2;
            int y = (e.Y - 300) * -2;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.IsPressed)
            {
                int x = (e.X - 300) * 2;
                int y = (e.Y - 300) * -2;
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
            window.Title = "CG_N3";
            window.Run(1.0 / 60.0);
        }
    }
}
