// 

using System;
using System.IO;

namespace AutoHitCounter.Utilities;

public static class Logger
{
    private static readonly object Lock = new();

    private static readonly string LogPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AutoHitCounter",
        "AutoHitCounter.log");

    public static void Error(Exception exception, string message)
    {
        try
        {
            lock (Lock)
            {
                var directory = Path.GetDirectoryName(LogPath);
                if (directory != null)
                    Directory.CreateDirectory(directory);

                File.AppendAllText(
                    LogPath,
                    $@"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {message}" +
                    Environment.NewLine +
                    exception +
                    Environment.NewLine +
                    Environment.NewLine);
            }
        }
        catch
        {
            // Logging must never crash the application.
        }
    }
}