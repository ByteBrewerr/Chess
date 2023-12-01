using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinChess
{
    public class ChessBoard
    {
        private ChessPiece[,] board;

        public ChessPiece this[int r, int c]
        {
            get
            {
                return board[r, c];
            }
            set
            {
                board[r, c] = value;
            }
        }

        public bool whiteToMove = false;

        public bool whiteInCheck = false;
        public bool blackInCheck = false;

        internal void Initialize()
        {
            board = new ChessPiece[8, 8];
            whiteToMove = true;
            whiteInCheck = false;
            blackInCheck = false;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    board[r,c] = null;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                board[1,i] = new Pieces.WhitePawn(1, i);
                board[6,i] = new Pieces.BlackPawn(6, i);
            }

            board[0,0] = new Pieces.WhiteRook(0, 0);
            board[0,1] = new Pieces.WhiteKnight(0, 1);
            board[0,2] = new Pieces.WhiteBishop(0, 2);
            board[0,3] = new Pieces.WhiteQueen(0, 3);
            board[0,4] = new Pieces.WhiteKing(0, 4);
            board[0,5] = new Pieces.WhiteBishop(0, 5);
            board[0,6] = new Pieces.WhiteKnight(0, 6);
            board[0,7] = new Pieces.WhiteRook(0, 7);
            board[7,0] = new Pieces.BlackRook(7, 0);
            board[7,1] = new Pieces.BlackKnight(7, 1);
            board[7,2] = new Pieces.BlackBishop(7, 2);
            board[7,3] = new Pieces.BlackQueen(7, 3);
            board[7,4] = new Pieces.BlackKing(7, 4);
            board[7,5] = new Pieces.BlackBishop(7, 5);
            board[7,6] = new Pieces.BlackKnight(7, 6);
            board[7,7] = new Pieces.BlackRook(7, 7);
        }

        internal ChessPiece GetSelected()
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (board[r, c] != null && board[r, c].IsSelected)
                        return board[r, c];
            return null;
        }

        internal bool SetSelected(Point p)
        {
            ClearSelected();
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (board[r, c] != null && board[r, c].Rect.Contains(p))
                    {
                        if ((whiteToMove && board[r, c].IsWhite) || (!whiteToMove && board[r, c].IsBlack))
                        {
                            board[r, c].IsSelected = true;
                            return true;
                        }
                        return false;
                    }
            return false;
        }

        internal void ClearSelected()
        {
            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (board[r, c] != null)
                        board[r, c].IsSelected = false;
        }

        internal void CalculateMoves(ChessPiece piece)
        {
            piece.CanMoves.Clear();
            piece.CanAttackes.Clear();

            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                {
                    if (IsCanMove(piece.row, piece.col, r, c, true)) //Вызываем метод проверки хода, с включенным флагом onlyCheck
                    {
                        if (board[r, c] == null)
                            piece.CanMoves.Add(r * 10 + c);
                        else
                            piece.CanAttackes.Add(r * 10 + c);
                    }
                }
        }

        public bool IsCanMove(int fromR, int fromC, int toR, int toC, bool onlyCheck)
        {
            if (fromR == toR && fromC == toC) return false;

            if (!board[fromR, fromC].IsCanMove(this, toR, toC, onlyCheck)) return false;

            
            

            ChessPiece whiteKing = null;
            ChessPiece blackKing = null;
            ChessPiece temp = board[toR, toC];

            //Получим королей
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c] is Pieces.WhiteKing) whiteKing = board[r, c];
                    if (board[r, c] is Pieces.BlackKing) blackKing = board[r, c];
                }
            }

            //Проверяем, если король уже под Шахом и после этого хода он также будет под шахом, то ходить нельзя. Сделали ход, который не убирает короля из под шаха
            if (whiteKing.IsInCheck(this))
            {
                movePiece(fromR, fromC, toR, toC);
                if (whiteKing.IsInCheck(this))
                {
                    movePiece(toR, toC, fromR, fromC);
                    board[toR, toC] = temp;
                    return false;
                }
                movePiece(toR, toC, fromR, fromC);
                board[toR, toC] = temp;
            }

            if (blackKing.IsInCheck(this))
            {
                movePiece(fromR, fromC, toR, toC);
                if (blackKing.IsInCheck(this))
                {
                    movePiece(toR, toC, fromR, fromC);
                    board[toR, toC] = temp;
                    return false;
                }
                movePiece(toR, toC, fromR, fromC);
                board[toR, toC] = temp;
            }

            //Проверяем, что после хода, король окажется под Шахом. Сделали ход, который подставляет своего короля под шах
            movePiece(fromR, fromC, toR, toC);
            if (whiteToMove && whiteKing.IsInCheck(this))
            {
                movePiece(toR, toC, fromR, fromC);
                board[toR, toC] = temp;
                return false;
            }
            if (!whiteToMove && blackKing.IsInCheck(this))
            {
                movePiece(toR, toC, fromR, fromC);
                board[toR, toC] = temp;
                return false;
            }

            if (onlyCheck)
            {
                movePiece(toR, toC, fromR, fromC);
                board[toR, toC] = temp;
                return true;
            }

            //Выставляем флаг шах
            if (whiteKing.IsInCheck(this))
            {
                whiteInCheck = true;
            }
            else whiteInCheck = false;
            if (blackKing.IsInCheck(this))
            {
                blackInCheck = true;
            }
            else blackInCheck = false;

            movePiece(toR, toC, fromR, fromC);
            board[toR, toC] = temp;

            whiteToMove = !whiteToMove;
            return true;
        }

        internal void movePiece(int fromR, int fromC, int toR, int toC)
        {
            ChessPiece aPiece = board[fromR, fromC];
            aPiece.row = toR;
            aPiece.col = toC;
            board[toR, toC] = aPiece;
            board[fromR, fromC] = null;
        }

        internal bool IsCheckmate()     //Получаем все возможные ходы
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c] == null) continue;
                    if (whiteToMove && board[r, c].IsBlack) continue;
                    if (!whiteToMove && board[r, c].IsWhite) continue;
                    int[] possMoves = GetPossibleMoves(board[r, c]);
                    if (possMoves.Length > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int[] GetPossibleMoves(ChessPiece piece)
        {
            List<int> result = new();

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (IsCanMove(piece.row, piece.col, r, c, true))
                    {
                        result.Add((10 * r) + c);
                    }
                }
            }
            return result.ToArray();
        }

        internal void ClearIsJump() //Очищаем флаг прыжка пешки
        {
            //Если текущий ход белых или черных, то уже был ход противоположной стороны и флаг нужно очистить даже не воспользовались
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c] == null) continue;
                    if (whiteToMove)
                    {
                        if (board[r, c].IsWhite) board[r, c].clearIsJump();
                    }
                    else
                    {
                        if (board[r, c].IsBlack) board[r, c].clearIsJump();
                    }
                }
            }
        }

        //Проверяем если пешка дошла до границы, то позволяем выбрать фигуру
        internal bool IsPawnTransformation(int fromR, int fromC, int toR, int toC) 
        {
            if (board[fromR, fromC] == null) return false;
            if (board[fromR, fromC] is Pieces.BlackPawn)
            {
                if (toR == 0) return true;
            }
            if (board[fromR, fromC] is Pieces.WhitePawn)
            {
                if (toR == 7) return true;
            }
            return false;
        }
    }
}
