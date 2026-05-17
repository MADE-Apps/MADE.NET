---
uid: runtime-action-chaining
title: Action chaining
---

# Action chaining

The `MADE.Runtime.Actions.Chain<T>` type provides a fluent API for invoking an action across one or more instances of a type. Instead of writing a loop or repeating the same method call for each instance, you chain them together.

## Basic usage

```csharp
using MADE.Runtime.Actions;

var handler1 = new MessageHandler();
var handler2 = new MessageHandler();

new Chain<MessageHandler>(handler1)
    .With(handler2)
    .Invoke(handler => handler.Initialize());
```

## Async chaining

For asynchronous operations, use `InvokeAsync`:

```csharp
await new Chain<MessageHandler>(handler1)
    .With(handler2)
    .InvokeAsync(handler => handler.InitializeAsync());
```

## When to use this

`Chain<T>` is useful in initialization and teardown sequences where you need to perform the same operation on multiple instances:

```csharp
// Initialize all services
new Chain<IService>(authService)
    .With(dataService)
    .With(notificationService)
    .Invoke(service => service.Configure(config));
```

For simple cases with a collection of items, a `foreach` loop is more readable. `Chain<T>` shines when you're working with individually named instances and want a fluent, declarative style.
