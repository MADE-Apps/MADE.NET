---
uid: collections-observable-collections
title: Observable collections
---

# Observable collections

`ObservableCollection<T>` is the standard choice for data-bound collections in .NET UI frameworks, but it has two notable gaps: it doesn't propagate property changes from individual items, and it has no built-in sorting.

`MADE.Collections` fills both gaps with `ObservableItemCollection<T>` and sorting extensions.

## Tracking item property changes with ObservableItemCollection

Standard `ObservableCollection<T>` only raises `CollectionChanged` when items are added, removed, or replaced. If a property on an existing item changes, the collection stays silent and your UI won't update.

`ObservableItemCollection<T>` solves this by subscribing to `INotifyPropertyChanged` on each item and surfacing those changes through the collection's own change notifications.

```csharp
using MADE.Collections;

public class TodoItem : INotifyPropertyChanged
{
    private bool isComplete;

    public string Title { get; set; }

    public bool IsComplete
    {
        get => isComplete;
        set
        {
            isComplete = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsComplete)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

// Items' property changes are now visible to data binding
var todos = new ObservableItemCollection<TodoItem>
{
    new TodoItem { Title = "Write docs", IsComplete = false },
};

// This property change is propagated through the collection
todos[0].IsComplete = true;
```

This is particularly useful in MVVM applications where you bind a list to a UI control and need the UI to react when individual items are modified, not just when items are added or removed.

## Sorting with Sort and SortDescending

`ObservableCollection<T>` provides no sorting mechanism. The typical workaround - creating a new sorted collection and replacing the binding source - breaks UI state and animations.

The `Sort` and `SortDescending` extensions sort the collection in place while correctly raising `CollectionChanged` notifications, so the UI animates the reorder smoothly:

```csharp
using MADE.Collections;

var products = new ObservableCollection<Product>
{
    new Product { Name = "Keyboard", Price = 79.99m },
    new Product { Name = "Mouse", Price = 49.99m },
    new Product { Name = "Monitor", Price = 399.99m },
};

// Sort ascending by name
products.Sort(p => p.Name);

// Sort descending by price
products.SortDescending(p => p.Price);
```

## Best practices

- **Use `ObservableItemCollection<T>` when items are mutable** and your UI needs to reflect property-level changes. If your items are immutable (or you replace the entire item when it changes), the standard `ObservableCollection<T>` is sufficient.
- **Prefer in-place sorting** with `Sort`/`SortDescending` over creating new sorted collections. This preserves selection state and scroll position in UI frameworks.
- **Ensure your items implement `INotifyPropertyChanged`** before using `ObservableItemCollection<T>`. The collection subscribes to this interface, so items that don't implement it won't have their changes tracked.
