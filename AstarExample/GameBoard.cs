using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public class GameBoard : Panel, IInputBoardObserver
    {
        public Man[,] Men;

        public GameBoard(int Height, int Width, int X, int Y)
        {
            this.Size = new Size(Width, Height);
            this.Location = new System.Drawing.Point(X, Y);
            this.BackColor = SystemColors.ButtonShadow;
        }

        public void OnMenValueChanged(Position position, string value)
        {
            foreach (Man man in Men)
            {
                if (man.Position.Equals(position))
                {
                    man.Value = value;

                }
            }

        }

        public BoardState GetBoardState()
        {


            return  new BoardState(Men,null, null);


        }
    }
}
