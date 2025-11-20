using SurfComm.Connect.Models;
using SurfComm.Core;
using SurfComm.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SurfComm.Connect.ViewModels 
{
    public class SetupViewModel : BaseViewModel
    {
        // ----------------- FILE PATHS -----------------
        


        // ----------------- PRIVATE FIELDS -----------------
        private DeviceConfig _deviceConfig = new();
        private string _connectionString = string.Empty;

        // ----------------- PROPERTIES -----------------
        
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                OnPropertyChanged();
            }
        }
        public DeviceConfig DeviceConfig
        {
            get { return _deviceConfig; }
            set
            {
                _deviceConfig = value;
                OnPropertyChanged();
            }
        }
        public SerialPortClass PortInfo { get; set; } = new();

        #region Automation / Ethernet IP Mapping
        private bool _isAutomationEnabled;
        public bool IsAutomationEnabled
        {
            get => _isAutomationEnabled;
            set { _isAutomationEnabled = value; OnPropertyChanged(); }
        }

        private string _lcaIpAddress = "192.168.1.50";
        public string LcaIpAddress
        {
            get => _lcaIpAddress;
            set { _lcaIpAddress = value; OnPropertyChanged(); }
        }

        // Mapping: Which Result goes to which PLC Data Word?
        private string _mapResult1 = "Ra"; // Maps to Word 2-3
        public string MapResult1
        {
            get => _mapResult1;
            set { _mapResult1 = value; OnPropertyChanged(); }
        }

        private string _mapResult2 = "Rz"; // Maps to Word 4-5
        public string MapResult2
        {
            get => _mapResult2;
            set { _mapResult2 = value; OnPropertyChanged(); }
        }
        #endregion




        //-----------------Collections------------------------
        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<string> AvailableDevices { get; } = new();
        public ObservableCollection<string> StartTriggerList { get; } = new() { "LCA Module", "Calypso" };
        public ObservableCollection<string> AvailablePorts { get; set; } = new();
        

        //-------------------Actions----------------------
        public ICommand SaveCommand { get; }
        public ICommand TestDeviceCommand { get; }
        public ICommand ApplyToDevice { get; set; }

        //------------------Constructor--------------------
        public SetupViewModel()
        {
            LoadAvailableDeviceListXml();
            LoadDeviceDefinitionConfigXml();
            LoadSavedDeviceConfigXml();
            UpdateAvailablePortsList();
            //App.CommService.Enqueue("SMEAS");


            SaveCommand = new RelayCommand(_=>SaveCurrentDeviceConfigToXml());
            TestDeviceCommand = new RelayCommand(_=>CheckForDevice());
            ApplyToDevice = new RelayCommand(_ => ExecuteDeviceConfig());
        }



        // ==========================================================
        //                 MAIN ACTION METHODS
        // ==========================================================

        void ExecuteDeviceConfig()
        {
            // Builds Command Sequence and Executes
            if (PortEnumerator.IsSurfCommPlugedIn())
            {
                foreach (var setting in DeviceConfig.SelectedSettings)
                {

                }
            }
            else
            {
                var result = MessageBox.Show(
                                            "No SurfComm device found.\nOK to start in demo mode, Cancel to exit.",
                                            "Device Not Found",
                                            MessageBoxButton.OKCancel,
                                            MessageBoxImage.Error);

                if (result == MessageBoxResult.Cancel)
                {
                    Application.Current.Shutdown();
                }


            }
        }

        /// <summary>
        /// Checks if any SurfComm device is connected.
        /// </summary>
        private void CheckForDevice()
        {
            if (PortEnumerator.IsSurfCommPlugedIn())
                ConnectionString = "Device Found";
            else
                ConnectionString = "No Device Found";
        }


        // ==========================================================
        //                 Read and Write to XML
        // ==========================================================
        //Loads all avialble Devices from Device List from XML 
        void LoadAvailableDeviceListXml()
        {
            try
            {
                var list = XmlHandler.LoadFromXml<DeviceList>(FilePath.DeviceListFile);
                if (list != null) 
                {
                    AvailableDevices.Clear();
                    foreach (var item in list.Devices)
                    {
                        AvailableDevices.Add(item);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        void LoadDeviceDefinitionConfigXml()
        {
            try
            {
                var loadedConfig = XmlHandler.LoadFromXml<GlobalParameters>(FilePath.DeviceDefinitionFile);
                if (loadedConfig != null)
                {
                    Categories.Clear();

                    foreach (var cat in loadedConfig.Categories)
                    {
                        Categories.Add(cat);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Config Load Failed",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void LoadSavedDeviceConfigXml()
        {
            try
            {
                var loadedConfig = XmlHandler.LoadFromXml<DeviceConfig>(FilePath.ConfigFile);
                if (loadedConfig == null)
                    return;

                DeviceConfig = loadedConfig;

                // For each saved setting in DeviceConfig
                foreach (var confSetting in loadedConfig.SelectedSettings)
                {
                    foreach (var category in Categories)
                    {
                        // Find matching setting by name
                        var catSetting = category.Settings
                            .FirstOrDefault(s => s.Name.Equals(confSetting.Name, StringComparison.OrdinalIgnoreCase));

                        if (catSetting == null)
                            continue;

                        // Find matching option by value
                        var matchingOption = catSetting.Options
                            .FirstOrDefault(o => o.Value == confSetting.Value);

                        if (matchingOption != null)
                            catSetting.SelectedOption = matchingOption;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" ! Failed to load DeviceConfig: {ex.Message}",
                                "Load Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
        void SaveCurrentDeviceConfigToXml()
        {
            
            DeviceConfig.SelectedSettings.Clear();
            foreach (var category in Categories)
            {
                //Get Curent option from UI
                
                foreach (var setting in category.Settings)
                {
                    if (setting.SelectedOption != null)
                    {
                        DeviceConfig.SelectedSettings.Add(new SelectedSetting
                        {
                            Name = setting.Name,
                            Command = SurfCommHelper.CommandStringBuilder(setting.Command, setting.SelectedOption.Value),
                            CmdName = setting.Command,
                            Value = setting.SelectedOption.Value
                                    
                        });
                    }
                }
            }
            try
            {

                
                XmlHandler.SaveToXml(DeviceConfig, FilePath.ConfigFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        void SaveSerialPortSettingsToXml()
        {
            try
            {
                XmlHandler.SaveToXml<SerialPortClass>(PortInfo , FilePath.SerialPortConfigFile);
            }
            catch
            {
                MessageBox.Show("Could not find serialport config file", "File Not Found", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        // ==========================================================
        //                 SUPPORT METHODS
        // ==========================================================
        void UpdateAvailablePortsList()
        {
            var ports = PortEnumerator.GetListOfSerialPorts();
            AvailablePorts.Clear();
            foreach (var p in ports)
            {
                AvailablePorts.Add(p);
            }


        }

    }
}
