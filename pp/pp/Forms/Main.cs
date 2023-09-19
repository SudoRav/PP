using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace pp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            EnableTab(tabControl1.TabPages[0], false);
            EnableTab(tabControl1.TabPages[1], false);
            EnableTab(tabControl1.TabPages[2], false);

            switch (Stat.ier)
            {
                case "Сотрудник":
                    updateDay.Visible = false;
                    button11.Visible = false;
                    EnableTab(tabControl1.TabPages[0], true);
                    break;

                case "Отдел Кадров":
                    updateDay.Visible = true;
                    button11.Visible = true;
                    EnableTab(tabControl1.TabPages[0], true);
                    EnableTab(tabControl1.TabPages[1], true);
                    break;

                case "Администратор":
                    updateDay.Visible = true;
                    button11.Visible = true;
                    EnableTab(tabControl1.TabPages[0], true);
                    EnableTab(tabControl1.TabPages[1], true);
                    EnableTab(tabControl1.TabPages[2], true);
                    break;
            }

            DBf.LoadDates(otch_tab_table);
            DBf.updateTotalTime();
            DBf.LoadWorkers(uchot_sotrudniki_table);
            DBf.LoadWaiting(uchot_podtverjdenia_table);

            DBf.fillProblems(otchet_prob_problem);
            DBf.fillProblemsToWorker(kab_svod_problem);

            uchot_sotrudniki_sorts.SelectedIndex = 0;
            otch_tab_sorts.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;

            label3.Text = "Вы вошли как: " + Stat.F + " " + Stat.I + " " + Stat.O + " - " + Stat.ier;

            label13.Visible = false;
            kab_svod_staff.Visible = false;
            button9.Visible = false;
            kab_svod_subproblem.Visible = false;
            label21.Visible = false;
            svod_status.Visible = false;
            svod_discription.Visible = false;
            label19.Visible = false;
            svod_date_ending.Visible = false;
            button7.Visible = false;
            button14.Visible = false;
            dataGridView1.Visible = false;

            timer.Enabled = true;
            timer_save.Enabled = true;
            watch.Restart();
            wr_start();
        }

        static void EnableTab(TabPage page, bool en)
        {
            foreach (Control ctl in page.Controls) ctl.Enabled = en;
            foreach (Control ctl in page.Controls) ctl.Visible = en;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            wr_storp();
            timer.Enabled = false;
            timer_save.Enabled = false;

            Application.Exit();
        }

        private void uchot_sotrudniki_findbox_TextChanged(object sender, EventArgs e)
        {
            switch (uchot_sotrudniki_sorts.SelectedIndex)
            {
                case 0:
                    DBf.findWorkerFIO(uchot_sotrudniki_table, uchot_sotrudniki_findbox.Text);
                    break;
                case 1:
                    DBf.findWorkerIER(uchot_sotrudniki_table, uchot_sotrudniki_findbox.Text);
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите принять заявку?", "Принятие заявки", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DBf.waitAcc(uchot_podtverjdenia_table);
                MessageBox.Show("Заявка принята");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите отклонить заявку?", "Отклонение заявки", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DBf.waitDev(uchot_podtverjdenia_table);
                DBf.LoadWaiting(uchot_podtverjdenia_table);
                MessageBox.Show("Заявка отклонена");
            }
        }

        private void updateDay_Click(object sender, EventArgs e)
        {
            DBf.rewindDay(1);
            DBf.LoadDates(otch_tab_table);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            DBf.rewindDay(30);
            DBf.LoadDates(otch_tab_table);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            wr_storp();
            timer.Enabled = false;
            timer_save.Enabled = false;

            Stat.ID = null;
            Stat.login = null;
            Stat.password = null;
            Stat.F = null;
            Stat.I = null;
            Stat.O = null;
            Stat.mail = null;
            Stat.phone = null;
            Stat.ier = null;

            this.Hide();
            new Auth().Show();
        }

        private void but_createProblem_Click(object sender, EventArgs e)
        {
            new Forms.createProblem().Show();
        }

        private void but_delProblem_Click(object sender, EventArgs e)
        {
            if (DBf.getStatusProblem())
                DBf.delProblem();
            else
            {
                DialogResult dialogResult = MessageBox.Show("Задача не завершена!\nВы уверены что хотите удалить задачу?", "Удаление задачи", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DBf.delProblem();
                }
            }
        }

        private void but_endProblem_Click(object sender, EventArgs e)
        {
            DBf.endProblem();
            DBf.LoadProblem(otchet_prob_status, otchet_prob_desc, otchet_prob_dtct, otchet_prob_dted);
        }

        private void otchet_prob_problem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Stat.selProb_ID = otchet_prob_problem.Text;
            try
            {
                DBf.LoadProblem(otchet_prob_status, otchet_prob_desc, otchet_prob_dtct, otchet_prob_dted);
                DBf.LoadStaff(listStaff);
                DBf.LoadSubproblem(listSubproblem);
                DBf.LoadFiles(listFile);
            }
            catch
            { }
        }

        private void btn_addStaff_Click(object sender, EventArgs e)
        {
            new Forms.listUser().Show();
        }
        private void btn_delStaff_Click(object sender, EventArgs e)
        {
            DBf.delStaff(listStaff);
            DBf.LoadStaff(listStaff);
        }

        private void btn_addSubproblem_Click(object sender, EventArgs e)
        {
            new Forms.createSubroblem().Show();
        }

        private void btn_delSubproblem_Click(object sender, EventArgs e)
        {
            DBf.delSubproblem(listSubproblem);
            DBf.LoadSubproblem(listSubproblem);
        }

        private void btn_endSubproblem_Click(object sender, EventArgs e)
        {
            DBf.endSubproblem(listSubproblem.SelectedRows[0].Cells[0].Value.ToString());
            DBf.LoadSubproblem(listSubproblem);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
           
        }

        private void kab_svod_problem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Stat.selProb_ID = kab_svod_problem.Text;
            try
            {
                DBf.LoadProblem(svod_status, svod_discription, svod_date_ending, svod_date_ending);
                DBf.LoadSubproblem(kab_svod_subproblem);
                DBf.LoadStaff(kab_svod_staff);
                DBf.LoadFiles(dataGridView1);
            }
            catch
            { }

            label13.Visible = true;
            kab_svod_staff.Visible = true;
            button9.Visible = true;
            kab_svod_subproblem.Visible = true;
            label21.Visible = true;
            svod_status.Visible = true;
            svod_discription.Visible = true;
            label19.Visible = true;
            svod_date_ending.Visible = true;
            button7.Visible = true;
            button14.Visible = true;
            dataGridView1.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DBf.endSubproblem(kab_svod_subproblem.SelectedRows[0].Cells[0].Value.ToString());
            DBf.LoadSubproblem(kab_svod_subproblem);
        }

        private void uchot_sotrud_del_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы уверены что хотите удалить сотрудника?", "Удаление сотрудника", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DBf.delWorkers(uchot_sotrudniki_table);
                DBf.LoadWorkers(uchot_sotrudniki_table);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stat.selProb_ID = otchet_prob_problem.Text;
            try
            {
                DBf.LoadStaff(listStaff);
                DBf.LoadSubproblem(listSubproblem);
                DBf.fillProblems(otchet_prob_problem);
            }
            catch
            { }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    DBf.findWaitingFIO(uchot_podtverjdenia_table, textBox3.Text);
                    break;
                case 1:
                    DBf.findWaitingIER(uchot_podtverjdenia_table, textBox3.Text);
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            new Forms.listFiles().Show();
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            DBf.openFile(listFile);
        }

        private void delFile_Click(object sender, EventArgs e)
        {
            DBf.delFile(listFile);
            DBf.LoadFiles(listFile);
        }

        private void copyFile_Click(object sender, EventArgs e)
        {
            DBf.copyFile(listFile);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DBf.copyFile(dataGridView1);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DBf.openFile(dataGridView1);
        }


        private static readonly Stopwatch watch = new Stopwatch();
        Point pos;
        private string GetTimeString(TimeSpan elapsed)
        {
            string result = string.Empty;

            result = string.Format("{0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds);

            return result;
        }

        bool pause = false;
        private int SecondWait = Prm.SecondWait;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pos == Cursor.Position)
            {
                status_work.Text = "W";
                copy_status_work.Text = "W";
                pause = true;
            }
            else
            {
                status_work.Text = "";
                copy_status_work.Text = "";
                pause = false;
            }
            pos = Cursor.Position;

            if (pause)
            {
                SecondWait--;
                time_wait.Text = sToTime(SecondWait);
                copy_time_wait.Text = sToTime(SecondWait);

                if (SecondWait <= 240)// 1 минута бездействия
                {
                    t_stop();
                    status_work.Text = "P";
                    copy_status_work.Text = "P";
                    timer_save.Enabled = false;
                }
                if (SecondWait <= 60)// 4 минуты бездействия
                {
                    time_wait.ForeColor = Color.Red;
                    copy_time_wait.ForeColor = Color.Red;
                }
                if (SecondWait <= -1)// 5 минут бездействия
                {
                    button5_Click(sender, e);
                }

            }
            else
            {
                SecondWait = Prm.SecondWait;
                time_wait.Text = "";
                copy_time_wait.Text = "";
                time_wait.ForeColor = Color.Black;
                copy_time_wait.ForeColor = Color.Black;
                t_start();
                timer_save.Enabled = true;
            }
        }

        private void work_timer_Tick(object sender, EventArgs e)
        {
            tm.Text = GetTimeString(watch.Elapsed);
            copy_tm.Text = GetTimeString(watch.Elapsed);
        }

        public void t_start()
        {
            watch.Start();
            work_timer.Enabled = true;
        }
        public void t_stop()
        {
            watch.Stop();
        }

        private void wr_start()
        {
            t_start();
            tm_st.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void wr_storp()
        {
            t_stop();
            tm_ed.Text = DateTime.Now.ToString("HH:mm:ss");

            if (!Prm.autosave)
            {
                DBf.updateWorkTime(tm.Text);
                DBf.updateTotalTime();
                DBf.LoadDates(otch_tab_table);
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            DBf.endSubproblem(kab_svod_subproblem.SelectedRows[0].Cells[0].Value.ToString());
            DBf.LoadSubproblem(kab_svod_subproblem);
        }

        private void timer_save_Tick(object sender, EventArgs e)
        {
            if (Prm.autosave)
            {
                DBf.updateWorkTime("00:01:00");
                DBf.updateTotalTime();
            }
        }
        private string sToTime(int seconds)
        {
            int m = 00;
            int s = 00;

            string s_m = "--";
            string s_s = "--";

            s = seconds;

            while (s > 59)
            {
                m++;
                s -= 60;
            }

            if (s < 10)
                s_s = "0" + s.ToString();
            else
                s_s = s.ToString();
            if (m < 10)
                s_m = "0" + m.ToString();
            else
                s_m = s.ToString();

            return s_m + ":" + s_s;
        }

        private void otch_tab_findbox_TextChanged(object sender, EventArgs e)
        {
            switch (otch_tab_sorts.SelectedIndex)
            {
                case 0:
                    DBf.findDatesFIO(otch_tab_table, otch_tab_findbox.Text);
                    break;
                case 1:
                    DBf.findDatesIER(otch_tab_table, otch_tab_findbox.Text);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBf.CreateWordFile();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void otch_tab_sorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
