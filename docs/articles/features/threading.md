---
uid: package-threading
title: Using the Threading package
---

# Using the Threading package

The Threading package contains a collection of `System.Threading` extensions and helpers to improve the developer experience.

## Modernizing System.Threading.Timer with the MADE.Threading.Timer

Setting up and managing a `System.Threading.Timer` can sometimes be cumbersome. How do you control the start and stop state? 

The `MADE.Threading.Timer` is a modern take on `System.Threading.Timer` providing properties for configuring the `Interval` and `DueTime`, plus an event handler for `Tick`. 

It also includes simple methods to `Start` and `Stop` the timer running.

Below is an example of using the `MADE.Threading.Timer` to setup and start running a timed job.

```csharp
public class TimerJob
{
    private MADE.Threading.Timer processTimer;

    public TimerJob()
    {
        processTimer = new MADE.Threading.Timer { Interval = TimeSpan.FromMinutes(1) };
        processTimer.Tick += OnProcessTimerTick;
    }

    public void StartTimer()
    {
        processTimer.Start();
    }

    public void StopTimer()
    {
        processTimer.Stop();
    }


    private void OnProcessTimerTick(object sender, object e)
    {
        // Do work.
    }
}
```

The equivalent for the `System.Threading.Timer` would look like

```csharp
public class TimerJob
{
    private System.Threading.Timer processTimer;

    public void StartTimer()
    {
        if (processTimer == null)
        {
            processTimer = new System.Threading.Timer(
                c => this.OnProcessTimerTick(),
                null,
                0,
                (int)Math.Ceiling(TimeSpan.FromMinutes(1).TotalMilliseconds));
        }
        else
        {
            processTimer.Change(
               0,
               (int)Math.Ceiling(TimeSpan.FromMinutes(1).TotalMilliseconds));
        }
    }

    public void StopTimer()
    {
        processTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
    }


    private void OnProcessTimerTick()
    {
        // Do work.
    }
}
```

As you can see, the MADE implementation performs the same actions, but is much more concise and a lot easier to understand.

The `ITimer` interface is also available if you need to define your own timer implementation or use it for dependency injection and testing.

## Task extensions

The `MADE.Threading.TaskExtensions` class provides extensions for working with asynchronous tasks:

### AndObserveExceptions

Observes the exceptions of faulted tasks, allowing you to handle errors without causing unobserved task exceptions.

```csharp
await myTask.AndObserveExceptions(ex => logger.LogError(ex, "Task faulted"));
```

### WhenAll and WhenAny for Task IEnumerable

Convenience extensions that call `Task.WhenAll` and `Task.WhenAny` directly on an `IEnumerable<Task>` collection.

```csharp
var tasks = myItems.Select(item => ProcessAsync(item));
await tasks.WhenAll();
```

## Lazy asynchronous initialization with AsyncLazy

The `MADE.Threading.AsyncLazy<T>` type provides a way to lazily initialize a value using an asynchronous factory method. The value is computed once on first access and cached for subsequent uses.

```csharp
private readonly AsyncLazy<Configuration> config = new(async () =>
{
    return await LoadConfigurationAsync();
});

public async Task UseConfigAsync()
{
    var configuration = await config;
    // Use configuration
}
```

You can check whether the value has been created using the `IsValueCreated` property, or use `GetValueAsync()` if you prefer an explicit task return.

## Rate-limiting actions with Debouncer

The `MADE.Threading.Debouncer` delays execution of an action until a specified period of inactivity has elapsed. This is useful for scenarios where rapid invocations should be collapsed into a single execution, such as search-as-you-type.

```csharp
private readonly Debouncer debouncer = new() { Delay = TimeSpan.FromMilliseconds(300) };

public void OnSearchTextChanged(string text)
{
    debouncer.Debounce(() => PerformSearch(text));
}
```

Each call to `Debounce` resets the timer. The action only executes after the delay elapses with no further calls. Use `Cancel()` to cancel a pending action, and `Dispose()` to clean up resources.

An async variant `DebounceAsync` is also available for asynchronous actions.

## Rate-limiting actions with Throttler

The `MADE.Threading.Throttler` limits execution of an action to at most once per specified time interval. Unlike the debouncer, the throttler executes the first invocation immediately and suppresses subsequent invocations until the interval elapses.

```csharp
private readonly Throttler throttler = new() { Interval = TimeSpan.FromMilliseconds(500) };

public void OnButtonClicked()
{
    throttler.Throttle(() => SubmitForm());
}
```

An async variant `ThrottleAsync` is also available for asynchronous actions with cancellation support.
