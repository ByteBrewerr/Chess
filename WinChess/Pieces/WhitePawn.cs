using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class WhitePawn : ChessPiece
    {
        public bool isJump = false;       //Флаг обозначает, что пешка прыгнула через клетку в первом своем ходе

        public WhitePawn(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.whitePawn);
            this.IsWhite = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
            //Проверяем, может ли пешка прыгнуть через клетку в начале хода
            if (!(hasMoved) && row == 1 && toR == 3 && col == toC)
            {
                if (Board[toR, toC] == null && Board[toR - 1, toC] == null)
                {
                    if(!onlyCheck) 
                        isJump = true;
                    return true;
                }
            }

            //Может ли пешка пройти на клетку вперед
            if (Board[toR, toC] == null && toR - row == 1 && col == toC)
            {
                return true;
            }

            //Бьет ли пешка влево?
            if (toR == (row + 1) && toC == (col - 1) && Board[toR, toC] != null && Board[toR, toC].IsBlack)
            {
                return true;
            }

            //Бьет ли пешка вправо?
            if (toR == (row + 1) && toC == (col + 1) && Board[toR, toC] != null && Board[toR, toC].IsBlack)
            {
                return true;
            }

            //Проверяем взятие на проходе влево
            if (toR - row == 1 && col - toC == 1 && Board[toR, toC] == null && Board[toR - 1 , toC] != null &&
            Board[toR - 1, toC] is BlackPawn &&
            Board[toR - 1, toC].IsJump())
            {
                if(!onlyCheck)
                    Board[toR - 1, toC] = null;
                return true;
            }

            //Проверяем взятие на проходе вправо
            if (toR - row == 1 && toC - col == 1 && Board[toR, toC] == null && Board[toR - 1, toC] != null &&
            Board[toR - 1, toC] is BlackPawn &&
            Board[toR - 1, toC].IsJump())
            {
                if (!onlyCheck)
                    Board[toR - 1, toC] = null;
                return true;
            }

            return false;

        }

        public override bool IsJump() { return isJump; }
        public override void clearIsJump() { isJump = false; }

    }
}
