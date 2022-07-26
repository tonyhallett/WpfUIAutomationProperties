namespace AutomationTest
{
    public static class ApplyWhenDebugOnly
    {
        static ApplyWhenDebugOnly()
        {
#if DEBUG
            Apply = true;
#endif
        }

        public static bool Apply { get; set; }
    }
}
