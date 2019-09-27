using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using DelegateCommandLib;
using HttpServiceRequestLib;


namespace PatientDataBaseCollectorLib
{
    public class PatientAdmitViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _patientId;
        private string _patientName;
        private string _patientGender;
        private string _patientDob;
        private string _patientMobile;
        #endregion

        #region Properties
        public string PatientId
        {
            get => this._patientId;
            set
            {
                this._patientId = value;
                OnPropertyChanged(nameof(PatientId));
            }
        }
        public string PatientName
        {
            get => this._patientName;
            set
            {
                this._patientName = value;
                OnPropertyChanged(nameof(PatientName));
            }
        }
        public string Gender
        {
            get => this._patientGender;
            set
            {
                this._patientGender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string DateOfBirth
        {
            get => this._patientDob;
            set
            {
                this._patientDob = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public string Mobile
        {
            get => this._patientMobile;
            set
            {
                this._patientMobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        #region Commands
        public ICommand AdmitCommand { get; set; }
        public ICommand EmergencyAdmitCommand { get; set; }
        #endregion


        #endregion

        #region Initializers
        public PatientAdmitViewModel()
        {
           AdmitCommand = new DelegateCommand((object obj) => this.WriteData(), (object obj) => true);
            EmergencyAdmitCommand = new DelegateCommand((object obj) =>  this.WriteEmergencyPatientData(), (object obj) => true );
        }
        #endregion

        #region Logic
        void WriteData()
        {
            string patientDetails = PatientId + "," + PatientName + "," + DateOfBirth + "," + Gender + "," + Mobile;
            PatientDataWriter(patientDetails);
        }

        void WriteEmergencyPatientData()
        {

           
                
            string patientDetails = PatientId + "NA,NA,NA,NA";
            PatientDataWriter(patientDetails);
        }


        #endregion


        #region Wrappers
     #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private Methods
        
        private bool CheckEmptyFields()
        {
            bool status = false || (this.PatientName != null) && (this.DateOfBirth != null) && (this.Mobile != null) &&
                          (this.Gender != null) && (this.PatientId != null);
            return status;
        }

        private void MakeNull()
        {
            PatientId = null;
            PatientName = null;
            Gender = null;
            DateOfBirth = "dd/mm/yyyy";
            Mobile = null;
        }

        private void PatientDataWriter(string patientData)
        {
            if (CheckEmptyFields())
            {
                string result = HttpServiceRequest.HttpPostRequest("http://localhost:5000/", "api/PatientMonitor/StoreData/", patientData);
                System.Windows.Forms.MessageBox.Show(result.ToLower().Contains("true")
                    ? "Successfully Registered"
                    : "The PatientId Entered Already Exists");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please Enter All The Details");
            }

            MakeNull();
        }
        #endregion


    }

}
