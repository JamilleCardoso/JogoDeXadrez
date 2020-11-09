using System;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = Char.ToLower(coluna);
            Linha = linha;
        }
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
        public static PosicaoXadrez ToPosicaoXadrez(Posicao posicao)
        { 
            return new PosicaoXadrez((Char)(0097 + posicao.Coluna), (8 - posicao.Linha));
        }
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
