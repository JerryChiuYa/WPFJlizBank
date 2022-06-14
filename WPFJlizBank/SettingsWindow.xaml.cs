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
    /// SettingsWindow.xaml 的互動邏輯
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private string _dbConnStr;
        private BankPersonalInfo selectedPersonalInfo { get; set; }
        public SettingsWindow()
        {
            InitializeComponent();
            _dbConnStr=ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
            GetData();
        }

        private void GetData()
        {
            var settingsService = new SettingsService(_dbConnStr);
            var data=settingsService.GetData();
            this.dataList.ItemsSource = data;
        }

        private void Query_Click(object sender, RoutedEventArgs e)
        {
            var settingsService = new SettingsService(_dbConnStr);
            var data=settingsService.GetData(this.UserName.Text, this.Email.Text);
            this.dataList.ItemsSource = data;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new Register();
            createWindow.ShowDialog();
        }

        private void dataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dataList.SelectedItem!=null)
            {
                selectedPersonalInfo = (BankPersonalInfo)this.dataList.SelectedItem;
                this.showDetails.DataContext= selectedPersonalInfo;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPersonalInfo != null)
            {
                var settingsService = new SettingsService(_dbConnStr);
                settingsService.UpdateData(selectedPersonalInfo);
                MessageBox.Show("Update Success!!");
            }
 
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var result=MessageBox.Show("Are you sure to delete this data?", "Warning!!!", MessageBoxButton.OKCancel , MessageBoxImage.Warning);
            if (selectedPersonalInfo!=null && result==MessageBoxResult.OK)
            {
                var settingsService = new SettingsService(_dbConnStr);
                settingsService.DeleteData(selectedPersonalInfo);
                MessageBox.Show("Delete Success!!");
            }
            

        }
    }
}
