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

                    if (value.Equals(""))
                    {
                        man.DisplayLabel.Visible = false;
                    }
                    else
                    {
                        man.DisplayLabel.Visible = true;
                    }
                }
            }

        }

        public void ExchangeManLocations(Position source, Position destination)
        {
            int sourceX = source.X;
            int sourceY = source.Y;
            int destinationX = destination.X;
            int destinationY = destination.Y;

            Man tempMan = Men[sourceX, sourceY].Clone(true);

            Men[sourceX, sourceY] = Men[destinationX, destinationY].Clone(true);
            Men[sourceX, sourceY].Position = new Position(sourceX, sourceY);


            Men[destinationX, destinationY] = tempMan;
            Men[destinationX, destinationY].Position = new Position(destinationX, destinationY);
        }

        public BoardState GetBoardState()
        {


            return  new BoardState(Men,null, null);


        }
        public Man GetMan(string value)
        {
            foreach (Man man in this.Men)
            {
                if (man.Value.Equals(value))
                {
                    return man;
                }
            }
            return null;
        }

       
    }


}
