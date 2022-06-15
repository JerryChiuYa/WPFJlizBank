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
    /// CreateData.xaml 的互動邏輯
    /// </summary>
    public partial class Register : Window
    {
        private string _dbConnStr= ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        private BankPersonalInfo personalInfo { get; set; }
        private BankAccount bankInfo { get; set; }
        private int _coountError = 0;
        public Register()
        {
            InitializeComponent();

            personalInfo = new BankPersonalInfo();
            personalInfo.CustomerId = Guid.NewGuid();
            this.BasicInfo.DataContext = personalInfo;

            bankInfo = new BankAccount();
            this.bankInfoData.DataContext = bankInfo;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
          
            if (bankInfo.BankName == "-----請選擇分行-----" || string.IsNullOrEmpty(bankInfo.BankName))
            {
                MessageBox.Show("請選擇分行!!");
                return;
            }

            var hashService=new HashService();
            bankInfo.HashPassword=hashService.HashPwd(this.password.Password, bankInfo.CustomerId.ToString().ToUpper());

            var settingsService = new SettingsService(_dbConnStr);
            var allData = settingsService.GetBankAllData();
            foreach (var item in allData)
            {
                Random random= new Random();
                var randNum=random.Next(10000000, 99999999);
                if (randNum.ToString() != item.AccountNum)
                {
                    bankInfo.AccountNum = randNum.ToString();
                    break;
                }
            }
            var bankIdList=settingsService.GetBankId(BankList.SelectedValue.ToString());
            foreach (var item in bankIdList)
            {
                bankInfo.BankId = item.BankId;
            }
         

            var personalObj=settingsService.AddPersonalData(personalInfo);
            settingsService.AddBankData(bankInfo, personalObj);
            

            MessageBox.Show("新增資料成功!!!!");
        }

        private void BankList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.BankList.SelectedItem!=null)
            {
                bankInfo.BankName = this.BankList.SelectedValue.ToString();
            }
           
        }

        private void password2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPwd();
        }

     

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            CheckPwd();
        }

        public void CheckPwd()
        {
            if (password.Password != password2.Password)
            {
                PwdMsg.Content = "密碼不相同,請重新輸入!!";
                password2.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                password2.BorderThickness = new Thickness(2, 2, 2, 2);
            }
            else
            {
                PwdMsg.Content = "";
                password2.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                password2.BorderThickness = new Thickness(1, 1, 1, 1);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Check_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action==ValidationErrorEventAction.Added)
            {
                _coountError++;
            }
            else
            {
                _coountError--;
            }
            this.SaveData.IsEnabled = _coountError > 0 ? false : true;
        }
    }
}
