using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Operation {
    public Operation(int id, string name, Action executible) {
        Id = id;
        Name = name;
        ExecuteOperation = executible;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public Action ExecuteOperation { get; set; }

    // Method to display the operation option
    public string GetOperationName() {
        return $" {this.Id}. {this.Name}";
    }        

    public void RunOperation() {
        ExecuteOperation();
    }

    /*
    The methods below are used to carry out the actions available to the user in the console.
    */

    // Method to add a new log entry
    public static void AddLogEntry() {
        Console.Write("Enter the log text: ");
        string? logText = Console.ReadLine();
        while (string.IsNullOrEmpty(logText)) {
            Console.Write("Invalid search string. Enter the log text: ");
            logText = Console.ReadLine();
        }
        DateTime timestamp = DateTime.Now;
        int id = (Program.logs.Count > 0 ? Program.logs[^1].Id + 1 : 1);
        Program.logs.Add(new LogEntry {Id = id, Timestamp = timestamp, Text = (string.IsNullOrEmpty(logText) ? default! : logText) });

        // Not a great way to puase the program, but I want to make sure the user knows it was added successfully.
        Console.WriteLine("Log entry added successfully.");
        System.Threading.Thread.Sleep(1000);
    }

    // Function to list all log entries
    public static void ListAllLogEntries() {
        PrintLogs(Program.logs);
        System.Threading.Thread.Sleep(1000);
    }

    // Method to search log entries based on a string criteria
    public static void SearchLogEntries() {
        Console.Write("Enter text to search: ");
        string? searchCriteria = Console.ReadLine();
        if (String.IsNullOrEmpty(searchCriteria)) {
            ListAllLogEntries();
        } else {
            var matchingLogs = Program.logs.Where(log => log.Text.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase)).ToList();

            // Print the results of the search to the console
            PrintLogs(matchingLogs);
            System.Threading.Thread.Sleep(1000);
        }
    }

    // Function to save logs to a JSON file
    public static void SaveLogsToFile() {
        if (Program.logs.Count == 0) {
            Console.WriteLine("No log entries to save.");
            System.Threading.Thread.Sleep(1000);
            return;
        }

        Console.Write("Enter the file name to save logs (without extension): ");
        string? fileName = Console.ReadLine();

        // Serialize the Object to JSON using Newtonsoft
        string json = JsonConvert.SerializeObject(Program.logs, Formatting.Indented);
        string filePath = !String.IsNullOrEmpty(fileName) ? $"{fileName}.json" : $"logs-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")}.json";

        // try saving the JSON file. Catch errors if they occur and print to console
        try {
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Logs saved to '{filePath}' successfully.");
        } catch (Exception ex) {
            Console.WriteLine($"Error saving logs: {ex.Message}");
        }
        System.Threading.Thread.Sleep(1000);
    }

    // Method to print list of logs to the console (DRY)
    public static void PrintLogs(List<LogEntry> logList) {
        if (logList.Count == 0) {
            Console.WriteLine("No log entries found.");
        } else {
            Console.WriteLine("Log Entries:");
            foreach (var log in logList) {
                Console.WriteLine(log.GetLog());
            }
        }
        System.Threading.Thread.Sleep(1000);
    }

    public static void DeleteLogEntry() {
        Console.Write("Enter the ID log to delete: ");
        int logId;
        if (int.TryParse(Console.ReadLine(), out logId)) {
            try {
                var matchingLog = Program.logs.Single(log => log.Id == logId);
                Program.logs.Remove(matchingLog);
                Console.WriteLine($"Log Entry {matchingLog.Id} deleted.");
                System.Threading.Thread.Sleep(1000);
            } catch {
                // Catches when the user enters a number outside the range of menu options
                Console.WriteLine("Invalid Selection... Returning to menu.");
                System.Threading.Thread.Sleep(1000);
            }
        } else {
            // Catches edge cases when the user enters a string
            Console.WriteLine("Invalid Selection... Returning to menu.");
            System.Threading.Thread.Sleep(1000);
        }
    }
}
