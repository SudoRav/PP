using System;
using System.Windows.Forms;

namespace pp
{
    static class Program
    {
        public static string connectionString;

        [STAThread]
        static void Main()
        {
                 if (System.Net.Dns.GetHostName() == "LAPTOP-NK04PI6P")
                connectionString = @"Data Source=LAPTOP-NK04PI6P;Initial Catalog=pp;Integrated Security=True";
            else if (System.Net.Dns.GetHostName() == "DESKTOP-6ELC94T")
                connectionString = @"Data Source=DESKTOP-6ELC94T\SQLEXPRESS;Initial Catalog=pp;Integrated Security=True";
            else
                MessageBox.Show(System.Net.Dns.GetHostName());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Auth());
        }
    }
}
