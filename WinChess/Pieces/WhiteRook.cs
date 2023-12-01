using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class WhiteRook : ChessPiece
    {
        public WhiteRook(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.whiteRook);
            this.IsWhite = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
			if (row != toR && col != toC) { return false; }

			//Если фигура белая в конце, то ходить нельзя
			if (Board[toR, toC] != null && Board[toR, toC].IsWhite)
				return false;

			if (row == toR)       //Проверяем, что нет фигур по вертикали
			{
				int dx = toC - col;
				int ddx = dx / Math.Abs(dx);
				for (int c = col + ddx; c != toC; c += ddx)
				{
					if (Board[row, c] != null) return false;
				}
			}
			else if (col == toC)  //Проверяем, что нет фигур по горизонтали
			{
				int dy = toR - row;
				int ddy = dy / Math.Abs(dy);
				for (int r = row + ddy; r != toR; r += ddy)
				{
					if (Board[r, col] != null) return false;
				}
			}
			return true;
		}

	}
}
