using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BedMonitorViewModelLib.View;

namespace CompositeMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _bedCount=0;
        private UserControl1[] _bedControl;

        public MainWindow()
        {
            InitializeComponent();
        }

        
       
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var control in _bedControl)
            {
                InvokeStartMonitor(control);
            }
        }

        private void ComboBox_OnSelect(object sender, RoutedEventArgs e)
        {
            string value = BedCountComboBox.SelectedValue.ToString();
            value = value.Substring(value.IndexOf(':')+1);
            _bedCount = Int32.Parse(value);
            MonitorIcuUniformGrid.Children.Clear();
            _bedControl = new UserControl1[_bedCount];
            for (int i = 0; i < _bedCount; i++)
            {
                _bedControl[i]= new UserControl1();
                SetBedIdTextBox(_bedControl[i],i+1);
                MonitorIcuUniformGrid.Children.Add(_bedControl[i]);
            }
        }
        private void InvokeStartMonitor(UserControl1 name)
        {
            if (GetPatientIdDetails(name) == "")
            {
                if (name.FindName("StartMonitorButton") is Button startButton)
                {
                    ButtonAutomationPeer peer = new ButtonAutomationPeer(startButton);
                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    invokeProv?.Invoke();
                }
            }
        }
        private void DisableStartMonitorButton(UserControl1 name)
        {
            if (name.FindName("StartMonitorButton") is Button startButton)
            {
                startButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private string GetPatientIdDetails(UserControl1 name)
        {
            string returnValue = "";
            if (name.FindName("PatientIdTextBox1") is TextBox patientIdTextBox)
            {
                returnValue = patientIdTextBox.Text;
            }
            return returnValue;
        }

        private void SetBedIdTextBox(UserControl1 name,int count)
        {
            if (name.FindName("BedIdTextBox") is TextBox bedIdTextBox)
            {
                bedIdTextBox.Text = count.ToString();
            }
        }

       
    }
}
