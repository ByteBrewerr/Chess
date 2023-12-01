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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.ClientSize = new Size(consts.gridSize * 10, consts.gridSize * 10 + consts.gridSize / 2);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            cBoard.Initialize();

            cBoard.UpdateData();
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            cBoard._KeyUp(e);
        }

        private void maintool_newgame_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы хотите начать игру заново?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                cBoard.Initialize();

                cBoard.UpdateData();
            }
        }
    }
}
