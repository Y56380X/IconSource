# IconSource

Source generator for generating icon sets from codepoint files.
**Currently supporting Blazor (and AvaloniaUI) in .NET 8.**

## Blazor

### Getting started

* Add NuGet package `IconSource.Blazor` to the project
* Add a codepoint file from a icon font as `AdditionalFiles` into the project
* Add the assembly attribute `IconSource` with an icon set name, codepoint file name, and css class to use
* Add css style for the icon font to your projects css
* The source generator generates an `Icon` type with the property `Name` where the icon can be set
* The generated icons of the codepoint file are in the class with the name of the icon set

### Example

Example for usage with [Google Material Symbols](https://fonts.google.com/icons) with a blazor application.

style.css:
```css
@font-face {
    font-family: "Material Symbols Round";
    font-style: normal;
    font-weight: 400;
    src: url("./material-symbols-rounded.woff2") format('woff2');
}

.material-symbols {
    font-family: "Material Symbols Round";
    font-weight: normal;
    font-style: normal;
    font-size: 24px;
    line-height: 1;
    letter-spacing: normal;
    text-transform: none;
    display: inline-block;
    white-space: nowrap;
    word-wrap: normal;
    direction: ltr;
    -webkit-font-feature-settings: 'liga';
    -webkit-font-smoothing: antialiased;
}
```

IconSource.cs:
```csharp
using IconSource;

[assembly:IconSource("MaterialIcons", "MaterialSymbols.codepoints", "material-symbols")]
```

[MaterialSymbols.codepoints](https://github.com/google/material-design-icons/tree/master/font) added to project:
```xml
<AdditionalFiles Include="MaterialSymbols.codepoints" />
```

Example usage as a `ButtonWithIcon.razor` component:
```razor
@using IconSource
<button @onclick="async () => await OnClick.InvokeAsync()">
    @if (Icon is {} icon)
    {
        <Icon Name="Icon" />
    }
    @if (ChildContent is {} childContent)
    {
        <span class="text">@childContent</span>
    }
</button>

@code
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public IconName? Icon { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
}
```

Example usage of the `ButtonWithIcon.razor` component:

```razor
@using IconSource
<ButtonWithIcon Icon="MaterialIcons.Upload">Upload</ButtonWithIcon>
```
