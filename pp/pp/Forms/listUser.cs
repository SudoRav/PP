using System;
using System.Windows.Forms;

namespace pp.Forms
{
    public partial class listUser : Form
    {
        public listUser()
        {
            InitializeComponent();

            DBf.LoadWorkers(dataGrid_listUser);

            sorts.SelectedIndex = 0;
        }

        private void dataGrid_listUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DBf.addStaff(dataGrid_listUser.SelectedRows[0].Cells[0].Value.ToString());
            MessageBox.Show("Пользователь добавлен");
            //MessageBox.Show("Пользователь добавлен(нет)");
        }

        private void btn_addStaff_Click(object sender, EventArgs e)
        {
            DBf.addStaff(dataGrid_listUser.SelectedRows[0].Cells[0].Value.ToString());
            MessageBox.Show("Пользователь добавлен");
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void uchot_sotrudniki_findbox_TextChanged(object sender, EventArgs e)
        {
            switch (sorts.SelectedIndex)
            {
                case 0:
                    DBf.findWorkerFIO(dataGrid_listUser, findbox.Text);
                    break;
                case 1:
                    DBf.findWorkerIER(dataGrid_listUser, findbox.Text);
                    break;
            }
        }
    }
}
