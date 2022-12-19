using Npgsql;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public Registration()
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Шифрование
        /// </summary>
        /// <param name="decrypted">Строка для шифрования</param>
        /// <returns>Зашифрованная строка</returns>
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
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void tb_registration_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidPassword(tb_password.Password) != true)
            {
                MessageBox.Show("Пароль не соответствет условию");
                return;
            }
            connect.Open();
            NpgsqlCommand com = new NpgsqlCommand("", connect);
            com = new NpgsqlCommand($"select count(*) from Client where Client_Login = '{tb_login.Text}'", connect);
            if (com.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("Такой логин уже существует");
                return;
            }
            if (tb_login.Text != "" && tb_password.Password != "" && tb_name.Text != "" && tb_surname.Text != "")
            {
                NpgsqlCommand command = new NpgsqlCommand($@"Call Client_Insert('{tb_login.Text}','{Encrypt(tb_password.Password)}','{tb_surname.Text}','{tb_name.Text}', '{tb_patronymic.Text}')", connect);
                command.ExecuteNonQuery();
                connect.Close();
            }
            Mw.MainFrame.NavigationService.Navigate(new Authorization());
        }
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Authorization());
        }
        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="pswd">пароль</param>
        /// <returns>результат проверки</returns>
        public static bool IsValidPassword(string pswd)
        {


            bool b1 = pswd.Length >= 8;


            bool b3 = false;
            foreach (char c in pswd)
            {
                if (Char.IsDigit(c))
                {
                    b3 = true;
                    break;
                }
            }

            return b1 && b3;
        }
    }
}
