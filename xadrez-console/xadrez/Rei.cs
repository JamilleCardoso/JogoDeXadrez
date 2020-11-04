using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base (tabuleiro, cor)
        {
            
        }
        public override string ToString()
        {
            return "R";
        }

    }
}
