﻿using System;
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
        public static int clicks = 0;

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

        private void Acquire_Click(object sender, RoutedEventArgs e)
        {
            if (clicks == 0)
            {
                settings.IsEnabled = false;
                acquire.Content = "Stop Acquiring";
                Timer_Start();
                clicks++;
            }

            else if (clicks == 1)
            {
                settings.IsEnabled = true;
                acquire.Content = "Acquire Data!";
                clicks++;
            }

            else if (clicks == 2)
            {
                MessageBoxResult result = MessageBox.Show("Overwrite Data?", "Confirm", MessageBoxButton.YesNo);
                //  MessageBox.Show("Overwrite?");
                switch (result)
                {
                    case MessageBoxResult.Yes:

                        //  dg.ItemsSource=null;

                        FinalDataList.Clear();
                      //  dg.Items.Refresh();
                  //      dg.ItemsSource = ViewModels.SettingsViewModel.FinalDataList;
                        //ViewModels.SettingsViewModel.AcquireCommand.CanExecute(null);
                      //  ViewModels.SettingsViewModel.AcquireCommand.Execute(null);
                       
                   //     dg.ItemsSource = this.dg.DataContext();
                        settings.IsEnabled = false;
                        acquire.Content = "Stop Acquiring";
                        Timer_Stop();
                        //clicks++;
                    break;

                    case MessageBoxResult.No:

                    break;
                }
            }
        }

        public DispatcherTimer timer = new DispatcherTimer();

        public void Timer_Start()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += CheckDone_Tick;
            timer.Start();
        }

        public void Timer_Stop()
        {
            timer.Stop();
        }

        public void CheckDone_Tick(object sender, EventArgs e)
        {
            if (ViewModels.SettingsViewModel.done == 1)
            {
                settings.IsEnabled = true;
                acquire.Content = "Acquire Data!";
                clicks = 2;
            }
        }
    }
}
