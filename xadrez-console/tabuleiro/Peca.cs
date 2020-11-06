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

        public virtual bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return (peca == null) || (peca.Cor != Cor);
        }

        public abstract bool[,] MovimentosPossiveis();

    }
}
