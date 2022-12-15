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
    /// Логика взаимодействия для ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        string ORDERID;
        int ID;
        DataTable dt = new DataTable();
        public ShoppingCart(int iD_CLIENT, string ORDER_ID)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            ID = iD_CLIENT;
            ORDERID = ORDER_ID;
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_OrderGood as \"Код товара\", Good_Name as \"Название товара\", Good_Price as \"Цена, руб\", OrderGood_Amount as \"Количество\" FROM OrderGood join Good on Good_ID=ID_Good join Orderr on Order_ID=ID_Orderr where Client_ID={ID} and payment_ready = false", connect);
            dt.Load(command.ExecuteReader());
            dg_shoppingcart.ItemsSource = dt.DefaultView;
            command = new NpgsqlCommand($"select Good_Price FROM OrderGood join Good on Good_ID=ID_Good join Orderr on Order_ID=ID_Orderr where Client_ID={ID} and payment_ready = false", connect);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            double amount = 0;
            while (dataReader.Read())
            {
                amount = Convert.ToDouble(dataReader["good_price"].ToString());
            }
            tb_total.Text = amount.ToString();
            dataReader.Close();
            connect.Close();
        }
        public NpgsqlConnection connect { get; }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new GoodsClient(ID));
        }

        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_OrderGood as \"Код товара\", Good_Name as \"Название товара\", Good_Price as \"Цена, руб\", OrderGood_Amount as \"Количество\" FROM OrderGood join Good on Good_ID=ID_Good join Orderr on Order_ID=ID_Orderr where Client_ID={ID}", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_shoppingcart.ItemsSource = dt.DefaultView;
            command = new NpgsqlCommand($"select Good_Price FROM OrderGood join Good on Good_ID=ID_Good join Orderr on Order_ID=ID_Orderr where Client_ID={ID} and payment_ready = false", connect);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            double amount = 0;
            while (dataReader.Read())
            {
                amount = Convert.ToDouble(dataReader["good_price"].ToString());
            }
            tb_total.Text = amount.ToString();
            dataReader.Close();
            connect.Close();

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
           
            DataRowView row = (DataRowView)dg_shoppingcart.SelectedItem;
            if (row != null)
            {
                connect.Open();



                NpgsqlCommand command = new NpgsqlCommand($@"Call OrderGood_Delete({(int)row["Код товара"]})", connect);
                command.ExecuteNonQuery();

                connect.Close();
                Refresh();
            }

        }

        List<string> IDGoods = new List<string>();
        private void btn_buy_Click(object sender, RoutedEventArgs e)
        {
          
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($@"select id_orderr FROM OrderGood join Good on Good_ID=ID_Good join Orderr on Order_ID=ID_Orderr where Client_ID={ID}", connect);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    IDGoods.Add(dataReader["id_orderr"].ToString());
                }
            dataReader.Close();
            foreach (string id in IDGoods)
            {
                command = new NpgsqlCommand($@"UPDATE Orderr SET payment_ready = true where id_orderr = {id};", connect);
                command.ExecuteNonQuery();
            }
                MessageBox.Show("Следуйте указаниям продавца");
                    connect.Close();
                Mw.MainFrame.NavigationService.Navigate(new GoodsClient(ID));
            
        }
    }
}
