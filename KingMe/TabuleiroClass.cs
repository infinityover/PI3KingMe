using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMe
{
    class TabuleiroClass : ICloneable
    {
        public string rei;
        public string[][] tabuleiro = new string[6][];
        public int pontuacao = 0;
        public string cartas;
        public int votos;
        public string ultimoVoto;
        public int statusJogo;
        public string ultimaJogada;
        public TabuleiroClass melhorJogada;
        public TabuleiroClass piorJogada;
        public string estadoAtual;
        public List<Personagem> personagemsUsados;
        public List<TabuleiroClass> jogadasPossiveis = new List<TabuleiroClass>();
        public TabuleiroClass jogadaPai;

        public List<Personagem> personagensPossiveis = new List<Personagem> {
            new Personagem("A"),
            new Personagem("B"),
            new Personagem("C"),
            new Personagem("D"),
            new Personagem("E"),
            new Personagem("F"),
            new Personagem("G"),
            new Personagem("I"),
            new Personagem("L"),
            new Personagem("M"),
            new Personagem("N"),
            new Personagem("O"),
            new Personagem("P")};

        public TabuleiroClass(string estadoAtual) {
            this.estadoAtual = estadoAtual;
            for (int i = 0; i < 6; i++) tabuleiro[i] = new string[4];
            if (estadoAtual.Replace("\n", "").Replace("\r", "") != "") montaTabuleiro(estadoAtual.Substring(2, estadoAtual.Length-4));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void montaTabuleiro(string tabuleiro)
        {
            //Tratando tabuleiro recebido
            string tabuleiroTratado = tabuleiro.Replace("\n", "");
            string [] matriz = tabuleiroTratado.Split('\r');
            string[] personagem;
            int proximovazio = 0;
            int posicaopersonagem;
            for (int i = 0; i < matriz.Length; i++){
                personagem = matriz[i].Split(',');
                proximovazio = 0;
                posicaopersonagem = Convert.ToInt32(personagem[0]);
                for (int j = 0; j < this.tabuleiro[posicaopersonagem].Length; j++) if (!String.IsNullOrEmpty(this.tabuleiro[posicaopersonagem][j])) proximovazio++;

                if (personagem[0] == "10")
                {
                    this.rei = personagem[1];
                    continue;
                }


                this.tabuleiro[posicaopersonagem][proximovazio] = personagem[1];
                for (int j = 0; j < this.personagensPossiveis.Count; j++)
                {
                    if (this.personagensPossiveis[j].Apelido == personagem[1])
                    {
                        //this.personagemsUsados.Add(new Personagem(this.personagensPossiveis[j].Apelido));
                        this.personagensPossiveis.RemoveAt(j);
                    }
                    
                }
            }
           
        }
    }
}
