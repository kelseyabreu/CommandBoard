using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Command_Board
{
    public partial class Form3 : Form
    {
        public Form3(Panel p)
        {
            InitializeComponent();

            foreach(Panel child in p.Controls){
                var eventsField = typeof(Component).GetField("events", BindingFlags.NonPublic | BindingFlags.Instance);
                var eventHandlerList = eventsField.GetValue(child);

                Panel newP = new Panel {Location = child.Location,Size = child.Size, BorderStyle = child.BorderStyle, BackColor = child.BackColor};
                newP.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
                eventsField.SetValue(newP, eventHandlerList);
                newP.MouseClick += new System.Windows.Forms.MouseEventHandler(panelClick2);
                panel1.Controls.Add(newP);
            }

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        public bool valid { set; get; }
        public int index { set; get; }
        private void button1_Click(object sender, EventArgs e) {
            valid = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            valid = false;
            this.Close();
        }

        Panel lastClicked;
        private void panelClick2(Object sender, MouseEventArgs e) {
            int i = panel1.Controls.IndexOf((Control)sender);

            if (lastClicked != null)
                lastClicked.Visible = false;

            if (i == 0) {
                panel2.Visible = true;
                lastClicked = panel2;
            } else if (i == 1) {
                panel3.Visible = true;
                lastClicked = panel3;
            } else if (i == 2) {
                panel4.Visible = true;
                lastClicked = panel4;
            } else if (i == 3) {
                panel5.Visible = true;
                lastClicked = panel5;
            } else if (i == 4) {
                panel6.Visible = true;
                lastClicked = panel6;
            }

            index = i;

        }

    }
}
