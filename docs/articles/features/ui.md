---
uid: package-ui
title: Using the UI package | MADE.NET
---

# Using the UI package

The UI package is a base package for building out UI components for native applications for Windows, Android, iOS, macOS, Linux, and the web. 

Its main purpose is to be used by the additional MADE.NET UI packages. However, it can be used to build your own custom UI elements for your own applications.

## Building custom controls for Windows or Uno Platform applications

The `MADE.UI.Controls.Control` is an extension to the [`Windows.UI.Xaml.Controls.Control`](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.control) that provides additional base components to make it easier to build custom controls.

For detail on building the custom control itself, we highly recommend these resources for information on building them.

- [Nick's .NET Travels - How to create a XAML templated control](https://nicksnettravels.builttoroam.com/tutorial-how-to-create-a-xaml-templated-control/)

With this base class, you get the following additional features to work with.

### IsVisible property

`IsVisible` is a boolean property that controls the `Visibility` state of the control.

When updated, the `IsVisibleChanged` event is fired which can be used to handle additional functions when a control is shown or hidden.

### GetChildView{TView} method

`GetChildView{TView}(string)` is a method which sits on top of the `GetTemplateChild` method of the underlying `Control` class.

The method simplifies the understanding and usability of retrieving child UI elements by retrieving your UI elements in the expected type.
