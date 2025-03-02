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

        private static Point FindLocation(int x, int y, int Gap_H, int Gap_V, int SquareLenght)
        {
            int loc_x = ((Gap_H * (x + 1)) + (x * SquareLenght));
            int loc_y = ((Gap_V * (y + 1)) + (y * SquareLenght));
            return new Point(loc_x, loc_y);
        }

 


        public static void CreateMen(GameBoard board, BoardState source = null, int fontSize = 64, int manPerEdge = 3, Position nextMoveSource = null)
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
                    Man man = new Man(new Position(j, i), new Size(SquareWidth, SquareLenght));

                    man.InitLabel().Location = FindLocation(j, i, Gap_H, Gap_V, SquareLenght);
                    man.DisplayLabel.Font = new Font("Arial", fontSize);

                    man.Value = source != null ? source.Men[j, i].Value : "";
                    if(man.Value == "") { man.DisplayLabel.Visible = false; }
                    board.Men[j, i] = man;

                    if (nextMoveSource != null && man.Position.Equals(nextMoveSource))
                    {
                        man.DisplayLabel.BackColor = Color.PowderBlue;
                    }

                    board.Controls.Add(man.DisplayLabel);
                    man.DisplayLabel.BringToFront();

                }
            }

            board.Refresh();

        }

    

        private static void CheckInput(InputMan sender, EventArgs e)
        {
            string temp = sender.DisplayTextBox.Text;
            if (temp.Length > 1)
            {
                sender.DisplayTextBox.Text = temp.Substring(1, 1);
                sender.DisplayTextBox.SelectionStart = 1;
            }

            sender.Value = sender.DisplayTextBox.Text;
        }


        public static void CreateInputMen(InputBoard board, string[] seed = null,int manPerEdge = 3)
        {
            board.Men = new Man[manPerEdge, manPerEdge];
            board.Height = board.Width;
            int Gap_H = board.Size.Width / 40;
            int Gap_V = board.Size.Height / 40;
            int SquareWidth = ((board.Size.Width - ((manPerEdge + 1) * Gap_H)) / manPerEdge);
            int SquareLenght = ((board.Size.Height - ((manPerEdge + 1) * Gap_V)) / manPerEdge);

            int seedIndex = 0;
            for (int i = 0; i < manPerEdge; i++)
            {
                for (int j = 0; j < manPerEdge; j++)
                {
                    InputMan man = new InputMan(new Position(j, i), new Size(SquareWidth, SquareLenght));

                    man.DisplayTextBox.TextChanged += (sender, e) => CheckInput(man, e);
                    man.DisplayTextBox.Location = FindLocation(j,i, Gap_H, Gap_V, SquareLenght);
                    man.Value = seed != null ? seed[seedIndex++] : "";
                    board.Men[j, i] = man;
                    board.Controls.Add(man.DisplayTextBox);

                }
            }
        }

        
    }
}
