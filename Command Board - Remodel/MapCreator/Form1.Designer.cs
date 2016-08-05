namespace MapCreator
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.shapeColorDiag = new System.Windows.Forms.ColorDialog();
            this.rectColorDiag = new System.Windows.Forms.ColorDialog();
            this.innerShapeDiag = new System.Windows.Forms.ColorDialog();
            this.innerRectDiag = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.selectionBox = new System.Windows.Forms.ComboBox();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.circleSubmitButton = new System.Windows.Forms.Button();
            this.innerRectButton = new System.Windows.Forms.Button();
            this.innerShapeButton = new System.Windows.Forms.Button();
            this.rectColorButton = new System.Windows.Forms.Button();
            this.shapeColorButton = new System.Windows.Forms.Button();
            this.propertyLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.submitValueButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.valueText = new System.Windows.Forms.TextBox();
            this.valueLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new CustomFlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Column";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Row";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 59);
            this.textBox1.MaxLength = 2;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(32, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(61, 85);
            this.textBox2.MaxLength = 2;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(32, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Preview:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // selectionBox
            // 
            this.selectionBox.FormattingEnabled = true;
            this.selectionBox.Items.AddRange(new object[] {
            "Properties",
            "Specials",
            "Checkpoints",
            "Lines",
            "Home",
            "Deadzones",
            "MovingCloud"});
            this.selectionBox.Location = new System.Drawing.Point(17, 186);
            this.selectionBox.Name = "selectionBox";
            this.selectionBox.Size = new System.Drawing.Size(121, 21);
            this.selectionBox.TabIndex = 17;
            this.selectionBox.SelectedIndexChanged += new System.EventHandler(this.selectionBox_SelectedIndexChanged);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.circleSubmitButton);
            this.propertiesPanel.Controls.Add(this.innerRectButton);
            this.propertiesPanel.Controls.Add(this.innerShapeButton);
            this.propertiesPanel.Controls.Add(this.rectColorButton);
            this.propertiesPanel.Controls.Add(this.shapeColorButton);
            this.propertiesPanel.Controls.Add(this.propertyLayoutPanel);
            this.propertiesPanel.Location = new System.Drawing.Point(12, 214);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(213, 320);
            this.propertiesPanel.TabIndex = 0;
            // 
            // circleSubmitButton
            // 
            this.circleSubmitButton.Location = new System.Drawing.Point(5, 289);
            this.circleSubmitButton.Name = "circleSubmitButton";
            this.circleSubmitButton.Size = new System.Drawing.Size(158, 31);
            this.circleSubmitButton.TabIndex = 20;
            this.circleSubmitButton.Text = "Submit";
            this.circleSubmitButton.UseVisualStyleBackColor = true;
            this.circleSubmitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // innerRectButton
            // 
            this.innerRectButton.Location = new System.Drawing.Point(88, 249);
            this.innerRectButton.Name = "innerRectButton";
            this.innerRectButton.Size = new System.Drawing.Size(75, 34);
            this.innerRectButton.TabIndex = 19;
            this.innerRectButton.Text = "Inner Rectangle";
            this.innerRectButton.UseVisualStyleBackColor = true;
            this.innerRectButton.Click += new System.EventHandler(this.innerRectButton_Click);
            // 
            // innerShapeButton
            // 
            this.innerShapeButton.Location = new System.Drawing.Point(88, 1);
            this.innerShapeButton.Name = "innerShapeButton";
            this.innerShapeButton.Size = new System.Drawing.Size(75, 34);
            this.innerShapeButton.TabIndex = 18;
            this.innerShapeButton.Text = "Inner Shape";
            this.innerShapeButton.UseVisualStyleBackColor = true;
            this.innerShapeButton.Click += new System.EventHandler(this.innerShapeButton_Click);
            // 
            // rectColorButton
            // 
            this.rectColorButton.Location = new System.Drawing.Point(5, 249);
            this.rectColorButton.Name = "rectColorButton";
            this.rectColorButton.Size = new System.Drawing.Size(77, 34);
            this.rectColorButton.TabIndex = 17;
            this.rectColorButton.Text = "Rectangle Color";
            this.rectColorButton.UseVisualStyleBackColor = true;
            this.rectColorButton.Click += new System.EventHandler(this.rectColorButton_Click);
            // 
            // shapeColorButton
            // 
            this.shapeColorButton.Location = new System.Drawing.Point(5, 1);
            this.shapeColorButton.Name = "shapeColorButton";
            this.shapeColorButton.Size = new System.Drawing.Size(77, 34);
            this.shapeColorButton.TabIndex = 16;
            this.shapeColorButton.Text = "Shape Color";
            this.shapeColorButton.UseVisualStyleBackColor = true;
            this.shapeColorButton.Click += new System.EventHandler(this.shapeColorButton_Click);
            // 
            // propertyLayoutPanel
            // 
            this.propertyLayoutPanel.Location = new System.Drawing.Point(5, 41);
            this.propertyLayoutPanel.Name = "propertyLayoutPanel";
            this.propertyLayoutPanel.Size = new System.Drawing.Size(202, 202);
            this.propertyLayoutPanel.TabIndex = 15;
            // 
            // submitValueButton
            // 
            this.submitValueButton.Location = new System.Drawing.Point(119, 111);
            this.submitValueButton.Name = "submitValueButton";
            this.submitValueButton.Size = new System.Drawing.Size(86, 23);
            this.submitValueButton.TabIndex = 18;
            this.submitValueButton.Text = "Submit Value";
            this.submitValueButton.UseVisualStyleBackColor = true;
            this.submitValueButton.Click += new System.EventHandler(this.submitValueButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(116, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Value : ";
            // 
            // valueText
            // 
            this.valueText.Location = new System.Drawing.Point(119, 85);
            this.valueText.MaxLength = 5;
            this.valueText.Name = "valueText";
            this.valueText.Size = new System.Drawing.Size(86, 20);
            this.valueText.TabIndex = 20;
            this.valueText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.valueText_KeyDown);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(165, 43);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(0, 13);
            this.valueLabel.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Change Value";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(234, 33);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(500, 500);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            this.flowLayoutPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1106, 700);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.valueText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.submitValueButton);
            this.Controls.Add(this.propertiesPanel);
            this.Controls.Add(this.selectionBox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.propertiesPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private CustomFlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ColorDialog shapeColorDiag;
        private System.Windows.Forms.ColorDialog rectColorDiag;
        private System.Windows.Forms.ColorDialog innerShapeDiag;
        private System.Windows.Forms.ColorDialog innerRectDiag;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox selectionBox;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.Button circleSubmitButton;
        private System.Windows.Forms.Button innerRectButton;
        private System.Windows.Forms.Button innerShapeButton;
        private System.Windows.Forms.Button rectColorButton;
        private System.Windows.Forms.Button shapeColorButton;
        private System.Windows.Forms.FlowLayoutPanel propertyLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button submitValueButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox valueText;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label label6;
    }
}

