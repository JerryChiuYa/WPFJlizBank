using BankLibrary.BankEntity;
using BankLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<BankPersonalInfo> _currentAccount { get; set; }
        public string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        public CustomerTransactionWindow(ObservableCollection<BankPersonalInfo> currentAccount)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
        }

        private void QueryTransaction_Click(object sender, RoutedEventArgs e)
        {
            var start = sDate.SelectedDate;
            var end = eDate.SelectedDate;
            if ( !start.HasValue || !end.HasValue || start > end )
            {
                ErrorMsg.Text = $"開始時間需小於結束時間, 且不得為空!";
                return;
            }
            var startTime=DateTime.Parse(start.ToString()).ToShortDateString();
            var endTime = DateTime.Parse(end.ToString()).ToShortDateString();

            foreach (var item in _currentAccount)
            {
                foreach (var item2 in item.bankInfoList)
                {
                    TransactionList.ItemsSource = item2.GetTransactionList(DateTime.Parse(start.ToString()), DateTime.Parse(end.ToString()));
                }
                
            }

            ShowTime.Text = $"查詢期間為 {startTime} ~ {endTime}";
        }
    }
}
