using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }
        public override string ToString()
        {
            return "C";
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
                        
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 1, Posicao.Coluna - 2);
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 2, Posicao.Coluna - 1);

            DefineETestaPosicao(matriz, pos, Posicao.Linha - 1, Posicao.Coluna + 2);
            DefineETestaPosicao(matriz, pos, Posicao.Linha - 2, Posicao.Coluna + 1);

            DefineETestaPosicao(matriz, pos, Posicao.Linha + 1, Posicao.Coluna - 2);
            DefineETestaPosicao(matriz, pos, Posicao.Linha + 2, Posicao.Coluna - 1);

            DefineETestaPosicao(matriz, pos, Posicao.Linha + 1, Posicao.Coluna + 2);
            DefineETestaPosicao(matriz, pos, Posicao.Linha + 2, Posicao.Coluna + 1);


            return matriz;
        }
    }
}
