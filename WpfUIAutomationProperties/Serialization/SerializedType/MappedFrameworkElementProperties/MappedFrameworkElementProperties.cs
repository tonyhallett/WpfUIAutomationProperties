using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Expr = System.Linq.Expressions.Expression;

namespace WpfUIAutomationProperties.Serialization
{
    internal class MappedFrameworkElementProperties : IMappedFrameworkElementProperties
    {
        internal static class FrameworkElementType
        {
            private class FrameworkElementTypeKey : IEquatable<FrameworkElementTypeKey>
            {
                public FrameworkElementTypeKey(
                    Type frameworkElementType,
                    Type serializedPropertyType,
                    string serializedPropertyName
                )
                {
                    this.FrameworkElementType = frameworkElementType;
                    this.SerializedPropertyType = serializedPropertyType;
                    this.SerializedPropertyName = serializedPropertyName;
                }

                public Type FrameworkElementType { get; }
                public Type SerializedPropertyType { get; }

                public string SerializedPropertyName { get; }

                public bool Equals(FrameworkElementTypeKey other)
                {
                    return other.SerializedPropertyName == SerializedPropertyName && other.FrameworkElementType == FrameworkElementType
                        && other.SerializedPropertyType == SerializedPropertyType;
                }
            }

            private class FrameworkElementTypeDependencyPropertyKey
            {
                public FrameworkElementTypeDependencyPropertyKey(
                    Type frameworkElementType,
                    string serializedPropertyName
                )
                {
                    FrameworkElementType = frameworkElementType;
                    SerializedPropertyName = serializedPropertyName;
                }
                public Type FrameworkElementType { get; }
                public string SerializedPropertyName { get; }
            }

            private static readonly Dictionary<FrameworkElementTypeKey, FuncConvertDependencyProperty<object>> lookup =
                new Dictionary<FrameworkElementTypeKey, FuncConvertDependencyProperty<object>>();

            private static readonly Dictionary<FrameworkElementTypeDependencyPropertyKey, DependencyProperty> dependencyPropertyLookup =
                new Dictionary<FrameworkElementTypeDependencyPropertyKey, DependencyProperty>();

            public static FuncConvertDependencyProperty<object> GetConvertDependencyProperty(
                Type frameworkElementType, PropertyInfo serializedProperty)
            {

                var key = new FrameworkElementTypeKey(
                    frameworkElementType,
                    serializedProperty.PropertyType,
                    serializedProperty.Name
                );

                if (!lookup.TryGetValue(key, out var convertDependencyProperty))
                {
                    convertDependencyProperty = GetConvertDependencyPropertyActual(key);
                    lookup[key] = convertDependencyProperty;
                }

                return convertDependencyProperty;
            }

            private static FuncConvertDependencyProperty<object> GetConvertDependencyPropertyActual(
                FrameworkElementTypeKey key
            )
            {
                var frameworkElementTypeDependencyPropertyKey = new FrameworkElementTypeDependencyPropertyKey(
                    key.FrameworkElementType,
                    key.SerializedPropertyName
                );
                if (!dependencyPropertyLookup.TryGetValue(frameworkElementTypeDependencyPropertyKey, out var dependencyProperty))
                {
                    dependencyProperty = key.FrameworkElementType.FindDependencyProperty(key.SerializedPropertyName);
                    dependencyPropertyLookup[frameworkElementTypeDependencyPropertyKey] = dependencyProperty;
                }
                return ConvertDependencyPropertyFactory.Get(key.SerializedPropertyType, dependencyProperty);
            }
        }

        internal static class AttributedSerializedProperty
        {
            class SerializedPropertyDetails
            {
                public SerializedPropertyDetails(PropertyInfo serializedProperty)
                {
                    this.SerializedProperty = serializedProperty;
                }
                public PropertyInfo SerializedProperty { get; set; }
                private bool hasCheckedForAttribute;
                private AttachedPropertyAttribute attachedPropertyAttribute;
                public AttachedPropertyAttribute AttachedPropertyAttribute
                {
                    get
                    {
                        if (!hasCheckedForAttribute)
                        {
                            attachedPropertyAttribute = SerializedProperty.GetCustomAttribute<AttachedPropertyAttribute>();
                            hasCheckedForAttribute = true;
                        }
                        return attachedPropertyAttribute;
                    }
                }
                public FuncConvertDependencyProperty<object> ConvertDependencyProperty { get; set; }
            }

