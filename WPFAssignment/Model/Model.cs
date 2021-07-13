using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFAssignment.ViewModels;

namespace WPFAssignment.Model
{
    // Creating a base class that implements INotifyPropertyChanged - can be used throughout application
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    // Main model of the application
    public class DataModel : Observable, IDataErrorInfo
    {
        private int wells_num;
        public int Wells_Num
        {
            get { return wells_num; }
            set { wells_num = value; OnPropertyChanged("wells_num"); }
        }

        private int wavelengths_num;
        public int Wavelengths_Num
        {
            get { return wavelengths_num; }
            set { wavelengths_num = value; OnPropertyChanged("wavelengths_num"); }
        }

        private int lm1;
        public int Lm1
        {
            get { return lm1; }
            set { lm1 = value; OnPropertyChanged("lm1"); }
        }

        private int lm2;
        public int Lm2
        {
            get { return lm2; }
            set { lm2 = value; OnPropertyChanged("lm2"); }
        }

        private int lm3;
        public int Lm3
        {
            get { return lm3; }
            set { lm3 = value; OnPropertyChanged("lm3"); }
        }

        private int lm4;
        public int Lm4
        {
            get { return lm4; }
            set { lm4 = value; OnPropertyChanged("lm4"); }
        }

        private int lm5;
        public int Lm5
        {
            get { return lm5; }
            set { lm5 = value; OnPropertyChanged("lm5"); }
        }

        private int lm6;
        public int Lm6
        {
            get { return lm6; }
            set { lm6 = value; OnPropertyChanged("lm6"); }
        }

        // IDataErrorInfo implementation - for validations in settings dialog
        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string PropertyName]
        {
            get
            {
                string result = String.Empty;

                switch (PropertyName)
                {
                    case "Lm1":
                        if (string.IsNullOrEmpty(Lm1.ToString()))
                        {
                            result = "Value of Lm1 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm1.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm1 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    case "Lm2":
                        if (string.IsNullOrEmpty(Lm2.ToString()))
                        {
                            result = "Value of Lm2 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm2.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm2 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    case "Lm3":
                        if (string.IsNullOrEmpty(Lm3.ToString()))
                        {
                            result = "Value of Lm3 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm3.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm3 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    case "Lm4":
                        if (string.IsNullOrEmpty(Lm4.ToString()))
                        {
                            result = "Value of Lm4 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm4.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm4 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    case "Lm5":
                        if (string.IsNullOrEmpty(Lm5.ToString()))
                        {
                            result = "Value of Lm5 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm5.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm5 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    case "Lm6":
                        if (string.IsNullOrEmpty(Lm6.ToString()))
                        {
                            result = "Value of Lm6 must be an Integer";
                        }
                        else
                        {
                            Int32.TryParse(Lm6.ToString(), out int lm);
                            if (lm < 199 || lm > 1001)
                            {
                                result = "Value of Lm6 must be an Integer within the range of 200-1000";
                            }
                        }
                        break;

                    default:
                        result = "Value of Lm must be an Integer within the range of 200-1000";
                        break;
                        
                }
                return result;
            }
        }

        // Constructor for the model - populating model instance with default values
        public DataModel()
        {
            wells_num = 96;
            wavelengths_num = 1;
            lm1 = 0;
            lm2 = 0;
            lm3 = 0;
            lm4 = 0;
            lm5 = 0;
            lm6 = 0;
        }
    }
}