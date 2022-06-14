/**
  Autor: Dalton Solano dos Reis
**/

using System;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
    internal abstract class ObjetoGeometria : Objeto
    {
        protected List<Ponto4D> pontosLista = new List<Ponto4D>();

        public ObjetoGeometria(char rotulo, Objeto paiRef) : base(rotulo, paiRef) { }

        protected override void DesenharGeometria()
        {
            DesenharObjeto();
        }
        protected abstract void DesenharObjeto();
        public void PontosAdicionar(Ponto4D pto)
        {
            pontosLista.Add(pto);
            if (pontosLista.Count.Equals(1))
                base.BBox.Atribuir(pto);
            else
                base.BBox.Atualizar(pto);
            base.BBox.ProcessarCentro();
        }

        public void PontosRemoverUltimo()
        {
            pontosLista.RemoveAt(pontosLista.Count - 1);
        }

        protected void PontosRemoverTodos()
        {
            pontosLista.Clear();
        }

        public Ponto4D PontosUltimo()
        {
            return pontosLista[pontosLista.Count - 1];
        }

        public void PontosAlterar(Ponto4D pto, int posicao)
        {
            pontosLista[posicao] = pto;
        }

        /// <summary>
        /// Calcular ponto mais próximo do objeto com base em ponto informado
        /// </summary>
        /// <param name="ptoInformado">Coordenadas do ponto a ser comparado</param>
        /// <returns>Ponto mais próximo</returns>
        public Ponto4D CalculaPontoProximo(Ponto4D ptoInformado)
        {
            // Ponto4D ptoInformadoCalculado = new Ponto4D((ptoInformado.X * ptoInformado.X), (ptoInformado.Y * ptoInformado.Y));
            Ponto4D ptoMaisProximo = null;
            double distanciaPtoMaisProximo = double.MaxValue;
            foreach (var pto in pontosLista)
            {
                double distancia = ((ptoInformado.X - pto.X) * (ptoInformado.X - pto.X) + (ptoInformado.Y - pto.Y) * (ptoInformado.Y - pto.Y));
                if (distancia < distanciaPtoMaisProximo)
                {
                    distanciaPtoMaisProximo = distancia;
                    ptoMaisProximo = pto;
                }
            }

            return ptoMaisProximo;
        }

        /// <summary>
        /// Remove um ponto específico
        /// </summary>
        /// <param name="pto">Ponto a ser removido</param>
        public void RemoverPonto(Ponto4D pto)
        {
            pontosLista.Remove(pto);
        }

        /// <summary>
        /// Faz operação de scanline
        /// </summary>
        /// <param name="ptoClick">Ponto do Click</param>
        /// <returns>True se clicou dentro, false se clicou fora</returns>
        public bool ScanLine(Ponto4D ptoClick)
        {
            // índice para acessar outros pontos na lista
            int i = 1;
            // contador scanLine
            int scanLineCount = 0;

            // Percorre a lista de pontos
            foreach (var pto in pontosLista)
            {
                var ti = ((ptoClick.Y - pto.Y) / (pontosLista[i].Y - pto.Y));
                if (ti >= 0 && ti <= 1)
                {
                    var xi = (pto.X + (pontosLista[i].X - pto.X) * ti);
                    if (xi > ptoClick.X)
                    {
                        scanLineCount++;
                    }
                }
                i++;
                if (i == pontosLista.Count)
                {
                    i = 0;
                }
            }

            // Valida se o contador é par ou ímpar
            if (scanLineCount % 2 == 0)
            {
                // clique fora
                return false;
            }
            else
            {
                // clique dentro
                return true;
            }
        }

        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
    }
}