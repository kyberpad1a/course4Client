using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string ORDERID;
        int ID;
        DataTable dt = new DataTable();
        public Seller(int iD_EMPLOYEE)
        {
            InitializeComponent();
            ID = iD_EMPLOYEE;
            connect = new NpgsqlConnection(connection.constring);
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Orderr as \"Код заказа\", Order_Date as \"Дата заказа\", concat(Client.Client_Surname, ' ', Client.Client_Name, ' ', Client.Client_Patronymic) as \"Клиент\", Client_ID as \"Код клиента\" FROM Orderr join Client on Client_ID=ID_Client where Payment_Ready='true' and payment = 'false'", connect);

            dt.Load(command.ExecuteReader());
            dg_orders.ItemsSource = dt.DefaultView;
            connect.Close();
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Orderr as \"Код заказа\", Order_Date as \"Дата заказа\", concat(Client.Client_Surname, ' ', Client.Client_Name, ' ', Client.Client_Patronymic) as \"Клиент\", Client_ID as \"Код клиента\" FROM Orderr join Client on Client_ID=ID_Client where Payment_Ready='true' and payment = 'false'", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_orders.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Подтверждение заказа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_orders.SelectedItem;
            if (row != null)
            {
                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"Call Orderr_Update({(int)row["Код заказа"]}, '{DateTime.Now.ToString()}', 'true', 'true', {(int)row["Код клиента"]}, {ID})", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                    Refresh();
                    MessageBox.Show("Оплата подтверждена");
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
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Authorization());
        }
    }
}
