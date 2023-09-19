using System;
using System.Windows.Forms;

namespace pp.Forms
{
    public partial class createProblem : Form
    {
        public createProblem()
        {
            InitializeComponent();
        }

        private void createProblem_create_Click(object sender, EventArgs e)
        {
            DBf.addProblem(textBox1.Text, textBox2.Text, dateTimePicker1.Value.ToString("dd.MM.yyy"));
                    this.Hide();
        }
        private void end_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
