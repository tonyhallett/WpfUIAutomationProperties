using System;
using System.Reflection;
using System.Windows;

namespace WpfUIAutomationProperties
{
    internal static class DepedencyPropertyExtensions
    {
        public static DependencyProperty CorrespondingDependencyProperty(this PropertyInfo property)
        {
            var declaringType = property.DeclaringType;
            return declaringType.DependencyPropertyByPropertyName(property.Name);
        }
        public static DependencyProperty DependencyPropertyByFullName(this Type type, string fullName)
        {
            return type.GetField(fullName, BindingFlags.Public | BindingFlags.Static).GetValue(null) as DependencyProperty;
        }

        public static DependencyProperty DependencyPropertyByPropertyName(this Type type, string propertyName)
        {
            return type.DependencyPropertyByFullName($"{propertyName}Property");
        }

        public static DependencyProperty FindDependencyProperty(this Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            return property.CorrespondingDependencyProperty();
        }
    }
}
