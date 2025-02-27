using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public class Man
    {
        public Position Position { get; set; }

        public String _value = String.Empty;
        public virtual String Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
                DisplayLabel.Text = value;
            }
        }

        public int CalculateMannhattanDistance(Man other)
        {
            return Math.Abs(this.Position.X - other.Position.X) + Math.Abs(this.Position.Y - other.Position.Y);
        }

        public Label DisplayLabel { get; private set; }

        public Man(Position position,Size size)
        {
            Label label = new Label();
            this.Position = position;
            label.Size = size;
            label.Text = Value;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.ForeColor = Color.Black;
            label.Font = new Font("Helvetica", 64, FontStyle.Bold);
            label.BackColor = SystemColors.ControlDarkDark;
            this.DisplayLabel = label;
         

        }

        public Man(Position position)
        {
                this.Position = position;
        }
    }
}
