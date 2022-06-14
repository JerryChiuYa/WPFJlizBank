using BankLibrary.BankEntity;
using BankLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace WPFJlizBank
{
    /// <summary>
    /// CustomerWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private string _dbConnStr;

        public CustomerWindow()
        {
            InitializeComponent();
            _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
            GetCustomerData();
        }

        private void GetCustomerData()
        {
            CustomerServices services = new CustomerServices(_dbConnStr);
            var data=services.GetPersonalInfo();
            CustomerInfoList.ItemsSource = data;
        }

        private void CustomerInfoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerInfoList.SelectedItem!=null)
            {
                var SelectedCustomer = (BankPersonalInfo)CustomerInfoList.SelectedItem;
                var personalCustomer = new CustomerPersonalWindow(SelectedCustomer);
                personalCustomer.ShowDialog();
            }
            
        }
    }
}



