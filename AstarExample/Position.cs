using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarExample
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            var other = obj as Position;
            if (other == null)
            {
                return false;
            }
            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            
            
            int hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();
            return hash;
            
        }

    }
}
