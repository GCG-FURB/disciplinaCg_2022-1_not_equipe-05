/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using CG_N4;
using System;

namespace gcgcg
{
    internal class Retangulo : ObjetoGeometria
    {
        public Ponto4D PtoInfEsq { get; set; }
        public Ponto4D PtoSupDireito { get; set; }
        public Retangulo(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir, TipoBloco tipoBloco) : base(rotulo, paiRef)
        {
            PtoInfEsq = ptoInfEsq;
            PtoSupDireito = ptoSupDir;
            base.PontosAdicionar(ptoInfEsq);
            base.PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
            base.PontosAdicionar(ptoSupDir);
            base.PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));

            TipoBloco = tipoBloco;
        }

        public TipoBloco TipoBloco { get; }

        protected override void DesenharObjeto()
        {
            GL.Begin(PrimitiveType.Quads);
            foreach (Ponto4D pto in pontosLista)
            {
                switch (TipoBloco)
                {
                    case TipoBloco.I:
                        GL.Color3(Convert.ToByte(176), Convert.ToByte(224), Convert.ToByte(230));
                        break;
                    case TipoBloco.J:
                        GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
                        break;
                    case TipoBloco.L:
                        GL.Color3(Convert.ToByte(255), Convert.ToByte(165), Convert.ToByte(0));
                        break;
                    case TipoBloco.O:
                        GL.Color3(Convert.ToByte(255), Convert.ToByte(255), Convert.ToByte(0));
                        break;
                    case TipoBloco.S:
                        GL.Color3(Convert.ToByte(50), Convert.ToByte(205), Convert.ToByte(50));
                        break;
                    case TipoBloco.T:
                        GL.Color3(Convert.ToByte(153), Convert.ToByte(50), Convert.ToByte(204));
                        break;
                    case TipoBloco.Z:
                        GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
                        break;
                    default:
                        break;
                }

                GL.Vertex2(pto.X, pto.Y);
            }
            GL.End();
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Retangulo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }

    }
}