namespace AutomationTest
{
    public interface IItemStatusTest
    {
        bool IsDebug { get; set; }
        string AutomationId { get; }
        bool IsExpectedInitialItemStatus(string initialItemStatus);
        bool IsExpectedChangedItemStatus(string changedItemStatus);
    }
}
