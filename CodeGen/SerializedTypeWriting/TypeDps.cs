using System.Reflection;
using System.Windows;

namespace CodeGen
{
    public class TypeDps
    {
        public TypeDps(Type type)
        {
            Type = type;
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            DependencyProperties = properties.Select(property => GetDependencyProperty(property))
                .Where(dp => dp != null).OfType<DependencyProperty>().OrderBy(dp => dp.PropertyType.FullName).ToList();
        }
        private DependencyProperty? GetDependencyProperty(PropertyInfo property)
        {
            var field = property.DeclaringType!.GetField($"{property.Name}Property", BindingFlags.Public | BindingFlags.Static);
            if (field != null)
            {
                return field.GetValue(null) as DependencyProperty;
            }
            return null;
        }
        public Type Type { get; set; }
        public List<DependencyProperty> DependencyProperties { get; set; }
    }
}
