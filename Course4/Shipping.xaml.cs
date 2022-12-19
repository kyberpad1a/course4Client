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
    /// Логика взаимодействия для Shipping.xaml
    /// </summary>
    public partial class Shipping : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        int ID;
        public NpgsqlConnection connect { get; }

        public Shipping(int id)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            BindComboBox();
            ID = id;
        }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Shipping as \"Код накладной\", Shipping_Date as \"Дата накладной\", Supply_ID as \"Код поставки\", Warehouse_Address as \"Адрес склада\", concat(Personel.Personel_Surname, ' ', Personel.Personel_Name, ' ', Personel.Personel_Patronymic) as \"Добавил\" from Shipping join Supply on Supply_ID=ID_Supply join Personel on Shipping.Personel_ID=ID_Personel join Warehouse on Shipping.Warehouse_ID=ID_Warehouse", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_shipping.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Привязка комбобоксов
        /// </summary>
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter("select * from supply", connect);
            NpgsqlDataAdapter.Fill(datatbl1);
            cb_supply.ItemsSource = datatbl1.DefaultView;
            cb_supply.DisplayMemberPath = "id_supply";
            cb_supply.SelectedValuePath = "id_supply";
            DataTable datatbl2 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter2 = new NpgsqlDataAdapter("select * from warehouse", connect);
            NpgsqlDataAdapter2.Fill(datatbl2);
            cb_address.ItemsSource = datatbl2.DefaultView;
            cb_address.DisplayMemberPath = "warehouse_address";
            cb_address.SelectedValuePath = "id_warehouse";

            connect.Close();

        }
        /// <summary>
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg_shipping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_shipping.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_shipping.SelectedItem;
            dp_shippingdate.Text = row["Дата накладной"].ToString();
            cb_address.Text = row["Адрес склада"].ToString();
            cb_supply.Text = row["Код поставки"].ToString();
        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (cb_address.SelectedValue != null && dp_shippingdate.SelectedDate != null && cb_supply.Text != null)
            {

                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"Call Shipping_Insert('{dp_shippingdate.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}', '{cb_supply.SelectedValue}', '{cb_address.SelectedValue}', '{ID}')", connect);
                    command.ExecuteNonQuery();

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
        /// <summary>
        /// обновление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_shipping.SelectedItem;

            if (row != null)
            {
                if (cb_address.SelectedValue != null && dp_shippingdate.SelectedDate != null && cb_supply.Text != null)
                {
                    try { 
                    connect.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand($@"Call Shipping_Update({(int)row["Код накладной"]}, '{dp_shippingdate.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}', '{cb_supply.SelectedValue}' ,'{cb_address.SelectedValue}', '{ID}')", connect);
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
        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_shipping.SelectedItem;
            if (row != null)
            {
                try { 
                connect.Open();



                string com = $@"call Shipping_Delete ({(int)row["Код накладной"]})";
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
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.GoBack();
        }


    }
}
