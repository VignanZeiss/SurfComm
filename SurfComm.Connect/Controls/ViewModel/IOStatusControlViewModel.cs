using SurfComm.Connect.ViewModels;
using System.Windows.Media;

namespace SurfComm.Connect.Controls.ViewModels
{
    public class IOStatusControlViewModel : BaseViewModel
    {
        // Helper method
        private Brush OnBackground => Brushes.White;
        private Brush OffBackground => Brushes.Red;

        private Brush OnText => Brushes.Black;
        private Brush OffText => Brushes.White;

        private void RaiseAll()
        {
            // Inputs
            OnPropertyChanged(nameof(StartMeasurementColor));
            OnPropertyChanged(nameof(StartMeasurementText));
            OnPropertyChanged(nameof(StartMeasurementTextColor));

            OnPropertyChanged(nameof(ErrorResetColor));
            OnPropertyChanged(nameof(ErrorResetText));
            OnPropertyChanged(nameof(ErrorResetTextColor));

            OnPropertyChanged(nameof(Param1InColor));
            OnPropertyChanged(nameof(Param1InText));
            OnPropertyChanged(nameof(Param1InTextColor));

            OnPropertyChanged(nameof(Param2InColor));
            OnPropertyChanged(nameof(Param2InText));
            OnPropertyChanged(nameof(Param2InTextColor));

            OnPropertyChanged(nameof(Param3InColor));
            OnPropertyChanged(nameof(Param3InText));
            OnPropertyChanged(nameof(Param3InTextColor));

            // Outputs
            OnPropertyChanged(nameof(DeviceReadyColor));
            OnPropertyChanged(nameof(DeviceReadyText));
            OnPropertyChanged(nameof(DeviceReadyTextColor));

            OnPropertyChanged(nameof(ProbeConnectedColor));
            OnPropertyChanged(nameof(ProbeConnectedText));
            OnPropertyChanged(nameof(ProbeConnectedTextColor));

            OnPropertyChanged(nameof(AutomaticModeColor));
            OnPropertyChanged(nameof(AutomaticModeText));
            OnPropertyChanged(nameof(AutomaticModeTextColor));

            OnPropertyChanged(nameof(CmmErrorColor));
            OnPropertyChanged(nameof(CmmErrorText));
            OnPropertyChanged(nameof(CmmErrorTextColor));

            OnPropertyChanged(nameof(MeasurementRunningColor));
            OnPropertyChanged(nameof(MeasurementRunningText));
            OnPropertyChanged(nameof(MeasurementRunningTextColor));

            OnPropertyChanged(nameof(MeasurementFinishedColor));
            OnPropertyChanged(nameof(MeasurementFinishedText));
            OnPropertyChanged(nameof(MeasurementFinishedTextColor));

            OnPropertyChanged(nameof(ResultGoodColor));
            OnPropertyChanged(nameof(ResultGoodText));
            OnPropertyChanged(nameof(ResultGoodTextColor));

            OnPropertyChanged(nameof(ResultWarningColor));
            OnPropertyChanged(nameof(ResultWarningText));
            OnPropertyChanged(nameof(ResultWarningTextColor));

            OnPropertyChanged(nameof(ResultBadColor));
            OnPropertyChanged(nameof(ResultBadText));
            OnPropertyChanged(nameof(ResultBadTextColor));

            OnPropertyChanged(nameof(AliveColor));
            OnPropertyChanged(nameof(AliveText));
            OnPropertyChanged(nameof(AliveTextColor));

            OnPropertyChanged(nameof(InspectionNumberColor));
            OnPropertyChanged(nameof(InspectionNumberText));
            OnPropertyChanged(nameof(InspectionNumberTextColor));
        }

        // ========== INPUT BACKING FIELDS ==========
        private bool _startMeasurement;
        public bool StartMeasurement
        {
            get => _startMeasurement;
            set { _startMeasurement = value; RaiseAll(); }
        }

        private bool _errorReset;
        public bool ErrorReset
        {
            get => _errorReset;
            set { _errorReset = value; RaiseAll(); }
        }

        private bool _param1In;
        public bool Param1In
        {
            get => _param1In;
            set { _param1In = value; RaiseAll(); }
        }

        private bool _param2In;
        public bool Param2In
        {
            get => _param2In;
            set { _param2In = value; RaiseAll(); }
        }

        private bool _param3In;
        public bool Param3In
        {
            get => _param3In;
            set { _param3In = value; RaiseAll(); }
        }

        // ========== OUTPUT BACKING FIELDS ==========
        private bool _deviceReady;
        public bool DeviceReady
        {
            get => _deviceReady;
            set { _deviceReady = value; RaiseAll(); }
        }

        private bool _probeConnected;
        public bool ProbeConnected
        {
            get => _probeConnected;
            set { _probeConnected = value; RaiseAll(); }
        }

        private bool _automaticMode;
        public bool AutomaticMode
        {
            get => _automaticMode;
            set { _automaticMode = value; RaiseAll(); }
        }

        private bool _cmmError;
        public bool CmmError
        {
            get => _cmmError;
            set { _cmmError = value; RaiseAll(); }
        }

        private bool _measurementRunning;
        public bool MeasurementRunning
        {
            get => _measurementRunning;
            set { _measurementRunning = value; RaiseAll(); }
        }

        private bool _measurementFinished;
        public bool MeasurementFinished
        {
            get => _measurementFinished;
            set { _measurementFinished = value; RaiseAll(); }
        }

