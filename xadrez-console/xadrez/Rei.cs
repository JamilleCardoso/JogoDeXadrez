using System.Threading;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }               

        private void DefineETestaPosicao(bool[,] matriz, Posicao pos, int linha, int coluna)
        {
            pos.DefinirValores(linha, coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }
        }

        private bool TestaTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return (peca != null) && (peca is Torre) && (peca.Cor == Cor) && (peca.QtdeMovimentos == 0);
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            // Verificação das possíveis posições de movimentação desta peça (Rei)
            Posicao pos = new Posicao(0, 0);

            // Acima
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 1, Posicao.Coluna);
            // Diagonal superior direita (nordeste)
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 1, Posicao.Coluna + 1);
            // Lateral direita
            DefineETestaPosicao(matriz, pos, Posicao.Linha, Posicao.Coluna + 1);
            // Diagonal inferior direita (sudeste)
            DefineETestaPosicao(matriz, pos, Posicao.Linha + 1, Posicao.Coluna + 1);
            // Abaixo
            DefineETestaPosicao(matriz, pos, Posicao.Linha + 1, Posicao.Coluna);
            // Diagonal inferior esquerda (sudoeste)
            DefineETestaPosicao(matriz, pos, Posicao.Linha + 1, Posicao.Coluna - 1);
            // Lateral esquerda
            DefineETestaPosicao(matriz, pos, Posicao.Linha, Posicao.Coluna - 1);
            // Diagonal superior esquerda (noroeste)
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 1, Posicao.Coluna - 1);

            // #JodaEspecial Roque

            if (QtdeMovimentos == 0 && !_partida.Xeque)
            {
                // #JodaEspecial RoquePequeno
                Posicao posTorre = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (TestaTorreParaRoque(posTorre))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // #JodaEspecial RoqueGrande
                posTorre.Linha = Posicao.Linha;
                posTorre.Coluna = Posicao.Coluna - 4;
                if (TestaTorreParaRoque(posTorre))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.Peca(p1) == null && Tabuleiro.Peca(p2) == null && Tabuleiro.Peca(p3) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

            return matriz;
        }

    }
}
