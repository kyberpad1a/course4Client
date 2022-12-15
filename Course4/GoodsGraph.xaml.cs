using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Course4
{
    public partial class GoodsGraph : Page
    {
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        //Названия товаров
        List<string> axisXData = new List<string>();
        //Цены товаров
        List<string> axisYData = new List<string>();
        ConString connection = new ConString();
        public NpgsqlConnection connect { get; }
        public GoodsGraph()
        {
            InitializeComponent();
            connect = new NpgsqlConnection(connection.constring);
            connect.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select good_name from good", connect);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                axisXData.Add(dataReader["good_name"].ToString());
            }
            dataReader.Close();
            command = new NpgsqlCommand("select good_price from good", connect);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                axisYData.Add(dataReader["good_price"].ToString());
            }
            dataReader.Close();
            connect.Close();

            chart.ChartAreas.Clear();
            chart.Series.Clear();
            // Все графики находятся в пределах области построения ChartArea, создадим ее
            chart.ChartAreas.Add(new ChartArea("Default"));
            // Добавим линию, и назначим ее в ранее созданную область "Default"
            chart.Series.Add(new Series("Ценообразование"));
            chart.Series["Ценообразование"].ChartArea = "Default";
            chart.Series["Ценообразование"].ChartType = SeriesChartType.Pie;
            chart.Series["Ценообразование"].IsValueShownAsLabel = true;
            chart.Series["Ценообразование"].Points.DataBindXY(axisXData, axisYData);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.GoBack(); //Фокусы
        }
    }
}
