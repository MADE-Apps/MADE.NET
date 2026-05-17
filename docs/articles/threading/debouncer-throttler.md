---
uid: threading-debouncer-throttler
title: Debouncer and Throttler
---

# Debouncer and Throttler

Debouncing and throttling are two rate-limiting strategies for handling rapid invocations. They solve different problems:

- **Debouncing** waits for a pause in activity before executing. Use it when you want the *last* action in a burst (e.g., search-as-you-type).
- **Throttling** executes immediately and then ignores subsequent calls until an interval elapses. Use it when you want the *first* action in a burst (e.g., preventing double-clicks).

## Debouncer

The `Debouncer` delays execution until a specified period of inactivity has elapsed. Each new call resets the timer.

```csharp
using MADE.Threading;

private readonly Debouncer debouncer = new() { Delay = TimeSpan.FromMilliseconds(300) };

public void OnSearchTextChanged(string text)
{
    debouncer.Debounce(() => PerformSearch(text));
}
```

In this example, if the user types "hello" character by character, the search only executes once - 300ms after the last keystroke.

### Async debouncing

For async actions, use `DebounceAsync`:

```csharp
public void OnSearchTextChanged(string text)
{
    debouncer.DebounceAsync(async () => await SearchApiAsync(text));
}
```

### Cancelling a pending action

```csharp
debouncer.Cancel();
```

### Cleanup

`Debouncer` implements `IDisposable`. Dispose it when the owning component is torn down:

```csharp
debouncer.Dispose();
```

## Throttler

The `Throttler` limits execution to at most once per interval. The first call executes immediately; subsequent calls within the interval are suppressed.

```csharp
using MADE.Threading;

private readonly Throttler throttler = new() { Interval = TimeSpan.FromMilliseconds(500) };

public void OnSubmitClicked()
{
    throttler.Throttle(() => SubmitForm());
}
```

If the user clicks the submit button three times in quick succession, only the first click triggers `SubmitForm()`.

### Async throttling

```csharp
public void OnSubmitClicked()
{
    throttler.ThrottleAsync(async () => await SubmitFormAsync());
}
```

## Choosing between Debouncer and Throttler

| Scenario | Use |
| --- | --- |
| Search-as-you-type | **Debouncer** - wait for the user to stop typing. |
| Window resize handler | **Debouncer** - wait for the resize to finish. |
| Form submission button | **Throttler** - process the first click, ignore rapid follow-ups. |
| Scroll event handler | **Throttler** - execute at a steady rate during scrolling. |
| Auto-save on text change | **Debouncer** - save after the user pauses editing. |

The key distinction: debouncing delays execution until *after* the burst ends, while throttling executes at the *start* of the burst and then rate-limits.
