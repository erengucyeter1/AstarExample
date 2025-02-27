using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarExample
{
    public class Move
    {
        public Position Source { get; set; }
        public Position Destination { get; set; }
        public Move(Position Source, Position Destination)
        {
             this.Destination = Destination;
            this.Source = Source;
        }
    }
}
