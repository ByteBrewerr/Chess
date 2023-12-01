using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class BlackQueen : ChessPiece
    {
        public BlackQueen(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.blackQueen);
            this.IsBlack = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
			int dx = toC - col;
			int dy = toR - row;

			//Проверяем, может ли королева двигаться к клетке
			if (!(
			  (dx == 0 && dy != 0) ||
			  (dx != 0 && dy == 0) ||
			  (Math.Abs(dx) == Math.Abs(dy))))
			{
				return false;
			}

			//Если фигура черная в конце, то ходить нельзя
			if (Board[toR, toC] != null && Board[toR, toC].IsBlack)
				return false;

			//Проверяем клетки между точками пути
			//по вертикали
			if (dx == 0 && dy != 0)
			{
				int ddy = dy / Math.Abs(dy);
				for (int r = row + ddy ; r != toR; r += ddy)
				{
					if (Board[r, col] != null) return false;
				}
			}

			//по горизонтали
			if (dx != 0 && dy == 0)
			{
				int ddx = dx / Math.Abs(dx);
				for (int c = col + ddx; c != toC; c += ddx)
				{
					if (Board[row, c] != null) return false;
				}
			}
			if (dx != 0 && dy != 0)
			{
				int ddx = dx / Math.Abs(dx);
				int ddy = dy / Math.Abs(dy);
				for (int i = 1; i < Math.Abs(dx); i++)
				{
					if (Board[row + (i * ddy), col + (i * ddx)] != null) return false;
				}
			}

			return true;
		}

    }
}
