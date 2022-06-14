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
        private Ponto4D vertice = null;
        private char objetoId = '@';
        private bool bBoxDesenhar = false;
        int mouseX, mouseY;   //TODO: achar método MouseDown para não ter variável Global
        private Poligono poligono = null;
#if CG_Privado
    private Privado_SegReta obj_SegReta;
    private Privado_Circulo obj_Circulo;
#endif

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            camera.xmin = -0; camera.xmax = 600; camera.ymin = -0; camera.ymax = 600;

            Console.WriteLine(" --- Ajuda / Teclas: ");
            Console.WriteLine(" [  H     ] mostra teclas usadas. ");

            objetoId = Utilitario.charProximo(objetoId);
            poligono = new Poligono(objetoId, null);
            objetosLista.Add(poligono);
            objetoSelecionado = poligono;
            poligono = null;

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

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.H)
                Utilitario.AjudaTeclado();
            else if (e.Key == Key.Escape)
                Exit();
            else if (e.Key == Key.E)
            {
                Console.WriteLine("--- Objetos / Pontos: ");
                for (var i = 0; i < objetosLista.Count; i++)
                {
                    Console.WriteLine(objetosLista[i]);
                }
            }
            else if (e.Key == Key.O)
                bBoxDesenhar = !bBoxDesenhar;
            else if (e.Key == Key.Enter)
            {
                if (poligono != null)
                {
                    poligono.PontosRemoverUltimo();   
                    objetoSelecionado = poligono;
                    poligono = null;
                }
            }
            else if (e.Key == Key.Space)
            {
                if (poligono == null)
                {

                    objetoId = Utilitario.charProximo(objetoId);
                    poligono = new Poligono(objetoId, null);
                    if (objetoSelecionado != null)
                    {
                        objetoSelecionado.FilhoAdicionar(poligono);
                    }
                    else
                    {
                        objetosLista.Add(poligono);
                    }

                    poligono.PontosAdicionar(new Ponto4D(mouseX, mouseY));
                    poligono.PontosAdicionar(new Ponto4D(mouseX, mouseY));  
                }
                else
                    poligono.PontosAdicionar(new Ponto4D(mouseX, mouseY));
            }
            else if (e.Key == Key.A)
            {
                // Seleciona o objeto onde o cursor esteja na boundbox
                foreach (var objeto in objetosLista)
                {
                    if (mouseX > objeto.BBox.obterMenorX && mouseX < objeto.BBox.obterMaiorX)
                    {
                        if (mouseY > objeto.BBox.obterMenorY && mouseY < objeto.BBox.obterMaiorY)
                        {
                            var verificaObjeto = (ObjetoGeometria)objeto;
                            bool clicouDentro = verificaObjeto.ScanLine(new Ponto4D(mouseX, mouseY));
                            if (clicouDentro)
                            {
                                objetoSelecionado = (ObjetoGeometria)objeto;
                            }
                            else
                            {
                                objetoSelecionado = null;
                            }
                            return;
                        }
                    }
                }
                objetoSelecionado = null;
            }
            else if (objetoSelecionado != null)
            {
                if (e.Key == Key.M)
                    Console.WriteLine(objetoSelecionado.Matriz);
                else if (e.Key == Key.P)
                    Console.WriteLine(objetoSelecionado);
                else if (e.Key == Key.I)
                    objetoSelecionado.AtribuirIdentidade();
                //TODO: não está atualizando a BBox com as transformações geométricas
                else if (e.Key == Key.Left)
                    objetoSelecionado.TranslacaoXYZ(-10, 0, 0);
                else if (e.Key == Key.Right)
                    objetoSelecionado.TranslacaoXYZ(10, 0, 0);
                else if (e.Key == Key.Up)
                    objetoSelecionado.TranslacaoXYZ(0, 10, 0);
                else if (e.Key == Key.Down)
                    objetoSelecionado.TranslacaoXYZ(0, -10, 0);
                else if (e.Key == Key.PageUp)
                    objetoSelecionado.EscalaXYZ(2, 2, 2);
                else if (e.Key == Key.PageDown)
                    objetoSelecionado.EscalaXYZ(0.5, 0.5, 0.5);
                else if (e.Key == Key.Home)
                    objetoSelecionado.EscalaXYZBBox(0.5, 0.5, 0.5);
                else if (e.Key == Key.End)
                    objetoSelecionado.EscalaXYZBBox(2, 2, 2);
                else if (e.Key == Key.Number1)
                    objetoSelecionado.Rotacao(10);
                else if (e.Key == Key.Number2)
                    objetoSelecionado.Rotacao(-10);
                else if (e.Key == Key.Number3)
                    objetoSelecionado.RotacaoZBBox(10);
                else if (e.Key == Key.Number4)
                    objetoSelecionado.RotacaoZBBox(-10);
                else if (e.Key == Key.Number9)
                    objetoSelecionado = null;                     // desmacar objeto selecionado

                else if (e.Key == Key.C)
                {
                    // Remove Objeto selecionado
                    objetosLista.Remove(objetoSelecionado);
                    var objetoOld = objetoSelecionado;
                    // Seleciona o próximo objeto da lista
                    if (objetosLista.Count > 0)
                        objetoSelecionado = (ObjetoGeometria)objetosLista[objetosLista.Count - 1];
                    objetoSelecionado.FilhoRemover(objetoOld);
                }
                else if (e.Key == Key.V)
                // Seleciona vértice
                {
                    if (vertice == null)
                    {
                        vertice = objetoSelecionado.CalculaPontoProximo(new Ponto4D(mouseX, mouseY));
                    }
                    else
                    {
                        vertice = null;
                    }

                }
                else if (e.Key == Key.D)
                {
                    // Remover vértice selecionado
                    if (vertice != null)
                    {
                        objetoSelecionado.RemoverPonto(vertice);
                    }
                }
                else if (e.Key == Key.S)
                {
                    // Altera a primitiva do polígono a ser desenhado para Aberto ou fechado
                    if (poligono != null)
                    {
                        if (poligono.PrimitivaTipo == PrimitiveType.LineLoop)
                        {
                            poligono.PrimitivaTipo = PrimitiveType.LineStrip;
                        }
                        else
                        {
                            poligono.PrimitivaTipo = PrimitiveType.LineLoop;
                        }
                    }
                }
                else if (e.Key == Key.R)
                {
                    // Alter a cor do objeto selecionado para vemelho
                    objetoSelecionado.ObjetoCor.CorR = 255;
                    objetoSelecionado.ObjetoCor.CorG = 0;
                    objetoSelecionado.ObjetoCor.CorB = 0;
                }
                else if (e.Key == Key.G)
                {
                    // Alter a cor do objeto selecionado para Verde
                    objetoSelecionado.ObjetoCor.CorR = 0;
                    objetoSelecionado.ObjetoCor.CorG = 255;
                    objetoSelecionado.ObjetoCor.CorB = 0;
                }
                else if (e.Key == Key.B)
                {
                    // Alter a cor do objeto selecionado para Azul
                    objetoSelecionado.ObjetoCor.CorR = 0;
                    objetoSelecionado.ObjetoCor.CorG = 0;
                    objetoSelecionado.ObjetoCor.CorB = 255;
                }

                else
                    Console.WriteLine(" __ Tecla não implementada.");
            }
            else
                Console.WriteLine(" __ Tecla não implementada.");
        }

       
        //TODO: não está considerando o NDC
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {

            mouseX = e.Position.X;
            mouseY = 600 - e.Position.Y; // Inverti eixo Y
            if (poligono != null)
            {
                poligono.PontosUltimo().X = mouseX;
                poligono.PontosUltimo().Y = mouseY;
            }
            else
            {
                if (vertice != null)
                {
                    vertice.X = mouseX;
                    vertice.Y = mouseY;
                }

            }
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
