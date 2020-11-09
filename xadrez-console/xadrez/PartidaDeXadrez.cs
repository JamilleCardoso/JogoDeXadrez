using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console.xadrez
{
    class PartidaDeXadrez
    {
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public Tabuleiro Tabuleiro { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();
            Terminada = false;
            Xeque = false;
            ColocarPecas();            
        }

        private Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtdeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                _capturadas.Add(pecaCapturada);
            }

            // #JogadaEspecial RoquePequeno
            if (p is Rei && destino.Linha == origem.Linha && destino.Coluna == origem.Coluna + 2) 
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                ExecutaMovimento(origemTorre, destinoTorre);
                /* 
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
                */
            }

            // #JogadaEspecial RoqueGrande
            if (p is Rei && destino.Linha == origem.Linha && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                ExecutaMovimento(origemTorre, destinoTorre);
                /* 
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
                */
            }


            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                _desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Xeque = EstaEmXeque(Adversario(JogadorAtual));

            if (TestaXequeMate(Adversario(JogadorAtual)))
            {
                Terminada = true;
            } 
            else
            {
                Turno++;
                MudaJogador();
            }            
        }

        private void _desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);            
            p.DecrementarQtdeMovimentos();            
            if (pecaCapturada != null)
            {                
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);


            if (p is Rei && destino.Linha == origem.Linha && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                _desfazMovimento(origemTorre, destinoTorre, null);
                /* 
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
                */
            }

            // #JogadaEspecial RoqueGrande
            if (p is Rei && destino.Linha == origem.Linha && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                _desfazMovimento(origemTorre, destinoTorre, null);
                /* 
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQtdeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
                */
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if (Tabuleiro.Peca(origem) == null)
            {
                throw new TabuleiroException("Não existe peça nesta posição!");
            };
            if (Tabuleiro.Peca(origem).Cor != JogadorAtual)
            {
                throw new TabuleiroException("Esta peça não é sua!");
            };
            if (!Tabuleiro.Peca(origem).ExistemMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possíveis para esta peça!");
            };
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Não é possível mover a peça para esta posição!");
            };
        }


        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in _capturadas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in _pecas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversario (Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }            
        }

        private Peca _rei (Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }
            throw new TabuleiroException("Rei " + cor + " não encontrado no tabuleiro");
        }

        private bool EstaEmXeque(Cor cor)
        {
            Peca rei = _rei(cor);
            if (rei == null)
            {
                throw new TabuleiroException("Rei " + cor + " não encontrado no tabuleiro");
            }
            foreach (Peca pecaAdv in PecasEmJogo(Adversario(cor)))
            {                
                if (pecaAdv.MovimentosPossiveis()[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        private bool TestaXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] movPossiveis = peca.MovimentosPossiveis();
                for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                    {
                        if (movPossiveis[linha, coluna])
                        {
                            Posicao posicaoOrigem = peca.Posicao;
                            Posicao posicaoDestino = new Posicao(linha, coluna);
                            Peca pecaCapturada = ExecutaMovimento(posicaoOrigem, posicaoDestino);
                            bool testaXeque = EstaEmXeque(cor);
                            _desfazMovimento(posicaoOrigem, posicaoDestino, pecaCapturada);
                            if (!testaXeque) // Indica q o movimento retiraria do Chegue
                            {
                                return false;
                            }                            
                        }
                    }
                }
            }
            return true;  // Se o Méetodo não foi cortado pelo return false, significa q ainda continuaria em xeque, ou seja, xeque-mate
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('A', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('B', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('C', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('F', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('G', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('H', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('A', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('B', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('C', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('F', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('G', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('H', 2, new Peao(Tabuleiro, Cor.Branca));


            ColocarNovaPeca('A', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('B', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('C', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('F', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('G', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('H', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('A', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('B', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('C', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('F', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('G', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('H', 7, new Peao(Tabuleiro, Cor.Preta));
        }
    }
}
