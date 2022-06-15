using BankLibrary.BankEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
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
    /// HomeWindow.xaml 的互動邏輯
    /// </summary>
    public partial class HomeWindow : Window
    {
        private BankPersonalInfo _currentAccount;
        public HomeWindow(BankPersonalInfo currentAccount)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            var cusWindow=new CustomerWindow();
            cusWindow.ShowDialog();
        }

        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            var tranWindow = new CustomerTransactionWindow(_currentAccount);
            tranWindow.ShowDialog();

        }

        private void Balance_Click(object sender, RoutedEventArgs e)
        {
            var balanceWin = new BalanceWindow(_currentAccount);
            balanceWin.ShowDialog();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            var tranWindow = new TransferMoneyWindow(_currentAccount);
            tranWindow.ShowDialog();
        }
    }
}
