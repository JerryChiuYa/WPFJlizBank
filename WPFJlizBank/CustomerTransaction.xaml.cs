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
    /// CustomerTransaction.xaml 的互動邏輯
    /// </summary>
    public partial class CustomerTransactionWindow : Window
    {
        private BankAccount personalAccount { get; set; }
        public string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        public CustomerTransactionWindow(BankAccount personalAccount)
        {
            InitializeComponent();
            this.personalAccount = personalAccount;
        }

        private void QueryTransaction_Click(object sender, RoutedEventArgs e)
        {
            var start = sDate.SelectedDate.Value.Date;
            var end=eDate.SelectedDate.Value.Date;
            TransactionList.ItemsSource = personalAccount.GetTransactionList(start, end);

            ShowTime.Text = $"查詢期間為 {start.ToShortDateString()} ~ {end.ToShortDateString()}";
        }
    }
}
