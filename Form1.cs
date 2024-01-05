using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace Caro_PhamNguyenNgocHan
{
    public partial class Form1 : Form
    {
        private string player1Name;
        private string player2Name;

        #region Properties
        cls_NgocHan ChessBoard;
        #endregion
        public Form1()
        {
            InitializeComponent();
            

            ChessBoard = new  cls_NgocHan(pnlChessBoad, txbPlayerName, pctbMark);
            pnlChessBoad.Enabled = false;
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerMarked += ChessBoard_PlayerMarked;

            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;

            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            ChessBoard.DrawChessBoard();

          

        }
        void EndGame()
        {
            tmCoolDown.Stop();
            pnlChessBoad.Enabled = false;
            int scorePlayer1 = int.Parse(lblPlayer1Score.Text);
            int scorePlayer2 = int.Parse(lblPlayer2Score.Text);
            if (ChessBoard.CurrentPlayer == 1)
            {
                scorePlayer1++;
                lblPlayer1Score.Text = scorePlayer1.ToString();
            }
            else
            {
                scorePlayer2++;
                lblPlayer2Score.Text = scorePlayer2.ToString();
            }


            MessageBox.Show("Kết thúc");
        }
       

        void ChessBoard_PlayerMarked(object sender, EventArgs e)
        {
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
        }

        void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            EndGame();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
          
            player1Name = player1TextBox.Text;
            player2Name = player2TextBox.Text;

            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                btnStart.Enabled = true;
                MessageBox.Show("Vui lòng nhập tên của cả hai người chơi.");
                return;
            }
            pnlChessBoad.Enabled = true;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblPlayer1Score.Text = "0";
            lblPlayer2Score.Text = "0";

            player1TextBox.Text = string.Empty;
            player2TextBox.Text = string.Empty;

            foreach (var row in ChessBoard.Matrix)
            {
                foreach (var button in row)
                {
                    button.BackgroundImage = null;
                }
            }
            pnlChessBoad.Enabled = false;

        }


        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(player1Name) || string.IsNullOrEmpty(player2Name))
            {
                btnStart.Enabled = true;
                MessageBox.Show("Nhấn Start để bắt đầu game");
                return;
            }
            pnlChessBoad.Enabled = true;

            foreach (var row in ChessBoard.Matrix)
            {
                foreach (var button in row)
                {
                    button.BackgroundImage = null;
                }
            }
        }

        private void lblPlayer1Score_Click(object sender, EventArgs e)
        {

        }
    }
}
