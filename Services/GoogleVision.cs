using DataStructures;
using ServicesInterfaces;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Services
{
    public class GoogleVision : ITextExtractor
    {
        public bool ReadText(string fileLocation)
        {
            String pythonInstallation = @"C:\Python";
            String outputDirectory = pythonInstallation + @"\FourFrontScripts\tempFiles";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInstallation + @"\python.exe";
            start.Arguments = pythonInstallation + @"\FourFrontScripts\googleVisionOCR.py " + fileLocation + " " + outputDirectory;

            start.RedirectStandardOutput = true;
            start.UseShellExecute = false;
            start.CreateNoWindow = true;

            Trace.WriteLine(start.FileName);
            Trace.WriteLine(start.Arguments);
                
            using (Process process = new Process())
            {
                process.StartInfo = start;
                process.Start();
                process.WaitForExit();
            }

            return true;
        }

    }

}
