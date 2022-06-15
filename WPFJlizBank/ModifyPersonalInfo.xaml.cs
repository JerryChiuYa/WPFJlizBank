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
    /// ModifyPersonalInfo.xaml 的互動邏輯
    /// </summary>
    public partial class ModifyPersonalInfo : Window
    {
        private BankPersonalInfo _currentAccount;
        private string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
        private int _coountError;

        public ModifyPersonalInfo(BankPersonalInfo currentAccount)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            this.BasicInfo.DataContext = _currentAccount;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var services = new CustomerServices(_dbConnStr);
            services.UpdatePersonalInfo(_currentAccount);

            MessageBox.Show("修改成功!!");
            this.Close();
            new HomeWindow(_currentAccount).ShowDialog();
        }

        private void Check_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
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
