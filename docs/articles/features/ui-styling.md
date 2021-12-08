---
uid: package-ui-styling
title: Using the UI Styling package
---

# Using the UI Styling package

The UI Styling package is designed for improving the flow of designing native applications for Windows, Android, iOS, macOS, Linux, and the web.

## Converting HEX strings to colors and SolidColorBrush with ColorExtensions

The `MADE.UI.Styling.Colors.ColorExtensions` provides a collection of extensions that can be used to manipulate `System.Drawing.Color`, `Windows.UI.Color` or `Windows.UI.Xaml.Media.SolidColorBrush` objects such as converting a HEX value to them and back.

Below are some example usages for your applications built for Windows and the Uno Platform.

### Convert HEX String to Color and back example

```csharp
private void HexColorConversion()
{
    Windows.UI.Color redWindowsColor = "#FF0000".ToWindowsColor();
    System.Drawing.Color redSystemColor = "#FF0000".ToSystemColor();

    string redHex = redColor.ToHexString(); // or redSystemColor.ToHexString();
}
```

**Note**, the `ToColor()` extension for HEX strings supported both RGB and ARGB formats, taking into consideration the transparency layer.

### Convert Color to SolidColorBrush example

```csharp
private void ColorSolidColorBrushConversion()
{
    SolidColorBrush redBrush = Windows.UI.Colors.Red.ToSolidColorBrush(); // or System.Drawing.Color.Red.ToSolidColorBrush();
}
```
