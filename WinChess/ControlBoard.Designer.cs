
namespace WinChess
{
    partial class ControlBoard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer_check = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer_check
            // 
            this.timer_check.Tick += new System.EventHandler(this.timer_check_Tick);
            // 
            // ControlBoard
            // 
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ControlBoard_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlBoard_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer_check;
    }
}
