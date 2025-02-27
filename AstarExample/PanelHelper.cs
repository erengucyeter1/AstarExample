using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AstarExample
{
    internal class PanelHelper
    {

        public PanelHelper()
        {
        }

        private Point FindLocation(int x, int y, int Gap_H, int Gap_V, int SquareLenght)
        {
            int loc_x = ((Gap_H * (x + 1)) + (x * SquareLenght));
            int loc_y = ((Gap_V * (y + 1)) + (y * SquareLenght));
            return new Point(loc_x, loc_y);
        }

 


        public void CreateMen(GameBoard board, int manPerEdge = 3)
        {
            board.Men = new Man[manPerEdge, manPerEdge];
            board.Height = board.Width;
            int Gap_H = board.Size.Width / 40;
            int Gap_V = board.Size.Height / 40;

            int SquareWidth = ((board.Size.Width - ((manPerEdge + 1) * Gap_H)) / manPerEdge);
            int SquareLenght = ((board.Size.Height - ((manPerEdge + 1) * Gap_V)) / manPerEdge);

            for (int i = 0; i < manPerEdge; i++)
            {
                for (int j = 0; j < manPerEdge; j++)
                {
                    Man man = new Man(new Position(i, j), new Size(SquareWidth, SquareLenght));

                    man.DisplayLabel.Location = FindLocation(i, j, Gap_H, Gap_V, SquareLenght);
                    board.Men[i,j] = man;
                    board.Controls.Add(man.DisplayLabel);

                }
            }

        }

        private void CheckInput(InputMan sender, EventArgs e)
        {
            string temp = sender.DisplayTextBox.Text;
            if (temp.Length > 1)
            {
                sender.DisplayTextBox.Text = temp.Substring(1, 1);
                sender.DisplayTextBox.SelectionStart = 1;
            }

            sender.Value = sender.DisplayTextBox.Text;
        }


        public void CreateInputMen(InputBoard board, int manPerEdge = 3)
        {
            board.Men = new Man[manPerEdge, manPerEdge];
            board.Height = board.Width;
            int Gap_H = board.Size.Width / 40;
            int Gap_V = board.Size.Height / 40;
            int SquareWidth = ((board.Size.Width - ((manPerEdge + 1) * Gap_H)) / manPerEdge);
            int SquareLenght = ((board.Size.Height - ((manPerEdge + 1) * Gap_V)) / manPerEdge);

            for (int i = 0; i < manPerEdge; i++)
            {
                for (int j = 0; j < manPerEdge; j++)
                {
                    InputMan man = new InputMan(new Position(i, j), new Size(SquareWidth, SquareLenght));

                    man.DisplayTextBox.TextChanged += (sender, e) => CheckInput(man, e);
                    man.DisplayTextBox.Location = FindLocation(i, j, Gap_H, Gap_V, SquareLenght);
                    board.Men[i, j] = man;
                    board.Controls.Add(man.DisplayTextBox);

                }
            }
        }
    }
}
