using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess.Pieces
{
    class BlackKing : ChessPiece
    {
        public BlackKing(int _row, int _col)
        {
            this.row = _row;
            this.col = _col;
            this.Image = new Bitmap(Properties.Resources.blackKing);
            this.IsBlack = true;
        }

        public override bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
            int dx = toC - col;
            int dy = toR - row;

            //Если фигура черная в конце, то ходить нельзя
            if (Board[toR, toC] != null && Board[toR, toC].IsBlack)
                return false;

            if (dy == 0 && dx == 2) //Проверяем если рокировка вправо. Там может быть только ладья с выключенным флагом hasMoved
            {
                if (!hasMoved && Board[7, 7] != null && !Board[7, 7].hasMoved)
                {
                    for (int c = col + 1; c <= toC; c++)
                    {
                        if (Board[row, c] != null) return false;
                    }

                    //Не будет ли Шах
                    col++;
                    if (IsInCheck(Board))
                    {
                        col--;
                        return false;
                    }
                    col++;
                    if (IsInCheck(Board))
                    {
                        col -= 2;
                        return false;
                    }
                    col -= 2;

                    //Делаем рокировку
                    if (!onlyCheck)
                    {
                        ChessPiece rook = Board[7, 7];
                        Board[7, 5] = rook;
                        Board[7, 7] = null;
                        rook.col = 5;
                        rook.hasMoved = true;
                    }
                    return true;
                }
                else return false;
            }
            else if (dy == 0 && dx == -2) //Проверяем если рокировка влево. Там может быть только ладья с выключенным флагом hasMoved
            {
                if (!hasMoved && Board[7, 0] != null && !Board[7, 0].hasMoved)
                {
                    for (int c = toC; c < col; c++)
                    {
                        if (Board[row, c] != null) return false;
                    }

                    //Не будет ли Шах
                    col--;
                    if (IsInCheck(Board))
                    {
                        col++;
                        return false;
                    }
                    col--;
                    if (IsInCheck(Board))
                    {
                        col += 2;
                        return false;
                    }
                    col += 2;

                    //Делаем рокировку
                    if (!onlyCheck)
                    {
                        ChessPiece rook = Board[7, 0];
                        Board[7, 3] = rook;
                        Board[7, 0] = null;
                        rook.col = 3;
                        rook.hasMoved = true;
                    }
                    return true;
                }
                else return false;
            }
            else if ((Math.Abs(dx) > 1) || (Math.Abs(dy) > 1)) //Если ход по диагонали более одной клетки то "нельзя" 
                return false;

            return true;
        }

        public override bool IsInCheck(ChessBoard Board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (Board[r, c] != null && Board[r, c].IsWhite)
                    {
                        if (Board[r, c].IsCanMove(Board, row, col, true)) return true;  //Проверяем, если белая фигура сможет пойти на нашу клетку, то будет Шах
                    }
                }
            }
            return false;
        }

    }
}
