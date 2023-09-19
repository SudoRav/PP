using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace pp
{
    public partial class Reg : Form
    {
        public Reg()
        {
            InitializeComponent();
            ier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ier.SelectedIndex = 0;
        }

        private void Reg_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            phone.Text = Regex.Replace(phone.Text, @"(?:\+7|8)?(?:\()?(\d{3})(?:\))?(\d{3})(?:-)?(\d{2})(?:-)?(\d{2})", "+7($1)$2-$3-$4");

            DBf.Register(log.Text, pas.Text, F.Text, I.Text, O.Text, mail.Text, phone.Text, ier.Text);
        }

        private void toAuth_Click(object sender, EventArgs e)
        {
            this.Hide();
            Auth form = new Auth();
            form.Show();
        }

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }
    }
}
