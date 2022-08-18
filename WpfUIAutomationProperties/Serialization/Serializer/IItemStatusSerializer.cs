namespace WpfUIAutomationProperties.Serialization
{
    public interface IItemStatusSerializer
    {
        string Serialize(object itemStatus);

        T Deserialize<T>(string serialized);
        
    }
}
