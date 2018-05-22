using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace ExchangeRate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                string url = @"http://data.egov.kz/api/v2/valutalar_bagamdary4/v1";
                string json;

                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }

                List<Currency> currencyList = JsonConvert.DeserializeObject<List<Currency>>(json);
                Thread.Sleep(5000);

                DataGrid.Dispatcher.Invoke(new Action(() =>
                {
                    DataGrid.ItemsSource = currencyList;
                }));
            });
            thread.Start();
        }
    }
}
