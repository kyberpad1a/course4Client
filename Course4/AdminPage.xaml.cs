using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course4
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        public AdminPage(int iD_EMPLOYEE)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            BindComboBox();
        }
        public NpgsqlConnection connect { get; }

        /// <summary>
        /// Хэширование
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        public string Encrypt(string decrypted)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(decrypted);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
            tripDes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("123"));
            tripDes.Mode = CipherMode.ECB;
            ICryptoTransform transform = tripDes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result);
        }

        public void Refresh()
        {
            connect.Close();
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Personel as \"Код сотрудника\", Personel_Login as \"Логин\", Personel_Password as \"Пароль\", Personel_Surname as \"Фамилия\", Personel_Name as \"Имя\", Personel_Patronymic as \"Отчество\", Job_Name as \"Должность\" from Personel join Job on Job_ID=ID_Job", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_personel.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter("select * from job", connect);
            NpgsqlDataAdapter.Fill(datatbl1);
            cb_job.ItemsSource = datatbl1.DefaultView;
            cb_job.DisplayMemberPath = "job_name";
            cb_job.SelectedValuePath = "id_job";

            connect.Close();

        }

        private void dg_personel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_personel.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_personel.SelectedItem;
            tb_surname.Text = row["Фамилия"].ToString();
            tb_name.Text = row["Имя"].ToString();
            tb_patronymic.Text = row["Отчество"].ToString();
            tb_login.Text = row["Логин"].ToString();
            tb_password.Password = row["Пароль"].ToString();
            cb_job.Text = row["Должность"].ToString();


        }

        /// <summary>
        /// try catch!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (cb_job.SelectedValue != null && tb_surname.Text != null && tb_name.Text != null && tb_patronymic.Text != null && tb_login.Text != null && tb_password.Password !=null)
            {

                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"Call Personel_Insert('{tb_login.Text}', '{Encrypt(tb_password.Password)}','{tb_surname.Text}', '{tb_name.Text}', '{tb_patronymic.Text}', '{cb_job.SelectedValue}')", connect);
                    command.ExecuteNonQuery();
                    Refresh();

                }
                catch (NpgsqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                finally
                {
                    connect.Close();
                }

            }
        }

        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_personel.SelectedItem;

            if (row != null)
            {
                if (cb_job.SelectedValue != null && tb_surname.Text != null && tb_name.Text != null && tb_patronymic.Text != null && tb_login.Text != null && tb_password.Password != null)
                {
                    try
                    {
                        connect.Open();
                        string password = tb_password.Password;
                        if (new NpgsqlCommand($"select count(personel_password) from personel where personel_password = '{tb_password.Password}'", connect).ExecuteScalar().ToString() == "0")
                            password = Encrypt(tb_password.Password);
                        NpgsqlCommand command2 = new NpgsqlCommand($@"Call Personel_Update({(int)row["Код сотрудника"]}, '{tb_login.Text}', '{password}','{tb_surname.Text}', '{tb_name.Text}', '{tb_patronymic.Text}', '{cb_job.SelectedValue}')", connect);
                        command2.ExecuteNonQuery();

                        connect.Close();
                        Refresh();
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connect.Close();
                    }
            }
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_personel.SelectedItem;
            if (row != null)
            {
                try
                {
                    connect.Open();
                    string com = $@"call Personel_Delete ({(int)row["Код сотрудника"]})";
                    NpgsqlCommand command2 = new NpgsqlCommand(com, connect);
                    command2.ExecuteNonQuery();
                    connect.Close();
                    Refresh();
                }
                catch (NpgsqlException ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
                finally
                {
                    connect.Close();
                }

        }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Authorization());
        }

        private void btnDataBase_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new DataBaseController());
        }
    }
}

