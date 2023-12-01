using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChess
{
    public class ChessPiece
    {
        public Bitmap Image = null;
        public Rectangle Rect = Rectangle.Empty;

        public int row { get; set; } =  0;       //Строка где сейчас фигура
        public int col { get; set; } =  0;       //Столбец где сейчас фигура
        public bool hasMoved { get; set; } =  false;     //Флаг, ходила ли фигура уже или нет
        public bool IsWhite { get; set; } = false;       //Флаг что белая фигура
        public bool IsBlack { get; set; } = false;       //Флаг что черная фигура
        public bool IsSelected { get; set; } = false;

        public List<int> CanMoves = new List<int>();      //Координаты возможного хода. Формат row*10+col
        public List<int> CanAttackes = new List<int>();   //Координаты возможной атаки. Формат row*10+col

        public virtual bool IsCanMove(ChessBoard Board, int toR, int toC, bool onlyCheck)
        {
            return false;
        }

        public virtual bool IsJump() { return false; }  //Если пешка прыгнула на две клетки
        public virtual void clearIsJump() { }           //Очищаем этот прыжок пешки
        public virtual bool IsInCheck(ChessBoard Board) { return false; }   //Если фигура будет в "шахе"
    }
}
