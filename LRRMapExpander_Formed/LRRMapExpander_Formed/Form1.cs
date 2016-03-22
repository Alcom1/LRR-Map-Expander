using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LRRMapExpander_Formed
{
    public partial class Form1 : Form
    {
        int x_expand;
        int y_expand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Expand(object sender, EventArgs e)
        {
            textBox_output.AppendText("TESTING" + Environment.NewLine);
            textBox_output.AppendText(x_expand + Environment.NewLine);
            textBox_output.AppendText(y_expand + Environment.NewLine);
            textBox_output.AppendText(Environment.NewLine);
        }

        private void Change_X_expand(object sender, EventArgs e)
        {
            try
            {
                x_expand = int.Parse(textBox_x.Text);
            }
            catch
            {

            }
        }

        private void Change_Y_expand(object sender, EventArgs e)
        {
            try
            {
                y_expand = int.Parse(textBox_y.Text);
            }
            catch
            {

            }
        }
    }
}
