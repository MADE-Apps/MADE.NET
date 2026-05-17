---
uid: threading-timer
title: Timer
---

# Timer

`System.Threading.Timer` works, but its API is cumbersome. You construct it with a callback, due time, and period as separate parameters. Starting and stopping requires calling `Change` with magic values like `Timeout.InfiniteTimeSpan`. There's no event-based pattern, and the state management is error-prone.

`MADE.Threading.Timer` wraps this into a modern API with properties for `Interval` and `DueTime`, a `Tick` event, and simple `Start`/`Stop` methods.

## Basic usage

```csharp
using MADE.Threading;

var timer = new Timer
{
    Interval = TimeSpan.FromMinutes(1),
};

timer.Tick += (sender, args) =>
{
    // Runs every minute
    ProcessPendingItems();
};

timer.Start();
```

To stop the timer:

```csharp
timer.Stop();
```

## Comparison with System.Threading.Timer

Here's the same functionality using the standard timer:

```csharp
private System.Threading.Timer processTimer;

public void StartTimer()
{
    if (processTimer == null)
    {
        processTimer = new System.Threading.Timer(
            _ => ProcessPendingItems(),
            null,
            0,
            (int)Math.Ceiling(TimeSpan.FromMinutes(1).TotalMilliseconds));
    }
    else
    {
        processTimer.Change(0,
            (int)Math.Ceiling(TimeSpan.FromMinutes(1).TotalMilliseconds));
    }
}

public void StopTimer()
{
    processTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
}
```

The MADE implementation is more concise, easier to read, and harder to get wrong.

## Configuring the due time

By default, the timer fires immediately when started. Set `DueTime` to delay the first tick:

```csharp
var timer = new Timer
{
    Interval = TimeSpan.FromMinutes(5),
    DueTime = TimeSpan.FromSeconds(30), // Wait 30 seconds before first tick
};
```

## Using the ITimer interface

`ITimer` is available for dependency injection and testing:

```csharp
public class PollingService
{
    private readonly ITimer timer;

    public PollingService(ITimer timer)
    {
        this.timer = timer;
        this.timer.Interval = TimeSpan.FromSeconds(30);
        this.timer.Tick += OnTimerTick;
    }

    public void Start() => timer.Start();
    public void Stop() => timer.Stop();

    private void OnTimerTick(object sender, object e)
    {
        // Poll for updates
    }
}
```

In tests, you can mock `ITimer` to control when ticks occur without waiting for real time to pass.
