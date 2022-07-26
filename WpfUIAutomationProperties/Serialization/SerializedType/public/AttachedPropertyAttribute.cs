using System;

namespace WpfUIAutomationProperties.Serialization
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttachedPropertyAttribute : Attribute
    {
        public AttachedPropertyAttribute(Type ownerType,string dependencyPropertyFieldName)
        {
            OwnerType = ownerType;
            DependencyPropertyFieldName = dependencyPropertyFieldName;
        }

        public Type OwnerType { get; }
        public string DependencyPropertyFieldName { get; }
    }
}
