using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public class AStar
    {
        public BoardState StartState { get; set; }

        public BoardState GoalState { get; set; }

        private HashSet<BoardState> CloseList { get; set; }
        private PriorityQueue<BoardState> OpenList { get; set; }



        public AStar(BoardState startState, BoardState goalState)
        {
            this.StartState = startState;
            this.GoalState = goalState;

            this.CloseList = new HashSet<BoardState>();
           this.OpenList = new PriorityQueue<BoardState>();

        }



        public List<Move> Solve()
        {
            StartState.CalculateF(GoalState);
            OpenList.Enqueue(StartState);

            while (OpenList.Count > 0)
            {
                BoardState currentState = OpenList.Dequeue();

                CloseList.Add(currentState);

                foreach(BoardState temp in currentState.GetPossibleMoves())
                {
                    temp.CalculateF(GoalState);
                    if (OpenList.Contains(temp))
                    {
                        BoardState oldVersion = OpenList.Get(temp);

                        if (temp.F < oldVersion.F)
                        {
                            OpenList.Remove(oldVersion);
                            temp.Parent = currentState;
                            temp.CalculateF(GoalState);
                            OpenList.Enqueue(temp);
                        }
                    }
                    else
                    {
                        temp.Parent = currentState;
                        OpenList.Enqueue(temp);
                    }
                }

                if (currentState.Equals(GoalState))
                {
                    List<Move> moves = new List<Move>();

                    while (currentState.Parent != null)
                    {
                        moves.Add(currentState.LastMove);
                        currentState = currentState.Parent;
                    }

                    moves.Reverse();
                    return moves;
                }


            }

            return null;

        }
    }
}

