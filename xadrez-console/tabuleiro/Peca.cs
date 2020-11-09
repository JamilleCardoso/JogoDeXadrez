using System.Net.Sockets;

namespace Xadrez_Console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {            
            Cor = cor;            
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
            Posicao = null;
        }    
        
        public void IncrementarQtdeMovimentos()
        {
            QtdeMovimentos++;
        }
               
        public void DecrementarQtdeMovimentos()
        {
            QtdeMovimentos--;
        }

        public virtual bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return (peca == null) || (peca.Cor != Cor);
        }

        public virtual bool PodeMoverPara(Posicao destino)
        {
            return MovimentosPossiveis()[destino.Linha, destino.Coluna];
        }

        public bool ExistemMovimentosPossiveis()
        {            
            bool[,] mov = MovimentosPossiveis();
            for (int lin = 0; lin < Tabuleiro.Linhas; lin++)
            {
                for (int col = 0; col < Tabuleiro.Colunas; col++)
                {
                    if (mov[lin, col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] MovimentosPossiveis();

    }
}
