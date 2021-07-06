using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static WPFAssignment.ViewModels.SettingsViewModel;

namespace WPFAssignment.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
    
        }

        

        public int i = 0;
        public Window1 win1 = new Window1();

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            win1.Show();
        }

        //private void Acquire_Click(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Tick += new EventHandler(RowReveal_Tick);
        //    timer.Interval = new TimeSpan(0, 0, 5);
        //    timer.Start();
        //}

        //public void ShowRow(DataGridRow row)
        //{
        //    row.Visibility = Visibility.Visible;
        //}

        //public static int index = 0;
        //public void RowReveal_Tick(object sender, EventArgs e)
        //{
        //    if (index < dmlist.ElementAt(0))
        //    {
        //        //dg.Items.Refresh();
                
        //        //var datatype = dg.Items.GetItemAt(0).GetType();
        //        //var row = dg.Items.GetItemAt(0);
        //        //string rowstr = row.ToString();
                
        //        index++;
        //    }
        //}

    }
}
