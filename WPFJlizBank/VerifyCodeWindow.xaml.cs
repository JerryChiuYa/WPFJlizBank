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
    /// VerifyCodeWindow.xaml 的互動邏輯
    /// </summary>
    public partial class VerifyCodeWindow : Window
    {
        private BankPersonalInfo _currentAccount;
        private string _verifyCode;

        public VerifyCodeWindow(BankPersonalInfo currentAccount, string verifyCode)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            _verifyCode = verifyCode;

            if (this.inputCode.Text== _verifyCode)
            {

            }
        }
    }
}
