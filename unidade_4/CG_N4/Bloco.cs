using System;
using System.Collections.Generic;
using System.Linq;
using CG_Biblioteca;
using CG_N4;
using gcgcg;
using OpenTK.Graphics.OpenGL;

namespace CG_N4
{
    internal class Bloco : Retangulo
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="rotulo">Rótulo do bloco</param>
        /// <param name="paiRef">Referência pai</param>
        /// <param name="ptoInfEsq">Ponto inferior esquerdo</param>
        /// <param name="ptoSupDir">Ponto superior direito</param>
        /// <param name="tipoBloco">Tipo de bloco</param>
        public Bloco(char rotulo, Objeto paiRef, Ponto4D ptoInfEsq, Ponto4D ptoSupDir, TipoBloco tipoBloco) : base(rotulo, paiRef, ptoInfEsq, ptoSupDir, tipoBloco)
        {
            TipoBloco = tipoBloco;
            Mode = "Mode1";

            // Gera o bloco de acordo com o parâmetro
            switch (TipoBloco)
            {
                case TipoBloco.T:
                    this.GeraBlocoT();
                    break;
                case TipoBloco.I:
                    this.GeraBlocoI();
                    break;
                case TipoBloco.Z:
                    this.GeraBlocoZ();
                    break;
                case TipoBloco.L:
                    this.GeraBlocoL();
                    break;
                case TipoBloco.J:
                    this.GeraBlocoJ();
                    break;
                case TipoBloco.S:
                    this.GeraBlocoS();
                    break;
                case TipoBloco.O:
                    this.GeraBlocoO();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Tipo do bloco gerado
        /// </summary>
        public new TipoBloco TipoBloco { get; }

        /// <summary>
        /// Modo de rotação do bloco
        /// </summary>
        internal string Mode { get; set; }

        /// <summary>
        /// Gera o bloco T
        /// </summary>
        private void GeraBlocoT()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('L', this, new Ponto4D(pto.X - 30, pto.Y), new Ponto4D(pto.X, pto.Y + 30), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('R', this, new Ponto4D(pto.X + 30, pto.Y), new Ponto4D(pto.X + 60, pto.Y + 30), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco I
        /// </summary>
        private void GeraBlocoI()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X, pto.Y + 30), new Ponto4D(pto.X + 30, pto.Y + 60), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X, pto.Y - 60), new Ponto4D(pto.X + 30, pto.Y - 30), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco Z
        /// </summary>
        private void GeraBlocoZ()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X, pto.Y + 30), new Ponto4D(pto.X + 30, pto.Y + 60), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X - 30, pto.Y), new Ponto4D(pto.X, pto.Y + 30), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X - 30, pto.Y - 30), new Ponto4D(pto.X, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco L
        /// </summary>
        private void GeraBlocoL()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X, pto.Y + 30), new Ponto4D(pto.X + 30, pto.Y + 60), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X + 30, pto.Y - 30), new Ponto4D(pto.X + 60, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco J
        /// </summary>
        private void GeraBlocoJ()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X, pto.Y + 30), new Ponto4D(pto.X + 30, pto.Y + 60), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X - 30, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco S
        /// </summary>
        private void GeraBlocoS()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X + 30, pto.Y), new Ponto4D(pto.X + 60, pto.Y + 30), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X - 30, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Gera o bloco O
        /// </summary>
        private void GeraBlocoO()
        {
            Ponto4D pto = pontosLista[0];
            this.FilhoAdicionar(new Retangulo('A', this, new Ponto4D(pto.X + 30, pto.Y), new Ponto4D(pto.X + 60, pto.Y + 30), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('B', this, new Ponto4D(pto.X + 30, pto.Y - 30), new Ponto4D(pto.X + 60, pto.Y), this.TipoBloco));
            this.FilhoAdicionar(new Retangulo('C', this, new Ponto4D(pto.X, pto.Y - 30), new Ponto4D(pto.X + 30, pto.Y), this.TipoBloco));
        }

        /// <summary>
        /// Move um objeto
        /// </summary>
        /// <param name="x">Deslocamento em X</param>
        /// <param name="y">Deslocamento em Y</param>
        public bool MoverObjeto(double x, double y, CameraOrtho camera, List<ObjetoGeometria> objetosMundo)
        {
            if (this.movimentoValido(x, y, camera, objetosMundo))
            {
                // Percorre os pontos do quadro principal
                foreach (var pto in pontosLista)
                {
                    pto.X += x;
                    pto.Y += y;
                }

                // Percorre os filhos do objeto principal
                foreach (var filho in this.GetFilhos())
                {
                    foreach (var pto in filho.pontosLista)
                    {
                        pto.X += x;
                        pto.Y += y;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Função que define qual rotação será feita com base no tipo de bloco
        /// </summary>
        public void Rotaciona(CameraOrtho camera)
        {
            switch (TipoBloco)
            {
                case TipoBloco.T:
                    this.RotacionaT(camera);
                    break;
                case TipoBloco.I:
                    this.RotacionaI(camera);
                    break;
                case TipoBloco.Z:
                    this.RotacionaZ(camera);
                    break;
                case TipoBloco.L:
                    this.RotacionaL(camera);
                    break;
                case TipoBloco.J:
                    this.RotacionaJ(camera);
                    break;
                case TipoBloco.S:
                    this.RotacionaS(camera);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Valida se a rotação está dentro dos limites da câmera
        /// </summary>
        /// <param name="pto">Pontos da rotação</param>
        /// <param name="camera">Câmera</param>
        /// <returns>true se é valida, false se não é</returns>
        private bool RotacaoValida(Ponto4D pto, CameraOrtho camera)
        {
            if (pto.X > camera.xmax || pto.X < camera.xmin || pto.Y > camera.ymax || pto.Y < camera.ymin)
                return false;
            return true;
        }

        /// <summary>
        /// Restaura valores originais dos filhos
        /// </summary>
        /// <param name="filhosPontosBackup">Rotulos dos filhos</param>
        /// <param name="filhosRotuloBackup">Pontos dos filhos</param>
        private void RestaurarFilhos(List<List<Ponto4D>> filhosPontosBackup, List<char> filhosRotuloBackup)
        {
            int i = 0;
            foreach (var filho in this.GetFilhos())
            {
                filho.rotulo = filhosRotuloBackup[i];
                filho.pontosLista = filhosPontosBackup[i];
                i++;
            }
        }

        /// <summary>
        /// Rotação do objeto T
        /// </summary>
        private void RotacionaT(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            foreach (var filho in this.GetFilhos())
            {
                int j = 0;
                switch (filho.rotulo)
                {
                    case 'L':
                        foreach (var pto in filho.pontosLista)
                        {
                            filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                            j++;
                            pto.X += 30;
                            pto.Y -= 30;
                            if (!RotacaoValida(pto, camera))
                            {
                                RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                return;
                            }
                        }
                        filho.rotulo = 'B';
                        break;
                    case 'B':
                        foreach (var pto in filho.pontosLista)
                        {
                            filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                            j++;
                            pto.X += 30;
                            pto.Y += 30;
                            if (!RotacaoValida(pto, camera))
                            {
                                RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                return;
                            }
                        }
                        filho.rotulo = 'R';
                        break;
                    case 'R':
                        foreach (var pto in filho.pontosLista)
                        {
                            filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                            j++;
                            pto.X -= 30;
                            pto.Y += 30;
                            if (!RotacaoValida(pto, camera))
                            {
                                RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                return;
                            }
                        }
                        filho.rotulo = 'T';
                        break;
                    case 'T':
                        foreach (var pto in filho.pontosLista)
                        {
                            filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                            j++;
                            pto.X -= 30;
                            pto.Y -= 30;
                            if (!RotacaoValida(pto, camera))
                            {
                                RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                return;
                            }
                        }
                        filho.rotulo = 'L';
                        break;
                    default:
                        break;
                }
                i++;
            }
        }

        /// <summary>
        /// Rotação do objeto I
        /// </summary>
        private void RotacionaI(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X -= 30;
                                    pto.Y -= 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X += 30;
                                    pto.Y += 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X += 60;
                                    pto.Y += 60;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X += 30;
                                    pto.Y += 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X -= 30;
                                    pto.Y -= 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X -= 60;
                                    pto.Y -= 60;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotação do objeto Z
        /// </summary>
        private void RotacionaZ(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.Y += 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X += 60;
                                    pto.Y += 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.Y -= 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;
                                    pto.X -= 60;
                                    pto.Y -= 30;
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotação do objeto L
        /// </summary>
        private void RotacionaL(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y += 60;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode3";
                    break;
                case "Mode3":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode4";
                    break;
                case "Mode4":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotação do objeto J
        /// </summary>
        private void RotacionaJ(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode3";
                    break;
                case "Mode3":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode4";
                    break;
                case "Mode4":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.Y -= 60;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotação do objeto S
        /// </summary>
        private void RotacionaS(CameraOrtho camera)
        {
            var filhosRotuloBackup = this.GetFilhos().Select(filho => filho.rotulo).ToList();
            var filhosPontosBackup = this.GetFilhos().Select(filho => new List<Ponto4D>(filho.pontosLista)).ToList();
            int i = 0;
            switch (this.Mode)
            {
                case "Mode1":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y += 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 60;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode2";
                    break;
                case "Mode2":
                    foreach (var filho in this.GetFilhos())
                    {
                        int j = 0;
                        switch (filho.rotulo)
                        {
                            case 'A':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X += 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'B':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 30;
                                    pto.Y -= 30;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            case 'C':
                                foreach (var pto in filho.pontosLista)
                                {
                                    // Faz o backup dos pontos atuais
                                    filhosPontosBackup[i][j] = new Ponto4D(pto.X, pto.Y);
                                    j++;

                                    // Modifica os pontos
                                    pto.X -= 60;

                                    // Valida modificação
                                    if (!RotacaoValida(pto, camera))
                                    {
                                        RestaurarFilhos(filhosPontosBackup, filhosRotuloBackup);
                                        return;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        i++;
                    }
                    this.Mode = "Mode1";
                    break;
                default:
                    break;
            }
        }

        public bool VerificaSeHaColisao(double x, double y, List<ObjetoGeometria> objetosMundo)
        {
            List<ObjetoGeometria> newObjetosMundo = new List<ObjetoGeometria>(objetosMundo);
            if (newObjetosMundo == null)
                return false;

            newObjetosMundo.RemoveAt(objetosMundo.Count - 1);
            // valida colisão para o bloco pivô
            foreach (var objeto in newObjetosMundo)
            {
                if (objeto.pontosLista.Count() > 0)
                {
                    if ((this.pontosLista[0].X + x == objeto.pontosLista[0].X
                    && this.pontosLista[2].X + x == objeto.pontosLista[2].X
                    || this.pontosLista[0].X + x == objeto.pontosLista[2].X
                    && this.pontosLista[2].X + x == objeto.pontosLista[0].X
                    )
                    && this.pontosLista[0].Y + y == objeto.pontosLista[0].Y
                    && this.pontosLista[2].Y + y == objeto.pontosLista[2].Y
                    )
                    {
                        return true;
                    }
                }

                foreach (var objetoMundoFilho in objeto.GetFilhos())
                {
                    if (objetoMundoFilho.pontosLista.Count() > 0)
                    {
                        if ((this.pontosLista[0].X + x == objetoMundoFilho.pontosLista[0].X
                        && this.pontosLista[2].X + x == objetoMundoFilho.pontosLista[2].X ||
                        this.pontosLista[0].X + x == objetoMundoFilho.pontosLista[2].X
                        && this.pontosLista[2].X + x == objetoMundoFilho.pontosLista[0].X
                        )
                        && this.pontosLista[0].Y + y == objetoMundoFilho.pontosLista[0].Y
                        && this.pontosLista[2].Y + y == objetoMundoFilho.pontosLista[2].Y
                        )
                        {
                            return true;
                        }
                    }

                }
            }

            // valida colisão para os demais blocos
            foreach (var blocoFilho in this.GetFilhos())
            {
                foreach (var objeto in newObjetosMundo)
                {
                    if (objeto.pontosLista.Count() > 0)
                    {
                        if ((blocoFilho.pontosLista[0].X + x == objeto.pontosLista[0].X
                             && blocoFilho.pontosLista[2].X + x == objeto.pontosLista[2].X ||
                             blocoFilho.pontosLista[0].X + x == objeto.pontosLista[2].X
                             && blocoFilho.pontosLista[2].X + x == objeto.pontosLista[0].X
                             )
                             && blocoFilho.pontosLista[0].Y + y == objeto.pontosLista[0].Y
                             && blocoFilho.pontosLista[2].Y + y == objeto.pontosLista[2].Y
                             )
                        {
                            return true;
                        }


                    }


                    foreach (var objetoMundoFilho in objeto.GetFilhos())
                    {
                        if (objetoMundoFilho.pontosLista.Count() > 0)
                        {
                            if ((blocoFilho.pontosLista[0].X + x == objetoMundoFilho.pontosLista[0].X
                                && blocoFilho.pontosLista[2].X + x == objetoMundoFilho.pontosLista[2].X ||
                                blocoFilho.pontosLista[0].X + x == objetoMundoFilho.pontosLista[2].X
                                && blocoFilho.pontosLista[2].X + x == objetoMundoFilho.pontosLista[0].X
                                )
                                && blocoFilho.pontosLista[0].Y + y == objetoMundoFilho.pontosLista[0].Y
                                && blocoFilho.pontosLista[2].Y + y == objetoMundoFilho.pontosLista[2].Y
                                )
                            {
                                return true;
                            }

                        }

                    }
                }
            }

            return false;
        }

        private bool movimentoValido(double x, double y, CameraOrtho camera, List<ObjetoGeometria> objetosMundo)
        {
            if (VerificaSeHaColisao(x, y, objetosMundo))
            {
                return false;
            }

            foreach (var pto in pontosLista)
            {
                if (pto.X + x > camera.xmax || pto.X + x < camera.xmin || pto.Y + y > camera.ymax || pto.Y + y < camera.ymin)
                {

                    return false;
                }
            }

            foreach (var filho in this.GetFilhos())
            {
                foreach (var pto in filho.pontosLista)
                {
                    if (pto.X + x > camera.xmax || pto.X + x < camera.xmin || pto.Y + y > camera.ymax || pto.Y + y < camera.ymin)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
