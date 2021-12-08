---
uid: package-ui-view-management
title: Using the UI View Management package
---

# Using the UI View Management package

The UI View Management package is designed for improving how your applications can create and manage additional windows in Windows applications.

## Creating a new application window with a Page type

Windows application developers working with a navigation `Frame` will be aware of the ease of use for navigating your applications with `Page` object types.

You can simply call `Navigate` passing a parameter object. So why shouldn't creating a new window follow the same way.

The `WindowManager` helper class provides a `CreateNewWindowForPageAsync` method with multiple overloads with this exact capability.

Below is an example of launching a new application window with a page type.

```csharp
private async Task LaunchNewWindow()
{
    await WindowManager.CreateNewWindowForPageAsync(typeof(MainPage), "ParameterObject");
}
```

This example will launch a new Window with an initial page showing the `MainPage`, passing the string as a page navigation parameter.

### Accessing the window's CoreDispatcher

When a new window is launched using the `CreateNewWindowForPageAsync` methods, the view's `CoreDispatcher` instance will be registered with the `ViewCoreDispatcherManager` instance. 

This can be accessed from anywhere in your application's executing code using the `ViewCoreDispatcherManager.Current.Get` method passing the identifier of the view.

To keep an instance of the view ID associated with your page, when initializing your page class, you can retrieve the view ID using the following code.

```csharp
public MainPage()
{
    var view = ApplicationView.GetForCurrentView();
    this.ViewId = view?.Id ?? -1;
}

public int ViewId { get; }
```
