using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMe
{
    class NiveisTabuleiro
    {
        private string[] Niveis = new string[6];

       

        private void inserirPersonagemNivel(ObjNiveis Niveis)
        {
            Matriz
        }

        public bool promoverPersonagem(int PosX, int PosY, string acao)
        {
            if (this.coluna >= 4) { return false; }
            this.nivelTabuleiro[this.coluna++] = Convert.ToString(PosX + PosY + acao);
            return true;
        }

        public bool removerPersonagem(int PosX, int PosY, string acao)
        {
            if (this.coluna >= 4) { return false; }
            this.nivelTabuleiro[this.coluna++] = Convert.ToString(PosX + PosY + acao);
            return true;
        }
    }
}
