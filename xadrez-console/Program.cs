using System;
using Xadrez_Console.xadrez;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {                
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        Console.Clear();
                        Tela.ImprimirPartida(partida, partida.Tabuleiro.Peca(origem).MovimentosPossiveis());
                        /* 
                        Tela.ImprimirTabuleiro(partida.Tabuleiro, partida.Tabuleiro.Peca(origem).MovimentosPossiveis());
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partida.Turno);
                        Console.Write("Jogada em andamento: ");
                        Tela.ImprimeJogadorNaCor(partida.JogadorAtual);
                        */
                        Console.WriteLine();
                        Console.WriteLine("Origem: " + PosicaoXadrez.ToPosicaoXadrez(origem));
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);                        
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }          
                
                               
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            /* catch (Exception e)  Comentei pq quero ser informada dos detalhes erro durante a execução
            {
                Console.WriteLine(e.Message);
            } */
            finally
            {
                Console.ReadLine();
            }
                       


        }
    }
}
