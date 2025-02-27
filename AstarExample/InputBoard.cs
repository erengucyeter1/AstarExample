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
        private IInputBoardObserver observer;

        public InputBoard(int Height, int Width, int X, int Y) : base(Height, Width, X, Y)
        {

        }

        private void onTextChanged(InputMan sender, EventArgs e)
        {

            if (observer != null)
            {
                observer.OnMenValueChanged(sender.Position, sender.Value);
            }
        }

        

        public void SetObserver(IInputBoardObserver observer)
        {
            this.observer = observer;

            foreach (InputMan man in Men)
            {
                man.DisplayTextBox.TextChanged += (sender, e) => onTextChanged(man, e);
            }
        }
    }
}
