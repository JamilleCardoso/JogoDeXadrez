namespace Xadrez_Console.tabuleiro
{
    class Peca
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
    }
}
