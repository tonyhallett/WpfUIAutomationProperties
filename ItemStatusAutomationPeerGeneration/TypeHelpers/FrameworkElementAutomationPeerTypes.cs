namespace ItemStatusAutomationPeerGeneration
{
    public class FrameworkElementAutomationPeerTypes
    {
        public FrameworkElementAutomationPeerTypes(Type frameworkElementType, Type? automationPeerType)
        {
            FrameworkElementType = frameworkElementType;
            AutomationPeerType = automationPeerType;
        }

        public Type FrameworkElementType { get; set; }
        public Type? AutomationPeerType { get; set; }
    }
}
