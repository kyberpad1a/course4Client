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
    /// Логика взаимодействия для Storage.xaml
    /// </summary>
    public partial class Storage : Page
    {
        int ID;
        ConString connection = new ConString();
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        public Storage(int iD_EMPLOYEE)
        {
            ID = iD_EMPLOYEE;
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Warehouse as \"Код склада\", Warehouse_Address as \"Адрес склада\" from Warehouse", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_storages.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_address.Text != null)
            {

                try
                { 
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($@"Call Warehouse_Insert('{tb_address.Text}')", connect);
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
            DataRowView row = (DataRowView)dg_storages.SelectedItem;
            if (row != null)
            {
                if (tb_address.Text != null)
                {
                    try 
                    { 
                    connect.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand($@"Call Warehouse_Update({(int)row["Код склада"]}, '{tb_address.Text}')", connect);
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
            DataRowView row = (DataRowView)dg_storages.SelectedItem;
            if (row != null)
            {
                try 
                { 
                connect.Open();



                string com = $@"call Warehouse_Delete ({(int)row["Код склада"]})";
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

                Mw.MainFrame.NavigationService.Navigate(new MenuWarehouse(ID));
            
        }
        /// <summary>
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg_storages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_storages.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_storages.SelectedItem;
            tb_address.Text = row["Адрес склада"].ToString();
            
        }
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            string com = "select ID_Warehouse as \"Код склада\", Warehouse_Address as \"Адрес склада\" from Warehouse where Warehouse.Warehouse_Address like"+ $@"'%{tb_src.Text}%'";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_storages.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }
    }
}
