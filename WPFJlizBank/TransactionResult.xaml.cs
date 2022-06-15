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
    /// TransactionResult.xaml 的互動邏輯
    /// </summary>
    public partial class TransactionResult : Window
    {
        private BankPersonalInfo _currentAccount;

        public TransactionResult(BankPersonalInfo currentAccount, TransactionRecordsDetails details)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            this.Results.DataContext = details;


        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var home = new HomeWindow(_currentAccount);
            home.ShowDialog();
        }
    }
}
