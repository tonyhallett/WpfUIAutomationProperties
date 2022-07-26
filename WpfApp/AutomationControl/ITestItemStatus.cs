namespace AutomationTest
{
    public interface ITestItemStatus
    {
        bool IsDebug { get; set; }
        string AutomationId { get; }
        bool IsExpectedInitialItemStatus(string initialItemStatus);
        bool IsExpectedChangedItemStatus(string changedItemStatus);
    }
}
