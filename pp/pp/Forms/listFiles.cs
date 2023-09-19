using System;
using System.Windows.Forms;

namespace pp.Forms
{
    public partial class listFiles : Form
    {
        public listFiles()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] tmp = dialog.FileNames;
                for (int i = 0; i < tmp.Length; i++)
                {
                    listBox1.Items.Add(tmp[i]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (listBox1.Items.Count > 0)
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    //MessageBox.Show(listBox1.Items[i].ToString());
                    DBf.addFile(listBox1.Items[i].ToString());
                }
                MessageBox.Show("Файлы сохранены");
            }
            else
                MessageBox.Show("Файлы не выбраны!");
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
