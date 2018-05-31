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



        public void geraSetup(TabuleiroClass tabuleiroAtual, int nivel, int jogadorVez){
            //Gera arvore de possibilidade
            //Até quando possivel;
            if(tabuleiroAtual.personagensPossiveis.Count == 0)
            {
                geraPromocao(tabuleiroAtual, nivel, jogadorVez);
                return;
            }
            if (jogadorVez == 0) jogadorVez = this.jogadores;
            //se a vez for = 0  significa que é a minha vez de jogar
            if (nivel == 0) return;
            //Caso seja a vez do jogador deve gerar a pior jogada para mim
            geraJogadaSetup(tabuleiroAtual);
            if (tabuleiroAtual.melhorJogada != null) geraSetup(tabuleiroAtual.melhorJogada, (nivel - 1), jogadorVez-1);
            if (tabuleiroAtual.piorJogada != null) geraSetup(tabuleiroAtual.piorJogada, (nivel - 1), jogadorVez-1);

            if (nivel == this.Nivel) {
                vasculhaJogadas(tabuleiroAtual, jogadorVez, nivel);
                if (this.novaJogada != null)
                {
                    this.proximaMelhorJogadaPersonagem = this.novaJogada.ultimaJogada.Substring(0, 1);
                    this.proximaMelhorJogadaPosicao = Convert.ToInt32(this.novaJogada.ultimaJogada.Substring(2, 1));
                }
                else
                {
                    this.proximaMelhorJogadaPersonagem = tabuleiroAtual.melhorJogada.ultimaJogada.Substring(0, 1);
                    this.proximaMelhorJogadaPosicao = Convert.ToInt32(tabuleiroAtual.melhorJogada.ultimaJogada.Substring(2, 1));
                }
                }
            }
        


        public int vasculhaJogadas(TabuleiroClass tabuleiroAtual, int jogadores, int nivel)
        {
            if (jogadores == 0) jogadores = this.jogadores;
            if(tabuleiroAtual.melhorJogada == null || tabuleiroAtual.piorJogada == null || nivel == 0)
                return tabuleiroAtual.pontuacao;

            if (jogadores == this.jogadores)
            {
                if ((tabuleiroAtual.piorJogada != null && tabuleiroAtual.melhorJogada != null) && vasculhaJogadas(tabuleiroAtual.piorJogada, jogadores-1, nivel - 1) > vasculhaJogadas(tabuleiroAtual.melhorJogada, jogadores-1, nivel - 1))
                {
                    if (this.Nivel == nivel)
                    {
                        if (tabuleiroAtual.piorJogada != null)
                        {
                            this.novaJogada = tabuleiroAtual.piorJogada;
                        }else if(tabuleiroAtual.melhorJogada != null) this.novaJogada = tabuleiroAtual.melhorJogada;
                    }
                    return vasculhaJogadas(tabuleiroAtual.piorJogada, jogadores-1, nivel - 1);
                }
                else
                {
                    if (this.Nivel == nivel)
                    {
                        if (tabuleiroAtual.melhorJogada != null)
                        {
                            this.novaJogada = tabuleiroAtual.melhorJogada;
                        }
                        else if (tabuleiroAtual.piorJogada != null) this.novaJogada = tabuleiroAtual.piorJogada;
                    }
                    
                    return vasculhaJogadas(tabuleiroAtual.melhorJogada, jogadores-1, nivel - 1);
                }
            }
            else return vasculhaJogadas(tabuleiroAtual.piorJogada, jogadores-1, nivel -1);

        }

        //Função de geração da proxima jogada
        //Gera apenas a proxima jogada, sendo ela a melhor/pior possivel dependendo do segundo parametro passado
        public void geraJogadaSetup(TabuleiroClass tabuleiroAtual)
            //int melhorPior - 1 = melhor caso, -1 pior caso
        {
            if (tabuleiroAtual.personagensPossiveis.Count == 0) return;
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
                    if (!String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][j])) continue;
                    for (int qtPersonagens = 0; qtPersonagens < tabuleiroAtual.personagensPossiveis.Count; qtPersonagens++)
                    {
                        TabuleiroClass jogadaAtual = new TabuleiroClass(tabuleiroAtual.estadoAtual);
                        for(int linha = 0; linha < 6; linha++){
                            for (int coluna = 0; coluna < 4; coluna++)
                            {
                                jogadaAtual.tabuleiro[linha][coluna] = tabuleiroAtual.tabuleiro[linha][coluna];
                            }
                        }

                        jogadaAtual.cartas = tabuleiroAtual.cartas;

                        jogadaAtual.tabuleiro[i][j] = tabuleiroAtual.personagensPossiveis[qtPersonagens].Apelido;
                        jogadaAtual.ultimaJogada = tabuleiroAtual.personagensPossiveis[qtPersonagens].Apelido + "," + i.ToString();
                        jogadaAtual.pontuacao = geraPontuacao(jogadaAtual);
                        //tabuleiroAtual.jogadasPossiveis.Add(jogadaAtual);
                        jogadaAtual.personagensPossiveis.RemoveAt(qtPersonagens);
                        jogadaAtual.jogadaPai = tabuleiroAtual;

                        if (tabuleiroAtual.melhorJogada == null || jogadaAtual.pontuacao > tabuleiroAtual.melhorJogada.pontuacao)
                        {
                            tabuleiroAtual.melhorJogada = jogadaAtual;
                        }
                        else if(tabuleiroAtual.piorJogada == null || jogadaAtual.pontuacao < tabuleiroAtual.piorJogada.pontuacao)
                        {
                            tabuleiroAtual.piorJogada = jogadaAtual;
                        }

                    }
                }
            }
        }

        public void geraPromocao(TabuleiroClass tabuleiroAtual,int nivel,int jogadorVez)
        {
            //Gera arvore de possibilidade
            //Até quando possivel;
            if (jogadorVez == 0) jogadorVez = this.jogadores;
            //se a vez for = 0  significa que é a minha vez de jogar
            if (nivel == 0) return;
            //Caso seja a vez do jogador deve gerar a pior jogada para mim
            geraJogadaPromocao(tabuleiroAtual);
            if (tabuleiroAtual.melhorJogada != null) geraPromocao(tabuleiroAtual.melhorJogada, (nivel - 1), jogadorVez-1);
            if (tabuleiroAtual.piorJogada != null) geraPromocao(tabuleiroAtual.piorJogada, (nivel - 1), jogadorVez-1);

            if (nivel == this.Nivel)
            {
                vasculhaJogadas(tabuleiroAtual, jogadorVez, nivel);
                if (this.novaJogada != null)
                {
                    this.proximaMelhorJogadaPersonagem = this.novaJogada.ultimaJogada.Substring(0, 1);
                }
                else if (tabuleiroAtual.melhorJogada != null)
                {
                    this.proximaMelhorJogadaPersonagem = tabuleiroAtual.melhorJogada.ultimaJogada.Substring(0, 1);
                }else
                {
                    this.proximaMelhorJogadaPersonagem = tabuleiroAtual.piorJogada.ultimaJogada.Substring(0, 1);
                }
            }
        }

        public void geraJogadaPromocao(TabuleiroClass tabuleiroAtual)
        {
            //Verificando setores com disponibilidade dentro do tabuleiro
            int[] setores = new int[6];
            for (int i = 0; i < tabuleiroAtual.tabuleiro.Length; i++)
            {
                for (int j = 0; j < 4; j++) if (!String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][j])) setores[i]++;
            }

            if (String.IsNullOrEmpty(tabuleiroAtual.rei)) geraJogadaVoto(tabuleiroAtual);
            for (int i = 1; i < tabuleiroAtual.tabuleiro.Length - 1; i++)
            {
                //caso o setor esteja cheio reinicia
                if (setores[i+1] == 4) continue;

                for (int j = 0; j < 4; j++)
                {
                    TabuleiroClass jogadaAtual = new TabuleiroClass(tabuleiroAtual.estadoAtual);
                    for (int linha = 0; linha < 6; linha++)
                    {
                        for (int coluna = 0; coluna < 4; coluna++)
                        {
                            jogadaAtual.tabuleiro[linha][coluna] = tabuleiroAtual.tabuleiro[linha][coluna];
                        }
                    }

                    jogadaAtual.cartas = tabuleiroAtual.cartas;

                    if (String.IsNullOrEmpty(tabuleiroAtual.tabuleiro[i][j])) continue;
                    int proximoLivre = posicaoLivre(tabuleiroAtual, i + 1);
                    if (proximoLivre != -1)
                    {
                        jogadaAtual.tabuleiro[i + 1][proximoLivre] = tabuleiroAtual.tabuleiro[i][j];
                        jogadaAtual.tabuleiro[i][j] = "";
                        jogadaAtual.ultimaJogada = jogadaAtual.tabuleiro[i + 1][proximoLivre];
                    }
                    else continue;

                    jogadaAtual.pontuacao = geraPontuacao(jogadaAtual);
                    if(tabuleiroAtual.melhorJogada == null || jogadaAtual.pontuacao> tabuleiroAtual.melhorJogada.pontuacao)
                    {
                        tabuleiroAtual.melhorJogada = jogadaAtual;
                    }else if (tabuleiroAtual.piorJogada == null || jogadaAtual.pontuacao < tabuleiroAtual.piorJogada.pontuacao)
                    {
                        tabuleiroAtual.piorJogada = jogadaAtual;
                    }
                }
            }
        }
        public void geraJogadaVoto(TabuleiroClass tabuleiroAtual)
        {
            tabuleiroAtual.pontuacao = geraPontuacao(tabuleiroAtual);

            TabuleiroClass jogadaAtual = new TabuleiroClass(tabuleiroAtual.estadoAtual);
            for (int linha = 0; linha < 6; linha++)
            {
                for (int coluna = 0; coluna < 4; coluna++)
                {
                    jogadaAtual.tabuleiro[linha][coluna] = tabuleiroAtual.tabuleiro[linha][coluna];
                }
            }
            jogadaAtual.cartas = tabuleiroAtual.cartas;
            jogadaAtual.rei = "";
            jogadaAtual.pontuacao = geraPontuacao(jogadaAtual);

            if(jogadaAtual.pontuacao > tabuleiroAtual.pontuacao)
            {
                tabuleiroAtual.melhorJogada = jogadaAtual;
                tabuleiroAtual.piorJogada = tabuleiroAtual;
                tabuleiroAtual.ultimoVoto = "S";
            }
            else
            {

                tabuleiroAtual.melhorJogada = tabuleiroAtual;
                tabuleiroAtual.piorJogada = jogadaAtual;
                tabuleiroAtual.ultimoVoto = "N";
            }

        }

        public int posicaoLivre(TabuleiroClass tabuleiro, int i)
        {
            for(int j =0; j < 4; j++)
            {
                if (String.IsNullOrEmpty(tabuleiro.tabuleiro[i][j])) return j;
            }
            return -1;
        }

        public int geraPontuacao(TabuleiroClass tabuleiro)
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
            if (tabuleiro.cartas.Contains(tabuleiro.rei)) pontuacao = pontuacao + 10;

            return pontuacao;
        }

    }
}
