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
    /// TransferMoneyWindow.xaml 的互動邏輯
    /// </summary>
    public partial class TransferMoneyWindow : Window
    {
        private BankPersonalInfo _currentAccount;
        private BankAccount _accountDetails;
        private TransactionRecordsDetails _transactionRecordsDetails;
        private string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;

        public TransferMoneyWindow(BankPersonalInfo currentAccount)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            foreach (var item in _currentAccount.bankInfoList)
            {
                _accountDetails = item;
                this.YourAccount.DataContext = item;
            }
            _transactionRecordsDetails = new TransactionRecordsDetails();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {
            if (_transactionRecordsDetails.ToBankName == "-----請選擇分行-----" || string.IsNullOrEmpty(_transactionRecordsDetails.ToBankName))
            {
                MessageBox.Show("請選擇分行!!");
                return;
            }
            if (this.ToAccountNum.Text=="")
            {
                MessageBox.Show("請輸入金額!!");
                return;
            }
            if (decimal.Parse(TransactionMoney.Text)>50000 || decimal.Parse(TransactionMoney.Text) <= 0 || string.IsNullOrEmpty(TransactionMoney.Text))
            {
                MessageBox.Show("請輸入1~50000的金額!!");
                return;
            }
            if (decimal.Parse(TransactionMoney.Text) > _accountDetails.AccountBalance)
            {
                MessageBox.Show("您的轉帳金額超過帳戶餘額,請重新輸入!!");
                return;
            }
            var services = new CustomerServices(_dbConnStr);
            var toAccount=services.GetBankAccount(this.ToAccountNum.Text);
            if (toAccount==null || toAccount.BankName!=this.BankList.SelectedValue.ToString())
            {
                MessageBox.Show("此帳號或銀行名稱有誤,請重新輸入!!");
                return;
            }
            _transactionRecordsDetails.TransactionMoney = decimal.Parse(TransactionMoney.Text);
            _transactionRecordsDetails.ToAccountNum = this.ToAccountNum.Text;
            _transactionRecordsDetails.ToBankName= this.BankList.SelectedValue.ToString();
            _transactionRecordsDetails.Remark = this.Remark.Text;

            //var verifyCode = "123";
            var verifyCode = GetCodeViaEmail.GetVerifyCode(_currentAccount.Email);
            this.Close();
            var window = new VerifyCodeWindow(_currentAccount, _transactionRecordsDetails, verifyCode);
            window.ShowDialog();
        }

        private void BankList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.BankList.SelectedItem != null)
            {
                _transactionRecordsDetails.ToBankName = this.BankList.SelectedValue.ToString();
            }
        }
    }
}
