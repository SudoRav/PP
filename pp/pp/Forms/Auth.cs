using System;
using System.Windows.Forms;

namespace pp
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DBf.Login(log.Text, pas.Text, this);
        }

        private void toReg_Click(object sender, EventArgs e)
        {
            new Reg().Show();
            this.Hide();
        }
    }
}