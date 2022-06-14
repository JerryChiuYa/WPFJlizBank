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
    /// LoginWindow.xaml 的互動邏輯
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string _dbConnStr;

        public LoginWindow()
        {
            InitializeComponent();
            _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            var loginService = new LoginService(_dbConnStr);
            var result = loginService.VerifyUser(this.userName.Text, this.password.Password);

            if (result)
            {
                var allAccounts = new CustomerServices(_dbConnStr).GetAllAccountsInfo(this.userName.Text);
                var currentAccount = new CustomerServices(_dbConnStr).GetCurrentAccountInfo(this.userName.Text);
                var homeWindow = new HomeWindow(allAccounts, currentAccount);
                homeWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Your UserName or password is incorrect, please try again!!", "Wrong Id or Password", MessageBoxButton.OK);
                Reset_Click(sender, e);
            }

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.userName.Clear();
            this.password.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.ShowDialog();
        }

        private void ForgetPwd_Click(object sender, RoutedEventArgs e)
        {
            var forgetPwd=new ForgetPassword();
            forgetPwd.ShowDialog();
        }
    }
}
