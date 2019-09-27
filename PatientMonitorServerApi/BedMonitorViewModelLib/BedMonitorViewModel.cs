using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DelegateCommandLib;
using HttpServiceRequestLib;

namespace BedMonitorViewModelLib
{
    public class BedMonitorViewModel : INotifyPropertyChanged
    {

        #region Private Fields
        private DispatcherTimer _timer;
        private Brush _spo2AlarmColor;
        private Brush _temperatureAlarmColor;
        private Brush _pulseRateAlarmColor;
        private bool _spo2Flag = false;
        private bool _temperatureFlag = false;
        private bool _pulseRateFlag = false;
        private string _patientId;
        private string _bedId;
        
        #endregion

        #region Public Properties
        public Brush Spo2AlarmColor
        {
            get => this._spo2AlarmColor;
            set
            {
               this._spo2AlarmColor = value;
                    OnPropertyChanged(nameof(Spo2AlarmColor));
            }
        }
        public Brush TemperatureAlarmColor
        {
            get => this._temperatureAlarmColor;
            set
            {
                this._temperatureAlarmColor = value;
                OnPropertyChanged(nameof(TemperatureAlarmColor));
            }
        }
        public Brush PulseRateAlarmColor
        {
            get => this._pulseRateAlarmColor;
            set
            {
                this._pulseRateAlarmColor = value;
                OnPropertyChanged(nameof(PulseRateAlarmColor));
            }
        }

        public string PatientId
        {
            get => this._patientId;
            set
            {
                this._patientId = value;
               
                OnPropertyChanged(nameof(PatientId));
            }
        }
        #endregion

        public string BedId
        {
            get => this._bedId;
            set
            {
                this._bedId = value;
                OnPropertyChanged(nameof(BedId));
            }
        }


        #region Initializer

        public BedMonitorViewModel()
        {
            
            Spo2AlarmColor = (Brush) new BrushConverter().ConvertFromString("White");
            TemperatureAlarmColor = (Brush)new BrushConverter().ConvertFromString("White");
            PulseRateAlarmColor = (Brush)new BrushConverter().ConvertFromString("White");
            StartCommand = new DelegateCommand((object obj) => { this.StartMonitor(); }, (object obj) => true);
            ResetCommand = new DelegateCommand((object obj) => { this.ResetAlarm(); }, (object obj) => true);
            DischargeCommand = new DelegateCommand((object obj) => { this.DischargePatient(); }, (object obj) => true);
        }

        #endregion

        #region Commands

        public ICommand StartCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand DischargeCommand { get; set; }

        #endregion

        #region ViewLogic

        public void StartMonitor()
        {
            PatientId = HttpServiceRequest.HttpGetRequest("http://localhost:5000/", "api/BedMonitor/");
            PatientId = PatientId.Trim('"');
            if (PatientId != "")
            {
                _timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(5)};
                _timer.Tick += InvokeMonitoring;
                _timer.Start();
            }
        }

        public void ResetAlarm()
        {
            _spo2Flag = false;
            _temperatureFlag = false;
            _pulseRateFlag = false;
            Spo2AlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
            TemperatureAlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
            PulseRateAlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
        }

        public void DischargePatient()
        {
            if (PatientId != "")
            {
                HttpServiceRequest.HttpPostRequest("http://localhost:5000/", "api/PatientMonitor/Discharge/", PatientId);
                PatientId = "";
                Spo2AlarmColor = (Brush) new BrushConverter().ConvertFromString("White");
                TemperatureAlarmColor = (Brush) new BrushConverter().ConvertFromString("White");
                PulseRateAlarmColor = (Brush) new BrushConverter().ConvertFromString("White");
            }
            _timer.Stop();
        }
        #endregion

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Private Methods

        private void InvokeMonitoring(object sender, EventArgs e)
        {
            if (PatientId == "") return;
            string patientVitals = HttpServiceRequest.HttpPostRequest("http://localhost:5000/", "api/BedMonitor/", PatientId);

            if (patientVitals.Contains("Abnormal Spo2"))
            {
                _spo2Flag = true;
            }

            if (patientVitals.Contains("Abnormal PulseRate"))
            {
                _pulseRateFlag = true;
            }

            if (patientVitals.Contains("Abnormal Temperature"))
            {
                _temperatureFlag = true;
            }

            if (_spo2Flag == false)
                Spo2AlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
            else
            {
                Spo2AlarmColor = (Brush)new BrushConverter().ConvertFromString("Red");
            }

            if (_pulseRateFlag == false)
                PulseRateAlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
            else
            {
                PulseRateAlarmColor = (Brush)new BrushConverter().ConvertFromString("Red");
            }

            if (_temperatureFlag == false)
                TemperatureAlarmColor = (Brush)new BrushConverter().ConvertFromString("Green");
            else
            {
                TemperatureAlarmColor = (Brush)new BrushConverter().ConvertFromString("Red");
            }
        }

        #endregion
    }
}
