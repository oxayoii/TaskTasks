using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

public class Program_
{
    static void Main(string[] args)
    {
        string inputFilePath = "C:\\Users\\Катя\\Desktop\\Test\\Task3\\input.log";
        string outputFilePath = "C:\\Users\\Катя\\Desktop\\Test\\Task3\\output.log";
        string problemsFilePath = "C:\\Users\\Катя\\Desktop\\Test\\Task3\\problems.txt";

        try
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            using (StreamWriter writerProblems = new StreamWriter(problemsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        if (IsFormat1(line))
                        {
                            ProcessFormat1(line, writer);
                        }
                        else if (IsFormat2(line))
                        {
                            ProcessFormat2(line, writer);
                        }
                        else
                        {
                            writerProblems.WriteLine(line);
                        }
                    }
                    catch
                    {
                        writerProblems.WriteLine(line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    public static bool IsFormat1(string line)
    {
        return Regex.IsMatch(line, @"^\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}\.\d+ \w+ ");
    }
    public static bool IsFormat2(string line)
    {
        return Regex.IsMatch(line, @"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d+\|");
    }
    public static void ProcessFormat1(string line, TextWriter writer)
    {
        string[] parts = line.Split(new[] { ' ' }, 4);
        if (parts.Length < 4) throw new Exception("Некорректный формат");

        string dateStr = parts[0];
        string time = parts[1];
        string levelStr = parts[2];
        string message = parts[3];

        DateTime date = DateTime.ParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        string formattedDate = date.ToString("dd-MM-yyyy");

        string level = levelStr;
        if (level == "INFORMATION") level = "INFO";
        else if (level == "WARNING") level = "WARN";
        else if (level == "ERROR") level = "ERROR";
        else if (level == "DEBUG") level = "DEBUG";

        string callingMethod = "DEFAULT";

        Match callMatch = Regex.Match(message, @"ВызвавшийМетод:\s*(\S+)");
        if (callMatch.Success)
        {
            callingMethod = callMatch.Groups[1].Value;
        }

        writer.WriteLine(formattedDate);
        writer.WriteLine($"{time}\t{level}\t{callingMethod}");
        writer.WriteLine(message);
    }
    public static void ProcessFormat2(string line, TextWriter writer)
    {
        string[] parts = line.Split('|');
        if (parts.Length < 5) throw new Exception("Некорректный формат");

        string dateTimeStr = parts[0].Trim();
        string levelStr = parts[1].Trim();
        string number = parts[2].Trim(); 
        string method = parts[3].Trim();
        string message = parts[4].Trim();

        DateTime dateTime = DateTime.ParseExact(dateTimeStr, "yyyy-MM-dd HH:mm:ss.ffff", CultureInfo.InvariantCulture);
        string formattedDate = dateTime.ToString("dd-MM-yyyy");
        string time = dateTime.ToString("HH:mm:ss.ffff");

        string level;
        switch (levelStr)
        {
            case "INFO":
                level = "INFO";
                break;
            case "WARN":
                level = "WARN";
                break;
            case "ERROR":
                level = "ERROR";
                break;
            case "DEBUG":
                level = "DEBUG";
                break;
            default:
                level = levelStr; 
                break;
        }

        string callingMethod = "DEFAULT";
        if (!string.IsNullOrEmpty(method))
        {
            callingMethod = method;
        }

        writer.WriteLine(formattedDate);
        writer.WriteLine($"{time}\t{level}\t{callingMethod}");
        writer.WriteLine(message);
    }
}