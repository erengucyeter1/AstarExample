using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

       

        public BoardState Clone()
        {
            Man[,] clonedMen = new Man[Men.GetLength(0), Men.GetLength(1)];
            for (int i = 0; i < Men.GetLength(0); i++)
            {
                for (int j = 0; j < Men.GetLength(1); j++)
                {
                    clonedMen[i, j] = Men[i, j].Clone(includeLabels: true);
                }
            }
            return new BoardState(clonedMen, this.Parent, this.LastMove)
            {
                F = this.F,
                G = this.G,
                H = this.H
            };
        }
        public BoardState ExchangeManLocations(Position source, Position destination)
        {
            BoardState newState = this.Clone();

            int sourceX = source.X;
            int sourceY = source.Y;
            int destinationX = destination.X;
            int destinationY = destination.Y;

            Man tempMan = newState.Men[sourceX, sourceY].Clone();

            newState.Men[sourceX, sourceY] = newState.Men[destinationX, destinationY].Clone();
            newState.Men[sourceX, sourceY].Position = new Position(sourceX, sourceY);


            newState.Men[destinationX, destinationY] = tempMan;
            newState.Men[destinationX, destinationY].Position = new Position(destinationX, destinationY);

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
                possibleMoves.Add(this.Clone().ExchangeManLocations(movableMan.Position, up).SetLastMove(new Move(movableMan.Position, up)));
            }
            if (down.Y < Men.GetLength(1))
            {
                possibleMoves.Add(this.Clone().ExchangeManLocations(movableMan.Position, down).SetLastMove(new Move(movableMan.Position, down)));
            }
            if (left.X >= 0)
            {
                possibleMoves.Add(this.Clone().ExchangeManLocations(movableMan.Position, left).SetLastMove(new Move(movableMan.Position, left)));
            }
            if (right.X < Men.GetLength(0))
            {
                possibleMoves.Add(this.Clone().ExchangeManLocations(movableMan.Position, right).SetLastMove(new Move(movableMan.Position, right)));
            }

            return possibleMoves;
        }

        public int CalculateF(BoardState goalState)
        {

            return F = CalculateG() + CalculateH(goalState);
        }

        public int CalculateG()
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
                if (currentMan.Value == "") { continue; }
                Man goalMan = goalState.GetMan(currentMan.Value);
                h += currentMan.CalculateMannhattanDistance(goalMan);
            }

            H = h;
            return h;

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

        public List<Move> GetPath()
        {
            List<Move> path = new List<Move>();
            BoardState temp = this;
            while (temp.Parent != null)
            {
                path.Add(temp.LastMove);
                temp = temp.Parent;
            }
            path.Reverse();
            return path;
        }



        public int CompareTo(BoardState other)
        {
            if (other == null) return 1;

            int compareF = this.F.CompareTo(other.F);
            if (compareF != 0) return compareF;

            return this.H.CompareTo(other.H); // F eşitse H küçük olanı seç
        }
      

        public override bool Equals(object obj)
        {

            BoardState other = obj as BoardState;
            if (this.GetHashCode().Equals(other.GetHashCode()))
            {
                return true;
            }

            return false;
        }


        public bool HasSameElements(BoardState other)
        {
            HashSet<string> set = new HashSet<string>();
            HashSet<string> otherSet = new HashSet<string>();

            foreach (Man temp in this.Men) { set.Add(temp.Value); }
            foreach (Man temp in other.Men) { otherSet.Add(temp.Value); }

            return set.SetEquals(otherSet);
        }

        public bool IsIncludeKey(string key)
        {
            for (int i = 0; i < this.Men.GetLength(0);i++){
                for (int j = 0; j < this.Men.GetLength(1); j++)
                {
                    if(Men[i, j].Value == key)
                    {
                        return true;
                    }
                }
            }

            return false;   
        }

        public override int GetHashCode()
        {
            if (this == null)
                return 0;

            int hash = 17;
            for (int i = 0; i < this.Men.GetLength(0); i++)
            {
                for (int j = 0; j < this.Men.GetLength(1); j++)
                {

                    hash = hash * 31 + (this.Men[j, i]?.Value?.GetHashCode() ?? 0);
                    hash = hash * 31 + (this.Men[j, i]?.Position?.GetHashCode() ?? 0);

                }
            }
            return hash;
        }


        public GameBoard Illustrate()
        {
            GameBoard temp = new GameBoard(160, 160, 0, 0);
            temp.BackColor = System.Drawing.Color.Pink;
            PanelHelper.CreateMen(temp, this, 8);
            return temp;

        }


        public override string ToString()
        {
            int row = this.Men.GetLength(0);
            int column = this.Men.GetLength(1);

            StringBuilder sb = new StringBuilder(row*column);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    sb.Append(this.Men[j,i].Value + ",");

                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();



        }
    }
}
