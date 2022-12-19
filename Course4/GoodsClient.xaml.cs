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
    /// Логика взаимодействия для GoodsClientxaml.xaml
    /// </summary>
    public partial class GoodsClient : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string ORDER_ID;
        int ID;
        DataTable dt = new DataTable();
        public GoodsClient(int iD_CLIENT)
        {
            InitializeComponent();
            ID = iD_CLIENT;
            connect = new NpgsqlConnection(connection.constring);
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Good as \"Код товара\", Good_Name as \"Название товара\", Good_Material as \"Материал\", Good_Price as \"Цена, руб\", Certification_Bureau as \"Сертифицирован\" FROM Good join Certificate on Certificate_ID=ID_Certificate where Status_ID=1", connect);

            dt.Load(command.ExecuteReader());
            dg_goodsclient.ItemsSource = dt.DefaultView;
            connect.Close();
        }
        public NpgsqlConnection connect { get; }

        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            string com = $"select ID_Good as \"Код товара\", Good_Name as \"Название товара\", Good_Material as \"Материал\", Good_Price as \"Цена, руб\", Certification_Bureau as \"Сертифицирован\" FROM Good join Certificate on Certificate_ID=ID_Certificate where Status_ID=1 and Good.Good_Name like '%{tb_src.Text}%'";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_goodsclient.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }
        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_buy_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            DataRowView row = (DataRowView)dg_goodsclient.SelectedItem;
            NpgsqlCommand command3 = new NpgsqlCommand($@"Select count(*) from Orderr where Client_ID={ID} and Payment='false'", connect);
            if (row != null)
            {
                if (!tb_amount.Text.StartsWith("0") && tb_amount!=null)
                {
                    if (command3.ExecuteScalar().ToString() == "0")
                    {
                        NpgsqlCommand command2 = new NpgsqlCommand($@"Call Orderr_Insert('{DateTime.Now.ToString()}', 'false', 'false', '{ID}')", connect);
                        command2.ExecuteNonQuery();

                    }
                    try { 
                    NpgsqlCommand command5 = new NpgsqlCommand($@"Select ID_Orderr from Orderr where Client_ID={ID} and Payment='false'", connect);
                    string Order_ID = command5.ExecuteScalar().ToString();
                    ORDER_ID = Order_ID;
                    NpgsqlCommand command4 = new NpgsqlCommand($@"Call OrderGood_Insert('{tb_amount.Text}','{ORDER_ID}', '{(int)row["Код товара"]}')", connect);                
                    command4.ExecuteNonQuery();
                    MessageBox.Show("Товар добавлен в корзину");
                    tb_amount.Text = "";
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
            connect.Close();
        }
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_shoppingcart_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new ShoppingCart(ID, ORDER_ID));
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
