---
uid: diagnostics-file-logging
title: File logging
---

# File logging

The `MADE.Diagnostics.Logging.FileEventLogger` writes structured log entries to files on disk. It supports five severity levels, automatic daily log file rotation, and configurable output paths.

## Setting up the logger

```csharp
using MADE.Diagnostics.Logging;

var logger = new FileEventLogger();
```

By default, logs are written to a `Logs` folder in the application's root directory, with files named `Log-20260517.txt` (one file per day).

### Customizing the log location

```csharp
// Change the folder name (relative to app root)
logger.LogsFolderName = "AppLogs";

// Change the file name format (must include {0} for the DateTime parameter)
logger.LogFileNameFormat = "MyApp-{0:yyyyMMdd}.txt";

// Or set a fully custom path
logger.LogPath = @"C:\Logs\MyApp\custom-log.txt";
```

## Writing log entries

The logger provides methods for five severity levels, each with three overloads:

```csharp
// String message
await logger.WriteInfo("Application started successfully.");

// String message with exception
await logger.WriteError("Failed to process order.", exception);

// Exception only
await logger.WriteCritical(exception);
```

### All severity levels

| Method | When to use |
| --- | --- |
| `WriteDebug` | Detailed diagnostic information for development and troubleshooting. |
| `WriteInfo` | General operational events (startup, shutdown, configuration loaded). |
| `WriteWarning` | Unexpected situations that don't prevent operation but should be investigated. |
| `WriteError` | Errors that affect a specific operation but don't crash the application. |
| `WriteCritical` | Severe errors that may cause the application to stop functioning. |

## Log output format

Each entry is written as a tab-separated line with a timestamp, level, correlation ID, and message:

```
4/6/2021 8:58:40 AM	Level: Info	Id: 230b8802-4ba5-465d-bfdf-433698628673	Message: 'Application diagnostics initialized.'
4/6/2021 8:58:41 AM	Level: Error	Id: f04134e3-7a06-4688-832f-e1e4567aa6e4	Message: 'Error: 'System.InvalidOperationException: Could not complete delete action''
```

The correlation ID is unique per log entry and can be used to reference specific events from other systems (e.g., `AppDiagnostics`).

## Using the IEventLogger interface

`FileEventLogger` implements `IEventLogger`, which you can use for dependency injection and testing:

```csharp
public class OrderService
{
    private readonly IEventLogger logger;

    public OrderService(IEventLogger logger)
    {
        this.logger = logger;
    }

    public async Task ProcessOrderAsync(Order order)
    {
        await logger.WriteInfo($"Processing order {order.Id}");

        try
        {
            // Process the order
        }
        catch (Exception ex)
        {
            await logger.WriteError($"Failed to process order {order.Id}", ex);
            throw;
        }
    }
}
```

> **Note:** All `IEventLogger` methods return `Task`. Make sure to `await` them to ensure log entries are flushed to disk.
