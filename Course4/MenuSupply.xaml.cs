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
    /// Логика взаимодействия для MenuSupply.xaml
    /// </summary>
    public partial class MenuSupply : Page
    {
        int id;
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        public MenuSupply(int iD_EMPLOYEE)
        {
            InitializeComponent();
            id = iD_EMPLOYEE;
        }


        private void btn_certificates_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Certificate(id));
        }

        private void btn_goods_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new GoodsStaff(id));
        }

        private void btn_supplies_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Supply(id));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Authorization());
        }
    }
}
