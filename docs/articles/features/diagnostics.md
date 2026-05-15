---
uid: package-diagnostics
title: Using the Diagnostics package
---

# Using the Diagnostics package

The Diagnostic package contains a set of simple application logging mechanisms for applications.

## Logging events to files on disk with FileEventLogger

Using the `MADE.Diagnostics.Logging.FileEventLogger`, you can quickly and easily get up and running with event logging for your application's diagnostic needs.

The `FileEventLogger` will append event logs to a file on disk. The logs folder and file name can be configured using the `LogsFolderName` (default is `Logs`) and `LogFileNameFormat` (default is `Log-{0:yyyyMMdd}.txt`) properties.

The `LogFileNameFormat` has a `DateTime` parameter that can used to ensure logs are created based on a date, for example, daily using the `yyyyMMdd` format.

By default, the logs are stored in the application's root directory, however, this can also be overridden completely using the `LogPath` property which requires a full directory path including log file name.

Here's an example of the output using the default configuration.

```log
4/6/2021 8:58:40 AM	Level: Info	Id: 230b8802-4ba5-465d-bfdf-433698628673	Message: 'Application diagnostics initialized.'
4/6/2021 8:58:41 AM	Level: Debug	Id: b8f2137d-f436-4ee6-88aa-59fa9e745117	Message: 'This is a debug message'
4/6/2021 8:58:41 AM	Level: Error	Id: f04134e3-7a06-4688-832f-e1e4567aa6e4	Message: 'Error: 'System.InvalidOperationException: Could not complete delete action''
```

### Logging levels

The `FileEventLogger` provides the following methods for logging events in your application.

- `WriteDebug(string)`
- `WriteDebug(string, Exception)`
- `WriteDebug(Exception)`
- `WriteInfo(string)`
- `WriteInfo(string, Exception)`
- `WriteInfo(Exception)`
- `WriteWarning(string)`
- `WriteWarning(string, Exception)`
- `WriteWarning(Exception)`
- `WriteError(string)`
- `WriteError(string, Exception)`
- `WriteError(Exception)`
- `WriteCritical(string)`
- `WriteCritical(string, Exception)`
- `WriteCritical(Exception)`

## Implement application-wide exception handling with AppDiagnostics

There are occasions when you want to build an application that will not crash when non-critical, unhandled exceptions are thrown by your application.

The `MADE.Diagnostics.AppDiagnostics` helper provides you with the means to ensure your users can keep using your application with no worry about minor issues causing crashes.

Taking advantage of the `IEventLogger` interface available in the Diagnostics package, the `AppDiagnostics` helper can track exceptions thrown by the `AppDomain.UnhandledException` and `TaskScheduler.UnobservedTaskException` handlers, logging them at the **Critical** level with a correlation ID.

### Observing the unhandled exceptions

The `AppDiagnostics` helper exposes its own event handler `ExceptionObserved` that can be used in your own application code if you wish to perform additional actions when an exception is logged. 

The event argument provided by this handler will include the correlation ID to the event in the log, as well as the exception that was thrown.
