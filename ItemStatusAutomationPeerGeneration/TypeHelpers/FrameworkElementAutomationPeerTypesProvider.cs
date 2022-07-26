using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Automation.Peers;

namespace ItemStatusAutomationPeerGeneration
{
    internal static class FrameworkElementAutomationPeerTypesProvider
    {
        private static readonly Type UIElementType = typeof(UIElement);
        public static List<FrameworkElementAutomationPeerTypes> Provide()
        {
            List<FrameworkElementAutomationPeerTypes>? result = null;
            var thread = new Thread(() =>
            {
                result = FrameworkElementTypeProvider.DerivableTypes().Select(frameworkElementType =>
                {
                    return new FrameworkElementAutomationPeerTypes(frameworkElementType, GetAllowedAutomationPeerType(frameworkElementType));
                }).Where(types => types.AutomationPeerType != null).ToList();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            return result!;
        }

        private static Type? GetAllowedAutomationPeerType(Type frameworkElementType)
        {
            if (DoesNotOverrideOnCreateAutomationPeer(frameworkElementType))
            {
                return null;
            }
            return GetDerivableAutomationPeerType(frameworkElementType);
        }

        private static Type? GetDerivableAutomationPeerType(Type frameworkElementType)
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            var instance = Activator.CreateInstance(frameworkElementType) as FrameworkElement;
            var automationPeer = UIElementAutomationPeer.CreatePeerForElement(instance);
            var automationPeerType = automationPeer.GetType();
            return MsTypeHelper.IsDerivable(automationPeerType) ? automationPeerType : null;
        }

        private static bool DoesNotOverrideOnCreateAutomationPeer(Type frameworkElementType)
        {
            var onCreateAutomationPeerDeclaringType = frameworkElementType.GetMethod("OnCreateAutomationPeer", BindingFlags.Instance | BindingFlags.NonPublic)!.DeclaringType;
            return onCreateAutomationPeerDeclaringType == UIElementType;
        }
    }
}
