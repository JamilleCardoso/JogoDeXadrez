using System;
using Xadrez_Console.tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int lin = 0; lin < tabuleiro.Linhas; lin++)
            {
                for (int col = 0; col < tabuleiro.Colunas; col++)
                {
                    if (tabuleiro.Peca(lin, col) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.Peca(lin, col) + " ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}
