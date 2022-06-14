using BankLibrary.BankEntity;
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
    /// CustomerPersonalWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CustomerPersonalWindow : Window
    {
       
        public CustomerPersonalWindow(BankPersonalInfo SelectedCustomer)
        {
            InitializeComponent();

            BankInfoList.ItemsSource = SelectedCustomer.bankInfoList;
        }

        private void BankInfoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BankInfoList.SelectedItem!=null)
            {
                //var personalAccount=(BankAccount)BankInfoList.SelectedItem;
                //var tranWindow = new CustomerTransactionWindow(personalAccount);
                //tranWindow.ShowDialog();
            }
            
        }
    }
}
