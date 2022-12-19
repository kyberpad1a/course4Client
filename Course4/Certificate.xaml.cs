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
    /// Логика взаимодействия для Certificate.xaml
    /// </summary>
    public partial class Certificate : Page
    {
        
        ConString connection = new ConString();
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        int ID;
        public Certificate(int id)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            ID = id;
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Certificate as \"Код сертификата\", Certification_Bureau as \"Сертифицирован\" from Certificate", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_certificates.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_bureau.Text != null)
            {

                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"Call Certificate_Insert('{tb_bureau.Text}')", connect);
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
            DataRowView row = (DataRowView)dg_certificates.SelectedItem;
            if (row != null)
            {
                if (tb_bureau.Text != null)
                {
                    try
                    {
                        connect.Open();
                        NpgsqlCommand command2 = new NpgsqlCommand($@"Call Certificate_Update({(int)row["Код сертификата"]}, '{tb_bureau.Text}')", connect);
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
            DataRowView row = (DataRowView)dg_certificates.SelectedItem;
            if (row != null)
            {
                try
                {
                    connect.Open();



                    string com = $@"call Certificate_Delete ({(int)row["Код сертификата"]})";
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

                Mw.MainFrame.NavigationService.Navigate(new MenuSupply(ID));
            
        }
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            string com = $"select ID_Certificate as \"Код сертификата\", Certification_Bureau as \"Сертифицирован\" from Certificate where Certificate.Certification_Bureau like '%{tb_src.Text}%'";
            NpgsqlCommand command = new NpgsqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_certificates.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }
        /// <summary>
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg_certificates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_certificates.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_certificates.SelectedItem;
            tb_bureau.Text = row["Сертифицирован"].ToString();
        }
    }
}
