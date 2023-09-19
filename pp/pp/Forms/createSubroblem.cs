using System;
using System.Windows.Forms;

namespace pp.Forms
{
    public partial class createSubroblem : Form
    {
        public createSubroblem()
        {
            InitializeComponent();
        }

        private void createSubproblem_create_Click(object sender, EventArgs e)
        {
            DBf.addSubproblem(textBox1.Text, dateTimePicker1.Value.ToString("dd.MM.yyyy"));
            //new DBf().LoadSubproblem(Main.listSubproblem, Stat.selProb_ID);
            MessageBox.Show("Подзадание добавлено");
            this.Hide();
        }

        private void end_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
