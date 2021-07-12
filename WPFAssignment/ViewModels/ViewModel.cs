using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFAssignment.Command;
using WPFAssignment.Model;

namespace WPFAssignment.ViewModels
{

    class ViewModel : Observable
    {
        public static int clicks = 0, stop = 0, done = 0, index = 0, restart=0, k = 0;
        public int wavelength = 1, wells = 96, wells_no, wavelengths;
        public int[] lmarr = new int[6];
        public static List<int> dmlist = new List<int>();
        public DispatcherTimer timer = new DispatcherTimer();
        
        private DataModel dataModel = new DataModel();

        public DataModel DataModel
        {
            get { return dataModel; }
            set { dataModel = value; OnPropertyChanged("dataModel"); }
        }
        

        private string selected_wavelength = "1";
        public string Selected_Wavelength {
            get
            {
                return selected_wavelength;
            }

            set
            {
                selected_wavelength = value.Split(' ').Last();
                wavelength = Convert.ToInt32(selected_wavelength);
                DataModel.Wavelengths_Num = wavelength;
                OnPropertyChanged();
            }
        }
    
        private string selected_wells = "96";       
        public string Selected_Wells 
        {
            get
            {
                return selected_wells;
            }

            set
            {
                selected_wells = value.Split(' ').Last();
                wells = Convert.ToInt32(selected_wells);
                DataModel.Wells_Num = wells;
                OnPropertyChanged();
            }
        }



        private ICommand _SubmitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_SubmitCommand == null)
                {
                    _SubmitCommand = new RelayCommand(SubmitExecute, canSubmitExecute, false);
                }

                return _SubmitCommand;
            }
        }

        private void SubmitExecute(object parameter)
        {
            dmlist.Add(DataModel.Wells_Num);
            dmlist.Add(DataModel.Wavelengths_Num);
            dmlist.Add(DataModel.Lm1);
            dmlist.Add(DataModel.Lm2);
            dmlist.Add(DataModel.Lm3);
            dmlist.Add(DataModel.Lm4);
            dmlist.Add(DataModel.Lm5);
            dmlist.Add(DataModel.Lm6);
            OnPropertyChanged();
        }

        private bool canSubmitExecute(object Parameter)
        {
            bool result = false;

            switch (DataModel.Wavelengths_Num)
            {
                case 1:
                    if(DataModel.Lm1 > 199 && DataModel.Lm1 < 1001)
                    {
                        result = true;
                    }
                break;

                case 2:
                    if (DataModel.Lm2 > 200 && DataModel.Lm2 < 1000)
                    {
                        result = true;
                    }
                break;

                case 3:
                    if (DataModel.Lm3 > 200 && DataModel.Lm3 < 1000)
                    {
                        result = true;
                    }
                break;

                case 4:
                    if (DataModel.Lm4 > 200 && DataModel.Lm4 < 1000)
                    {
                        result = true;
                    }
                break;

                case 5:
                    if (DataModel.Lm5 > 200 && DataModel.Lm5 < 1000)
                    {
                        result = true;
                    }
                break;

                case 6:
                    if (DataModel.Lm6 > 200 && DataModel.Lm6 < 1000)
                    {
                        result = true;
                    }
                break;
            }

            return result;
        }



        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand(CancelExecute, CanCancelExecute, false);
                }

                return _CancelCommand;
            }
        }

        private void CancelExecute(object parameter)
        {
            selected_wells = "System.Windows.Controls.ComboBoxItem: 96";
            Selected_Wavelength = "1";
            DataModel.Lm1 = 0;
            DataModel.Lm2 = 0;
            DataModel.Lm3 = 0;
            DataModel.Lm4 = 0;
            DataModel.Lm5 = 0;
            DataModel.Lm6 = 0;
        }

        private bool CanCancelExecute(object Parameter)
        {
            return true;
        }



        private  ICommand _AcquireCommand;
        public  ICommand AcquireCommand
        {
            get
            {
                if (_AcquireCommand == null)
                {
                    _AcquireCommand = new RelayCommand(AcquireExecute, CanAcquireExecute, false);
                }

                return _AcquireCommand;
            }
        }


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

        private static ObservableCollection<data> finaldatalist = new ObservableCollection<data>();

        public static ObservableCollection<data> FinalDataList
        {
            get { return finaldatalist; }

            set
            {
                finaldatalist = value;
            }
        }
  
       
        public void AcquireExecute(object parameter)
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
                stop = 0;
                index = 0;
                restart = 1;
            }

            if (dmlist.Count == 0)
            {
                wells_no = 10;
                wavelengths = 1;
                lmarr[0] = 200;
            }

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

            for (i = 0; i < wells_no; i++)
            {
                data dataitem = new data();
                double[] wavelensarr = new double[wavelengths];

                for (j = 0; j < wavelengths; j++) {
                    wavelensarr[j] = lmarr[j] * 0.1;
                }

                for (j = 0; j < wavelengths; j++) {
                    double num = (i + 1) + (wavelensarr[j]);
                    string numstr = num.ToString();

                    if (j != wavelengths - 1) {
                        wellsarr[i] += numstr + ", ";
                    }

                    else {
                        wellsarr[i] += numstr;
                    }       
                }
                dataitem.WellIndex = i + 1;
                dataitem.Wavelength = wellsarr[i];
                DataList.Add(dataitem);
            }

            if (restart == 0)
            {
                Generate_Timer(timer);
            }

            else if (restart == 1)
            {
                Debug.Print("Inside");
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
        }

        
        public void Generate_Timer(DispatcherTimer timer)
        {
            
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

        public void Timer_Start(DispatcherTimer timer)
        {
            timer.Start();
        }
        public void Timer_Stop(DispatcherTimer timer)
        {
            timer.Stop();
        }

        public void GetData_Tick(object sender, EventArgs e)
        {
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

        public bool CanAcquireExecute(object Parameter)
        {
            return true;
        }


        private ICommand _SettingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_SettingsCommand == null)
                {
                    _SettingsCommand = new RelayCommand(SettingsExecute, canSettingsExecute, false);
                }

                return _SettingsCommand;
            }
        }

        private void SettingsExecute(object parameter)
        {
            
        }

        private bool canSettingsExecute(object Parameter)
        {
            bool result = true;
           
            return result;
        }

        public ViewModel()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += GetData_Tick;
        }

    }
}
