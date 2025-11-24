using SurfComm.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Provides standard SurfComm command templates and parsing helpers.
/// </summary>
namespace SurfComm.Core
{
    public class SurfCommHelper
    {
        /// <summary>
        /// Builds a complete command by combining base command and value(s).
        /// Example: BuildCommand("SMSPD, n=1", "2") => "SMSPD,1,2"
        /// </summary>
        public static string CommandStringBuilder(string inputCmd, string inValue)
        {
            if (string.IsNullOrWhiteSpace(inputCmd))
                return string.Empty;

            inputCmd = inputCmd.Replace(" ", "");
            inValue = inValue.Replace(" ", "");
            // Split command and value parts
            var inCmdParts = inputCmd.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var inValParts = inValue.Split(',', StringSplitOptions.RemoveEmptyEntries);

            string cmdWord = inCmdParts[0].Trim();
            string nString = string.Empty;
            string mString = string.Empty;

            // --- Extract n= / m= from command string ---
            foreach (string part in inCmdParts)
            {
                string p = part.Trim();
                if (p.StartsWith("n=", StringComparison.OrdinalIgnoreCase))
                    nString = p.Substring(2).Trim();
                else if (p.StartsWith("m=", StringComparison.OrdinalIgnoreCase))
                    mString = p.Substring(2).Trim();
            }

            // --- Fill from value string if not present ---
            // Three possiblities 
            if (string.IsNullOrEmpty(nString) && inValParts.Length > 0)
            {
                if (inValParts.Length > 1)
                {
                    nString = inValParts[0].Trim();
                    mString = inValParts[1].Substring(2);

                }
                if (inValParts.Length == 1)
                {
                    nString = inValParts[0].Trim();
                }
            }
            else if (string.IsNullOrEmpty(mString) && inValParts.Length == 1)
                mString = inValParts[0];


            // --- Build final output ---
            List<string> parts = new() { cmdWord };
            if (!string.IsNullOrEmpty(nString)) parts.Add(nString);
            if (!string.IsNullOrEmpty(mString)) parts.Add(mString);

            string output = string.Join(",", parts);

            System.Diagnostics.Debug.WriteLine($"➡ {output}");
            return output;
        }

        public static void UpdateGlobalProgramList()
        {
            Global.ProgramsList.Clear();

            string path = Path.Combine(FilePath.DocumentsFolder,"Zeiss", "SurfComConnect", FilePath.ProgramsFolder);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var files = Directory.GetFiles(path, "*.xml");

            foreach (var file in files)
            {
                var program = XmlHandler.LoadFromXml<ProgramModel>(file);

                // Rebuild ParameterListDisplay for UI
                program.ParameterListDisplay =
                    string.Join(", ", program.Parameters.Select(p => $"{p.Name} ({p.Min}–{p.Max})"));

                // Add to global list
                Global.ProgramsList.Add(program);
            }
        }



    }
}
