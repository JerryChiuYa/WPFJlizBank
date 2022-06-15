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
    /// VerifyCodeWindow.xaml 的互動邏輯
    /// </summary>
    public partial class VerifyCodeWindow : Window
    {
        private BankPersonalInfo _currentAccount;
        private TransactionRecordsDetails _shortTransaction;
        private string _verifyCode;
        private string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        private BankAccount bankinfo;

        public VerifyCodeWindow(BankPersonalInfo currentAccount, TransactionRecordsDetails shortTransaction, string verifyCode)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            _shortTransaction = shortTransaction;
            _verifyCode = verifyCode;
            foreach (var item in _currentAccount.bankInfoList)
            {
                bankinfo=item;
            }
            this.YourAccount.DataContext = bankinfo;
            this.ToBankName.Text = _shortTransaction.ToBankName;
            this.ToAccountNum.Text = _shortTransaction.ToAccountNum;
            this.Remark.Text = _shortTransaction.Remark;
            this.TransactionMoney.Text = _shortTransaction.TransactionMoney.ToString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputCode.Text != _verifyCode)
            {
                MessageBox.Show("Your VeriyCode is incorrect, please try again!!", "Wrong Code", MessageBoxButton.OK);
                return;
            }
            var services = new CustomerServices(_dbConnStr);
            var transactionDetails=services.TransferMoneyService(bankinfo, _shortTransaction);
            this.Close();

            var succuss = new TransactionResult(_currentAccount, transactionDetails);
            succuss.ShowDialog();

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new HomeWindow(_currentAccount).ShowDialog();
        }
    }
}
