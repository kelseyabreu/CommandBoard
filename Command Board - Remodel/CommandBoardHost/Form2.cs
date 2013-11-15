using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommandBoardHost
{
    public partial class Form2 : Form
    {
        public int totalValue { set; get; }
        public int numberOfPlayer { set; get; }
        public Form2()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            numberOfPlayer = Convert.ToInt32(textBox1.Text);
            totalValue = Convert.ToInt32(textBox2.Text);
            this.Close();
        }
    }
}
