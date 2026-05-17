---
uid: threading-overview
title: Threading
---

# Threading

.NET's `System.Threading` namespace provides the building blocks for async and concurrent code, but several common patterns require more setup than they should. Setting up a repeating timer means wrestling with `System.Threading.Timer`'s callback-based API. Debouncing search input requires manual `CancellationTokenSource` management. Lazy-loading an async resource needs careful thread-safety handling.

`MADE.Threading` provides higher-level primitives for these scenarios: a modern timer, debouncer, throttler, async lazy initialization, and task extensions.

```bash
dotnet add package MADE.Threading
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Timer](timer.md) | A modern `Timer` with `Start`/`Stop`, `Interval` property, and `Tick` event. |
| [Debouncer and Throttler](debouncer-throttler.md) | Rate-limiting primitives for collapsing rapid invocations or limiting execution frequency. |
| [AsyncLazy](async-lazy.md) | Thread-safe lazy initialization with an async factory. |
| [Task extensions](task-extensions.md) | `WhenAll`/`WhenAny` on collections, and `AndObserveExceptions` for safe error handling. |

## When to use this package

- You need a timer that's simpler to use than `System.Threading.Timer`.
- You're implementing search-as-you-type and need to debounce the search action.
- You want to prevent rapid button clicks from submitting a form multiple times.
- You need to lazily initialize an async resource (configuration, cache, connection) with thread safety.

## Quick example

```csharp
using MADE.Threading;

// Debounce search input
var debouncer = new Debouncer { Delay = TimeSpan.FromMilliseconds(300) };
debouncer.Debounce(() => PerformSearch(query));

// Throttle button clicks
var throttler = new Throttler { Interval = TimeSpan.FromMilliseconds(500) };
throttler.Throttle(() => SubmitForm());

// Lazy async initialization
var config = new AsyncLazy<Config>(async () => await LoadConfigAsync());
var value = await config;
```