        private bool _resultGood;
        public bool ResultGood
        {
            get => _resultGood;
            set { _resultGood = value; RaiseAll(); }
        }

        private bool _resultWarning;
        public bool ResultWarning
        {
            get => _resultWarning;
            set { _resultWarning = value; RaiseAll(); }
        }

        private bool _resultBad;
        public bool ResultBad
        {
            get => _resultBad;
            set { _resultBad = value; RaiseAll(); }
        }

        private bool _alive;
        public bool Alive
        {
            get => _alive;
            set { _alive = value; RaiseAll(); }
        }

        private bool _inspectionNumber;
        public bool InspectionNumber
        {
            get => _inspectionNumber;
            set { _inspectionNumber = value; RaiseAll(); }
        }

        // ========== INPUT DERIVED PROPERTIES ==========
        public Brush StartMeasurementColor => StartMeasurement ? OnBackground : OffBackground;
        public string StartMeasurementText => StartMeasurement ? "ON" : "OFF";
        public Brush StartMeasurementTextColor => StartMeasurement ? OnText : OffText;

        public Brush ErrorResetColor => ErrorReset ? OnBackground : OffBackground;
        public string ErrorResetText => ErrorReset ? "ON" : "OFF";
        public Brush ErrorResetTextColor => ErrorReset ? OnText : OffText;

        public Brush Param1InColor => Param1In ? OnBackground : OffBackground;
        public string Param1InText => Param1In ? "ON" : "OFF";
        public Brush Param1InTextColor => Param1In ? OnText : OffText;

        public Brush Param2InColor => Param2In ? OnBackground : OffBackground;
        public string Param2InText => Param2In ? "ON" : "OFF";
        public Brush Param2InTextColor => Param2In ? OnText : OffText;

        public Brush Param3InColor => Param3In ? OnBackground : OffBackground;
        public string Param3InText => Param3In ? "ON" : "OFF";
        public Brush Param3InTextColor => Param3In ? OnText : OffText;

        // ========== OUTPUT DERIVED PROPERTIES ==========
        public Brush DeviceReadyColor => DeviceReady ? OnBackground : OffBackground;
        public string DeviceReadyText => DeviceReady ? "ON" : "OFF";
        public Brush DeviceReadyTextColor => DeviceReady ? OnText : OffText;

        public Brush ProbeConnectedColor => ProbeConnected ? OnBackground : OffBackground;
        public string ProbeConnectedText => ProbeConnected ? "ON" : "OFF";
        public Brush ProbeConnectedTextColor => ProbeConnected ? OnText : OffText;

        public Brush AutomaticModeColor => AutomaticMode ? OnBackground : OffBackground;
        public string AutomaticModeText => AutomaticMode ? "ON" : "OFF";
        public Brush AutomaticModeTextColor => AutomaticMode ? OnText : OffText;

        public Brush CmmErrorColor => CmmError ? OnBackground : OffBackground;
        public string CmmErrorText => CmmError ? "ON" : "OFF";
        public Brush CmmErrorTextColor => CmmError ? OnText : OffText;

        public Brush MeasurementRunningColor => MeasurementRunning ? OnBackground : OffBackground;
        public string MeasurementRunningText => MeasurementRunning ? "ON" : "OFF";
        public Brush MeasurementRunningTextColor => MeasurementRunning ? OnText : OffText;

        public Brush MeasurementFinishedColor => MeasurementFinished ? OnBackground : OffBackground;
        public string MeasurementFinishedText => MeasurementFinished ? "ON" : "OFF";
        public Brush MeasurementFinishedTextColor => MeasurementFinished ? OnText : OffText;

        public Brush ResultGoodColor => ResultGood ? OnBackground : OffBackground;
        public string ResultGoodText => ResultGood ? "ON" : "OFF";
        public Brush ResultGoodTextColor => ResultGood ? OnText : OffText;

        public Brush ResultWarningColor => ResultWarning ? OnBackground : OffBackground;
        public string ResultWarningText => ResultWarning ? "ON" : "OFF";
        public Brush ResultWarningTextColor => ResultWarning ? OnText : OffText;

        public Brush ResultBadColor => ResultBad ? OnBackground : OffBackground;
        public string ResultBadText => ResultBad ? "ON" : "OFF";
        public Brush ResultBadTextColor => ResultBad ? OnText : OffText;

        public Brush AliveColor => Alive ? OnBackground : OffBackground;
        public string AliveText => Alive ? "ON" : "OFF";
        public Brush AliveTextColor => Alive ? OnText : OffText;

        public Brush InspectionNumberColor => InspectionNumber ? OnBackground : OffBackground;
        public string InspectionNumberText => InspectionNumber ? "ON" : "OFF";
        public Brush InspectionNumberTextColor => InspectionNumber ? OnText : OffText;

        public IOStatusControlViewModel()
        {
            // For now: everything OFF by default
            StartMeasurement = false;
            ErrorReset = false;
            Param1In = false;
            Param2In = false;
            Param3In = false;

            DeviceReady = false;
            ProbeConnected = false;
            AutomaticMode = false;
            CmmError = false;
            MeasurementRunning = false;
            MeasurementFinished = false;
            ResultGood = false;
            ResultWarning = false;
            ResultBad = false;
            Alive = false;
            InspectionNumber = false;
        }
    }
}
