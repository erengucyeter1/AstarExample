using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public class InputBoard : GameBoard
    {
        private List<IInputBoardObserver> observers;

        public InputBoard(int Height, int Width, int X, int Y) : base(Height, Width, X, Y)
        {
            observers = new List<IInputBoardObserver>();
        }

        private void onTextChanged(InputMan sender, EventArgs e)
        {

            if (observers.Count != 0)
            {
                foreach (IInputBoardObserver observer in observers)
                {
                    observer.OnMenValueChanged(sender.Position, sender.DisplayTextBox.Text);
                }
            }
        }

        

        public void AddObserver(IInputBoardObserver observer)
        {
            this.observers.Add(observer);

            foreach (InputMan man in Men)
            {
                man.DisplayTextBox.TextChanged += (sender, e) => onTextChanged(man, e);
                onTextChanged(man, EventArgs.Empty);

            }


        }
    }
}
