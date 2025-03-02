namespace AstarExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SolutuionPathPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.mainPanel.Location = new System.Drawing.Point(14, 15);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1118, 815);
            this.mainPanel.TabIndex = 0;
            // 
            // SolutuionPathPanel
            // 
            this.SolutuionPathPanel.BackColor = System.Drawing.Color.MintCream;
            this.SolutuionPathPanel.Location = new System.Drawing.Point(1151, 15);
            this.SolutuionPathPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SolutuionPathPanel.Name = "SolutuionPathPanel";
            this.SolutuionPathPanel.Size = new System.Drawing.Size(345, 815);
            this.SolutuionPathPanel.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1497, 845);
            this.Controls.Add(this.SolutuionPathPanel);
            this.Controls.Add(this.mainPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel SolutuionPathPanel;
    }
}

