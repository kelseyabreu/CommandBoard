using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Command_Board {
    public partial class Form4 : Form {
        public Form4() {
            InitializeComponent();
        }

        public bool valid { set; get; }
        private void button1_Click(object sender, EventArgs e) {
            for (int i = 25500; i >= 0; i--) {
                this.BackColor = Color.FromArgb(0, 0, i/100);
                this.Update();
            }
            valid = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            valid = false;
            this.Close();
        }
    }
}
