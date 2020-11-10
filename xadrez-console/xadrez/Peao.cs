using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez _partida;
        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partida) : base(tabuleiro, cor)
        {
            _partida = partida;
        }
        public override string ToString()
        {
            return "P";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }
        private bool PosicaoLivre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            // Verificação das possíveis posições de movimentação desta peça (Rei)
            Posicao pos = new Posicao(0, 0);

            // Peao só anda pra "frente"

            // Se a cor for Branca só ando para as linhas menores
            if (Cor == Cor.Branca)
            {
                // O Peão só captura peças laterais, portando só ando para frente se tiver Livre
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && PosicaoLivre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && PosicaoLivre(pos) && QtdeMovimentos == 0)  // Na 1ª jogada o Peao pode andar 2 casas
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // O Peão só captura peças laterais, portando ando para a lateral se tiver Inimigo
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                // #JogadaEspecial EnPassant
                if (Posicao.Linha == 3)
                {
                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos) && Tabuleiro.Peca(pos) == _partida.VulneravemEnPassant)
                    {
                        matriz[Posicao.Linha - 1, Posicao.Coluna - 1] = true;                        
                    }

                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos) && Tabuleiro.Peca(pos) == _partida.VulneravemEnPassant)
                    {
                        matriz[Posicao.Linha - 1, Posicao.Coluna + 1] = true;                        
                    }
                }
            }
            else
            {
                // O Peão só captura peças laterais, portando só ando para frente se tiver Livre
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && PosicaoLivre(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(pos) && PosicaoLivre(pos) && QtdeMovimentos == 0)  // Na 1ª jogada o Peao pode andar 2 casas
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }

                // O Peão só captura peças laterais, portando ando para a lateral se tiver Inimigo
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    matriz[pos.Linha, pos.Coluna] = true;
                }
                // #JogadaEspecial EnPassant
                if (Posicao.Linha == 4)
                {
                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos) && Tabuleiro.Peca(pos) == _partida.VulneravemEnPassant)
                    {
                        matriz[Posicao.Linha + 1, Posicao.Coluna - 1] = true;                        
                    }

                    pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos) && Tabuleiro.Peca(pos) == _partida.VulneravemEnPassant)
                    {
                        matriz[Posicao.Linha + 1, Posicao.Coluna + 1] = true;                        
                    }
                }
            }

            return matriz;
        }
    }
}