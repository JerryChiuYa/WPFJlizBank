using BankLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// ForgetPasswordPage1.xaml 的互動邏輯
    /// </summary>
    public partial class ForgetPasswordPage1 : Page
    {
        private string _dbConnStr;

        public ForgetPasswordPage1()
        {
            InitializeComponent();
            _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        }

        private void SendCode_Click(object sender, RoutedEventArgs e)
        {
            var service = new CustomerServices(_dbConnStr);
            
            var data = service.VerifyIdentity(UserName.Text, Email.Text);

            if (data.Count >= 1)
            {
                //產生驗證碼
                string codes = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                Random random= new Random();
                string verifyCode = string.Empty;
                for (int i = 0; i < 8; i++)
                {
                    int index=random.Next(codes.Length);
                    verifyCode+=codes[index];
                }
                //發送Email驗證碼
                using (MailMessage mailMessage=new MailMessage())
                {
                    mailMessage.From = new MailAddress("jerry384052@gmail.com");
                    mailMessage.To.Add(Email.Text);
                    mailMessage.Subject = "傑力士商業銀行---重新設定密碼--驗證碼";
                    mailMessage.Body = $"您的驗證碼為 {verifyCode}";

                    //使用 SMTP 寄送 Email
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential("jerry384052@gmail.com", "xiwjmiggmzfbodvr");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);

                    }
                }
                //移至輸入驗證碼頁面
                MessageBox.Show("已成功傳送驗證碼");
                NavigationService.Navigate(new ForgetPasswordPage2(data, verifyCode));
            }
             
            else
            {
                Msg.Content = "此帳號或是Email並不存在於本行系統中,請重新輸入!";
            }
        }
    }
}
