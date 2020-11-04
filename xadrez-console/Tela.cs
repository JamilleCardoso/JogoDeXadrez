﻿using System;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int lin = 0; lin < tabuleiro.Linhas; lin++)
            {
                Console.Write(tabuleiro.Linhas - lin + " ");
                for (int col = 0; col < tabuleiro.Colunas; col++)
                {
                    if (tabuleiro.Peca(lin, col) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(lin, col));                        
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write(" ");
            for (int col = 0; col < tabuleiro.Colunas; col++)
            {
                // Console.Write(" " + Char.ConvertFromUtf32(0065+col)); // Também funciona  (65 A maiúsculo, 97 a minúsculo)
                Console.Write(" " + (Char)(0065+col));
            }

        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor corAux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca);
                Console.ForegroundColor = corAux;
            }
        }
    }
}
