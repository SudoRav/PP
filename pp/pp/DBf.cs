using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using Word = Microsoft.Office.Interop.Word;

namespace pp
{
    class DBf
    {
        static SqlConnection con;
        static SqlCommand cmd;

        static public void Login(string log, string pas, Form frm)
        {
            try
            {
                con = new SqlConnection(Program.connectionString);
                con.Open();
                DataTable tbl = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();


                cmd = new SqlCommand($"SELECT * FROM users WHERE login = '{log}' AND password = '{CalculateMD5Hash(pas)}' AND toreg = 'True'", con);
                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                if (tbl.Rows.Count != 0)
                {
                    frm.Hide();

                    //НАЙДИ ДРУГОЙ СПОСОБ ВЫБОРКИ ДАННЫХ
                    cmd = new SqlCommand($"SELECT ID FROM users       WHERE login = '{log}'", con);
                    Stat.ID = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT login FROM users    WHERE login = '{log}'", con);
                    Stat.login = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT password FROM users WHERE login = '{log}'", con);
                    Stat.password = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT F FROM users        WHERE login = '{log}'", con);
                    Stat.F = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT I FROM users        WHERE login = '{log}'", con);
                    Stat.I = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT O FROM users        WHERE login = '{log}'", con);
                    Stat.O = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT mail FROM users     WHERE login = '{log}'", con);
                    Stat.mail = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT phone FROM users    WHERE login = '{log}'", con);
                    Stat.phone = cmd.ExecuteScalar().ToString();
                    cmd = new SqlCommand($"SELECT ier FROM users      WHERE login = '{log}'", con);
                    Stat.ier = cmd.ExecuteScalar().ToString();
                    //НАЙДИ ДРУГОЙ СПОСОБ ВЫБОРКИ ДАННЫХ

                    new Main().Show();
                }
                else
                    MessageBox.Show("Не верный логин или пароль");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        static public bool validLog(string log)
        {
            DataTable tbl = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd = new SqlCommand($"SELECT * FROM users WHERE login = '{log}'", con);
            adapter.SelectCommand = cmd;
            adapter.Fill(tbl);

            if (tbl.Rows.Count == 0)
                return true;
            else
                return false;
        }
        static public void Register(string log, string pas, string F, string I, string O, string mail, string phone, string ier)
        {
            con = new SqlConnection(Program.connectionString);
            try
            {
                if (validLog(log))
                    if (log.Length != 0)
                        if (pas.Length != 0)
                            if (F.Length != 0)
                                if (I.Length != 0)
                                    if (O.Length != 0)
                                        if (mail.Length != 0)
                                            if (phone.Length == 16)
                                                if (ier.Length != 0)
                                                {
                                                    con.Open();
                                                    cmd = new SqlCommand($"INSERT INTO users (login, password, F, I, O, mail, phone, ier, toreg) VALUES ('{log}', '{CalculateMD5Hash(pas)}', '{F}', '{I}', '{O}', '{mail}', '{phone}', '{ier}', 'False')", con);
                                                    cmd.ExecuteNonQuery();

                                                    MessageBox.Show("Заявка на регисрацию успешно подана\nОжидайте подтверждения от Администратора");
                                                }
                                                else
                                                    MessageBox.Show("Выберите должность");
                                            else
                                                MessageBox.Show("Введите телефон");
                                        else
                                            MessageBox.Show("Введите почту");
                                    else
                                        MessageBox.Show("Введите отчество");
                                else
                                    MessageBox.Show("Введите имя");
                            else
                                MessageBox.Show("Введите фамилию");
                        else
                            MessageBox.Show("Введите пароль");
                    else
                        MessageBox.Show("Введите логин");
                else
                    MessageBox.Show("Пользователь с этим логином уже существует");
            }
            finally
            {
                con.Close();
            }
        }
        static private string recalculateTime(string org_time, string add_time)
        {
            if (org_time == "")
                org_time = "00:00:00";
            if (add_time == "")
                add_time = "00:00:00";

            string[] ttl = org_time.Split(':');
            string[] tim = add_time.Split(':');

            string sttl_s = "--";
            string sttl_m = "--";
            string sttl_h = "--";

            int ttl_h = Convert.ToInt32(ttl[0]);
            int ttl_m = Convert.ToInt32(ttl[1]);
            int ttl_s = Convert.ToInt32(ttl[2]);

            int tim_h = Convert.ToInt32(tim[0]);
            int tim_m = Convert.ToInt32(tim[1]);
            int tim_s = Convert.ToInt32(tim[2]);

            ttl_h += tim_h;
            ttl_m += tim_m;
            ttl_s += tim_s;

            while (ttl_s > 59)
            {
                ttl_m++;
                ttl_s -= 60;
            }
            while (ttl_m > 59)
            {
                ttl_h++;
                ttl_m -= 60;
            }

            if (ttl_s < 10)
                sttl_s = "0" + ttl_s.ToString();
            else
                sttl_s = ttl_s.ToString();
            if (ttl_m < 10)
                sttl_m = "0" + ttl_m.ToString();
            else
                sttl_m = ttl_m.ToString();
            if (ttl_h < 10)
                sttl_h = "0" + ttl_h.ToString();
            else
                sttl_h = ttl_h.ToString();

            org_time = sttl_h + ":" + sttl_m + ":" + sttl_s;

            return (org_time);
        }
        static public string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));
            return sb.ToString();
        }


