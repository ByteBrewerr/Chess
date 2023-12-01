
namespace WinChess
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.maintool = new System.Windows.Forms.ToolStrip();
            this.maintool_newgame = new System.Windows.Forms.ToolStripButton();
            this.cBoard = new WinChess.ControlBoard();
            this.maintool.SuspendLayout();
            this.SuspendLayout();
            // 
            // maintool
            // 
            this.maintool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.maintool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maintool_newgame});
            this.maintool.Location = new System.Drawing.Point(0, 0);
            this.maintool.Name = "maintool";
            this.maintool.Size = new System.Drawing.Size(727, 25);
            this.maintool.TabIndex = 1;
            // 
            // maintool_newgame
            // 
            this.maintool_newgame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.maintool_newgame.Image = ((System.Drawing.Image)(resources.GetObject("maintool_newgame.Image")));
            this.maintool_newgame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.maintool_newgame.Name = "maintool_newgame";
            this.maintool_newgame.Size = new System.Drawing.Size(119, 22);
            this.maintool_newgame.Text = "Начать игру заново";
            this.maintool_newgame.Click += new System.EventHandler(this.maintool_newgame_Click);
            // 
            // cBoard
            // 
            this.cBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBoard.Location = new System.Drawing.Point(0, 25);
            this.cBoard.Margin = new System.Windows.Forms.Padding(0);
            this.cBoard.Name = "cBoard";
            this.cBoard.Size = new System.Drawing.Size(727, 537);
            this.cBoard.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(727, 562);
            this.Controls.Add(this.cBoard);
            this.Controls.Add(this.maintool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinChess";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            this.maintool.ResumeLayout(false);
            this.maintool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlBoard cBoard;
        private System.Windows.Forms.ToolStrip maintool;
        private System.Windows.Forms.ToolStripButton maintool_newgame;
    }
}

