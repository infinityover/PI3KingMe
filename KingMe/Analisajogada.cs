using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMe
{
    class Analisajogada
    {
        public int jogadores;
        private TabuleiroClass melhorJogada, piorJogada;
        private TabuleiroClass novaJogada;
        public int Nivel { get; set; }
        public string proximaMelhorJogadaPersonagem;
        public int proximaMelhorJogadaPosicao;
        public TabuleiroClass tabuleiro;



        public int geraSetup(TabuleiroClass tabuleiroAtual, int nivel, int vez){

            this.melhorJogada = (TabuleiroClass)tabuleiroAtual.Clone();
            this.melhorJogada.pontuacao = 0;


            this.piorJogada = (TabuleiroClass)tabuleiroAtual.Clone();
            this.piorJogada.pontuacao = 1000;
            if (nivel == 0)
            {   
                //fim da execução deve retornar a pontuação
                return gerapontuacao(tabuleiroAtual);
            }
            //se a vez for = 0  significa que é a minha vez de jogar
            if (vez == 0) vez = jogadores;

            if(vez == jogadores)
            {
                //Caso seja a minha vez deve gerar a melhor jogada para mim
                geraJogadaSetup((TabuleiroClass)tabuleiroAtual.Clone(),1);
                if (this.Nivel == nivel)
                {
                    proximaMelhorJogadaPersonagem = melhorJogada.ultimaJogada.Split(',')[0];
                    proximaMelhorJogadaPosicao = Convert.ToInt32(melhorJogada.ultimaJogada.Split(',')[1]);
                }
                return geraSetup(melhorJogada, nivel--, vez--);
            }
            else{
                //Caso seja a vez do jogador deve gerar a pior jogada para mim
                geraJogadaSetup(tabuleiroAtual,-1);
                return geraSetup(piorJogada, nivel--, vez--);
            }
        }

        //Função de geração da proxima jogada
        //Gera apenas a proxima jogada, sendo ela a melhor/pior possivel dependendo do segundo parametro passado
        public void geraJogadaSetup(TabuleiroClass tabuleiroAtual,int melhorPior)
            //int melhorPior - 1 = melhor caso, -1 pior caso
        {
            int jogadoresSetor = 0;
            for (int qtPersonagens = 0; qtPersonagens <= tabuleiroAtual.personagensPossiveis.Count; qtPersonagens++)
            {
                for (int i = 1; i <= 4; i++)
                {
                    //Caso ja esteja totalmente preenchido deve pular
                    jogadoresSetor = 0;
                    for (int x = 0; x < 4; x++)
                    {
                        if (!String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][x])) jogadoresSetor++;
                    }
                    if (jogadoresSetor == 4) continue;

                    for (int j = 0; j <= 3; j++)
                    {
                        if (String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][j]))
                        {
                            novaJogada = (TabuleiroClass)tabuleiroAtual.Clone();

                            novaJogada.tabuleiro[i][j] = novaJogada.personagensPossiveis[qtPersonagens].Apelido;
                            novaJogada.pontuacao = gerapontuacao(novaJogada);
                            if (novaJogada.pontuacao > melhorJogada.pontuacao && melhorPior == 1)
                            {
                                novaJogada.ultimaJogada = novaJogada.personagensPossiveis[qtPersonagens].Apelido +"," + i.ToString() ;
                                novaJogada.personagensPossiveis.RemoveRange(qtPersonagens,1);
                                this.melhorJogada = novaJogada;
                            }
                            else if(novaJogada.pontuacao < melhorJogada.pontuacao && melhorPior == -1)
                            {
                                novaJogada.ultimaJogada = novaJogada.personagensPossiveis[qtPersonagens].Apelido + "," + i.ToString();
                                novaJogada.personagensPossiveis.RemoveRange(qtPersonagens, 1);
                                this.piorJogada = novaJogada;
                            }
                        }
                    }
                }
            }
        }

        //Revisar: gera jogada de promoção
        /*public TabuleiroClass gerajogada(TabuleiroClass tabuleiroAtual, int nivel, int vez)
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
        }*/

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
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < tabuleiro.tabuleiro[i].Length ; j++)
                {
                    if(String.IsNullOrEmpty(tabuleiro.tabuleiro[i][j])) continue;
                    if(tabuleiro.cartas.Contains(tabuleiro.tabuleiro[i][j])) pontuacao = pontuacao + i;
                }
            }
            return pontuacao;
        }

    }
}
