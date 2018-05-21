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
        public int pontuacao;
        public string cartas;
        public int votos;
        public int statusJogo;
        public string ultimaJogada;
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
            for (int i = 0; i < 6; i++) tabuleiro[i] = new string[4];
            if(estadoAtual.Replace("\n","").Replace("\r","") != "") montaTabuleiro(estadoAtual);
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
                proximovazio = this.tabuleiro.GetLength(Convert.ToInt32(personagem[0])) +1;
                posicaopersonagem = Convert.ToInt32(personagem[0]);
                this.tabuleiro[posicaopersonagem][proximovazio] = personagem[1];
            }
           
        }
    }
}
