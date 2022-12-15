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
    /// Логика взаимодействия для Supply.xaml
    /// </summary>
    public partial class Supply : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        int ID;
        public Supply(int id)
        {
            
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            BindComboBox();
            ID = id;
        }

        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Supply as \"Код поставки\", Good_Name as \"Наименование товара\", SuppliedGood_Quantity as \"Количество товара\", Supply_Total as \"Итого\", concat(Personel.Personel_Surname, ' ', Personel.Personel_Name, ' ', Personel.Personel_Patronymic) as \"Добавил\" from Supply join Good on Good_ID=ID_Good join Personel on Personel_ID=ID_Personel", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_supply.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter("select * from good", connect);
            NpgsqlDataAdapter.Fill(datatbl1);
            cb_good.ItemsSource = datatbl1.DefaultView;
            cb_good.DisplayMemberPath = "good_name";
            cb_good.SelectedValuePath = "id_good";


            connect.Close();

        }

        public NpgsqlConnection connect { get; }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (cb_good.SelectedValue != null && tb_total.Text != null && tb_goodquantity.Text != null)
            {


                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($@"Call Supply_Insert('{tb_goodquantity.Text}', '{tb_total.Text}', '{cb_good.SelectedValue}', '{ID}')", connect);
                command.ExecuteNonQuery();

                connect.Close();
                Refresh();

            }
        }

        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_supply.SelectedItem;

            if (row != null)
            {
                if (cb_good.SelectedValue != null && tb_total.Text != null && tb_goodquantity.Text != null)
                {
                    connect.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand($@"Call Supply_Update({(int)row["Код поставки"]}, '{tb_goodquantity.Text}', '{tb_total.Text}', '{cb_good.SelectedValue}', '{ID}')", connect);
                    command2.ExecuteNonQuery();

                    connect.Close();
                    Refresh();
                }
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_supply.SelectedItem;
            if (row != null)
            {
                connect.Open();



                string com = $@"call Supply_Delete ({(int)row["Код поставки"]})";
                NpgsqlCommand command2 = new NpgsqlCommand(com, connect);
                command2.ExecuteNonQuery();

                connect.Close();
                Refresh();

            }
        }

        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            string com = $"select ID_Supply as \"Код поставки\", Good_Name as \"Наименование товара\", SuppliedGood_Quantity as \"Количество товара\", Supply_Total as \"Итого\", concat(Personel.Personel_Surname, ' ', Personel.Personel_Name, ' ', Personel.Personel_Patronymic) as \"Добавил\" from Supply join Good on Good_ID=ID_Good join Personel on Personel_ID=ID_Personel where Good.Good_Name like '%{tb_src.Text}%'";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_supply.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new MenuSupply(ID));
        }

        private void dg_supply_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_supply.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_supply.SelectedItem;
            tb_total.Text = row["Итого"].ToString();
            tb_goodquantity.Text = row["Количество товара"].ToString();
            cb_good.Text = row["Наименование товара"].ToString();
        }
    }
}
