using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WPFAssignment.Classes;
using WPFAssignment.Command;
using WPFAssignment.Model;


namespace WPFAssignment.ViewModels
{
    class ViewModel : Observable
    {
        // Declaring variables and objects to be used in ViewModel
        public int wavelength = 1, wells = 96;
        public static List<int> dmlist = new List<int>();
        public AcquisitionEngine engine = new AcquisitionEngine();

        // Creating an instance of the Models
        private DataModel dataModel = new DataModel();

        public DataModel DataModel
        {
            get { return dataModel; }
            set { dataModel = value; OnPropertyChanged("dataModel"); }
        }

        // Logic for selection of wavelength in settings dialog
        private string selected_wavelength = "1";
        public string Selected_Wavelength
        {
            get
            {
                return selected_wavelength;
            }

            set
            {
                // Since data is acquired as a combobox item - converting it to required integer value
                selected_wavelength = value.Split(' ').Last();
                wavelength = Convert.ToInt32(selected_wavelength);
                DataModel.Wavelengths_Num = wavelength;
                OnPropertyChanged();
            }
        }

        // Logic for selection of wells in settings dialog
        private string selected_wells = "96";
        public string Selected_Wells
        {
            get
            {
                return selected_wells;
            }

            set
            {
                // Since data is acquired as a combobox item - converting it to required integer value
                selected_wells = value.Split(' ').Last();
                wells = Convert.ToInt32(selected_wells);
                DataModel.Wells_Num = wells;
                OnPropertyChanged();
            }
        }


        // Submit command - to be fired on click of 'OK' button in settings dialog
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

        // Actions to be taken on successful execution of SubmitCommand - Add all data user entered 
        // in settings dialog into a List - so that it can be accessed on the startup dialog screen
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

        // Conditions to be met for successful execution of Submit Command - Checking validations on fields
        private bool canSubmitExecute(object Parameter)
        {
            bool result = false;

            switch (DataModel.Wavelengths_Num)
            {
                case 1:
                    if (DataModel.Lm1 > 199 && DataModel.Lm1 < 1001)
                    {
                        result = true;
                    }
                    break;

                case 2:
                    if (DataModel.Lm2 > 199 && DataModel.Lm2 < 1001)
                    {
                        result = true;
                    }
                    break;

                case 3:
                    if (DataModel.Lm3 > 199 && DataModel.Lm3 < 1001)
                    {
                        result = true;
                    }
                    break;

                case 4:
                    if (DataModel.Lm4 > 199 && DataModel.Lm4 < 1001)
                    {
                        result = true;
                    }
                    break;

                case 5:
                    if (DataModel.Lm5 > 199 && DataModel.Lm5 < 1001)
                    {
                        result = true;
                    }
                    break;

                case 6:
                    if (DataModel.Lm6 > 199 && DataModel.Lm6 < 1001)
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }


        // Cancel command - to be fired on click of 'Cancel' button in settings dialog
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

        // Actions to be taken on successful execution of CancelCommand - setting all values to default 
        // (Resetting values entered by user in settings dialog)
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

        // Cancel command can always be executed - no specific conditions need to be met
        private bool CanCancelExecute(object Parameter)
        {
            return true;
        }


        // Acquire command - to be fired on click of 'Acquire Data!' button in startup dialog
        private ICommand _AcquireCommand;
        public ICommand AcquireCommand
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

        // Actions to be taken on successful execution of AcquireCommand - make call to 
        // appropriate method in Acquisition Engine
        public void AcquireExecute(object parameter)
        {
            engine.AcquireData();
        }

        // Acquire command can always be executed - no specific conditions need to be met
        public bool CanAcquireExecute(object Parameter)
        {
            return true;
        }


        // Settings command - to be fired on click of 'Settings' button in startup dialog
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

        // No specific actions to be taken on successful execution of SettingsCommand 
        private void SettingsExecute(object parameter)
        {

        }

        // Settings command can always be executed - no specific conditions need to be met
        private bool canSettingsExecute(object Parameter)
        {
            return true;
        }


        // Constructor for ViewModel
        public ViewModel()
        {

        }
    }
}