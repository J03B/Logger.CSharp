using System.Net.NetworkInformation;
// Declare needed scopes
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Define Program object
public class Program {
    // Initialize global variables
    public static List<LogEntry> logs = new List<LogEntry>();
    static List<Operation> operations = new List<Operation> {
        new Operation(1, "Add Log Entry", Operation.AddLogEntry),
        new Operation(2, "List All Log Entries", Operation.ListAllLogEntries),
        new Operation(3, "Search Log Entries", Operation.SearchLogEntries),
        new Operation(4, "Save Logs to JSON File", Operation.SaveLogsToFile),
        new Operation(5, "Delete Log Entry", Operation.DeleteLogEntry),
        new Operation(6, "Exit", ExitLoop)
    };
    static bool exit = false;

    static void Main() {
        // Run the program until the user manually exits
        while (!exit) {
            // MAIN MENU
            Console.WriteLine("\nJoe's Logging Storage Program");
            foreach (var operation in operations) {
                Console.WriteLine(operation.GetOperationName());
            }
            Console.Write("Enter your choice: ");

            // Handle user's choice from the menu
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice)) {
                try {
                    operations[choice - 1].RunOperation();
                } catch {
                    // Catches when the user enters a number outside the range of menu options
                    Console.WriteLine("Invalid choice. Please try again.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            // Handle edge cases by having the user re-enter a value
            else {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    // Quit Program Menu Loop
    static void ExitLoop() {
        exit = true;
        Console.WriteLine("Closing Program...");
    }
}
