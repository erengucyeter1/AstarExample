using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public class InputMan : Man
    {
        public TextBox DisplayTextBox { get; private set; }

        
        public override String  Value
        {
            get
            {
                return _value;
            }

            set
            {
                this._value = value;
                DisplayTextBox.Text = value;
            }
        }

        public InputMan(Position position, Size size):base(position)
        {
            TextBox textbox = new TextBox();
            this.Position = position;
            textbox.Size = size;
            textbox.Text = Value;
            textbox.TextAlign = HorizontalAlignment.Center;
            textbox.ForeColor = Color.Black;
            textbox.Font = new Font("Helvetica", 32, FontStyle.Bold);
            textbox.BackColor = SystemColors.ControlDarkDark;
            this.DisplayTextBox = textbox;



        }
    }
}
