using System;

// LogEntry class representing a log entry
public class LogEntry {
    public int Id { get; set; } // Add ID in case we want functionality to delete entries later on down the road
    public DateTime Timestamp { get; set; }
    public string Text { get; set; } = default!;

    // Method to return the entry with both the Timestamp and Text
    public string GetLog() {
        return $"{this.Id}. [{this.Timestamp}] - {this.Text}";
    }
}
