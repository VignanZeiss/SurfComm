using Microsoft.VisualBasic.FileIO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SurfComm.Core.Models
{
    [XmlRoot("GlobalParameters")]
    public class GlobalParameters
    {
        [XmlElement("Category")]
        public List<Category> Categories { get; set; } = new();
    }

    public class Category
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlElement("Setting")]
        public List<Setting> Settings { get; set; } = new();
    }
    public class Setting : BaseViewModel
    {
        [XmlAttribute("name")] public string Name { get; set; } = string.Empty;
        [XmlAttribute("command")] public string Command { get; set; } = string.Empty;
        [XmlAttribute("note")] public string Note { get; set; } = string.Empty;
        [XmlElement("Option")] public List<Option> Options { get; set; } = new();
        [XmlIgnore]   // Don’t write this into your definition XML
        private Option? _selectedOption;
        [XmlIgnore]
        public Option? SelectedOption
        {
            get => _selectedOption;
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }

    }
    public class Option
    {
        [XmlAttribute("name")] public string Name { get; set; } = string.Empty;
        [XmlAttribute("value")] public string Value { get; set; } = string.Empty;
        [XmlAttribute("devices")] public string? Devices { get; set; }
    }

   
}
