using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinChess
{
    public partial class ControlBoard : Panel
    {
        public ControlBoard()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private int C_WIDTH = consts.gridSize * 8 + (consts.gridSize / 2) * 3;            //8 клеток + 3 половины клетки    |25|25|50|50|50|50|50|50|50|50|25|
        private int C_HEIGHT = consts.gridSize * 8 + (consts.gridSize / 2) * 3;

        private Bitmap MainBitmap = null;

        private int initialX = consts.gridSize; 
        private int initialY = consts.gridSize / 2;

        private Brush BrushWhite = new SolidBrush(Color.White);
        private Brush BrushBlack = new SolidBrush(Color.FromArgb(179, 86, 5));

        private Pen PenSelected = new Pen(Color.Blue, 3);
        private Pen PenMoves = new Pen(Color.Green, 3);
        private Pen PenAttackes = new Pen(Color.Red, 3);

        private Font FontText = new Font("Arial", 10f, FontStyle.Bold);

        public ChessBoard Board = new ChessBoard();

        internal void Initialize()
        {
            this.Width = C_WIDTH;
            this.Height = C_HEIGHT;
            this.Left = this.Parent.ClientSize.Width / 2 - this.Width / 2;
            this.Top = this.Parent.ClientSize.Height / 2 - this.Height / 2;

            Board.Initialize();
        }

        internal void UpdateData()
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            try
            {
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                bmp.SetResolution(96f, 96f);
                using Graphics gr = Graphics.FromImage(bmp);
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                //Пишем кто ходит
                string text = Board.whiteToMove ? "Ход белых" : "Ход черных";
                Size size = _size(gr.MeasureString(text, FontText));
                gr.DrawString(text, FontText, Brushes.DarkBlue, this.Width / 2 - size.Width / 2, initialY / 2 - size.Height / 2);

                //Пишем у кого Шах
                text = "";
                if (Board.whiteInCheck) text = "Шах белому королю!";
                if (Board.blackInCheck) text = "Шах черному королю!";
                if (text.Length > 0)
                {
                    size = _size(gr.MeasureString(text, FontText));
                    gr.DrawString(text, FontText, Brushes.Red, 10, initialY / 2 - size.Height / 2);
                }

                //Рисуем доску
                int tempX = initialX;
                int tempY = initialY;
                for (int r = 0; r < 8; r++)
                {
                    tempX = initialX;
                    for (int c = 0; c < 8; c++)
                    {
                        if ((r + c) % 2 == 1)
                            gr.FillRectangle(BrushBlack, tempX, tempY, consts.gridSize, consts.gridSize);
                        else
                            gr.FillRectangle(BrushWhite, tempX, tempY, consts.gridSize, consts.gridSize);
                        tempX += consts.gridSize;
                    }
                    tempY += consts.gridSize;
                }



                //Рисуем фигуры
                for (int r = 0; r < 8; r++)
                {
                    for (int c = 0; c < 8; c++)
                    {
                        if (Board[r, c] == null) continue;
                        int x = (c * consts.gridSize) + initialX;
                        int y = consts.gridSize * 7 - (r * consts.gridSize) + initialY;  //Мы должны учитывать, что координаты на форме идут сверху вниз и нам нужно инверсировать координаты по вертикали
                        Board[r, c].Rect = new Rectangle(x, y, consts.gridSize, consts.gridSize);  //Задаем область расположения на доске
                        gr.DrawImage(Board[r, c].Image, x, y);
                    }
                }

                //Отрисовываем выбранную фигуру и возможные ходы
                {
                    ChessPiece piece = Board.GetSelected();
                    if (piece != null)
                    {
                        foreach (var rc in piece.CanMoves)               //Куда можно ходить
                        {
                            int r = rc / 10;
                            int c = rc - r * 10;
                            int x = (c * consts.gridSize) + initialX;
                            int y = consts.gridSize * 7 - (r * consts.gridSize) + initialY;  //Мы должны учитывать, что координаты на форме идут сверху вниз и нам нужно инверсировать координаты по вертикали
                            gr.DrawEllipse(PenMoves, x, y, consts.gridSize, consts.gridSize);
                        }

                        foreach (var rc in piece.CanAttackes)           //Куда будем атаковать
                        {
                            int r = rc / 10;
                            int c = rc - r * 10;
                            int x = (c * consts.gridSize) + initialX;
                            int y = consts.gridSize * 7 - (r * consts.gridSize) + initialY;  //Мы должны учитывать, что координаты на форме идут сверху вниз и нам нужно инверсировать координаты по вертикали
                            gr.DrawEllipse(PenAttackes, x, y, consts.gridSize, consts.gridSize);
                        }

                        gr.DrawEllipse(PenSelected, piece.Rect);
                    }
                }
                

                if (MainBitmap != null) MainBitmap.Dispose();
                MainBitmap = bmp;
            }
            catch { }
            finally { this.Refresh(); }
        }

        private Size _size(SizeF sizeF)
        {
            return new Size((int)sizeF.Width, (int)sizeF.Height);
        }

        private void ControlBoard_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Color.WhiteSmoke);

                if (MainBitmap != null)
                {
                    e.Graphics.DrawImage(MainBitmap, 0, 0);
                }
            }
            catch { }
        }

        private void ControlBoard_MouseUp(object sender, MouseEventArgs e)
        {
            ChessPiece piece = Board.GetSelected();
            if (piece == null) //Если выбранной фигуры нет, то проверяем координаты и выбираем фигуру
            {
                if (Board.SetSelected(e.Location))
                {
                    piece = Board.GetSelected();
                    Board.CalculateMoves(piece);
                }
            }
            else
            {
                if (Board.SetSelected(e.Location))
                {
                    piece = Board.GetSelected();
                    Board.CalculateMoves(piece);
                }
                else
                {
                    int toC = (e.Location.X - initialX) / consts.gridSize;
                    int toR = 7 - ((e.Location.Y - initialY) / 50);
                    MoveTo(piece, toR, toC);
                }
            }

            UpdateData();
        }

        private void MoveTo(ChessPiece piece, int toR, int toC)
        {
            if (Board.IsCanMove(piece.row, piece.col, toR, toC, false))               //Делаем ход с выключенным флагом
            {
                Board.movePiece(piece.row, piece.col, toR, toC);
                piece.hasMoved = true;

                Board.ClearIsJump();

                Board.ClearSelected();
                UpdateData();

                timer_check.Start();
            }
            UpdateData();
        }

        internal void _KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Board.ClearSelected();
                UpdateData();
            }
        }

        private void timer_check_Tick(object sender, EventArgs e)
        {
            timer_check.Stop();

            if (Board.IsCheckmate()) //Проверяем, что уже некуда ходить
            {
                if (Board.whiteInCheck || Board.blackInCheck)   //Если перед этим был шах, то получаем мат
                {
                    if (Board.whiteToMove)
                    {
                        MessageBox.Show("Белым поставили Мат!", "WinChess", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Черным поставили Мат!", "WinChess", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (!Board.whiteInCheck && !Board.blackInCheck) //иначе пат
                {
                    MessageBox.Show("В игре Пат!", "WinChess", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
