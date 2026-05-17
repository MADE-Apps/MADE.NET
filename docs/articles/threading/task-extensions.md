---
uid: threading-task-extensions
title: Task extensions
---

# Task extensions

The `MADE.Threading.TaskExtensions` class provides convenience methods for working with collections of tasks and handling task exceptions.

## WhenAll and WhenAny on collections

Instead of calling `Task.WhenAll(tasks.ToArray())`, use the extension methods directly on an `IEnumerable<Task>`:

```csharp
using MADE.Threading;

var tasks = items.Select(item => ProcessAsync(item));
await tasks.WhenAll();
```

```csharp
var completedTask = await tasks.WhenAny();
```

These are thin wrappers around `Task.WhenAll` and `Task.WhenAny`, but they read more naturally when you're already working with a collection of tasks.

## Observing exceptions with AndObserveExceptions

When you fire-and-forget a task, any exceptions it throws become unobserved. In .NET, unobserved task exceptions are eventually caught by the `TaskScheduler.UnobservedTaskException` handler, which can log warnings or, in older .NET versions, crash the process.

`AndObserveExceptions` ensures the task's exceptions are observed, and optionally executes a callback with the exception:

```csharp
using MADE.Threading;

// Fire-and-forget with exception observation
_ = backgroundTask.AndObserveExceptions(ex =>
{
    logger.LogError(ex, "Background task faulted");
});
```

This is useful for tasks that you intentionally don't await but still want to handle errors from gracefully.
