using BankLibrary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WPFJlizBank
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
      DispatcherTimer dispatcherTimer=new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            //計時多久後開啟登入畫面
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval =new TimeSpan(0,0,0,4,800);
            dispatcherTimer.Start();
        }

    

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var login = new LoginWindow();
            this.Close();
            login.Show();

            dispatcherTimer.Stop();
        }
    }
}
