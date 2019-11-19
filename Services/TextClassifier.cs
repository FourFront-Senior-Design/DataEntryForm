using ServicesInterfaces;
using System;
using System.Diagnostics;

namespace Services
{
    public class TextClassifier : ITextClassifier
    {
        public bool ClassifyText()
        {
            Trace.WriteLine("Classifying Text...");
            // Date extraction script
            String pythonInstallation = @"C:\Python\";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = pythonInstallation + "python.exe";
            start.Arguments = pythonInstallation + @"FourFrontScripts\date_extraction.py";
            start.RedirectStandardOutput = true;
            start.UseShellExecute = false;
            start.CreateNoWindow = true;

            Trace.WriteLine(start.FileName);
            Trace.WriteLine(start.Arguments);

            using(Process process = new Process())
            {
                process.StartInfo = start;
                process.Start();
                process.WaitForExit();
            }

            Trace.WriteLine("Classified Text.");
            return true;
        }
    }
}
