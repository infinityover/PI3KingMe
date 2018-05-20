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

        public TabuleiroClass[] gerajogada(TabuleiroClass tabuleiroAtual)
        {

            if(tabuleiroAtual.rei != "")
            {
                tabuleiroAtual.pontuacao = gerapontuacao(tabuleiroAtual);
                jogadas_possiveis[0] = tabuleiroAtual;

                tabuleiroAtual.rei = "";
                tabuleiroAtual.pontuacao = gerapontuacao(tabuleiroAtual);
                jogadas_possiveis[1] = tabuleiroAtual;

            }
            else
            {
                TabuleiroClass proximaJogada = tabuleiroAtual;
                for(int i = 0; i<= 5; i++)
                {
                    if(tabuleiroAtual.tabuleiro.GetLength(i+1) == 4)
                    {
                        continue;
                    }
                    for (int j = 0; j <= 4; j++)
                    {
                        proximaJogada.tabuleiro[i + 1][posicaolivre(proximaJogada, i + 1)] = proximaJogada.tabuleiro[i][j];
                    }
                }
            }
            return jogadas_possiveis;
            
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
