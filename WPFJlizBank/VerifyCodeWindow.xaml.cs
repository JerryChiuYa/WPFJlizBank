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
        private TransactionRecordsDetails _transactionRecordsDetails;
        private string _verifyCode;
        private string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        private BankAccount bankinfo;

        public VerifyCodeWindow(BankPersonalInfo currentAccount, TransactionRecordsDetails transactionRecordsDetails, string verifyCode)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            _transactionRecordsDetails = transactionRecordsDetails;
            _verifyCode = verifyCode;
            foreach (var item in _currentAccount.bankInfoList)
            {
                bankinfo=item;
            }
            this.YourAccount.DataContext = bankinfo;
            this.ToBankName.Text = transactionRecordsDetails.ToBankName;
            this.ToAccountNum.Text = transactionRecordsDetails.ToAccountNum;
            this.Remark.Text = transactionRecordsDetails.Remark;
            this.TransactionMoney.Text = transactionRecordsDetails.TransactionMoney.ToString();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (this.inputCode.Text != _verifyCode)
            {
                MessageBox.Show("Your VeriyCode is incorrect, please try again!!", "Wrong Code", MessageBoxButton.OK);
                return;
            }
            var services = new CustomerServices(_dbConnStr);
            services.TransferMoneyService(bankinfo, _transactionRecordsDetails);

            var succuss = new TransactionResult(_currentAccount, _transactionRecordsDetails);
            succuss.ShowDialog();
            this.Close();
        }
    }
}
