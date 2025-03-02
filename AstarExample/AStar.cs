using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AstarExample
{
    public class AStar
    {
        public BoardState StartState { get; set; }

        public BoardState GoalState { get; set; }

        private HashSet<BoardState> CloseList { get; set; }
        
        private PriorityQueue<BoardState> OpenList { get; set; }

        private Panel SolutionPathPanel { get; set; }

        GameBoard CurrentBoard;


        public AStar(BoardState startState, BoardState goalState, GameBoard currentBoard, Panel solutionPathPanel)
        {
            this.CurrentBoard = currentBoard;
            this.StartState = startState;
            this.GoalState = goalState;
            this.SolutionPathPanel = solutionPathPanel;


            this.CloseList = new HashSet<BoardState>(new BoardStateEqualityComparer()); // karşılaştırıcıyı override etmek için
            this.OpenList = new PriorityQueue<BoardState>();

        }

        private void ShowState(GameBoard board,BoardState state, string msg= "")
        {
            board.Controls.Clear();

            PanelHelper.CreateMen(board, source: state);

            board.Refresh();
            
        }

        private void shiftLabel(Label label, Label destinationLabel)
        {

            Point destination = destinationLabel.Location;
            destinationLabel.Location = label.Location;
            int x = label.Location.X;
            int y = label.Location.Y;

            while (x != destination.X || y != destination.Y)
            {
                if (x < destination.X)
                {
                    x++;
                }
                else if (x > destination.X)
                {
                    x--;
                }
                if (y < destination.Y)
                {
                    y++;
                }
                else if (y > destination.Y)
                {
                    y--;
                }
                label.Location = new Point(x, y);
                label.Refresh();
            }
        }

        private async Task AnimateLabelShift(GameBoard state, Move act)
        {
            Man srcMan = state.Men[act.Source.X, act.Source.Y];
            Man destMan = state.Men[act.Destination.X, act.Destination.Y];

            Label srcManDisplayLabel = srcMan.DisplayLabel;
            Label destManDisplayLabel = destMan.DisplayLabel;

            int srcX = srcManDisplayLabel.Location.X;
            int srcY = srcManDisplayLabel.Location.Y;

            int destX = destManDisplayLabel.Location.X;
            int destY = destManDisplayLabel.Location.Y;

            destManDisplayLabel.BackColor = Color.PowderBlue;
            state.Refresh();
            await Task.Delay(1000); // Sleep for 1500 ms

            shiftLabel(destManDisplayLabel, srcManDisplayLabel);

            state.Refresh();
            await Task.Delay(500);
            destManDisplayLabel.BackColor = SystemColors.ControlDarkDark;
        }

        public async Task IllustratePath(List<Move> path)
        {
            ShowState(CurrentBoard, StartState, "Path");

            foreach (Move move in path)
            {
                await AnimateLabelShift(CurrentBoard, move);
                CurrentBoard.ExchangeManLocations(move.Source, move.Destination);
            }
        }

        private void ShowStatesInPanel(Panel panel, BoardState state, int step, long elapsedTime)
        {
            panel.Controls.Clear();
            List<BoardState> states = new List<BoardState>();

            while (state != null)
            {
                states.Add(state);
                state = state.Parent;
            }

            states.Reverse();

            Label label1 = new Label();
            label1.Text = $"Step: {step}\nTime: {elapsedTime} ms";
            label1.Font = new Font("Helvetica", 14);
            label1.Location = new Point(15, 15);
            label1.AutoSize = true;
            panel.Controls.Add(label1);

            for (int i = 0; i < states.Count; i++)
            {
                BoardState currState = states[i];
                int Y = (label1.Size.Height + 30 + (175 * i));

                GameBoard temp = new GameBoard(150, 150, 15, Y);
                temp.BackColor = SystemColors.ControlDark;

                Label label = new Label();
                label.Text = $"{i + 1}\n\n\n\n\nF:{currState.F}\nG:{currState.G}\nH:{currState.H}";
                label.Font = new Font("Helvetica", 12);
                label.Location = new Point(165, Y + 5);
                label.AutoSize = true;
                panel.Controls.Add(label);

                Position nextMoveSource = null;
                if (i + 1 != states.Count)
                {
                    nextMoveSource = states[i + 1]?.LastMove?.Destination ?? null;
                }
                PanelHelper.CreateMen(temp, source: currState, 32, 3, nextMoveSource);
                panel.Controls.Add(temp);
                panel.Refresh();
            }
        }



        public List<Move> Solve()
        {
            int step = 0;
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            BoardState CurrentState;
            StartState.CalculateF(GoalState);
            OpenList.Enqueue(StartState);

            var denemeList = new HashSet<BoardState>(new BoardStateEqualityComparer()); // karşılaştırıcıyı override etmek için

            denemeList.Add(StartState);

            while (OpenList.Count > 0)
            {
                CurrentState = OpenList.Dequeue();
                step++;



                if (CurrentState.Equals(GoalState))
                {
                    stopwatch.Stop();

                    ShowStatesInPanel(SolutionPathPanel, CurrentState, step, stopwatch.ElapsedMilliseconds);
                    return CurrentState.GetPath();
                }

                CloseList.Add(CurrentState);

                List<BoardState> PossibleNextStates = CurrentState.GetPossibleMoves();

                for (int i = 0; i < PossibleNextStates.Count; i++)
                {
                    BoardState temp = PossibleNextStates[i];
                    temp.Parent = CurrentState;
                    temp.CalculateF(GoalState);

                    if (CloseList.Contains(temp))
                    {
                        continue;
                    }


                    if (!OpenList.Contains(temp))
                    {
                        OpenList.Enqueue(temp);
                    }
                    else
                    {
                        BoardState existingState = OpenList.Get(temp);
                        if (temp.G < existingState.G)
                        {
                            OpenList.Remove(existingState);
                            OpenList.Enqueue(temp);
                        }
                    }
                }
            }

            stopwatch.Stop();
            MessageBox.Show($"Solution not found\nTime taken: {stopwatch.ElapsedMilliseconds} ms\n Step: "+ step);
            return null;
        }

    }
}

