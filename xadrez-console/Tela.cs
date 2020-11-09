using System;
using System.Collections.Generic;
using Xadrez_Console.tabuleiro;
using Xadrez_Console.xadrez;

namespace Xadrez_Console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirPartida(partida, null);            
        }

        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] posicoesPossiveis)
        {
            ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {

                if (posicoesPossiveis == null)
                {
                    Console.Write("Aguardando jogada: ");
                }
                else
                {
                    Console.Write("Jogada em andamento: ");
                }
                Tela.ImprimeJogadorNaCor(partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine(" (VOCÊ ESTÁ EM XEQUE!)");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!!");                
                Console.WriteLine("O vencedor foi: " + partida.JogadorAtual);
            }
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor corAtual = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = corAtual;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {           
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");            
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
                Console.Write(" " +  (Char)(0097 + col));
            }
            Console.WriteLine();

        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string pos = Console.ReadLine();
            char coluna = Char.ToLower(pos[0]);
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