        static public void LoadDates(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter2 = new SqlDataAdapter($"SELECT tab.* FROM tab WHERE user_ID = '0'", con);
                DataSet data2 = new DataSet();
                adapter2.Fill(data2);

                string ss = "";
                for (int q = 1; q <= 30; q++)
                {
                    string s = data2.Tables[0].Rows[0].ItemArray[q + 1].ToString();
                    if (s == "")
                        s = "no date " + q.ToString();

                    ss += $"tab.day{q} as '{s}'";
                    if (q < 30)
                        ss += ", ";
                }

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.F + ' ' + users.I + ' ' + users.O as 'ФИО', users.ier as 'Должность', tab.total as 'Всего', {ss} FROM tab " +
                                                            $"JOIN users ON users.ID = tab.user_ID " +
                                                            $"WHERE users.toreg = 'True'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);

                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void rewindDay(int cout_day)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                for (int q = 1; q <= cout_day; q++)
                {
                    for (byte i = 30; i > 1; i--)
                    {
                        cmd = new SqlCommand($"UPDATE tab SET day{i} = day{i - 1} ", con);
                        cmd.ExecuteNonQuery();
                    }
                    cmd = new SqlCommand($"UPDATE tab SET day1 = ''", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand($"UPDATE tab SET day1 = '{DateTime.Now.ToString("dd.MM.yyyy")}' WHERE user_ID = '0'", con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                MessageBox.Show("Для корректного отображения дат - требуется перезапуск!");
            }
        }
        static public void updateWorkTime(string time)
        {
            string day1;

            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                DataTable tbl = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();

                cmd = new SqlCommand($"SELECT user_ID FROM tab WHERE user_ID = '{Stat.ID}'", con);
                adapter.SelectCommand = cmd;
                adapter.Fill(tbl);

                if (tbl.Rows.Count == 0)
                {
                    cmd = new SqlCommand($"INSERT INTO tab (user_ID, day1) VALUES ('{Stat.ID}', '{time}') ", con);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    //cmd = new SqlCommand($"UPDATE tab SET day1 = '{time}' " +
                    //                     $"WHERE user_ID = {Stat.ID}", con);
                    //cmd.ExecuteNonQuery();

                    cmd = new SqlCommand($"SELECT day1 FROM tab WHERE user_ID = '{Stat.ID}'", con);
                    day1 = cmd.ExecuteScalar().ToString();

                    cmd = new SqlCommand($"UPDATE tab SET day1 = '{recalculateTime(day1, time)}' " +
                                         $"WHERE user_ID = '{Stat.ID}'", con);
                    cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void updateTotalTime()
        {
            string total;
            string time_day;

            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"UPDATE tab SET total = '00:00:00' " +
                                 $"WHERE user_ID = '{Stat.ID}'", con);
            cmd.ExecuteScalar();

            for (int q = 1; q <= 30; q++)
            {
                cmd = new SqlCommand($"SELECT total FROM tab WHERE user_ID = '{Stat.ID}'", con);
                total = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT day{q} FROM tab WHERE user_ID = '{Stat.ID}'", con);
                time_day = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"UPDATE tab SET total = '{recalculateTime(total, time_day)}' " +
                                     $"WHERE user_ID = '{Stat.ID}'", con);
                cmd.ExecuteScalar();
            }

            //cmd = new SqlCommand($"SELECT total FROM tab WHERE user_ID = '{Stat.ID}'", con);
            //MessageBox.Show(cmd.ExecuteScalar().ToString());

            con.Close();

        }
        static public void findDatesFIO(DataGridView grid, string fio)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter2 = new SqlDataAdapter($"SELECT tab.* FROM tab WHERE user_ID = '0'", con);
                DataSet data2 = new DataSet();
                adapter2.Fill(data2);

                string ss = "";
                for (int q = 1; q <= 30; q++)
                {
                    string s = data2.Tables[0].Rows[0].ItemArray[q + 1].ToString();
                    if (s == "")
                        s = "no date " + q.ToString();

                    ss += $"tab.day{q} as '{s}'";
                    if (q < 30)
                        ss += ", ";
                }

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.F + ' ' + users.I + ' ' + users.O as 'ФИО', users.ier as 'Должность', tab.total as 'Всего', {ss} FROM tab " +
                                                            $"JOIN users ON users.ID = tab.user_ID " +
                                                            $"WHERE users.toreg = 'True' AND users.F + users.I + users.O  LIKE '%{fio}%'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);

                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void findDatesIER(DataGridView grid, string ier)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter2 = new SqlDataAdapter($"SELECT tab.* FROM tab WHERE user_ID = '0'", con);
                DataSet data2 = new DataSet();
                adapter2.Fill(data2);

                string ss = "";
                for (int q = 1; q <= 30; q++)
                {
                    string s = data2.Tables[0].Rows[0].ItemArray[q + 1].ToString();
                    if (s == "")
                        s = "no date " + q.ToString();

                    ss += $"tab.day{q} as '{s}'";
                    if (q < 30)
                        ss += ", ";
                }

                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.F + ' ' + users.I + ' ' + users.O as 'ФИО', users.ier as 'Должность', tab.total as 'Всего', {ss} FROM tab " +
                                                            $"JOIN users ON users.ID = tab.user_ID " +
                                                            $"WHERE users.toreg = 'True' AND users.ier  LIKE '%{ier}%'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);

                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static public void LoadWorkers(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность' FROM users " +
                                                            $"WHERE toreg = 'True'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void findWorkerFIO(DataGridView table, string fio)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность'  FROM users " +
                                                        $"WHERE f + i + o LIKE '%{fio}%' AND toreg = 'True'", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            table.DataSource = data.Tables[0];
            con.Close();
        }
        static public void findWorkerIER(DataGridView table, string ier)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность'  FROM users " +
                                                        $"WHERE ier LIKE '%{ier}%' AND toreg = 'True'", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            table.DataSource = data.Tables[0];
            con.Close();
        }
        static public void fillWorkersALL(ComboBox combo)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT f + ' ' + i + ' ' + o + ' - ' + ier as 'FIO-R', ID FROM users", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            combo.DataSource = data.Tables[0];
            combo.DisplayMember = "FIO-R";
            combo.ValueMember = "ID";

            con.Close();
        }
        static public void fillWorkerWHERE(ComboBox combo)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT f + ' ' + i + ' ' + o + ' - ' + ier as 'FIO-R' FROM users " +
                                                        $"WHERE ID = '{Stat.ID}'", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            combo.DataSource = data.Tables[0];
            combo.DisplayMember = "FIO-R";
            combo.ValueMember = "FIO-R";

