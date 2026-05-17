---
uid: runtime-overview
title: Runtime
---

# Runtime

`MADE.Runtime` provides a small set of utilities that extend .NET's base types: fluent action chaining for invoking operations across multiple instances, weak reference callbacks for memory-safe delegate storage, and reflection helpers.

```bash
dotnet add package MADE.Runtime
```

## What's included

| Guide | What it covers |
| --- | --- |
| [Action chaining](action-chaining.md) | `Chain<T>` for invoking the same action across multiple instances with a fluent API. |
| [Weak references](weak-references.md) | `WeakReferenceCallback` for storing delegates without preventing garbage collection. |

## When to use this package

- You need to invoke the same initialization or teardown action across multiple objects without writing loops.
- You want to store callbacks (e.g., in a request manager or event system) without creating memory leaks from strong references.
- You need to retrieve property names from objects via reflection.
