using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MenuWarehouse.xaml
    /// </summary>
    public partial class MenuWarehouse : Page
    {
        int id;
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public MenuWarehouse(int iD_EMPLOYEE)
        {
            InitializeComponent();
            id = iD_EMPLOYEE;
        }

        private void btn_warehouses_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Storage(id));
        }

        private void btn_shippings_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Shipping(id));
        }
    }
}
