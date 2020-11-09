using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

            Turno++;
            MudaJogador();
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
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('C', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('C', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('E', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('D', 1, new Rei(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('C', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('C', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('E', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('D', 8, new Rei(Tabuleiro, Cor.Preta));
        }
    }
}
