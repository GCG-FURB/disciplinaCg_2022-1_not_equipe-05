/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using OpenTK;
using System.Collections.Generic;
using System.Linq;

namespace gcgcg
{
    internal class Retangulo : ObjetoGeometria
    {
        Dictionary<Ponto4D, Color> coresPontos = new Dictionary<Ponto4D, Color>();
        public Retangulo(char rotulo, Objeto paiRef, Ponto4D pontoInfEsq, Ponto4D pontoSupDir) : base(rotulo, paiRef)
        {
            base.PontosAdicionar(pontoInfEsq);
            base.PontosAdicionar(new Ponto4D(pontoSupDir.X, pontoInfEsq.Y));
            base.PontosAdicionar(pontoSupDir);
            base.PontosAdicionar(new Ponto4D(pontoInfEsq.X, pontoSupDir.Y));
        }

        protected override void DesenharObjeto()
        {
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D ponto in pontosLista)
            {
                if (coresPontos.ContainsKey(ponto))
                {
                    Color cor = coresPontos.Where(k => k.Key == ponto).First().Value;
                    GL.Color3(cor);
                }
                GL.Vertex2(ponto.X, ponto.Y);
            }
            GL.End();
        }

        public void DefinirCorPonto(int numeroPonto, Color cor)
        {
            if (!coresPontos.ContainsKey(pontosLista[numeroPonto]))
                coresPontos.Add(pontosLista[numeroPonto], cor);
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