            con.Close();
        }
        static public void delWorkers(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM tab " +
                                     $"WHERE user_ID = {grid.SelectedRows[0].Cells[0].Value}", con);

                cmd = new SqlCommand($"DELETE FROM users " +
                                     $"WHERE ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM staff " +
                                     $"WHERE user_ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static public void LoadWaiting(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность'  FROM users " +
                                                            $"WHERE toreg = 'False'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void findWaitingFIO(DataGridView grid, string fio)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность'  FROM users " +
                                                        $"WHERE f + i + o LIKE '%{fio}%' AND toreg = 'False'", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            grid.DataSource = data.Tables[0];
            con.Close();
        }
        static public void findWaitingIER(DataGridView grid, string ier)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, login as 'Логин', password as 'Пароль', F as 'Фамилия', I as 'Имя', O as 'Отчество', mail as 'Почта', phone as 'Телефон', ier as 'Должность'  FROM users " +
                                                        $"WHERE ier LIKE '%{ier}%' AND toreg = 'False'", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            grid.DataSource = data.Tables[0];
            con.Close();
        }
        static public void waitAcc(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"INSERT INTO tab (user_ID) VALUES ('{grid.SelectedRows[0].Cells[0].Value}')", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"UPDATE users SET toreg = 'True' " +
                                     $"WHERE ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();

                LoadWaiting(grid);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void waitDev(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM users " +
                                     $"WHERE ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();
                LoadWaiting(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static public void LoadProblem(Label status, Label description, Label date_crate, Label date_ending)
        {

            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"SELECT status FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
                status.Text = cmd.ExecuteScalar().ToString();
                if (status.Text == "True")
                    status.Text = "Завершено";
                else
                    status.Text = "В разработке";

                cmd = new SqlCommand($"SELECT name FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
                description.Text = cmd.ExecuteScalar().ToString();
                description.Text += "\n";
            }
            catch
            { }
            cmd = new SqlCommand($"SELECT description FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
            description.Text += cmd.ExecuteScalar().ToString();
            cmd = new SqlCommand($"SELECT date_create FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
            date_crate.Text = cmd.ExecuteScalar().ToString();
            cmd = new SqlCommand($"SELECT date_ending FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
            date_ending.Text = cmd.ExecuteScalar().ToString();

            con.Close();

        }
        static public bool getStatusProblem()
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"SELECT status FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
            string status = cmd.ExecuteScalar().ToString();

            con.Close();

            if (status == "True")
                return true;
            else
                return false;
        }
        static public void fillProblems(ComboBox combo)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT name as 'name', ID as 'id' FROM problems ", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            combo.DataSource = data.Tables[0];
            combo.DisplayMember = "id";
            combo.ValueMember = "id";
            //combo.SelectedValue = "id";

            con.Close();
        }
        static public void fillProblemsToWorker(ComboBox combo)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            //SqlDataAdapter adapter = new SqlDataAdapter($"SELECT name + ' (' + date_ending + ')' as 'ND', ID FROM problems ", con);
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT name as 'name', ID as 'id' FROM problems " +
                                                        $"JOIN staff ON staff.problem_ID = problems.ID " +
                                                        $"WHERE staff.user_ID = {Stat.ID}", con);
            DataSet data = new DataSet();
            adapter.Fill(data);
            combo.DataSource = data.Tables[0];
            combo.DisplayMember = "id";
            combo.ValueMember = "id";

            con.Close();
        }
        static public void addProblem(string name, string description, string date_ending)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"INSERT INTO problems (status, name, description, date_create, date_ending) VALUES ('False', '{name}', '{description}', '{DateTime.Today.ToString("dd.MM.yyyy")}', '{date_ending}')", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        static public void delProblem()
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM problems " +
                                     $"WHERE ID = {Stat.selProb_ID}", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM staff " +
                                     $"WHERE problem_ID = {Stat.selProb_ID}", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM subproblems " +
                                     $"WHERE problem_ID = {Stat.selProb_ID}", con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand($"DELETE FROM files " +
                                     $"WHERE problem_ID = {Stat.selProb_ID}", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void endProblem()
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"SELECT status FROM problems WHERE ID = '{Stat.selProb_ID}'", con);
            string status = cmd.ExecuteScalar().ToString();

            if (status == "False")
                cmd = new SqlCommand($"UPDATE problems SET status = 'True'", con);
            else
                cmd = new SqlCommand($"UPDATE problems SET status = 'False'", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }


        static public void LoadStaff(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.ID, users.F as 'Фамилия', users.I as 'Имя', users.O as 'Отчество', users.ier as 'Должность' FROM staff " +
                                                            $"JOIN users ON users.ID = staff.user_ID " +
                                                            //$"JOIN subproblems ON subproblems.problem_ID = staff.problem_ID " +
                                                            $"WHERE problem_ID = '{Stat.selProb_ID}'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void addStaff(string user_id)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"INSERT INTO staff (problem_ID, user_ID) VALUES ('{Stat.selProb_ID}', '{user_id}')", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        static public void delStaff(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM staff " +
                                     $"WHERE user_ID = {grid.SelectedRows[0].Cells[0].Value} AND problem_ID = {Stat.selProb_ID}", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static public void LoadSubproblem(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, status as 'Статус', name as 'Название', date_ending as 'Дата завершения' FROM subproblems " +
                                                            $"WHERE problem_ID = '{Stat.selProb_ID}'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void addSubproblem(string name, string date_ending)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"INSERT INTO subproblems (problem_ID, status, name, date_ending) VALUES ('{Stat.selProb_ID}', 'False', '{name}', '{date_ending}')", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
        static public void delSubproblem(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM subproblems " +
                                     $"WHERE ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void endSubproblem(string ID)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();

            cmd = new SqlCommand($"SELECT status FROM subproblems WHERE ID = '{ID}' AND problem_ID = {Stat.selProb_ID}", con);
            string status = cmd.ExecuteScalar().ToString();

            if (status == "False")
                cmd = new SqlCommand($"UPDATE subproblems SET status = 'True' WHERE ID = '{ID}' AND problem_ID = {Stat.selProb_ID}", con);
            else
                cmd = new SqlCommand($"UPDATE subproblems SET status = 'False' WHERE ID = '{ID}' AND problem_ID = {Stat.selProb_ID}", con);
            cmd.ExecuteNonQuery();

            con.Close();
        }


        static public void LoadFiles(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"SELECT ID, FileName as 'Название', Extension as 'Разширение' FROM files " +
                                                            $"WHERE problem_ID = '{Stat.selProb_ID}'", con);
                DataSet data = new DataSet();
                adapter.Fill(data);
                grid.DataSource = data.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void addFile(string filePath)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                using (Stream stream = File.OpenRead(filePath))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);

                    string extn = new FileInfo(filePath).Extension;
                    string name = new FileInfo(filePath).Name;

                    string query = $"INSERT INTO files (problem_ID, Data, Extension, Filename) VALUES ({Stat.selProb_ID}, @data, @extn, @name)";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@extn", SqlDbType.Char).Value = extn;
                    cmd.ExecuteNonQuery();
                }

                //cmd = new SqlCommand($"INSERT INTO files (problem_ID, Data, Extension) VALUES ('{Stat.selProb_ID}', '{}', '{}')", con);
                //cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void openFile(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"SELECT Data, Extension, FileName FROM files " +
                                     $"WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var name = reader["FileName"].ToString();
                    var data = (byte[])reader["Data"];
                    var extn = reader["Extension"].ToString();
                    var file = name.Replace(extn, DateTime.Now.ToString("ddMMyyyhhmmss")) + extn;

                    File.WriteAllBytes(file, data);
                    System.Diagnostics.Process.Start(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void copyFile(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"SELECT FileName FROM files WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                string oldname = cmd.ExecuteScalar().ToString();

                //    cmd = new SqlCommand($"SELECT Extension FROM files WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                //string format = cmd.ExecuteScalar().ToString();

                cmd = new SqlCommand($"SELECT Data, Extension, FileName FROM files " +
                                         $"WHERE ID = '{grid.SelectedRows[0].Cells[0].Value}'", con);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var name = reader["FileName"].ToString();
                    var data = (byte[])reader["Data"];
                    var extn = reader["Extension"].ToString();
                    //var file = name.Replace(extn, DateTime.Now.ToString("ddMMyyyhhmmss")) + extn;

                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //File.WriteAllBytes(dialog.SelectedPath + @"\" + oldname + format, data);
                        File.WriteAllBytes(dialog.SelectedPath + @"\" + oldname, data);
                        MessageBox.Show("Файл скопирован");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        static public void delFile(DataGridView grid)
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                cmd = new SqlCommand($"DELETE FROM files " +
                                     $"WHERE ID = {grid.SelectedRows[0].Cells[0].Value}", con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        static public void CreateWordFile()
        {
            con = new SqlConnection(Program.connectionString);
            con.Open();
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Word._Application word_app = new Word.Application();
                    word_app.Visible = true;
                    object missing = Type.Missing;
                    Word._Document word_doc = word_app.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    object start = 0, end = 0;
                    Word.Range rng = word_doc.Range(ref start, ref end);

                    rng.InsertBefore("Статистика отработанного времени");
                    rng.Font.Name = "Times New Roman";
                    rng.Font.Size = 12;
                    rng.InsertParagraphAfter();
                    rng.InsertParagraphAfter();
                    rng.SetRange(rng.End, rng.End);

                    cmd = new SqlCommand($"SELECT COUNT(*) FROM tab", con);
                    int rows = Convert.ToInt32(cmd.ExecuteScalar());

                    rng.Tables.Add(word_doc.Paragraphs[2].Range, rows, 4, ref missing, ref missing);

                    Word.Table tbl = word_doc.Tables[1];
                    tbl.Range.Font.Size = 9;
                    tbl.Range.Font.Name = "Times New Roman";
                    tbl.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    tbl.Columns.DistributeWidth();

                    SqlDataAdapter adapter = new SqlDataAdapter($"SELECT users.F + ' ' + users.I + ' ' + users.O as 'ФИО', users.ier as 'Должность', tab.total as 'Всего' FROM tab " +
                                                                $"JOIN users ON users.ID = tab.user_ID " +
                                                                $"WHERE users.toreg = 'True'", con);
                    DataSet data = new DataSet();
                    adapter.Fill(data);

                    cmd = new SqlCommand($"SELECT day1 FROM tab WHERE user_ID = '0'", con);
                    string day_end = cmd.ExecuteScalar().ToString();
                    string day_start = "";

                    for (int w = 30; w > 1;)
                    {
                        cmd = new SqlCommand($"SELECT day{w} FROM tab WHERE user_ID = '0'", con);
                        day_start = cmd.ExecuteScalar().ToString();
                        if (day_start.Contains("no date") || day_start == "")
                            w--;
                        else
                            break;
                    }

                    tbl.Cell(1, 1).Range.Text = "№";
                    tbl.Cell(1, 2).Range.Text = "Должность";
                    tbl.Cell(1, 3).Range.Text = "Сотрудник";
                    tbl.Cell(1, 4).Range.Text = "Отработано за период\nот " + day_start + " до " + day_end;

                    for (int q = 2; q <= rows; q++)
                    {
                        tbl.Cell(q, 1).Range.Text = (q - 1).ToString();
                        tbl.Cell(q, 2).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[1].ToString();
                        tbl.Cell(q, 3).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[0].ToString();
                        tbl.Cell(q, 4).Range.Text = data.Tables[0].Rows[q - 2].ItemArray[2].ToString();
                    }

                    object filename = dialog.SelectedPath + @"\" + "Статистика отработанного времени";
                    word_doc.SaveAs(ref filename, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing);

                    //object save_changes = false;
                    //word_doc.Close(ref save_changes, ref missing, ref missing);
                    //word_app.Quit(ref save_changes, ref missing, ref missing);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
