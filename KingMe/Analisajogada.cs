using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public List<TabuleiroClass> estadosFinais;



        public void geraSetup(TabuleiroClass tabuleiroAtual, int nivel){
            //Gera arvore de possibilidade
            //Até quando possivel;
            int jogadorVez = jogadores;
            //se a vez for = 0  significa que é a minha vez de jogar
            if (nivel == 0) return;
            //Caso seja a vez do jogador deve gerar a pior jogada para mim
            geraJogadaSetup(tabuleiroAtual);
            if(tabuleiroAtual.melhorJogada != null) geraSetup(tabuleiroAtual.melhorJogada, (nivel - 1));
            if (tabuleiroAtual.piorJogada != null) geraSetup(tabuleiroAtual.piorJogada, (nivel - 1));

            //if(tabuleiroAtual.piorJogada != null) geraSetup(tabuleiroAtual.piorJogada, (nivel-1));

            if (nivel == this.Nivel) {
                vasculhaJogadas(tabuleiroAtual, true, nivel);
                this.proximaMelhorJogadaPersonagem = this.novaJogada.ultimaJogada.Substring(0, 1);
                this.proximaMelhorJogadaPosicao = Convert.ToInt32(this.novaJogada.ultimaJogada.Substring(2, 1));
                }
            }
        


        public int vasculhaJogadas(TabuleiroClass tabuleiroAtual, bool minmax, int nivel)
        {
            if((tabuleiroAtual.melhorJogada == null && tabuleiroAtual.piorJogada == null) || nivel == 0)
                return tabuleiroAtual.pontuacao;

            if (minmax)
            {
                if (vasculhaJogadas(tabuleiroAtual.piorJogada, false, nivel - 1) > vasculhaJogadas(tabuleiroAtual.melhorJogada, false, nivel - 1))
                {
                    if (this.Nivel == nivel) this.novaJogada = tabuleiroAtual.piorJogada;
                    return vasculhaJogadas(tabuleiroAtual.piorJogada, false, nivel - 1);
                }
                else
                {
                    this.novaJogada = tabuleiroAtual.melhorJogada;
                    return vasculhaJogadas(tabuleiroAtual.melhorJogada, false, nivel - 1);
                }
            }
            else return vasculhaJogadas(tabuleiroAtual.piorJogada, true,nivel -1);

        }

        //Função de geração da proxima jogada
        //Gera apenas a proxima jogada, sendo ela a melhor/pior possivel dependendo do segundo parametro passado
        public void geraJogadaSetup(TabuleiroClass tabuleiroAtual)
            //int melhorPior - 1 = melhor caso, -1 pior caso
        {
            //Verificando setores com disponibilidade dentro do tabuleiro
            int[] setores = new int[6];
            for(int i =0; i < tabuleiroAtual.tabuleiro.Length; i++)
            {
                for (int j = 0; j < 4; j++) if (!String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][j])) setores[i]++;
            }

            for (int i = 1; i < tabuleiroAtual.tabuleiro.Length -1; i++)
            {
                //caso o setor esteja cheio reinicia
                if (setores[i] == 4) continue;

                for (int j = 0; j < 4; j++)
                {
                    for (int qtPersonagens = 0; qtPersonagens < tabuleiroAtual.personagensPossiveis.Count; qtPersonagens++)
                    {
                        TabuleiroClass jogadaAtual = new TabuleiroClass(tabuleiroAtual.estadoAtual);
                        jogadaAtual.cartas = tabuleiroAtual.cartas;

                        jogadaAtual.tabuleiro[i][j] = tabuleiroAtual.personagensPossiveis[qtPersonagens].Apelido;
                        jogadaAtual.ultimaJogada = tabuleiroAtual.personagensPossiveis[qtPersonagens].Apelido + "," + i.ToString();
                        jogadaAtual.pontuacao = gerapontuacao(jogadaAtual);
                        tabuleiroAtual.jogadasPossiveis.Add(jogadaAtual);
                        jogadaAtual.personagensPossiveis.RemoveAt(qtPersonagens);
                        jogadaAtual.jogadaPai = tabuleiroAtual;

                        if (tabuleiroAtual.melhorJogada == null || jogadaAtual.pontuacao > tabuleiroAtual.melhorJogada.pontuacao)
                        {
                            tabuleiroAtual.melhorJogada = jogadaAtual;
                        }
                        else if(jogadaAtual.pontuacao < tabuleiroAtual.melhorJogada.pontuacao)
                        {
                            tabuleiroAtual.piorJogada = jogadaAtual;
                        }

                    }
                }
            }
        }

        public void geraPromocao(TabuleiroClass tabuleiroAtual,int nivel)
        {
            //Gera arvore de possibilidade
            //Até quando possivel;
            int jogadorVez = jogadores;
            //se a vez for = 0  significa que é a minha vez de jogar
            if (nivel == 0) return;
            //Caso seja a vez do jogador deve gerar a pior jogada para mim
            geraJogadaPromocao(tabuleiroAtual);
            if (tabuleiroAtual.melhorJogada != null) geraSetup(tabuleiroAtual.melhorJogada, (nivel - 1));
            if (tabuleiroAtual.piorJogada != null) geraSetup(tabuleiroAtual.piorJogada, (nivel - 1));



            if (nivel == this.Nivel)
            {
                this.novaJogada = tabuleiroAtual.melhorJogada;
                while (true)
                {
                    if (this.novaJogada.melhorJogada == null || this.novaJogada.piorJogada == null)
                    {
                        break;
                    }

                    if (jogadorVez == 0) jogadorVez = jogadores;

                    if (jogadorVez == jogadores)
                    {
                        this.novaJogada = this.novaJogada.melhorJogada;
                    }
                    else this.novaJogada = this.novaJogada.piorJogada;
                }
                jogadorVez--;
                this.proximaMelhorJogadaPersonagem = this.novaJogada.ultimaJogada.Substring(0, 1);
                this.proximaMelhorJogadaPosicao = Convert.ToInt32(this.novaJogada.ultimaJogada.Substring(2, 1));
            }
        }

        public void geraJogadaPromocao(TabuleiroClass tabuleiroAtual)
        {

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
