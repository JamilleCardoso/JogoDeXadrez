using System;
using Xadrez_Console.tabuleiro;
using Xadrez_Console.xadrez;

namespace Xadrez_Console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            ImprimirTabuleiro(tabuleiro, null);       
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor corFundoOriginal = Console.BackgroundColor;
            ConsoleColor corFundoPossivel = ConsoleColor.DarkBlue;

            for (int lin = 0; lin < tabuleiro.Linhas; lin++)
            {                
                Console.Write(tabuleiro.Linhas - lin + " ");
                for (int col = 0; col < tabuleiro.Colunas; col++)
                {
                    if ((posicoesPossiveis != null) && (posicoesPossiveis[lin, col]))
                    {
                        Console.BackgroundColor = corFundoPossivel;
                    }
                    else
                    {
                        Console.BackgroundColor = corFundoOriginal;
                    }
                    ImprimirPeca(tabuleiro.Peca(lin, col));
                }
                Console.WriteLine();
                Console.BackgroundColor = corFundoOriginal;
            }
                        
            Console.Write(" ");
            for (int col = 0; col < tabuleiro.Colunas; col++)
            {
                // Console.Write(" " + Char.ConvertFromUtf32(0065+col)); // Também funciona  (65 A maiúsculo, 97 a minúsculo)
                Console.Write(" " + (Char)(0065 + col));
            }
            Console.WriteLine();

        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string pos = Console.ReadLine();
            char coluna = Char.ToUpper(pos[0]);
            int linha = int.Parse(pos[1] + "");  // Coloco + "" para forçar a conversão para string (Parse trabalha com string, e pos[] retorna char
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("-");
            }
            else
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
            Console.Write(" ");
        }

        public static void ImprimeJogadorNaCor(Cor jogador)
        {
            if (jogador == Cor.Branca)
            {
                Console.Write(jogador);
            }
            else
            {
                ConsoleColor corAux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(jogador);
                Console.ForegroundColor = corAux;
            }            
        }
    }
}
