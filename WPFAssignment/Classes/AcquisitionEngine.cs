using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WPFAssignment.Model;
using WPFAssignment.ViewModels;

namespace WPFAssignment.Classes
{
    class AcquisitionEngine : Observable
    {
        // Declaring variables and objects to be used in Acquisition Engine
        public static int clicks = 0, stop = 0, done = 0, index = 0, restart = 0;
        public int wavelength = 1, wells = 96, wells_no, wavelengths;
        public int[] lmarr = new int[6];
        public static List<int> dmlist = new List<int>();
        public DispatcherTimer timer = new DispatcherTimer();

        // Creating a class to act as base class for holding calculated data 
        public class data : Observable
        {
            private int wellindex;

            public int WellIndex
            {
                get { return wellindex; }
                set { wellindex = value; OnPropertyChanged("wellindex"); }
            }

            private string wavelength;

            public string Wavelength
            {
                get { return wavelength; }
                set { wavelength = value; OnPropertyChanged("wavelength"); }
            }

        }

        // Creating an ObservableCollection of class data - to hold all calculated data that will be acquired
        private ObservableCollection<data> datalist = new ObservableCollection<data>();

        public ObservableCollection<data> DataList
        {
            get { return datalist; }

            set
            {
                datalist = value;
                OnPropertyChanged("DataList");
            }
        }

        // Creating another ObservableCollection - to be populated dynamically according to timer ticks - this is the items source for datagrid
        private static ObservableCollection<data> finaldatalist = new ObservableCollection<data>();

        public static ObservableCollection<data> FinalDataList
        {
            get { return finaldatalist; }

            set
            {
                finaldatalist = value;
            }
        }

        // Main function that acquires data
        public void AcquireData()
        {
            clicks++;

            if (clicks == 2)
            {
                stop = 1;
                done = 0;
            }

            else if (clicks == 3)
            {
                DataList.Clear();
                FinalDataList.Clear();
                clicks = 1;
                done = 0;
                stop = 0;
                index = 0;
                restart = 1;
            }

            // dmlist holds the values added by user in settings dialog
            dmlist = ViewModels.ViewModel.dmlist;

            // Setting default values to dmlist if no values are added by user
            if (dmlist.Count == 0)
            {
                wells_no = 96;
                wavelengths = 1;
                lmarr[0] = 200;
            }

            // re-assigning accurate names to data acquired from dmlist
            else if (dmlist.Count != 0)
            {
                wells_no = dmlist.ElementAt(0);
                wavelengths = dmlist.ElementAt(1);

                switch (wavelengths)
                {

                    case 1:
                        lmarr[0] = dmlist.ElementAt(2);
                        break;

                    case 2:
                        lmarr[0] = dmlist.ElementAt(2);
                        lmarr[1] = dmlist.ElementAt(3);
                        break;

                    case 3:
                        lmarr[0] = dmlist.ElementAt(2);
                        lmarr[1] = dmlist.ElementAt(3);
                        lmarr[2] = dmlist.ElementAt(4);
                        break;

                    case 4:
                        lmarr[0] = dmlist.ElementAt(2);
                        lmarr[1] = dmlist.ElementAt(3);
                        lmarr[2] = dmlist.ElementAt(4);
                        lmarr[3] = dmlist.ElementAt(5);
                        break;

                    case 5:
                        lmarr[0] = dmlist.ElementAt(2);
                        lmarr[1] = dmlist.ElementAt(3);
                        lmarr[2] = dmlist.ElementAt(4);
                        lmarr[3] = dmlist.ElementAt(5);
                        lmarr[4] = dmlist.ElementAt(6);
                        break;

                    case 6:
                        lmarr[0] = dmlist.ElementAt(2);
                        lmarr[1] = dmlist.ElementAt(3);
                        lmarr[2] = dmlist.ElementAt(4);
                        lmarr[3] = dmlist.ElementAt(5);
                        lmarr[4] = dmlist.ElementAt(6);
                        lmarr[5] = dmlist.ElementAt(7);
                        break;
                }
            }

            int i, j;
            string[] wellsarr = new string[wells_no];

            // Calculation function for displaying data in data grid
            for (i = 0; i < wells_no; i++)
            {
                data dataitem = new data();
                double[] wavelensarr = new double[wavelengths];

                for (j = 0; j < wavelengths; j++)
                {
                    wavelensarr[j] = lmarr[j] * 0.1;
                }

                for (j = 0; j < wavelengths; j++)
                {
                    double num = (i + 1) + (wavelensarr[j]);
                    string numstr = num.ToString();

                    if (j != wavelengths - 1)
                    {
                        wellsarr[i] += numstr + ", ";
                    }

                    else
                    {
                        wellsarr[i] += numstr;
                    }
                }
                dataitem.WellIndex = i + 1;
                dataitem.Wavelength = wellsarr[i];
                DataList.Add(dataitem);
            }

            // Timer Start-Stop Functionality
            if (stop == 0)
            {
                Timer_Start(timer);
            }
            else if (stop == 1)
            {
                Timer_Stop(timer);
                stop = 0;
            }
        }

        // Timer Start-Stop Functions
        public void Timer_Start(DispatcherTimer timer)
        {
            timer.Start();
        }
        public void Timer_Stop(DispatcherTimer timer)
        {
            timer.Stop();
        }

        // Function to be executed on Timer Tick
        public void GetData_Tick(object sender, EventArgs e)
        {
            // Add data one by one (once per second) from main DataList to FinalDataList (items source for datagrid)
            if (index < wells_no)
            {
                done = 0;
                FinalDataList.Add(DataList.ElementAt(index));
                index++;
            }

            else if (index == wells_no)
            {
                done = 1;
                clicks = 2;
                DataList.Clear();
            }
        }

        // Constructor for Acquisition Engine - Sets Timer Interval and Calls Tick function
        public AcquisitionEngine()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += GetData_Tick;
        }
    }   
}