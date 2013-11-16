namespace Command_Board
{
    partial class Form3
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.yellowPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.greenPanel = new System.Windows.Forms.Panel();
            this.bluePanel = new System.Windows.Forms.Panel();
            this.redPanel = new System.Windows.Forms.Panel();
            this.yellowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(41, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // yellowPanel
            // 
            this.yellowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yellowPanel.Controls.Add(this.panel2);
            this.yellowPanel.Location = new System.Drawing.Point(246, 28);
            this.yellowPanel.Name = "yellowPanel";
            this.yellowPanel.Size = new System.Drawing.Size(15, 15);
            this.yellowPanel.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(173, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(48, 36);
            this.panel2.TabIndex = 0;
            // 
            // greenPanel
            // 
            this.greenPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.greenPanel.Location = new System.Drawing.Point(262, 28);
            this.greenPanel.Name = "greenPanel";
            this.greenPanel.Size = new System.Drawing.Size(15, 15);
            this.greenPanel.TabIndex = 0;
            // 
            // bluePanel
            // 
            this.bluePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bluePanel.Location = new System.Drawing.Point(262, 12);
            this.bluePanel.Name = "bluePanel";
            this.bluePanel.Size = new System.Drawing.Size(15, 15);
            this.bluePanel.TabIndex = 0;
            // 
            // redPanel
            // 
            this.redPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.redPanel.Location = new System.Drawing.Point(246, 12);
            this.redPanel.Name = "redPanel";
            this.redPanel.Size = new System.Drawing.Size(15, 15);
            this.redPanel.TabIndex = 5;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 187);
            this.Controls.Add(this.greenPanel);
            this.Controls.Add(this.redPanel);
            this.Controls.Add(this.yellowPanel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.bluePanel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.yellowPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel yellowPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel greenPanel;
        private System.Windows.Forms.Panel bluePanel;
        private System.Windows.Forms.Panel redPanel;

    }
}