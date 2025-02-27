using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public partial class Form1 : Form
    {

        GameBoard gameBoard;
        InputBoard startBoard;
        InputBoard goalBoard;
        public Form1()
        {
            InitializeComponent();


            // mainGameboard

             gameBoard = new GameBoard(500, 500, 15, 15);
             startBoard = new InputBoard(200, 200, 600, 15);
             goalBoard = new InputBoard(200, 200, 600, 300);


            this.mainPanel.Controls.Add(gameBoard);
            this.mainPanel.Controls.Add(startBoard);
            this.mainPanel.Controls.Add(goalBoard);


            PanelHelper panelHelper = new PanelHelper();

            panelHelper.CreateMen(gameBoard);
            panelHelper.CreateInputMen(startBoard);
            panelHelper.CreateInputMen(goalBoard);


            startBoard.SetObserver(gameBoard);



        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void shiftLabel(Label label, Label destinationLabel)
        {
            
            Point destination = destinationLabel.Location;
            destinationLabel.Location = label.Location;
            int x = label.Location.X;
            int y = label.Location.Y;

            while (x != destination.X || y != destination.Y)
            {
                if (x < destination.X)
                {
                    x++;
                }
                else if (x > destination.X)
                {
                    x--;
                }
                if (y < destination.Y)
                {
                    y++;
                }
                else if (y > destination.Y)
                {
                    y--;
                }
                label.Location = new Point(x, y);
                label.Refresh();
                System.Threading.Thread.Sleep(5); // Sleep for 50 ms
            }
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
        }

        

        private void btnStart_Click(object sender, EventArgs e)
        {
         

            BoardState startState = gameBoard.GetBoardState();
            BoardState goalState = goalBoard.GetBoardState();


            // başlangıç ve hedef durumları aynı elemanları içermeli
            if(!startState.HasSameElements(goalState))
            {
                MessageBox.Show("Start and Goal states have to include same elements!");
                return;
            }

            AStar aStar = new AStar(startState, goalState);

            aStar.Solve();

        }
    }
}
