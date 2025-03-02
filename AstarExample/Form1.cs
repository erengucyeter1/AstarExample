using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AstarExample
{
    public partial class Form1 : Form,  IInputBoardObserver
    {

        GameBoard gameBoard;
        InputBoard startBoard;
        InputBoard goalBoard;
        AStar aStar;
        List<Move> CurrentProblemsPath;
        Button btnIllustrate;
        Button btnStart;



        public Form1()
        {
            InitializeComponent();



            // solution panel

            SolutuionPathPanel.AutoScroll = true;
            SolutuionPathPanel.VerticalScroll.Visible = true;
            SolutuionPathPanel.VerticalScroll.Enabled = true;
            SolutuionPathPanel.VerticalScroll.Minimum = 0;
            SolutuionPathPanel.VerticalScroll.Maximum = 1000;
     

            // mainGameboard



            gameBoard = new GameBoard(500, 500, 15, 15);
             startBoard = new InputBoard(200, 200, 530, 35);
             goalBoard = new InputBoard(200, 200, 530, 260);

            // start button
            btnStart = new Button();
            btnStart.Text = "Start";
            btnStart.Size = new Size(95, 40);
            btnStart.Location = new Point(530, 475);
            btnStart.Click += new EventHandler(btnStart_Click);
            this.mainPanel.Controls.Add(btnStart);

            // btn illustrate

            btnIllustrate = new Button();
            btnIllustrate.Text = "Illustrate";
            btnIllustrate.Size = new Size(95, 40);
            btnIllustrate.Location = new Point(635, 475);
            btnIllustrate.Click += new EventHandler(btnIllustrate_Click);
            btnIllustrate.Enabled = false;
            this.mainPanel.Controls.Add(btnIllustrate);

            // start state label

            Label startLabel = new Label();
            startLabel.Text = "Start State";
            startLabel.Location = new Point(530, 15);
            startLabel.Size = new Size(100, 20);
            this.mainPanel.Controls.Add(startLabel);

            // goal state label

            Label goalLabel = new Label();
            goalLabel.Text = "Goal State";
            goalLabel.Location = new Point(530, 240);
            goalLabel.Size = new Size(100, 20);
            this.mainPanel.Controls.Add(goalLabel);

            this.mainPanel.Controls.Add(gameBoard);
            this.mainPanel.Controls.Add(startBoard);
            this.mainPanel.Controls.Add(goalBoard);

            PanelHelper.CreateMen(gameBoard);

            string[] startBoardSeed = { "8", "7", "6", "1", "", "5", "2","3","4"};
            string[] goalBoardSeed = { "1", "2", "3", "8", "", "4", "7",  "6" ,"5"};

            PanelHelper.CreateInputMen(startBoard, startBoardSeed);
            PanelHelper.CreateInputMen(goalBoard, goalBoardSeed);

            startBoard.AddObserver(gameBoard);
            startBoard.AddObserver(this);
            goalBoard.AddObserver(this);



        }


        private  void btnStart_Click(object sender, EventArgs e)
        {
            BoardState startState = startBoard.GetBoardState();
            BoardState goalState = goalBoard.GetBoardState();

            // başlangıç ve hedef durumları aynı elemanları içermeli
            if (!startState.HasSameElements(goalState))
            {
                MessageBox.Show("The Start and Goal states must include the same elements!");
                return;
            }
            if (!startState.IsIncludeKey(""))
            {
                MessageBox.Show("Both the start state and goal state must contain an empty tile");
                return;
            }

            aStar = new AStar(startState, goalState, gameBoard, SolutuionPathPanel);

            List<Move> path = aStar.Solve();

            if (path == null)
            {
                MessageBox.Show("No solution found!");
                btnIllustrate.Enabled = false;
                return;
            }
            else
            {
                btnIllustrate.Enabled = true;
                btnStart.Enabled = false;
                this.CurrentProblemsPath = path;
            }
        }


        private async void btnIllustrate_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null; 

            btnIllustrate.Enabled = false;
            btnIllustrate.Text = "Illustrating...";
            btnStart.Enabled = false;

            await aStar.IllustratePath(CurrentProblemsPath);

            btnIllustrate.Enabled = true;
            btnIllustrate.Text = "Illustrate";
        }

        public void OnMenValueChanged(Position position, String value)
        {
            this.btnIllustrate.Enabled = false;
            this.btnStart.Enabled = true;
        }



        private string getMovePath(List<Move> path)
        {
            string result = "";
            int counter = 0;
            foreach (Move move in path)
            {
                result += "- "+ counter + " "+move.ToString() + "\n";
            }
            return result;
        }

      
        
       
        
    }
}
