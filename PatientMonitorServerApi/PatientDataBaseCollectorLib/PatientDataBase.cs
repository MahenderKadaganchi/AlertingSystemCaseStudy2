using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;


namespace PatientDataBaseCollectorLib
{
    public class PatientDataBase : INotifyPropertyChanged
    {
        #region Fields
        private string _patientId;
        private string _patientName;
        private string _patientGender;
        private string _patientDob;
        private string _patientMobile;
        PatientDataWriter writer = new PatientDataWriter();
        WritePatientId _writePatientId = new WritePatientId();
        List<string> patientIds = new List<string>();
        List<string> patientNameList = new List<string>();
        List<string> patientGenderList = new List<string>();
        List<string> patientMobileList = new List<string>();
        List<string> patientDobList = new List<string>();




        #endregion

        #region Properties
        public string PatientId
        {
            get { return this._patientId; }
            set
            {
                this._patientId = value;
                OnPropertyChanged(nameof(PatientId));
            }
        }
        public string PatientName
        {
            get { return this._patientName; }
            set
            {
                this._patientName = value;
                OnPropertyChanged(nameof(PatientName));
            }
        }
        public string Gender
        {
            get { return this._patientGender; }
            set
            {
                this._patientGender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string DateOfBirth
        {
            get { return this._patientDob; }
            set
            {
                this._patientDob = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public string Mobile
        {
            get { return this._patientMobile; }
            set
            {
                this._patientMobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        #region Commands
        public ICommand admitCommand { get; set; }
        public ICommand EmergencyAdmitCommand { get; set; }
        #endregion


        #endregion

        #region Initializers
        public PatientDataBase()
        {
            Action<object> executeAction = new Action<object>(this.admitCommandExecute);
            Func<object, bool> canExecuteAction = new Func<object, bool>(canExecuteAdmitCommand);
            admitCommand = new DelegateCommand(executeAction, canExecuteAction);
            EmergencyAdmitCommand = new DelegateCommand((object obj) => { this.WriteEmergencyPatientData(); }, (object obj) => { return true; });
        }
        #endregion

        #region Logic
        void WriteData()
        {


            //var details = new AdmissionDetails()
            //{
            //    PatientId = this.PatientId,
            //    PatientName = this.PatientName,
            //    Gender = this.Gender,
            //    DateOfBirth = this.DateOfBirth,
            //    Mobile = this.Mobile

            //};
            List<string> _detailsList = new List<string>();
            _detailsList.Add(this.PatientId);
            _detailsList.Add(this.PatientName);
            _detailsList.Add(this.DateOfBirth);
            _detailsList.Add(this.Gender);
            _detailsList.Add(this.Mobile);

            if ((this.patientIds.Count >= 0) && (!this.patientIds.Contains(this.PatientId)))
            {
                if (CheckEmptyFields())
                {
                    writer.Write(_detailsList);
                    _writePatientId.WritePId(this.PatientId);
                    this.patientIds.Add(this.PatientId);
                    System.Windows.Forms.MessageBox.Show("Successfully Registered");

                }
                else
                    System.Windows.Forms.MessageBox.Show("Enter Correct Credentials");


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Incorrect Patient ID");
            }
            PatientId = "";
            PatientName = "";
            Gender = "";
            DateOfBirth = "dd/mm/yyyy";
            Mobile = "";

        }

        void WriteEmergencyPatientData()
        {
            var details = new AdmissionDetails()
            {
                PatientId = this.PatientId,
                PatientName = "NA",
                Gender = "NA",
                DateOfBirth = "NA",
                Mobile = "NA"

            };
            List<string> _detailsList = new List<string>();
            _detailsList.Add(details.PatientId);
            _detailsList.Add(details.PatientName);
            _detailsList.Add(details.DateOfBirth);
            _detailsList.Add(details.Gender);
            _detailsList.Add(details.Mobile);


            if ((this.patientIds.Count >= 0) && (!this.patientIds.Contains(this.PatientId)) && (this.PatientId != null))
            {


                writer.Write(_detailsList);
                _writePatientId.WritePId(this.PatientId);
                this.patientIds.Add(this.PatientId);
                System.Windows.Forms.MessageBox.Show("Successfully Registered");
            }
            else
                System.Windows.Forms.MessageBox.Show("Incorrect Patient ID");

            PatientId = "";
            PatientName = "";
            Gender = "";
            DateOfBirth = "dd/mm/yyyy";
            Mobile = "";

        }


        #endregion


        #region Wrappers
        void admitCommandExecute(object obj)
        {
            this.WriteData();

        }

        bool canExecuteAdmitCommand(object obj)
        {
            return true;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private Methods
        private bool CheckPatientDataRepetition()
        {
            bool status = true;
            if ((this.patientNameList.Contains(this.PatientName)) && (this.patientDobList.Contains(this.DateOfBirth)) &&
                (this.patientGenderList.Contains(this.Gender)) && (this.patientMobileList.Contains(this.Mobile)))
            {
                status = false;
            }
            return status;
        }

        private bool CheckEmptyFields()
        {
            bool status = false;
            if ((this.PatientName != string.Empty) && (this.DateOfBirth != string.Empty) && (this.Mobile != string.Empty) &&
                (this.Gender != string.Empty) && (this.PatientId != string.Empty))
            {
                status = true;
            }
            return status;
        }
        #endregion

    }

}
