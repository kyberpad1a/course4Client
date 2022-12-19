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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        ConString connection = new ConString();

        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public Authorization()
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);


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

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_Auth_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (chkbox_personel.IsChecked == true)
            {
                connect.Open();
                NpgsqlCommand employee = new NpgsqlCommand($"select count(*) from Personel where Personel_Login = '{tb_login.Text}' and Personel_Password = '{Encrypt(tb_password.Password)}'", connect);

                if (employee.ExecuteScalar().ToString() == "1")
                {
                    NpgsqlCommand employeeid = new NpgsqlCommand($"select ID_Personel from Personel where Personel_Login = '{tb_login.Text}' and Personel_Password = '{Encrypt(tb_password.Password)}'", connect);
                    NpgsqlCommand postid = new NpgsqlCommand($"select Job_ID from Personel where Personel_Login = '{tb_login.Text}' and Personel_Password = '{Encrypt(tb_password.Password)}'", connect);
                    int ID_EMPLOYEE = (int)employeeid.ExecuteScalar();
                    if (postid.ExecuteScalar().ToString() == "1")
                    {


                        Mw.MainFrame.NavigationService.Navigate(new AdminPage(ID_EMPLOYEE));
                    }
                    if (postid.ExecuteScalar().ToString() == "2")
                    {

                        Mw.MainFrame.NavigationService.Navigate(new MenuSupply(ID_EMPLOYEE));
                    }
                    if (postid.ExecuteScalar().ToString() == "3")
                    {

                       Mw.MainFrame.NavigationService.Navigate(new Seller(ID_EMPLOYEE));
                    }
                    if (postid.ExecuteScalar().ToString() == "4")
                    {

                      Mw.MainFrame.NavigationService.Navigate(new MenuWarehouse(ID_EMPLOYEE));
                    }

                    if (postid.ExecuteScalar().ToString() == "5")
                    {

                       Mw.MainFrame.NavigationService.Navigate(new FinalCheck(ID_EMPLOYEE));
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте введенные данные");
                }
                connect.Close();
            }
            else
            {
                connect.Open();
                NpgsqlCommand client = new NpgsqlCommand($"select count(*) from Client where Client_Login = '{tb_login.Text}' and Client_Password = '{Encrypt(tb_password.Password)}'", connect);
                NpgsqlCommand clientid = new NpgsqlCommand($"select ID_Client from Client where Client_Login = '{tb_login.Text}' and Client_Password = '{Encrypt(tb_password.Password)}'", connect);
                if (client.ExecuteScalar().ToString() == "1")
                {
                    
                    
                    int ID_CLIENT = (int)clientid.ExecuteScalar();


                        Mw.MainFrame.NavigationService.Navigate(new GoodsClient(ID_CLIENT));
                }
                    else
                    {
                        MessageBox.Show("Проверьте введенные данные");
                    }
                connect.Close();
                
            }
        }
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Registration());
        }
    }
}
