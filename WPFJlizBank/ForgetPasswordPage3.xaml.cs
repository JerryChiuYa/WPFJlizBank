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
    /// ForgetPassword3.xaml 的互動邏輯
    /// </summary>
    public partial class ForgetPasswordPage3 : Page
    {
        private ObservableCollection<BankPersonalInfo> data;
        private string _dbConnStr;

        public ForgetPasswordPage3(ObservableCollection<BankPersonalInfo> data)
        {
            InitializeComponent();
            this.data = data;
            _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Guid custId = Guid.Empty;
            string account = string.Empty;
            if (Pwd1.Password!=Pwd2.Password)
            {
                Msg.Content = "密碼不一致,請再修改!";
                Pwd1.Password = "";
                Pwd2.Password = "";
            }
            else
            {
                foreach (var item in data)
                {
                    custId=item.CustomerId;
                    foreach (var item2 in item.bankInfoList)
                    {
                        account=item2.LoginAccount;
                    }
                }
                var hash = new HashService();
                var newPwd = hash.HashPwd(Pwd1.Password, custId.ToString().ToUpper());

                var service = new CustomerServices(_dbConnStr);
                service.ModifyPassword(account, newPwd);

                MessageBox.Show("密碼修改完成!");
            }
        }
    }
}
