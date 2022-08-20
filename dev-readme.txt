There are 8 projects.
**WpfUIAutomationProperties** is the nuget package that provides 3 different ways to supply additional WPF Framework Element
property values to WindowsDriver as the ItemStatus attribute of an AppiumElement.
This is a string value, so to provide multiple values an object is serialized.  This serializer, available as the ItemStatus.Serializer property, defaults to a 
JsonConvertSerializer that is set up to properly handle serialized dictionaries as this is one 
of the two ways this package serializes framework element properties).

The 3 methods are as follows - note that only multi-binding will be documented.

a) Static Item Status ( perhaps not the best name)
Here the **CodeGen** project ( using **ItemStatusAutomationPeerGeneration**) generates for some framework element types an 
AutomationPeer derivation that gets the item status from the Func constructor argument e.g 
Func<Button,string,string> getItemStatus
and a framework element type derivation that uses this peer with an internal Func e.g ItemStatus.SerializeItemStatusForTypeButton.
internal static string SerializeItemStatusForTypeButton(Button element, string itemStatus){
    if (UseItemStatusDependencyProperty){
        return itemStatus;
    }

    if(SerializeForButton != null){
        return Serializer.Serialize(SerializeForButton(element, itemStatus));
    }
    else{
        return ForButton(element, itemStatus);
    }
}
The dev needs to set either of the following two static Func properties.
orType to supply the ItemStatus string or SerializeForType to supply an object to be serialized.

Note that Static Item Status is not available for all framework element types hence solutions b) and c) which are configured the same but work differently.
--

b) Static constructor
This is so called as it requires the creation of a derived framework element class and configuration to be done in the static constructor.
The configuration of the Item Status thus applies to all instances of the derivation.
Again we use an ItemStatus static class. There is a serializer property and it is possible to supply 
a serializer just for the type.

Static constructor and multibinding are similar in that the serialized value takes the property values that you specify from the framework element.  
It does this with a list of DependencyProperty or IConvertDependencyProperty.  If you supply IConvertDependencyProperty 
( which includes the DependencyProperty ) then the dependency property value will be converted.
Both static constructor and multibinding allow you to supply a Type with properties that are reflected upon to supply these details.
This makes testing type safe. Otherwise you are working with a dictionary.  More on this process later.

Below is the signature if providing a list of DependencyProperty.
public static void ForType<TFrameworkElement>(
			List<DependencyProperty> dependencyProperties,
			Func<Dictionary<string, object>, object> itemStatusesConverter = null,
			Func<object, string> itemStatusSerializer = null
		)
The second argument defaults to no conversion and a dictionary is serialized.

How it works:

The ForType methods override the metadata for each DependencyProperty so as to provide the PropertyChangedCallback.
Each property change ( and initial ) updates a Dictionary with key the property name and value the possibly converted property value.
This dictionary is passed to the converter argument ( default no convert ) and then serialized for setting the ItemStatus dependency property.
When using the overload where the serialized type is supplied, a converter is provided to create the serialized type and set its properties.

c) Multi-binding

This too has a static ItemStatus class with an Serializer property and the ForElement methods can be passed a serializer similarly to b.
It too has a converter argument when supplying a list of DependencyProperty or IConvertDependencyProperty and has overloads for supplying a serialized type.
It is possible to set up in xaml too.  Both examples use a trigger so that no supplying ItemStatus when in release mode.

Using a serialized type.
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

Providing IConvertDependencyProperty
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

How it works

Unsurprisingly it creates a multi-binding to the ItemStatus dependency property.  There is a binding for each dependency property.  
The value is converted to a tuple with the property name and possibly converted property value.  The multi-binding has a converter
that turns the individual values to a dictionary to be possibly converted by the converter argument before being serialized.



Reflection process
For both b) and c) the MappedFrameworkElementPropertiesFactory determines the list of IConvertDependencyProperty from the 
public properties of the serialized type.  If not an attached property then the dependency property is found on the framework element type else the serialized type property 
should have the AttachedPropertyAttribute applied.
e.g
[AttachedProperty(typeof(OwnerType),nameof(OwnerType.APropertyProperty))]
public SomeType AProperty {get;set;}

Todo ** Should be possible to supply any property name in case the framework element also has the property.
( That was the point of the second argument)

There will be no conversion if the dependency property type can be assigned to the serialized property type.
If the serialized property type is a string then ToString()
If the serialized property type implements ISerializeConvert<> it is constructed ( must have default ctor) and then its Convert method is called
with the dependency property value.
Else the converted value will just be the default for the serialized property type.


The ReflectionActivatingPropertySetterFactory will create the Serialized Type and set its properties from the Dictionary.
Note that the serialized type must have a default constructor.

It is possible to use the Reflection process in a) with FrameworkElementMapper.
    var buttonMapper = new FrameworkElementMapper<SerializedTextBlock, TextBlock>();
    AutomationItemStatus.SerializeForButton = (button, itemStatus) =>
    {
        return buttonMapper.Map(button);
    };

The solution also has the SerializedTypeSourceGenerator** and **SerializedTypeSourceGeneratorAttributes** projects to aid in creating
serialized types.  The generator is tested by **SourceGeneratorTest**.

Just create a partial class / struct with attributes applied to ensure correct property names and types.


[SerializedType(typeof(TextBlock),nameof(TextBlock.Background), nameof(TextBlock.Foreground))]
public partial class SerializedTextBlock {

}
Generates
public partial class SerializedTextBlock {
  public Brush Background {get;set;}
  public Brush Foreground {get;set;}
}

[SerializedTypeTypedProperty(typeof(TextBlock),typeof(SolidColorBrush),nameof(TextBlock.Background), nameof(TextBlock.Foreground))]
public partial class SerializedTextBlock {

}

Generates
public partial class SerializedTextBlock {
  public SolidBrush Background {get;set;}
  public SolidBrush Foreground {get;set;}
}

There is also SerializedTypeAttachedAttribute and SerializedTypeAttachedTypedPropertyAttribute.

To do *****
When consuming the generator, SerializedTypeSourceGeneratorAttributes appears in Dependencies / Analyzers.
See https://stackoverflow.com/questions/70500618/how-to-hide-a-local-dll-reference-in-analyzer-dependencies-and-only-have-just-an
https://github.com/dotnet/roslyn/discussions/47517



WPFUIAutomationProperties is tested by **AutomationTest** that tests **WpfApp**, a WPF app that uses WPFUIAutomationProperties.
WpfApp exposes a set of tests and constants.  Each test sets up a TextBlock for ItemStatus with one of the three methods and has an AutomationIdentity 
that is passed to the app from AutomationTest.  AutomationTest will find the element from the automation id, ask the test if the initial items status 
is expected then change the test and ask if the changed item status is expected.
Note that both WpfApp and AutomationTest are multi targeted to show that WPFUIAutomationProperties works for WPF .Net Framework and WPF .Net Core.
Also tests are run for debug and release - only purpose is to show that it is possible to only set up for ItemStatus when in Debug mode.






