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
    /// Логика взаимодействия для GoodsStaff.xaml
    /// </summary>
    public partial class GoodsStaff : Page
    {
        ConString connection = new ConString();
        int ID;
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        DataTable dt = new DataTable();
        Microsoft.Office.Interop.Excel.Application excel;
        Microsoft.Office.Interop.Excel.Workbook workBook;
        Microsoft.Office.Interop.Excel.Worksheet workSheet;
        Microsoft.Office.Interop.Excel.Range cellRange;
        public GoodsStaff(int id)
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            Refresh();
            BindComboBox();
            ID = id;
        }
        public NpgsqlConnection connect { get; }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Good as \"Код товара\", Good_Name as \"Наименование\", Good_Material as \"Материал\", Good_Price as \"Цена товара\", Certification_Bureau as \"Сертифицирован\", Status_Name as \"Статус\" from Good join Certificate on Certificate_ID=ID_Certificate join Status on Status_ID=ID_Status", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_goodsstaff.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Привязка комбобоксов
        /// </summary>
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            NpgsqlDataAdapter NpgsqlDataAdapter = new NpgsqlDataAdapter("select * from certificate", connect);
            NpgsqlDataAdapter.Fill(datatbl1);
            cb_certificate.ItemsSource = datatbl1.DefaultView;
            cb_certificate.DisplayMemberPath = "certification_bureau";
            cb_certificate.SelectedValuePath = "id_certificate";


            connect.Close();

        }
        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_goodname.Text != null && tb_goodmaterial.Text !=null && tb_price.Text !=null && cb_certificate.SelectedValue!=null)
            {

                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand($@"Call Good_Insert('{tb_goodname.Text}', '{tb_goodmaterial.Text}', '{tb_price.Text}', '{cb_certificate.SelectedValue}', '2' )", connect);
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
            
            
            
            if (dg_goodsstaff.SelectedIndex != -1)
            {
                DataRowView row = (DataRowView)dg_goodsstaff.SelectedItem;
                string status = row["Статус"].ToString();
                if (tb_goodname.Text != null && tb_goodmaterial.Text != null && tb_price.Text != null && cb_certificate.SelectedValue != null)
                {
                    try
                    {
                        connect.Open();
                        NpgsqlCommand command2 = new NpgsqlCommand($@"Call Good_Update({(int)row["Код товара"]}, '{tb_goodname.Text}', '{tb_goodmaterial.Text}', '{tb_price.Text}', '{cb_certificate.SelectedValue}', '2')", connect);
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
            DataRowView row = (DataRowView)dg_goodsstaff.SelectedItem;
            if (row != null)
            {
                try
                {
                    connect.Open();



                    string com = $@"call Good_Delete ({(int)row["Код товара"]})";
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
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void dg_goodsstaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_goodsstaff.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg_goodsstaff.SelectedItem;
            tb_goodname.Text = row["Наименование"].ToString();
            tb_goodmaterial.Text = row["Материал"].ToString();
            tb_price.Text = row["Цена товара"].ToString();
            cb_certificate.Text = row["Сертифицирован"].ToString();

        }
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select ID_Good as \"Код товара\", Good_Name as \"Наименование\", Good_Material as \"Материал\", Good_Price as \"Цена товара\", Certification_Bureau as \"Сертифицирован\", Status_Name as \"Статус\" from Good join Certificate on Certificate_ID=ID_Certificate join Status on Status_ID=ID_Status where Good.Good_Name like '%{tb_src.Text}%'", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_goodsstaff.ItemsSource = datatbl.DefaultView;
            connect.Close();
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
        /// Экспорт
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_export_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcel(dt);
        }
        /// <summary>
        /// Экспорт
        /// </summary>
        /// <param name="DtIN">Дататэйбл с данными</param>
        private void GenerateExcel(DataTable DtIN)
        {
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.DisplayAlerts = false;
                excel.Visible = false;
                workBook = excel.Workbooks.Add(Type.Missing);
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
                workSheet.Name = "LearningExcel";
                System.Data.DataTable tempDt = DtIN;
                dg_goodsstaff.ItemsSource = tempDt.DefaultView;
                workSheet.Cells.Font.Size = 11;
                int rowcount = 1;
                for (int i = 1; i <= tempDt.Columns.Count; i++) //taking care of Headers.  
                {
                    workSheet.Cells[1, i] = tempDt.Columns[i - 1].ColumnName;
                }
                foreach (System.Data.DataRow row in tempDt.Rows) //taking care of each Row  
                {
                    rowcount += 1;
                    for (int i = 0; i < tempDt.Columns.Count; i++) //taking care of each column  
                    {
                        workSheet.Cells[rowcount, i + 1] = row[i].ToString();
                    }
                }
                cellRange = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rowcount, tempDt.Columns.Count]];
                cellRange.EntireColumn.AutoFit();
                excel.Visible = true;
                excel.UserControl = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Переход
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void btn_Graph_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new GoodsGraph());
        }
    }
}
