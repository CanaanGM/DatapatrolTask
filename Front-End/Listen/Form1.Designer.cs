namespace Listen
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewItem listViewItem1 = new ListViewItem("");
            apiLbl = new Label();
            BaseUrlTextBox = new TextBox();
            StartButton = new Button();
            StopButton = new Button();
            ListenersListView = new ListView();
            colName = new ColumnHeader();
            colTarget = new ColumnHeader();
            colCounter = new ColumnHeader();
            RegisterButton = new Button();
            UnregisterButton = new Button();
            stopLbl = new Label();
            SuspendLayout();
            // 
            // apiLbl
            // 
            apiLbl.AutoSize = true;
            apiLbl.Location = new Point(25, 26);
            apiLbl.Name = "apiLbl";
            apiLbl.Size = new Size(28, 15);
            apiLbl.TabIndex = 0;
            apiLbl.Text = "API:";
            // 
            // BaseUrlTextBox
            // 
            BaseUrlTextBox.BackColor = Color.White;
            BaseUrlTextBox.Location = new Point(69, 23);
            BaseUrlTextBox.Name = "BaseUrlTextBox";
            BaseUrlTextBox.Size = new Size(224, 23);
            BaseUrlTextBox.TabIndex = 1;
            BaseUrlTextBox.Text = "http://localhost:5010/api/generate";
            // 
            // StartButton
            // 
            StartButton.Location = new Point(69, 52);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(75, 23);
            StartButton.TabIndex = 2;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Enabled = false;
            StopButton.Location = new Point(150, 52);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(75, 23);
            StopButton.TabIndex = 3;
            StopButton.Text = "Stop";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // ListenersListView
            // 
            ListenersListView.Columns.AddRange(new ColumnHeader[] { colName, colTarget, colCounter });
            ListenersListView.Items.AddRange(new ListViewItem[] { listViewItem1 });
            ListenersListView.Location = new Point(69, 140);
            ListenersListView.Name = "ListenersListView";
            ListenersListView.Size = new Size(409, 245);
            ListenersListView.TabIndex = 4;
            ListenersListView.UseCompatibleStateImageBehavior = false;
            // 
            // colName
            // 
            colName.Text = "Name";
            colName.Width = 200;
            // 
            // colTarget
            // 
            colTarget.Text = "Target";
            colTarget.TextAlign = HorizontalAlignment.Center;
            // 
            // colCounter
            // 
            colCounter.Text = "Counter";
            colCounter.TextAlign = HorizontalAlignment.Center;
            // 
            // RegisterButton
            // 
            RegisterButton.Enabled = false;
            RegisterButton.Location = new Point(484, 140);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(75, 23);
            RegisterButton.TabIndex = 5;
            RegisterButton.Text = "Register";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // UnregisterButton
            // 
            UnregisterButton.Enabled = false;
            UnregisterButton.Location = new Point(484, 169);
            UnregisterButton.Name = "UnregisterButton";
            UnregisterButton.Size = new Size(75, 23);
            UnregisterButton.TabIndex = 6;
            UnregisterButton.Text = "Unregister";
            UnregisterButton.UseVisualStyleBackColor = true;
            UnregisterButton.Click += UnregisterButton_Click;
            // 
            // stopLbl
            // 
            stopLbl.AutoSize = true;
            stopLbl.ForeColor = Color.Red;
            stopLbl.Location = new Point(79, 78);
            stopLbl.Name = "stopLbl";
            stopLbl.Size = new Size(134, 15);
            stopLbl.TabIndex = 7;
            stopLbl.Text = "Stopping please wait . . .";
            stopLbl.Visible = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(stopLbl);
            Controls.Add(UnregisterButton);
            Controls.Add(RegisterButton);
            Controls.Add(ListenersListView);
            Controls.Add(StopButton);
            Controls.Add(StartButton);
            Controls.Add(BaseUrlTextBox);
            Controls.Add(apiLbl);
            Name = "MainForm";
            Text = "Epic name here!";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label apiLbl;
        private TextBox BaseUrlTextBox;
        private Button StartButton;
        private Button StopButton;
        private ListView ListenersListView;
        private ColumnHeader colName;
        private ColumnHeader colTarget;
        private ColumnHeader colCounter;
        private Button RegisterButton;
        private Button UnregisterButton;
        private Label stopLbl;
    }
}