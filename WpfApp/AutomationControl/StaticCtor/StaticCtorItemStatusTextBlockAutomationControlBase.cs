﻿using System.Windows.Controls;

namespace AutomationTest
{
    public abstract class StaticCtorItemStatusTextBlockAutomationControlBase : TextBlockAutomationControl
    {
        public StaticCtorItemStatusTextBlockAutomationControlBase(string automationId) : base(automationId) { }
        protected override void Setup()
        {
            TextBlock = IsDebug ? GetItemStatusTextBlock() : new TextBlock();
            base.Setup();
        }

        protected abstract TextBlock GetItemStatusTextBlock();
    }

}
