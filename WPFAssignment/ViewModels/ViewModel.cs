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
using WPFAssignment.Classes;


namespace WPFAssignment.ViewModels
{

    class ViewModel : Observable
    { 
        public int wavelength = 1, wells = 96;
        public static List<int> dmlist = new List<int>();
        public AcquisitionEngine engine = new AcquisitionEngine();
        
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
        
        public void AcquireExecute(object parameter)
        {
            engine.AcquireData();
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
            
        }

    }
}
