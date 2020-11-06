using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

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

            return matriz;
        }

    }
}
