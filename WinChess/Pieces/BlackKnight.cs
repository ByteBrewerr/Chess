using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class BlackKnight : ChessPiece
    {
        public BlackKnight(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.blackKnight);
            this.IsBlack = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
            int dx = toC - col;
            int dy = toR - row;
            if ((Math.Abs(dx) == 2 && Math.Abs(dy) == 1) || (Math.Abs(dx) == 1 && Math.Abs(dy) == 2))
            {
                if (Board[toR, toC] == null) return true;       //Если пусто
                if (Board[toR, toC].IsWhite) return true;       //Если белая фигура
            }
            return false;
        }

    }
}
