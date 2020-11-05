namespace Xadrez_Console.tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao posicao)
        {
            return _pecas[posicao.Linha, posicao.Coluna];
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return (Peca(posicao) != null);
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException($"Já existe uma peça nesta posição ({posicao.Linha}, {posicao.Coluna}");
            }
            // Não precisa do else pq o throw corta a execução
            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                // Peca peca = _pecas[posicao.Linha, posicao.Coluna];
                Peca peca = Peca(posicao);
                peca.Posicao = null;
                _pecas[posicao.Linha, posicao.Coluna] = null;                
                return peca;     
            }
            else
            {
                return null;
            }            
        }

        public bool PosicaoValida(Posicao posicao)
        {
            return (posicao.Linha >= 0 && posicao.Linha < Linhas && posicao.Coluna >= 0 && posicao.Coluna < Colunas);
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
