using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMe
{
    class ObjNiveis
    {
        private int PosX;
        private int PosY;
        private string status;
        private string[] posicaoNivel = new string[4];
        private int coluna = -1;
        private int i;

        public void setarPosicaoNivel(int[] Pos1, int[] Pos2, int[] Pos3, int[] Pos4)
        {
            this.posicaoNivel[0] = Convert.ToString(Pos1 + "vazia");
            this.posicaoNivel[1] = Convert.ToString(Pos2 + "vazia");
            this.posicaoNivel[2] = Convert.ToString(Pos3 + "vazia");
            this.posicaoNivel[3] = Convert.ToString(Pos4 + "vazia");
        }

        public bool inserirPersonagem(int PosX, int PosY, string status)
        {
            for (i = 0; i < 4; i++)
            {
                if (this.posicaoNivel[i] == "vazia")
                {
                    this.posicaoNivel[i] = Convert.ToString(PosX + PosY + status);
                }
            }
            if (i == 5) { return false; }

            return true;

        }

        public bool removerPersonagem(string status)
        {
            for (i = 0; i < 4; i++)
            {
                if (this.posicaoNivel[i] == status)
                {
                    string[] aux = { };
                    aux = this.posicaoNivel[i].Split(',');
                    this.posicaoNivel[i] = Convert.ToString(aux[0] + aux[1] + "vazia");
                }
            }
            if (i == 5) { return false; }

            return true;

        }

    }
}
