using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class PartidaDeXadrez
    {
        private int _turno;
        private Cor _jogadorAtual;
        public Tabuleiro Tabuleiro { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            ColocarPecas();
            Terminada = false;
        }              

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            if (Tabuleiro.Peca(origem).MovimentosPossiveis()[destino.Linha, destino.Coluna])
            {
                Peca p = Tabuleiro.RetirarPeca(origem);
                p.IncrementarQtdeMovimentos();
                Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);  // será usada posteriormente
                Tabuleiro.ColocarPeca(p, destino);
            }
            else
            {
                System.Console.WriteLine("Movimento não permitido");
            }



            /*
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtdeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);  // será usada posteriormente
            Tabuleiro.ColocarPeca(p, destino);
            */
        }

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('C', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('C', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('D', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('E', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('E', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('D', 1).ToPosicao());

            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('C', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('C', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('D', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('E', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('E', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('D', 8).ToPosicao());
        }
    }
}
