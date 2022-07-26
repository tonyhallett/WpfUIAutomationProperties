using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ItemStatusAutomationPeerGeneration
{
    public static class FrameworkElementTypeProvider
    {
        public static IEnumerable<Type> Types()
        {
            var assembly = typeof(Button).Assembly;
            var frameworkElementType = typeof(FrameworkElement);
            var frameworkElementTypes = assembly.ExportedTypes.Where(t => t.IsSubclassOf(frameworkElementType) && !t.IsAbstract);//117
            return frameworkElementTypes;
        }

        public static IEnumerable<Type> DerivableTypes()
        {
            return Types().Where(type => MsTypeHelper.IsDerivable(type));
        }
    }
}
