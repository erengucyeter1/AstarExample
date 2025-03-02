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
        public Label DisplayLabel { get; private set; }

        public Size LabelSize { get; set; }

        public virtual String Value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
                if(DisplayLabel != null)
                {
                    DisplayLabel.Text = value;
                }
            }
        }

        public int CalculateMannhattanDistance(Man other)
        {
            int result =  Math.Abs(this.Position.X - other.Position.X) + Math.Abs(this.Position.Y - other.Position.Y);
            return result;
        }


        public Man()
        {
        }

        public Label  InitLabel()
        {
            this.DisplayLabel = new Label();
            DisplayLabel.Size = LabelSize;
            DisplayLabel.Text = Value;
            DisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            DisplayLabel.ForeColor = Color.Black;
            DisplayLabel.Font = new Font("Helvetica", 64, FontStyle.Bold);
            DisplayLabel.BackColor = SystemColors.ControlDarkDark;
            
            
            return DisplayLabel;
        }

        public Label InitLabel(Label srcLabel)
        {
            this.DisplayLabel = srcLabel;


            return DisplayLabel;
        }


        public Man Clone(bool includeLabels = false)
        {
            Man temp = new Man();
            temp.Position = this.Position;
            temp.Value = this.Value;
            if(includeLabels && this.DisplayLabel != null)
            {
                temp.InitLabel(this.DisplayLabel);
                
            }
            return temp;
        }
        public Man(Position position,Size size):this()
        {
            
            this.Position = position;
            LabelSize = size;            
         

        }

        public Man(Position position): this()
        {
            this.Position = position;
        }


    }
}
