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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFJlizBank
{
    /// <summary>
    /// ForgetPassword2.xaml 的互動邏輯
    /// </summary>
    public partial class ForgetPasswordPage2 : Page
    {
        private string _dbConnStr;
        private string verifyCode;
        private ObservableCollection<BankPersonalInfo> data;

        public ForgetPasswordPage2(ObservableCollection<BankPersonalInfo> data, string verifyCode)
        {
            InitializeComponent();
            foreach (var item in data)
            {
                showMail.Content += item.Email;
            }
            this.data = data;
            
            _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
            this.verifyCode = verifyCode;
        }

        private void LastPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ForgetPasswordPage1());
        }

        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            if (UserTypeCode.Text==verifyCode)
            {
                NavigationService.Navigate(new ForgetPasswordPage3(data));
            }
            else
            {
                Msg.Content = "驗證碼錯誤,請重新輸入!";
            }
        }
    }
}
