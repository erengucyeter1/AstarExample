using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarExample
{
    public class BoardState : IComparable<BoardState>
    {
        public Man[,] Men { get; set; }
        public int F { get; set; }  // A* için f(n) = g(n) + h(n)
        public int G { get; set; }
        public int H { get; set; }

        public BoardState Parent { get; set; }
        public Move LastMove { get; set; }

        public BoardState(Man[,] board, BoardState parent, Move lastMove)
        {
            Men = board;
            Parent = parent;
            LastMove = lastMove;
        }

        public BoardState(BoardState source)
        {
            Men = (Man[,])source.Men.Clone();
            F = source.F;
            G = source.G;
            H = source.H;
            Parent = source.Parent;
            LastMove = source.LastMove;
        }

        private BoardState Clone()
        {
            return new BoardState (this);
        }
        private BoardState ExchangeManLocations(Position source, Position destination)
        {
            BoardState newState = this.Clone();

            int sourceX = source.X - 1;
            int sourceY = source.Y - 1;
            int destinationX = destination.X -1;
            int destinationY = destination.Y - 1;

            Man tempMan;
            tempMan =  newState.Men[sourceX, sourceY];

            newState.Men[sourceX, sourceY] = newState.Men[destinationX, destinationY];
            newState.Men[sourceX, sourceY].Position = destination;

            newState.Men[destinationX, destinationY] = tempMan;
            newState.Men[destinationX, destinationY].Position = source;

            return newState;
        }
        public BoardState SetLastMove(Move move)
        {
            this.LastMove = move;
            return this;    
        }
        public List<BoardState> GetPossibleMoves(String moveableKey = "")
        {
            List<BoardState> possibleMoves = new List<BoardState>();

            Man movableMan = this.GetMan(moveableKey);

            Position up = new Position(movableMan.Position.X, movableMan.Position.Y - 1);
            Position down = new Position(movableMan.Position.X, movableMan.Position.Y + 1);
            Position left = new Position(movableMan.Position.X - 1, movableMan.Position.Y);
            Position right = new Position(movableMan.Position.X + 1, movableMan.Position.Y);

            if (up.Y >= 0)
            {
                possibleMoves.Add(this.ExchangeManLocations(movableMan.Position, up).SetLastMove(new Move(movableMan.Position, up)));

            }
            if (down.Y < Men.GetLength(1))
            {
                possibleMoves.Add(this.ExchangeManLocations(movableMan.Position, down).SetLastMove(new Move(movableMan.Position, down)));
            }
            if (left.X >= 0)
            {
                possibleMoves.Add(this.ExchangeManLocations(movableMan.Position, left).SetLastMove(new Move(movableMan.Position, left)));
            }
            if (right.X <= Men.GetLength(1))
            {
                possibleMoves.Add(this.ExchangeManLocations(movableMan.Position, right).SetLastMove(new Move(movableMan.Position, right)));

            }

            return possibleMoves;   

        }
        public int CalculateF(BoardState goalState)
        {
            return F = CalculateG() + CalculateH(goalState);
        }

        public int  CalculateG()
        {
            int g = 0;
            BoardState temp = this;
            while (temp.Parent != null)
            {
                g++;
                temp = temp.Parent;
            }
            G = g;
            return g;
        }

        public int CalculateH(BoardState goalState)
        {
            int h = 0;

            foreach (Man currentMan in this.Men)
            {
                Man goalMan = goalState.GetMan(currentMan.Value);
                H += currentMan.CalculateMannhattanDistance(goalMan);
            }

            H = h;
            return h;

        }

        

        public Man GetMan(string value)
        {
            foreach (Man currentMan in this.Men)
            {
                if (currentMan.Value == value)
                {
                    return currentMan;
                }
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is BoardState other)
            {
                if (this.GetHashCode() != other.GetHashCode())
                {
                    return false;
                }
            }
            return false;
        }

        public int CompareTo(BoardState other)
        {
            return F.CompareTo(other.F);
        }

        public bool HasSameElements(BoardState other)
        {
            HashSet<string> set = new HashSet<string>();
            HashSet<string> otherSet = new HashSet<string>();

            foreach (Man temp in this.Men) { set.Add(temp.Value); }
            foreach (Man temp in other.Men) { otherSet.Add(temp.Value); }

            return set.SetEquals(otherSet);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var man in Men)
            {
                hash = hash * 31 + (man?.Value?.GetHashCode() ?? 0);
                hash = hash * 31 + man?.Position.GetHashCode() ?? 0;
            }
            return hash;
        }
    }
}
