using Microsoft.CodeAnalysis;
using System;

namespace SerializedTypeSourceGenerator
{
    internal abstract class SerializedTypeFrameworkElementAttributeGeneratorBase<T> : SerializedTypeAttributeGeneratorBase<T> where T : Attribute
    {
        protected INamedTypeSymbol frameworkElementNamedTypeSymbol;

        protected override Diagnostic Check()
        {
            frameworkElementNamedTypeSymbol = attributeData.ConstructorArgumentValue<INamedTypeSymbol>(0);
            return Check(frameworkElementNamedTypeSymbol);
        }

        private Diagnostic Check(INamedTypeSymbol frameworkElementNamedTypeSymbol)
        {
            if (!frameworkElementNamedTypeSymbol.DerivesFrom("FrameworkElement", "System.Windows"))
            {
                var typeArgument = attributeSyntax.ArgumentList.Arguments[0];
                var location = typeArgument.GetLocation();
                var diagnosticDescriptor = GetTypeIsNotAFrameworkElementDescriptor();
                return Diagnostic.Create(diagnosticDescriptor, location, TypeKeywords.Get(frameworkElementNamedTypeSymbol.Name));
            }
            return null;
        }

        private static DiagnosticDescriptor GetTypeIsNotAFrameworkElementDescriptor()
        {
            return SerializedTypeDiagnosticDescriptor.Create(
                    2,
                    "Type is not a framework element",
                    "Type {0} is not a framework element",
                    DiagnosticSeverity.Error
            );
        }

    }
}
