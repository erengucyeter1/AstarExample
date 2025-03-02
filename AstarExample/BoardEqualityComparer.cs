using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace AstarExample
{
    public class BoardStateEqualityComparer : IEqualityComparer<BoardState>
    {
        public bool Equals(BoardState x, BoardState y)
        {
            if (x == null || y == null)
                return false;

            // Compare Men arrays
            for (int i = 0; i < x.Men.GetLength(0); i++)
            {
                for (int j = 0; j < x.Men.GetLength(1); j++)
                {
                    if (!x.Men[i, j].Value.Equals(y.Men[i, j].Value) || !x.Men[i, j].Position.Equals(y.Men[i, j].Position))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /*
        public int GetHashCode(BoardState obj)
        {
            if (obj == null)
                return 0;

            int hash = 17;
            for (int i = 0; i < obj.Men.GetLength(0); i++)
            {
                for (int j = 0; j < obj.Men.GetLength(1); j++)
                {
                    
                        hash = hash * 31 + (obj.Men[j,i].Value?.GetHashCode() ?? 0);
                        hash = hash * 31 + (obj.Men[j, i].Position?.GetHashCode() ?? 0);
                    
                }
            }
            return hash;
        }*/

        public int GetHashCode(BoardState obj)
        {
            if (obj == null)
                return 0;

            int hash = 17;

            // Matrisin her bir elemanının Value'sunu al ve hash'e ekle
            foreach (var man in obj.Men)
            {
                hash = hash * 31 + (man?.Value?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }
}
