# WPFUIAutomationProperties

Facilitates additional WPF framework element property values to be reported to Appium and the WindowsDriver.

```c#
var element = windowsDriver.FindElement(MobileBy.AccessibilityId(automationId));

void AssertItemStatus(Func<string, bool> expectation)
{
    var itemStatus = element.GetAttribute("ItemStatus");
    Assert.That(expectation(itemStatus), Is.True);
}
```

There are two methods that work similarly.  One is the StaticConstructor method where you need to create a Framework Element derivation and configure in the static constructor.
This will apply for all instances of the derivation.

The other uses multi-binding and need to be configured for an instance of a framework element type and can be set in xaml.  This method is described below.

It works by supplying dependency properties for the framework element.  The ItemStatus string is by default an improved json convert serialization of a Dictionary with keys the property name and the value the 
property value.  If you supply IConvertDependencyProperty then the property value will be converted.  It is also possible to supply a serialized type that will be reflected upon.  
This is the better method as deserialization is typed.


# Configuration

The second method is for adding property values without converting them, the second allows for conversion.
The first method uses the TSerialized to create a list of [IConvertDependencyProperty](#IConvertDependencyProperty) calling the third method with a converter that will construct TSerialized ( with the default constructor )
and set its properties.

```C#
namespace WpfUIAutomationProperties.Serialization
{
    public interface IItemStatusSerializer
    {
        string Serialize(object itemStatus);

        T Deserialize<T>(string serialized);
        
    }
}

namespace WpfUIAutomationProperties.MultiBinding
{
    public static class ItemStatus
    {
        public static IItemStatusSerializer Serializer { get; set; } = new ItemStatusTypedDictionarySerializer();
        
        public static void ForElement<TSerialized,TFrameworkElement>(
            TFrameworkElement element, 
            Func<object, string> serializer = null
        ) where TFrameworkElement : FrameworkElement where TSerialized : new()
        
        public static void ForElement(
            FrameworkElement element, 
            IEnumerable<DependencyProperty> dps,
            Func<Dictionary<string,object>, object> converter = null,
            Func<object, string> serializer = null
        )

        public static void ForElement(
            FrameworkElement element, 
            IEnumerable<IConvertDependencyProperty> convertDependencyProperties,
            Func<Dictionary<string,object>, object> converter = null,
            Func<object, string> serializer = null
        )
        
    }
}
```
<a name="IConvertDependencyProperty"></a>
```C#
namespace WpfUIAutomationProperties
{
    public interface IConvertDependencyProperty
    {
        DependencyProperty DependencyProperty { get; }
        object Convert(object value);
    }

    public class FuncConvertDependencyProperty<T> : IConvertDependencyProperty
    {
        private readonly Func<T, object> convert;

        public FuncConvertDependencyProperty(DependencyProperty dependencyProperty, Func<T,object> convert)
        {
            DependencyProperty = dependencyProperty;
            this.convert = convert;
        }
        public DependencyProperty DependencyProperty { get; }

        public object Convert(object value) => this.convert((T)value);
    }

    public class NoConvertDependencyProperty : IConvertDependencyProperty
    {
        public NoConvertDependencyProperty(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty;
        }

        public NoConvertDependencyProperty() { }

        public DependencyProperty DependencyProperty { get; set; }

        public object Convert(object value) => value;
    }

    public class ToStringConvertDependencyProperty : IConvertDependencyProperty
    {
        public ToStringConvertDependencyProperty(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty;
        }

        public ToStringConvertDependencyProperty() { }

        public DependencyProperty DependencyProperty { get; set; }

        public object Convert(object value)
        {
            return value?.ToString();
        }
    }
}

```

Below setups two TextBlocks for ItemStatus.  Both are applied with a trigger so as to only apply when in Debug mode.
You could use a resource dictionary to apply to all instances.
The second is the better approach, providing the serialized type that is reflected upon for the IConvertDependencyProperty list.
```C#
<Window x:Class="AutomationTest.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:AutomationTest"
        xmlns:wpfAutomationMultiBinding="clr-namespace:WpfUIAutomationProperties.MultiBinding;assembly=WpfUIAutomationProperties"
        xmlns:wpfCommon="clr-namespace:WpfUIAutomationProperties;assembly=WpfUIAutomationProperties"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <StackPanel x:Name="stackPanel">
        <TextBlock AutomationProperties.AutomationId="{Binding Source={x:Static local:Constants.XamlAttachedConvertDependencyPropertiesTextBlockId}}">
            <TextBlock.Style>
                <Style>
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding Source={x:Static local:ApplyWhenDebugOnly.Apply}}" Value="True">
                            <Setter Property="wpfAutomationMultiBinding:Attacher.ItemStatusConvertDependencyProperties">
                                <Setter.Value>
                                    <wpfAutomationMultiBinding:ConvertDependencyPropertyList>
                                        <wpfCommon:NoConvertDependencyProperty>
                                            <wpfCommon:NoConvertDependencyProperty.DependencyProperty>
                                                <x:Static Member="TextBlock.FontFamilyProperty"/>
                                            </wpfCommon:NoConvertDependencyProperty.DependencyProperty>
                                        </wpfCommon:NoConvertDependencyProperty>
                                        <local:DemoForegroundConverterDependencyProperty/>
                                    </wpfAutomationMultiBinding:ConvertDependencyPropertyList>
                                </Setter.Value>
                            </Setter>
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
        <TextBlock AutomationProperties.AutomationId="{Binding Source={x:Static local:Constants.XamlAttachedSerializedTypeTextBlockId}}">
            <TextBlock.Style>
                <Style>
                    <Style.Triggers>
                        <DataTrigger Binding="{Binding Source={x:Static local:ApplyWhenDebugOnly.Apply}}" Value="True">
                            <Setter Property="wpfAutomationMultiBinding:Attacher.SerializedType" Value="{x:Type local:SerializedTextBlock}">
                            </Setter>
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </TextBlock.Style>
        </TextBlock>
    </StackPanel>
</Window>

```

## Serialized Type
The public properties of the serialized type determine the list of DependencyProperty.  
If not an attached property then the dependency property is found on the framework element type else the serialized type property 
should have the AttachedPropertyAttribute applied.
e.g
```C#
[AttachedProperty(typeof(OwnerType),nameof(OwnerType.APropertyProperty))]
public SomeType AProperty {get;set;}
```

Note that the type of the serialized property does not need to be the same as that of the DependencyProperty.

### Conversion
There will be no conversion if the dependency property type can be assigned to the serialized property type.

If the serialized property type is a `string` then `ToString()`

If the serialized property type implements `ISerializeConvert<>` it is constructed ( must have default ctor) and then its Convert method is called
with the dependency property value.

```
namespace WpfUIAutomationProperties.Serialization
{
    public interface ISerializeConvert<T>
    {
        void Convert(T value);
    }
}

```
example
```
    public class SerializeConvertBrush : ISerializeConvert<Brush>
    {
        public string BrushType { get; set; }
        public void Convert(Brush brush)
        {
            BrushType = brush.GetType().Name;
        }
    }

    public class SerializedTypeWithConvert
    {
        public SerializeConvertBrush Background { get; set; }
    }

    public static class SetUp {
        public static void ForItemStatus(TextBlock textBlock){
            ItemStatus.ForElement<SerializedTypeWithConvert, TextBlock>(TextBlock);
        }
    }
    

    // in test
    public class ItemStatusTest {
        [Test]
        public void Test(){
            // set up appium...
            var element = windowsDriver.FindElement(MobileBy.AccessibilityId(automationId));
            var itemStatus = element.GetAttribute("ItemStatus");
            var brushType = ItemStatus.Serializer.Deserialize<SerializedTypeWithConvert>(itemStatus).Background.BrushType;
            Assert.That(brushType, Is.EqualTo("SolidColorBrush"));
        }
    }

```

Else the converted value will just be the `default` for the serialized property type.

## Generator

There is a roslyn code generator, SerializedTypeSourceGenerator,  available to assist in creating serialized types.

Just create a partial class / struct with attributes from SerializedTypeSourceGeneratorAttributes applied to ensure correct property names and types.

```C#
[SerializedType(typeof(TextBlock),nameof(TextBlock.Background), nameof(TextBlock.Foreground)]
public partial class SerializedTextBlock {

}
```

Generates

```C#
public partial class SerializedTextBlock {
  public Brush Background {get;set;}
  public Brush Foreground {get;set;}
}
```

```
[SerializedTypeTypedProperty(typeof(TextBlock),typeof(SolidColorBrush),nameof(TextBlock.Background), nameof(TextBlock.Foreground))]
public partial class SerializedTextBlock {

}
```

Generates
``` C#
public partial class SerializedTextBlock {
  public SolidBrush Background {get;set;}
  public SolidBrush Foreground {get;set;}
}
```

There is also SerializedTypeAttachedAttribute and SerializedTypeAttachedTypedPropertyAttribute.









