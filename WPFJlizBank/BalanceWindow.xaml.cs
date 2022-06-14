using BankLibrary.BankEntity;
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
using System.Windows.Shapes;

namespace WPFJlizBank
{
    /// <summary>
    /// BalanceWindow.xaml 的互動邏輯
    /// </summary>
    public partial class BalanceWindow : Window
    {
        private BankPersonalInfo _currentAccount;

        public BalanceWindow(BankPersonalInfo currentAccount)
        {
            InitializeComponent();
            _currentAccount = currentAccount;

            foreach (var item in currentAccount.bankInfoList)
            {
                this.YourAccount.DataContext = item;
            }
        }

        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            var balanceWin = new CustomerTransactionWindow(_currentAccount);
            balanceWin.ShowDialog();
        }
    }
}