            private class AttachedPropertyKey : IEquatable<AttachedPropertyKey>
            {
                public AttachedPropertyKey(AttachedPropertyAttribute attachedPropertyAttribute, Type serializedPropertyType)
                {
                    this.SerializedPropertyType = serializedPropertyType;
                    this.AttachedOwnerType = attachedPropertyAttribute.OwnerType;
                    this.AttachedPropertyFieldName = attachedPropertyAttribute.DependencyPropertyFieldName;
                }

                public Type AttachedOwnerType { get; }
                public Type SerializedPropertyType { get; }

                public string AttachedPropertyFieldName { get; }

                public bool Equals(AttachedPropertyKey other)
                {
                    return other.SerializedPropertyType == SerializedPropertyType && other.AttachedOwnerType == AttachedOwnerType && other.AttachedPropertyFieldName.Equals(AttachedPropertyFieldName);
                }
            }


            private static readonly Dictionary<PropertyInfo, SerializedPropertyDetails> propertyDetailsLookup =
                new Dictionary<PropertyInfo, SerializedPropertyDetails>();

            private static readonly Dictionary<AttachedPropertyKey, FuncConvertDependencyProperty<object>> attributeConvertDependencyPropertyLookup =
                new Dictionary<AttachedPropertyKey, FuncConvertDependencyProperty<object>>();

            public static FuncConvertDependencyProperty<object> GetConvertDependencyProperty(PropertyInfo serializedProperty)
            {
                if (!propertyDetailsLookup.TryGetValue(serializedProperty, out var serializedPropertyDetails))
                {
                    serializedPropertyDetails = new SerializedPropertyDetails(serializedProperty);
                }

                if (serializedPropertyDetails.ConvertDependencyProperty != null)
                {
                    return serializedPropertyDetails.ConvertDependencyProperty;
                }
                return GetFromAttribute(serializedPropertyDetails);

            }

            private static FuncConvertDependencyProperty<object> GetFromAttribute(SerializedPropertyDetails serializedPropertyDetails)
            {
                var attachedPropertyAttribute = serializedPropertyDetails.AttachedPropertyAttribute;
                if (attachedPropertyAttribute != null)
                {
                    var key = new AttachedPropertyKey(attachedPropertyAttribute, serializedPropertyDetails.SerializedProperty.PropertyType);
                    if (!attributeConvertDependencyPropertyLookup.TryGetValue(key, out var convertDependencyProperty))
                    {
                        convertDependencyProperty = GetConvertDependencyProperty(key);
                        attributeConvertDependencyPropertyLookup[key] = convertDependencyProperty;
                    }
                    serializedPropertyDetails.ConvertDependencyProperty = convertDependencyProperty;
                    return convertDependencyProperty;
                }

                return null;
            }

            private static FuncConvertDependencyProperty<object> GetConvertDependencyProperty(AttachedPropertyKey attachedPropertyKey)
            {
                var dependencyProperty = attachedPropertyKey.AttachedOwnerType.DependencyPropertyByFullName(attachedPropertyKey.AttachedPropertyFieldName);
                return ConvertDependencyPropertyFactory.Get(attachedPropertyKey.SerializedPropertyType, dependencyProperty);
            }
        }

        internal static class ConvertDependencyPropertyFactory
        {
            class ConvertKey : IEquatable<ConvertKey>
            {
                public ConvertKey(Type frameworkElementPropertyType, Type serializedPropertyType)
                {
                    FrameworkElementPropertyType = frameworkElementPropertyType;
                    SerializedPropertyType = serializedPropertyType;
                }
                public Type FrameworkElementPropertyType { get; set; }
                public Type SerializedPropertyType { get; set; }

