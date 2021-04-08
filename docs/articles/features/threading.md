---
uid: package-threading
title: Using the Threading package | MADE.NET
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