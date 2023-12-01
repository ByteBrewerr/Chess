using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class WhiteBishop : ChessPiece
    {
        public WhiteBishop(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.whiteBishop);
            this.IsWhite = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
            int dx = toC - col;
            int dy = toR - row;
            if (Math.Abs(dx) != Math.Abs(dy)) return false; //Слон ходит только по диагонали

            if (Board[toR, toC] != null && Board[toR, toC].IsWhite)  //Если фигура белая в конце, то ходить нельзя
                return false;

            int ddx = dx / Math.Abs(dx);
            int ddy = dy / Math.Abs(dy);
            for (int i = 1; i < Math.Abs(dx); i++)          //Идем по диагонали от начальной позиции до конечной
            {
                if (Board[row + (i * ddy), col + (i * ddx)] != null) return false; //Если есть фигура на пути, то сотанавливаемся
            }

            return true;
        }

    }
}