                public bool Equals(ConvertKey other)
                {
                    return FrameworkElementPropertyType == other.FrameworkElementPropertyType &&
                        SerializedPropertyType == other.SerializedPropertyType;
                }
            }

            private static readonly Type stringType = typeof(string);
            private static readonly Type serializeConvertType = typeof(ISerializeConvert<>);
            private static readonly Dictionary<Type, Action<object, object>> serializeConvertConvertLookup =
                new Dictionary<Type, Action<object, object>>();
            private static readonly Dictionary<ConvertKey, Func<object, object>> typesConvertLookup =
                new Dictionary<ConvertKey, Func<object, object>>();
            public static FuncConvertDependencyProperty<object> Get(
                Type serializedPropertyType,
                DependencyProperty dependencyProperty
            )
            {
                var convertKey = new ConvertKey(dependencyProperty.PropertyType, serializedPropertyType);
                if (!typesConvertLookup.TryGetValue(convertKey, out var convert))
                {
                    convert = ConvertDependencyPropertyFactory.GetConvert(convertKey);
                    typesConvertLookup.Add(convertKey, convert);
                }

                return new FuncConvertDependencyProperty<object>(dependencyProperty, convert);
            }

            private static Action<object,object> CreateConvert(Type serializeConvertType,Type convertType)
            {
                var instanceParameter = Expr.Parameter(typeof(object));
                var argumentParameter = Expr.Parameter(typeof(object));
                var convertInstance = Expr.Convert(instanceParameter, serializeConvertType);
                var convertArgument = Expr.Convert(argumentParameter, convertType);

                var callExpression = Expr.Call(convertInstance, serializeConvertType.GetMethod("Convert"), convertArgument);
                return Expr.Lambda<Action<object, object>>(callExpression, instanceParameter, argumentParameter).Compile();
            }

            private static Func<object, object> GetConvert(ConvertKey convertKey)
            {
                var serializedPropertyType = convertKey.SerializedPropertyType;
                var frameworkElementPropertyType = convertKey.FrameworkElementPropertyType;

                Func<object, object> convert = null;
                if (frameworkElementPropertyType.IsAssignableFrom(serializedPropertyType))
                {
                    convert = obj => obj;
                }
                else if (serializedPropertyType == stringType)
                {
                    convert = obj => obj?.ToString();
                }
                else
                {
                    convert = obj => ReflectionHelper.GetDefault(serializedPropertyType);

                    var closedSerializedConvertType = serializedPropertyType.FindInterfaces((interfaceType, c) =>
                    {
                        return interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == serializeConvertType;
                    }, null).FirstOrDefault();

                    if (closedSerializedConvertType != null)
                    {
                        var convertType = closedSerializedConvertType.GenericTypeArguments[0];
                        if (convertType == frameworkElementPropertyType)
                        {
                            if (!serializeConvertConvertLookup.TryGetValue(serializeConvertType, out var convertAction))
                            {
                                convertAction = CreateConvert(serializedPropertyType, convertType);
                                serializeConvertConvertLookup.Add(serializedPropertyType, convertAction);
                            }
                            convert = obj =>
                            {
                                var value = Activator.CreateInstance(serializedPropertyType);
                                convertAction(value, obj);
                                return value;
                            };
                        }
                    }

                    
                }
                return convert;
            }
        }


        public MappedFrameworkElementProperties(Type serializedType, Type frameworkElementType)
        {
            var serializedTypeProperties = TypeProperties.Get(serializedType);

            ConvertDependencyProperties = serializedTypeProperties.Select(serializedProperty =>
            {
                return AttributedSerializedProperty.GetConvertDependencyProperty(serializedProperty) ??
                    FrameworkElementType.GetConvertDependencyProperty(frameworkElementType, serializedProperty);
            }).ToList();
        }

        public IEnumerable<IConvertDependencyProperty> ConvertDependencyProperties { get; private set; }
    }
}
