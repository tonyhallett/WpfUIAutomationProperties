using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal abstract class SerializedTypeAttributeCheckingGeneratorBase<T> : ISerializedTypeAttributeGenerator where T : Attribute
    {
        protected AttributeData attributeData;
        protected AttributeSyntax attributeSyntax;
        public SerializedTypeAttributeCheckingGeneratorBase()
        {
            var type = typeof(T);
            Namespace = type.Namespace;
            FullyQualifiedName = type.FullName;
            TypeNameWithoutAttribute = type.Name.Replace("Attribute", "");
            ContainingAssemblyName = type.Assembly.GetName().Name;
        }
        public string Namespace { get; }

        public string TypeNameWithoutAttribute { get; }

        public string FullyQualifiedName { get; }
        
        public string ContainingAssemblyName { get; }

        public SerializedPropertiesResult GetProperties(
            AttributeData attributeData, 
            AttributeSyntax attributeSyntax
        )
        {
            this.attributeData = attributeData;
            this.attributeSyntax = attributeSyntax;
            var diagnostic = Check();
            if (diagnostic != null)
            {
                return new SerializedPropertiesResult { Diagnostic = diagnostic };
            }

            return new SerializedPropertiesResult { SerializedProperties = GetSerializedProperties() };
        }

        protected virtual Diagnostic Check() => null;

        protected abstract IEnumerable<ISerializedProperty> GetSerializedProperties();
            
    }
}
