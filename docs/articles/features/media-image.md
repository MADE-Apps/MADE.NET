---
uid: package-media-image
title: Using the Media Image package | MADE.NET
---

# Using the Media Image package

## Loading Windows StorageFile thumbnails into an Image with LoadStorageFileThumbnailImageBehavior

The `MADE.Media.Image.Behaviors.LoadStorageFileThumbnailImageBehavior` is a custom behavior built on the [Microsoft XAML behaviors SDK](https://github.com/Microsoft/XamlBehaviors). 

It can be attached to an `Image` UI element and used to load the thumbnail of a `StorageFile`.

You can do this in your Windows XAML as shown below.

```xml
<Page
    x:Class="App.Media.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:behaviors="using:MADE.Media.Image.Behaviors"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:interactivity="using:Microsoft.Xaml.Interactivity"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"
    mc:Ignorable="d">

    <RelativePanel Padding="12">
        <Image>
            <interactivity:Interaction.Behaviors>
                <behaviors:LoadStorageFileThumbnailImageBehavior File="{x:Bind ViewModel.ImageFile}" />
            </interactivity:Interaction.Behaviors>
        </Image>
    </RelativePanel>
</Page>

```

This could result in a generated UI that looks like this.

<img src="../../images/ImageBehavior.png" alt="Result of using LoadStorageFileThumbnailImageBehavior" />