using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMe
{
    class Analisajogada
    {
        public TabuleiroClass[][] jogadas;
        private TabuleiroClass[] jogadas_possiveis;
        public int jogadores;
        public TabuleiroClass melhorjogada;
        public int nivel;

        public TabuleiroClass gerajogada(TabuleiroClass tabuleiroAtual, int nivel, int vez)
        {
            if(nivel == 0)
            {
                melhorjogada = tabuleiroAtual;
                return melhorjogada;
            }
            if(vez == 0)
            {
                vez = jogadores;
            }
            if (tabuleiroAtual.rei != "")
            {
                tabuleiroAtual.pontuacao = gerapontuacao(tabuleiroAtual);
                jogadas_possiveis[0] = tabuleiroAtual;

                tabuleiroAtual.rei = "";
                tabuleiroAtual.pontuacao = gerapontuacao(tabuleiroAtual);
                jogadas_possiveis[1] = tabuleiroAtual;

                if(jogadas_possiveis[0].pontuacao < jogadas_possiveis[1].pontuacao)
                {
                    return gerajogada(jogadas_possiveis[1], nivel--, vez--);
                }
                else
                {
                    melhorjogada = jogadas_possiveis[0];
                    return melhorjogada;
                }

            }
            else
            {

                TabuleiroClass proximaJogada = tabuleiroAtual;
                int indice = 0;
                int posicaoPossivel = 0;
                for(int i = 0; i<= 5; i++)
                {
                    if(tabuleiroAtual.tabuleiro.GetLength(i+1) == 4)
                    {
                        continue;
                    }
                    for (int j = 0; j <= 4; j++)
                    {
                        //reseta tabuleiro para condição inicial
                        proximaJogada = tabuleiroAtual;
                        posicaoPossivel = posicaolivre(proximaJogada, i + 1);
                        if (posicaoPossivel < 0) continue;
                        proximaJogada.tabuleiro[i + 1][posicaoPossivel] = proximaJogada.tabuleiro[i][j];
                        proximaJogada.pontuacao = gerapontuacao(proximaJogada);
                        jogadas_possiveis[++indice] = proximaJogada;
                    }
                }
                for(int i = 0; i <indice; i++)
                {
                    //caso seja a minha vez de jogar
                    if(vez == jogadores)
                    {
                        
                    }
                }
            }
            return gerajogada(jogadas_possiveis[0], nivel--, vez--);
        }

        public int posicaolivre(TabuleiroClass tabuleiro, int i)
        {
            for(int j =0; j < 4; j++)
            {
                if (tabuleiro.tabuleiro[i][j] == "") return j;
            }
            return -1;
        }

        public int gerapontuacao(TabuleiroClass tabuleiro)
        {
            int pontuacao = 0;
            for(int i = 0; i <= 5; i++)
            {
                for(int j = 0; j < tabuleiro.tabuleiro.GetLength(i); j++)
                {
                    if(tabuleiro.cartas.Contains(tabuleiro.tabuleiro[i][j])) pontuacao = pontuacao + i;
                }
            }
            return pontuacao;
        }

    }
}
