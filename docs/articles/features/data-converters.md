---
uid: package-data-converters
title: Using the Data Converters package | MADE.NET
---

# Using the Data Converters package

The Data Converters package provides a collection of value converters and extensions to manipulate data in your applications.

## Converting a DateTime to a String using the DateTimeToStringValueConverter

Value converters are a common coding practice for building XAML applications that allow values to be bound to a view, converted to a different type and back depending on the binding mode.

Why should that be limited to just XAML applications though? 

The `MADE.Data.Converters.DateTimeToStringValueConverter` works across any .NET application, including your XAML bindings.

It converts a `DateTime` value to a `String` using a format parameter. The format parameter must be a valid `DateTime` string format [based on the Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings).

Below is an example of this in use in any C# application.

```csharp
namespace App.Conversions
{
    using MADE.Data.Converters;

    public class ApplicationConverters
    {
        private readonly DateTimeToStringValueConverter DateTimeToStringConverter = new DateTimeToStringValueConverter();

        public string ConvertDateToString(DateTime date)
        {
            return DateTimeToStringConverter.Convert(date, "g");
        }

        public DateTime ConvertStringToDate(string dateString)
        {
            return DateTimeToStringConverter.ConvertBack(dateString, "g");
        }
    }
}
```

You can also take advantage of this converter in your XAML applications too.

```xml
<Page
    x:Class="App.Conversions.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:converters="using:MADE.Data.Converters"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"
    mc:Ignorable="d">

    <Page.Resources>
        <converters:DateTimeToStringValueConverter x:Key="DateTimeToStringValueConverter" />
    </Page.Resources>

    <RelativePanel Padding="12">
        <TextBlock Text="{x:Bind ViewModel.Date, Converter={StaticResource DateTimeToStringValueConverter}, ConverterParameter='g'}" />
    </RelativePanel>
</Page>
```

## Creating your own custom value converters

If you want to take advantage of what goes into a value converter, you can build your own using the `MADE.Data.Converters.IValueConverter` interface which provides the signatures for the `Convert` and `ConvertBack` methods.

These can be used to convert any type to another. Whatever data conversion you think you may need, you'll be able to build out a value converter to satisfy that need for your project.

You can then build out your own, similar to our `DateTimeToStringValueConverter`.

```csharp
namespace MADE.Data.Converters
{
    using System;
    using System.Globalization;

    public partial class DateTimeToStringValueConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime value, object parameter = default)
        {
            string format = parameter?.ToString();
            return !string.IsNullOrWhiteSpace(format)
                       ? value.ToString(format, CultureInfo.InvariantCulture)
                       : value.ToString(CultureInfo.InvariantCulture);
        }

        public DateTime ConvertBack(string value, object parameter = default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DateTime.MinValue;
            }

            bool parsed = DateTime.TryParse(value, out DateTime dateTime);
            return parsed ? dateTime : DateTime.MinValue;
        }
    }
}
```

If you want to build a XAML specific value converter, you can also apply the `Windows.UI.Xaml.Data.IValueConverter` to your class and implement the additional methods calling directly into your `Convert` and `ConvertBack` methods.

If there is a common value converter you think is missing from MADE.NET, [raise a tracking item on GitHub](https://github.com/MADE-Apps/MADE.NET/issues/new/choose) and we'll get it implemented.