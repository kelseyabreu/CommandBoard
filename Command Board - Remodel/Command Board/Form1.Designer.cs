namespace Command_Board
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cardPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnLabel = new System.Windows.Forms.Label();
            this.personsTurn = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.upgradeButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.movesLeftLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.selectionBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.finishButton = new System.Windows.Forms.Button();
            this.rollButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.skillButton = new System.Windows.Forms.Button();
            this.selectionBox1 = new System.Windows.Forms.ComboBox();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.levelPanel = new System.Windows.Forms.Panel();
            this.radioPanel = new System.Windows.Forms.Panel();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tollLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.propertyLayoutPanel = new CustomFlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.playerPanels = new System.Windows.Forms.Panel();
            this.rowPanel = new System.Windows.Forms.Panel();
            this.columnPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new CustomFlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.levelPanel.SuspendLayout();
            this.radioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cardPanel
            // 
            this.cardPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardPanel.Location = new System.Drawing.Point(29, 445);
            this.cardPanel.Name = "cardPanel";
            this.cardPanel.Size = new System.Drawing.Size(339, 102);
            this.cardPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1231, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.connectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabel.Location = new System.Drawing.Point(368, 445);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(139, 61);
            this.turnLabel.TabIndex = 4;
            this.turnLabel.Text = "Turn";
            this.turnLabel.Visible = false;
            // 
            // personsTurn
            // 
            this.personsTurn.AutoSize = true;
            this.personsTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personsTurn.Location = new System.Drawing.Point(373, 512);
            this.personsTurn.Name = "personsTurn";
            this.personsTurn.Size = new System.Drawing.Size(201, 33);
            this.personsTurn.TabIndex = 5;
            this.personsTurn.Text = "Player 1\'s turn";
            this.personsTurn.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.upgradeButton);
            this.panel2.Controls.Add(this.deleteButton);
            this.panel2.Controls.Add(this.movesLeftLabel);
            this.panel2.Controls.Add(this.submitButton);
            this.panel2.Controls.Add(this.selectionBox);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.selectionBox1);
            this.panel2.Controls.Add(this.propertiesPanel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.playerPanels);
            this.panel2.Location = new System.Drawing.Point(614, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(605, 516);
            this.panel2.TabIndex = 14;
            // 
            // upgradeButton
            // 
            this.upgradeButton.Location = new System.Drawing.Point(208, 416);
            this.upgradeButton.Name = "upgradeButton";
            this.upgradeButton.Size = new System.Drawing.Size(213, 23);
            this.upgradeButton.TabIndex = 23;
            this.upgradeButton.Text = "Upgrade";
            this.upgradeButton.UseVisualStyleBackColor = true;
            this.upgradeButton.Visible = false;
            this.upgradeButton.Click += new System.EventHandler(this.upgradeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(509, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(88, 40);
            this.deleteButton.TabIndex = 22;
            this.deleteButton.Text = "Delete Moves";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // movesLeftLabel
            // 
            this.movesLeftLabel.AutoSize = true;
            this.movesLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.movesLeftLabel.Location = new System.Drawing.Point(424, 416);
            this.movesLeftLabel.Name = "movesLeftLabel";
            this.movesLeftLabel.Size = new System.Drawing.Size(122, 24);
            this.movesLeftLabel.TabIndex = 21;
            this.movesLeftLabel.Text = "Moves Left: ";
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(427, 443);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(170, 23);
            this.submitButton.TabIndex = 20;
            this.submitButton.Text = "Submit Move";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // selectionBox
            // 
            this.selectionBox.FormattingEnabled = true;
            this.selectionBox.Location = new System.Drawing.Point(427, 43);
            this.selectionBox.Name = "selectionBox";
            this.selectionBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.selectionBox.Size = new System.Drawing.Size(170, 368);
            this.selectionBox.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.finishButton);
            this.panel1.Controls.Add(this.rollButton);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.skillButton);
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 100);
            this.panel1.TabIndex = 19;
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(110, 46);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(90, 36);
            this.finishButton.TabIndex = 9;
            this.finishButton.Text = "Finish Turn";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // rollButton
            // 
            this.rollButton.Location = new System.Drawing.Point(2, 4);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(90, 36);
            this.rollButton.TabIndex = 7;
            this.rollButton.Text = "Roll";
            this.rollButton.UseVisualStyleBackColor = true;
            this.rollButton.Click += new System.EventHandler(this.rollButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(2, 46);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(90, 36);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // skillButton
            // 
            this.skillButton.Location = new System.Drawing.Point(110, 4);
            this.skillButton.Name = "skillButton";
            this.skillButton.Size = new System.Drawing.Size(90, 36);
            this.skillButton.TabIndex = 8;
            this.skillButton.Text = "Skills";
            this.skillButton.UseVisualStyleBackColor = true;
            // 
            // selectionBox1
            // 
            this.selectionBox1.FormattingEnabled = true;
            this.selectionBox1.Items.AddRange(new object[] {
            "Properties",
            "Specials",
            "Checkpoints",
            "Lines",
            "Home",
            "Deadzones",
            "MovingCloud"});
            this.selectionBox1.Location = new System.Drawing.Point(387, 490);
            this.selectionBox1.Name = "selectionBox1";
            this.selectionBox1.Size = new System.Drawing.Size(121, 21);
            this.selectionBox1.TabIndex = 18;
            this.selectionBox1.Visible = false;
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.levelPanel);
            this.propertiesPanel.Controls.Add(this.propertyLayoutPanel);
            this.propertiesPanel.Location = new System.Drawing.Point(208, -3);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(213, 413);
            this.propertiesPanel.TabIndex = 17;
            // 
            // levelPanel
            // 
            this.levelPanel.Controls.Add(this.radioPanel);
            this.levelPanel.Controls.Add(this.label3);
            this.levelPanel.Controls.Add(this.label2);
            this.levelPanel.Controls.Add(this.tollLabel);
            this.levelPanel.Controls.Add(this.levelLabel);
            this.levelPanel.Location = new System.Drawing.Point(0, 207);
            this.levelPanel.Name = "levelPanel";
            this.levelPanel.Size = new System.Drawing.Size(223, 206);
            this.levelPanel.TabIndex = 25;
            this.levelPanel.Visible = false;
            this.levelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.levelPanel_Paint);
            // 
            // radioPanel
            // 
            this.radioPanel.Controls.Add(this.radioButton0);
            this.radioPanel.Controls.Add(this.radioButton1);
            this.radioPanel.Controls.Add(this.radioButton2);
            this.radioPanel.Controls.Add(this.radioButton3);
            this.radioPanel.Controls.Add(this.radioButton4);
            this.radioPanel.Location = new System.Drawing.Point(0, 83);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.Size = new System.Drawing.Size(200, 124);
            this.radioPanel.TabIndex = 24;
            // 
            // radioButton0
            // 
            this.radioButton0.AutoSize = true;
            this.radioButton0.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton0.Location = new System.Drawing.Point(3, 0);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(202, 22);
            this.radioButton0.TabIndex = 1;
            this.radioButton0.TabStop = true;
            this.radioButton0.Text = "Level 1. 40243 | 23332";
            this.radioButton0.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(3, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(122, 22);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton2";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(3, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(122, 22);
            this.radioButton2.TabIndex = 23;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton5";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(3, 77);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(122, 22);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(3, 106);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(122, 22);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(161, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Toll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Cost";
            // 
            // tollLabel
            // 
            this.tollLabel.AutoSize = true;
            this.tollLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tollLabel.Location = new System.Drawing.Point(56, 4);
            this.tollLabel.Name = "tollLabel";
            this.tollLabel.Size = new System.Drawing.Size(72, 25);
            this.tollLabel.TabIndex = 23;
            this.tollLabel.Text = "Toll : ";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelLabel.Location = new System.Drawing.Point(3, 32);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(186, 25);
            this.levelLabel.TabIndex = 24;
            this.levelLabel.Text = "Currently Level: ";
            // 
            // propertyLayoutPanel
            // 
            this.propertyLayoutPanel.Location = new System.Drawing.Point(6, 3);
            this.propertyLayoutPanel.Name = "propertyLayoutPanel";
            this.propertyLayoutPanel.Size = new System.Drawing.Size(202, 202);
            this.propertyLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(428, -3);
            this.label1.MaximumSize = new System.Drawing.Size(90, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 40);
            this.label1.TabIndex = 16;
            this.label1.Text = "Blocks Selected";
            // 
            // playerPanels
            // 
            this.playerPanels.AutoScroll = true;
            this.playerPanels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerPanels.Location = new System.Drawing.Point(0, 0);
            this.playerPanels.Name = "playerPanels";
            this.playerPanels.Size = new System.Drawing.Size(202, 410);
            this.playerPanels.TabIndex = 14;
            // 
            // rowPanel
            // 
            this.rowPanel.Location = new System.Drawing.Point(3, 45);
            this.rowPanel.Name = "rowPanel";
            this.rowPanel.Size = new System.Drawing.Size(23, 394);
            this.rowPanel.TabIndex = 15;
            // 
            // columnPanel
            // 
            this.columnPanel.Location = new System.Drawing.Point(29, 25);
            this.columnPanel.Name = "columnPanel";
            this.columnPanel.Size = new System.Drawing.Size(579, 19);
            this.columnPanel.TabIndex = 16;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(29, 45);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(579, 394);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1231, 568);
            this.Controls.Add(this.columnPanel);
            this.Controls.Add(this.rowPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.personsTurn);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.cardPanel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.propertiesPanel.ResumeLayout(false);
            this.levelPanel.ResumeLayout(false);
            this.levelPanel.PerformLayout();
            this.radioPanel.ResumeLayout(false);
            this.radioPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel cardPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private CustomFlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label personsTurn;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox selectionBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button rollButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button skillButton;
        private System.Windows.Forms.ComboBox selectionBox1;
        private System.Windows.Forms.Panel propertiesPanel;
        private CustomFlowLayoutPanel propertyLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel playerPanels;
        private System.Windows.Forms.Label movesLeftLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel rowPanel;
        private System.Windows.Forms.Panel columnPanel;
        private System.Windows.Forms.RadioButton radioButton0;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Panel levelPanel;
        private System.Windows.Forms.Label tollLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button upgradeButton;
        private System.Windows.Forms.Panel radioPanel;
    }
}

