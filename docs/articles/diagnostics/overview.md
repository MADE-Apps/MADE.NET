---
uid: diagnostics-overview
title: Diagnostics
---

# Diagnostics

Application logging and exception handling are infrastructure concerns that every project needs but nobody wants to spend time building. `MADE.Diagnostics` provides two focused utilities: a file-based event logger for writing structured logs to disk, and an application-wide exception handler that prevents unhandled exceptions from crashing your application.

```bash
dotnet add package MADE.Diagnostics
```

## What's included

| Guide | What it covers |
| --- | --- |
| [File logging](file-logging.md) | `FileEventLogger` for writing debug, info, warning, error, and critical log entries to files on disk. |
| [Exception handling](exception-handling.md) | `AppDiagnostics` for catching unhandled exceptions from `AppDomain` and `TaskScheduler` with correlation tracking. |

## When to use this package

- You need simple file-based logging without pulling in a full logging framework like Serilog or NLog.
- You want a safety net for unhandled exceptions in desktop, mobile, or background service applications.
- You need correlated exception tracking where log entries reference the same exception event.

> **Note:** For ASP.NET Core applications, consider using the built-in `ILogger` infrastructure with a file provider. `MADE.Diagnostics` is best suited for scenarios where the Microsoft.Extensions.Logging abstraction isn't available or is overkill.
