using System.Diagnostics;

Console.WriteLine();

#region EventLog
const string eventLogSource = "EventLog-EventViewer";

string logName = "ApplicationLog";

if(!EventLog.SourceExists(eventLogSource))
{
    EventLog.CreateEventSource(eventLogSource, logName);
    Console.WriteLine($"Event Source '{eventLogSource}' created for log '{logName}'");
}

string logMessage = $"This is a test message, Event Log: {DateTime.UtcNow}";
EventLog.WriteEntry(eventLogSource, logMessage, EventLogEntryType.Information);
Console.WriteLine($"Event Log entry written: {logMessage}");
Process.Start("eventvwr.exe");
#endregion