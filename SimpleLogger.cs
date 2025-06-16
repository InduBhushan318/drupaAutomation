using System;
using System.IO;

public class SimpleLogger
{
    private readonly string logFilePath;

    public SimpleLogger(string logFileName)
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", dateFolder);
        Directory.CreateDirectory(logDirectory);
        logFilePath = Path.Combine(logDirectory, logFileName + ".txt");
    }

    public void Log(string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
    }
}