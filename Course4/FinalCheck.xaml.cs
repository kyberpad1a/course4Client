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
    /// Логика взаимодействия для FinalCheck.xaml
    /// </summary>
    public partial class FinalCheck : Page
    {
        ConString connection = new ConString();
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        int ID;
        public FinalCheck(int iD_EMPLOYEE)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            BindComboBox();
            ID = iD_EMPLOYEE;
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_FinalCheck as \"Код проверки\", FinalCheck_Date as \"Дата проверки\", Good_Name as \"Название товара\", Status_Name as \"Статус\", concat(Personel.Personel_Surname, ' ', Personel.Personel_Name, ' ', Personel.Personel_Patronymic) as \"Проверил\" from FinalCheck join Good on Good_ID=ID_Good join Personel on Personel_ID=ID_Personel join Status on FinalCheck.Status_ID=Status.ID_Status where LogicalDelete = true", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_check.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Привязка комбобоксов
        /// </summary>
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter("select * from good", connect);
            NpgsqlDataAdapter.Fill(datatbl1);
            cb_good.ItemsSource = datatbl1.DefaultView;
            cb_good.DisplayMemberPath = "good_name";
            cb_good.SelectedValuePath = "id_good";
            DataTable datatbl2 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter2 = new NpgsqlDataAdapter("select * from status", connect);
            NpgsqlDataAdapter2.Fill(datatbl2);
            cb_status.ItemsSource = datatbl2.DefaultView;
            cb_status.DisplayMemberPath = "status_name";
            cb_status.SelectedValuePath = "id_status";

            connect.Close();

        }
        /// <summary>
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg_check_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_check.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_check.SelectedItem;
            dp_checkdate.Text = row["Дата проверки"].ToString();
            cb_good.Text = row["Название товара"].ToString();
            cb_status.Text = row["Статус"].ToString();
        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_check.SelectedItem;
            if (cb_good.SelectedValue != null && dp_checkdate.SelectedDate != null && cb_status.SelectedValue != null)
            {


                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($@"Call FinalCheck_Insert('{dp_checkdate.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}', '{ID}','{cb_good.SelectedValue}', '{cb_status.SelectedValue}')", connect);
                NpgsqlCommand command2 = new NpgsqlCommand($@"Update Good Set status_id='{cb_status.SelectedValue}' where id_good='{cb_good.SelectedValue}'", connect);
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                connect.Close();
                Refresh();

            }
        }
        /// <summary>
        /// обновление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_check.SelectedItem;

            if (row != null)
            {
                if (cb_good.SelectedValue != null && dp_checkdate.SelectedDate != null && cb_status.Text != null)
                {
                    connect.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand($@"Call FinalCheck_Update({(int)row["Код проверки"]}, '{dp_checkdate.SelectedDate.Value.Date.ToString("yyyy.MM.dd")}',  '{ID}', '{cb_good.SelectedValue}', '{cb_status.SelectedValue}')", connect);
                    command2.ExecuteNonQuery();

                    connect.Close();
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Логическое удаление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_check.SelectedItem;
            if (row != null)
            {
                if (MessageBox.Show("Логическое удаление?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    connect.Open();
                    string com = $@"UPDATE FinalCheck SET LogicalDelete = false where ID_FinalCheck = {(int)row["Код проверки"]}";
                    NpgsqlCommand command2 = new NpgsqlCommand(com, connect);
                    command2.ExecuteNonQuery();
                    connect.Close();
                    Refresh();
                }
                else
                {
                    connect.Open();
                    string com = $@"call FinalCheck_Delete ({(int)row["Код проверки"]})";
                    NpgsqlCommand command2 = new NpgsqlCommand(com, connect);
                    command2.ExecuteNonQuery();
                    connect.Close();
                    Refresh();
                }

            }
        }

        /// <summary>
        /// Логическое восстановление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_recoveryLogicalDelete_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            string com = $@"UPDATE FinalCheck SET LogicalDelete = true";
            NpgsqlCommand command2 = new NpgsqlCommand(com, connect);
            command2.ExecuteNonQuery();
            connect.Close();
            Refresh();
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
