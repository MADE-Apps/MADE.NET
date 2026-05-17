---
uid: diagnostics-exception-handling
title: Exception handling
---

# Exception handling

Unhandled exceptions in desktop, mobile, and background service applications can crash the process without warning. `AppDiagnostics` provides a safety net by subscribing to `AppDomain.UnhandledException` and `TaskScheduler.UnobservedTaskException`, logging the exceptions, and giving your code a chance to respond.

## Setting up AppDiagnostics

```csharp
using MADE.Diagnostics;
using MADE.Diagnostics.Logging;

var logger = new FileEventLogger();
var diagnostics = new AppDiagnostics(logger);
```

Once created, `AppDiagnostics` automatically subscribes to the global exception handlers. Any unhandled exception is logged at the **Critical** level with a correlation ID.

## Observing exceptions in your code

The `ExceptionObserved` event fires whenever an unhandled exception is caught. Use this to display error dialogs, send telemetry, or perform cleanup:

```csharp
diagnostics.ExceptionObserved += (sender, args) =>
{
    // args.CorrelationId matches the log entry ID
    // args.Exception is the unhandled exception
    ShowErrorDialog($"An unexpected error occurred. Reference: {args.CorrelationId}");
};
```

The correlation ID links the user-facing error message to the detailed log entry, making it easy to find the relevant diagnostics when a user reports an issue.

## When to use this

`AppDiagnostics` is designed for applications where you control the process lifecycle:

- **Desktop applications** (WPF, WinForms, MAUI) where a crash loses the user's work.
- **Background services** where you want to log the failure before the process exits.
- **Console applications** that need graceful error reporting.

For ASP.NET Core applications, use the built-in exception handling middleware instead. See [Exception middleware](../web/exception-middleware.md) for MADE's ASP.NET Core exception handler.
