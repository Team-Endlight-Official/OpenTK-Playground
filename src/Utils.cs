using System;
using System.Drawing;
using System.IO;

/// <summary>
/// A bunch of Utilities to help to tidy up the codebase a bit.
/// </summary>
public class Utils
{
    /// <summary>
    /// The Data Folder that is always packed on build.
    /// </summary>
    public const string DATA_PATH = "Data/";

    /// <summary>
    /// Reads the contents of an specified File within the App Data directory.
    /// </summary>
    /// <param name="path">: Your specified Path.</param>
    public static string ReadFileContentsFromData(string path)
    {
        string FINAL_PATH = DATA_PATH + path;

        if (File.Exists(FINAL_PATH))
        {
            string contents = File.ReadAllText(FINAL_PATH);
            return contents;
        }
        else
        {
            Console.WriteLine($"Err: {FINAL_PATH}  - File does not exist.");
            return null;
        }
    }

    /// <summary>
    /// A more sexy version of Console.WriteLine();  - plus it's performant.
    /// </summary>
    /// <param name="message">Your Message.</param>
    /// <param name="textColor">Your Text Color.</param>
    public static void Log(object? message, ConsoleColor textColor = ConsoleColor.White)
    {
        Console.ForegroundColor = textColor;
        Console.Write(message?.ToString() + "\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
