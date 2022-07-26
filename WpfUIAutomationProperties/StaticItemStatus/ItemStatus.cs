using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System;
using WpfUIAutomationProperties.Serialization;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public static class ItemStatus
    {
        public static bool UseItemStatusDependencyProperty {get;set;}
        public static IItemStatusSerializer ItemStatusSerializer { get; set; } = new ItemStatusTypedDictionarySerializer();
        
        internal static string SerializeItemStatusForTypeWindow(Window element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForWindow != null){
               return ItemStatusSerializer.Serialize(SerializeForWindow(element, itemStatus));
            }
            else{
                return ForWindow(element, itemStatus);
            }
        }
        public static Func<Window,string,object> SerializeForWindow {get;set;}
        public static Func<Window,string,string> ForWindow {get;set;}



        internal static string SerializeItemStatusForTypeNavigationWindow(NavigationWindow element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForNavigationWindow != null){
               return ItemStatusSerializer.Serialize(SerializeForNavigationWindow(element, itemStatus));
            }
            else{
                return ForNavigationWindow(element, itemStatus);
            }
        }
        public static Func<NavigationWindow,string,object> SerializeForNavigationWindow {get;set;}
        public static Func<NavigationWindow,string,string> ForNavigationWindow {get;set;}



        internal static string SerializeItemStatusForTypeButton(Button element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForButton != null){
               return ItemStatusSerializer.Serialize(SerializeForButton(element, itemStatus));
            }
            else{
                return ForButton(element, itemStatus);
            }
        }
        public static Func<Button,string,object> SerializeForButton {get;set;}
        public static Func<Button,string,string> ForButton {get;set;}



        internal static string SerializeItemStatusForTypeCheckBox(CheckBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForCheckBox != null){
               return ItemStatusSerializer.Serialize(SerializeForCheckBox(element, itemStatus));
            }
            else{
                return ForCheckBox(element, itemStatus);
            }
        }
        public static Func<CheckBox,string,object> SerializeForCheckBox {get;set;}
        public static Func<CheckBox,string,string> ForCheckBox {get;set;}



        internal static string SerializeItemStatusForTypeComboBox(ComboBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForComboBox != null){
               return ItemStatusSerializer.Serialize(SerializeForComboBox(element, itemStatus));
            }
            else{
                return ForComboBox(element, itemStatus);
            }
        }
        public static Func<ComboBox,string,object> SerializeForComboBox {get;set;}
        public static Func<ComboBox,string,string> ForComboBox {get;set;}



        internal static string SerializeItemStatusForTypeComboBoxItem(ComboBoxItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForComboBoxItem != null){
               return ItemStatusSerializer.Serialize(SerializeForComboBoxItem(element, itemStatus));
            }
            else{
                return ForComboBoxItem(element, itemStatus);
            }
        }
        public static Func<ComboBoxItem,string,object> SerializeForComboBoxItem {get;set;}
        public static Func<ComboBoxItem,string,string> ForComboBoxItem {get;set;}



        internal static string SerializeItemStatusForTypeContextMenu(ContextMenu element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForContextMenu != null){
               return ItemStatusSerializer.Serialize(SerializeForContextMenu(element, itemStatus));
            }
            else{
                return ForContextMenu(element, itemStatus);
            }
        }
        public static Func<ContextMenu,string,object> SerializeForContextMenu {get;set;}
        public static Func<ContextMenu,string,string> ForContextMenu {get;set;}



        internal static string SerializeItemStatusForTypeDocumentViewer(DocumentViewer element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForDocumentViewer != null){
               return ItemStatusSerializer.Serialize(SerializeForDocumentViewer(element, itemStatus));
            }
            else{
                return ForDocumentViewer(element, itemStatus);
            }
        }
        public static Func<DocumentViewer,string,object> SerializeForDocumentViewer {get;set;}
        public static Func<DocumentViewer,string,string> ForDocumentViewer {get;set;}



        internal static string SerializeItemStatusForTypeExpander(Expander element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForExpander != null){
               return ItemStatusSerializer.Serialize(SerializeForExpander(element, itemStatus));
            }
            else{
                return ForExpander(element, itemStatus);
            }
        }
        public static Func<Expander,string,object> SerializeForExpander {get;set;}
        public static Func<Expander,string,string> ForExpander {get;set;}



        internal static string SerializeItemStatusForTypeFlowDocumentReader(FlowDocumentReader element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForFlowDocumentReader != null){
               return ItemStatusSerializer.Serialize(SerializeForFlowDocumentReader(element, itemStatus));
            }
            else{
                return ForFlowDocumentReader(element, itemStatus);
            }
        }
        public static Func<FlowDocumentReader,string,object> SerializeForFlowDocumentReader {get;set;}
        public static Func<FlowDocumentReader,string,string> ForFlowDocumentReader {get;set;}



        internal static string SerializeItemStatusForTypeFlowDocumentScrollViewer(FlowDocumentScrollViewer element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForFlowDocumentScrollViewer != null){
               return ItemStatusSerializer.Serialize(SerializeForFlowDocumentScrollViewer(element, itemStatus));
            }
            else{
                return ForFlowDocumentScrollViewer(element, itemStatus);
            }
        }
        public static Func<FlowDocumentScrollViewer,string,object> SerializeForFlowDocumentScrollViewer {get;set;}
        public static Func<FlowDocumentScrollViewer,string,string> ForFlowDocumentScrollViewer {get;set;}



        internal static string SerializeItemStatusForTypeFrame(Frame element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForFrame != null){
               return ItemStatusSerializer.Serialize(SerializeForFrame(element, itemStatus));
            }
            else{
                return ForFrame(element, itemStatus);
            }
        }
        public static Func<Frame,string,object> SerializeForFrame {get;set;}
        public static Func<Frame,string,string> ForFrame {get;set;}



        internal static string SerializeItemStatusForTypeGridSplitter(GridSplitter element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForGridSplitter != null){
               return ItemStatusSerializer.Serialize(SerializeForGridSplitter(element, itemStatus));
            }
            else{
                return ForGridSplitter(element, itemStatus);
            }
        }
        public static Func<GridSplitter,string,object> SerializeForGridSplitter {get;set;}
        public static Func<GridSplitter,string,string> ForGridSplitter {get;set;}



        internal static string SerializeItemStatusForTypeGridViewColumnHeader(GridViewColumnHeader element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForGridViewColumnHeader != null){
               return ItemStatusSerializer.Serialize(SerializeForGridViewColumnHeader(element, itemStatus));
            }
            else{
                return ForGridViewColumnHeader(element, itemStatus);
            }
        }
        public static Func<GridViewColumnHeader,string,object> SerializeForGridViewColumnHeader {get;set;}
        public static Func<GridViewColumnHeader,string,string> ForGridViewColumnHeader {get;set;}



        internal static string SerializeItemStatusForTypeGridViewHeaderRowPresenter(GridViewHeaderRowPresenter element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForGridViewHeaderRowPresenter != null){
               return ItemStatusSerializer.Serialize(SerializeForGridViewHeaderRowPresenter(element, itemStatus));
            }
            else{
                return ForGridViewHeaderRowPresenter(element, itemStatus);
            }
        }
        public static Func<GridViewHeaderRowPresenter,string,object> SerializeForGridViewHeaderRowPresenter {get;set;}
        public static Func<GridViewHeaderRowPresenter,string,string> ForGridViewHeaderRowPresenter {get;set;}



        internal static string SerializeItemStatusForTypeGroupBox(GroupBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForGroupBox != null){
               return ItemStatusSerializer.Serialize(SerializeForGroupBox(element, itemStatus));
            }
            else{
                return ForGroupBox(element, itemStatus);
            }
        }
        public static Func<GroupBox,string,object> SerializeForGroupBox {get;set;}
        public static Func<GroupBox,string,string> ForGroupBox {get;set;}



        internal static string SerializeItemStatusForTypeGroupItem(GroupItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForGroupItem != null){
               return ItemStatusSerializer.Serialize(SerializeForGroupItem(element, itemStatus));
            }
            else{
                return ForGroupItem(element, itemStatus);
            }
        }
        public static Func<GroupItem,string,object> SerializeForGroupItem {get;set;}
        public static Func<GroupItem,string,string> ForGroupItem {get;set;}



        internal static string SerializeItemStatusForTypeImage(Image element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForImage != null){
               return ItemStatusSerializer.Serialize(SerializeForImage(element, itemStatus));
            }
            else{
                return ForImage(element, itemStatus);
            }
        }
        public static Func<Image,string,object> SerializeForImage {get;set;}
        public static Func<Image,string,string> ForImage {get;set;}



        internal static string SerializeItemStatusForTypeInkCanvas(InkCanvas element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForInkCanvas != null){
               return ItemStatusSerializer.Serialize(SerializeForInkCanvas(element, itemStatus));
            }
            else{
                return ForInkCanvas(element, itemStatus);
            }
        }
        public static Func<InkCanvas,string,object> SerializeForInkCanvas {get;set;}
        public static Func<InkCanvas,string,string> ForInkCanvas {get;set;}



        internal static string SerializeItemStatusForTypeInkPresenter(InkPresenter element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForInkPresenter != null){
               return ItemStatusSerializer.Serialize(SerializeForInkPresenter(element, itemStatus));
            }
            else{
                return ForInkPresenter(element, itemStatus);
            }
        }
        public static Func<InkPresenter,string,object> SerializeForInkPresenter {get;set;}
        public static Func<InkPresenter,string,string> ForInkPresenter {get;set;}



        internal static string SerializeItemStatusForTypeLabel(Label element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForLabel != null){
               return ItemStatusSerializer.Serialize(SerializeForLabel(element, itemStatus));
            }
            else{
                return ForLabel(element, itemStatus);
            }
        }
        public static Func<Label,string,object> SerializeForLabel {get;set;}
        public static Func<Label,string,string> ForLabel {get;set;}



        internal static string SerializeItemStatusForTypeListBox(ListBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForListBox != null){
               return ItemStatusSerializer.Serialize(SerializeForListBox(element, itemStatus));
            }
            else{
                return ForListBox(element, itemStatus);
            }
        }
        public static Func<ListBox,string,object> SerializeForListBox {get;set;}
        public static Func<ListBox,string,string> ForListBox {get;set;}



        internal static string SerializeItemStatusForTypeListBoxItem(ListBoxItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForListBoxItem != null){
               return ItemStatusSerializer.Serialize(SerializeForListBoxItem(element, itemStatus));
            }
            else{
                return ForListBoxItem(element, itemStatus);
            }
        }
        public static Func<ListBoxItem,string,object> SerializeForListBoxItem {get;set;}
        public static Func<ListBoxItem,string,string> ForListBoxItem {get;set;}



        internal static string SerializeItemStatusForTypeListView(ListView element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForListView != null){
               return ItemStatusSerializer.Serialize(SerializeForListView(element, itemStatus));
            }
            else{
                return ForListView(element, itemStatus);
            }
        }
        public static Func<ListView,string,object> SerializeForListView {get;set;}
        public static Func<ListView,string,string> ForListView {get;set;}



        internal static string SerializeItemStatusForTypeListViewItem(ListViewItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForListViewItem != null){
               return ItemStatusSerializer.Serialize(SerializeForListViewItem(element, itemStatus));
            }
            else{
                return ForListViewItem(element, itemStatus);
            }
        }
        public static Func<ListViewItem,string,object> SerializeForListViewItem {get;set;}
        public static Func<ListViewItem,string,string> ForListViewItem {get;set;}



        internal static string SerializeItemStatusForTypeMediaElement(MediaElement element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForMediaElement != null){
               return ItemStatusSerializer.Serialize(SerializeForMediaElement(element, itemStatus));
            }
            else{
                return ForMediaElement(element, itemStatus);
            }
        }
        public static Func<MediaElement,string,object> SerializeForMediaElement {get;set;}
        public static Func<MediaElement,string,string> ForMediaElement {get;set;}



        internal static string SerializeItemStatusForTypeMenu(Menu element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForMenu != null){
               return ItemStatusSerializer.Serialize(SerializeForMenu(element, itemStatus));
            }
            else{
                return ForMenu(element, itemStatus);
            }
        }
        public static Func<Menu,string,object> SerializeForMenu {get;set;}
        public static Func<Menu,string,string> ForMenu {get;set;}



        internal static string SerializeItemStatusForTypeMenuItem(MenuItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForMenuItem != null){
               return ItemStatusSerializer.Serialize(SerializeForMenuItem(element, itemStatus));
            }
            else{
                return ForMenuItem(element, itemStatus);
            }
        }
        public static Func<MenuItem,string,object> SerializeForMenuItem {get;set;}
        public static Func<MenuItem,string,string> ForMenuItem {get;set;}



        internal static string SerializeItemStatusForTypeProgressBar(ProgressBar element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForProgressBar != null){
               return ItemStatusSerializer.Serialize(SerializeForProgressBar(element, itemStatus));
            }
            else{
                return ForProgressBar(element, itemStatus);
            }
        }
        public static Func<ProgressBar,string,object> SerializeForProgressBar {get;set;}
        public static Func<ProgressBar,string,string> ForProgressBar {get;set;}



        internal static string SerializeItemStatusForTypeRadioButton(RadioButton element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForRadioButton != null){
               return ItemStatusSerializer.Serialize(SerializeForRadioButton(element, itemStatus));
            }
            else{
                return ForRadioButton(element, itemStatus);
            }
        }
        public static Func<RadioButton,string,object> SerializeForRadioButton {get;set;}
        public static Func<RadioButton,string,string> ForRadioButton {get;set;}



        internal static string SerializeItemStatusForTypeRichTextBox(RichTextBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForRichTextBox != null){
               return ItemStatusSerializer.Serialize(SerializeForRichTextBox(element, itemStatus));
            }
            else{
                return ForRichTextBox(element, itemStatus);
            }
        }
        public static Func<RichTextBox,string,object> SerializeForRichTextBox {get;set;}
        public static Func<RichTextBox,string,string> ForRichTextBox {get;set;}



        internal static string SerializeItemStatusForTypeScrollViewer(ScrollViewer element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForScrollViewer != null){
               return ItemStatusSerializer.Serialize(SerializeForScrollViewer(element, itemStatus));
            }
            else{
                return ForScrollViewer(element, itemStatus);
            }
        }
        public static Func<ScrollViewer,string,object> SerializeForScrollViewer {get;set;}
        public static Func<ScrollViewer,string,string> ForScrollViewer {get;set;}



        internal static string SerializeItemStatusForTypeSeparator(Separator element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForSeparator != null){
               return ItemStatusSerializer.Serialize(SerializeForSeparator(element, itemStatus));
            }
            else{
                return ForSeparator(element, itemStatus);
            }
        }
        public static Func<Separator,string,object> SerializeForSeparator {get;set;}
        public static Func<Separator,string,string> ForSeparator {get;set;}



        internal static string SerializeItemStatusForTypeFlowDocumentPageViewer(FlowDocumentPageViewer element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForFlowDocumentPageViewer != null){
               return ItemStatusSerializer.Serialize(SerializeForFlowDocumentPageViewer(element, itemStatus));
            }
            else{
                return ForFlowDocumentPageViewer(element, itemStatus);
            }
        }
        public static Func<FlowDocumentPageViewer,string,object> SerializeForFlowDocumentPageViewer {get;set;}
        public static Func<FlowDocumentPageViewer,string,string> ForFlowDocumentPageViewer {get;set;}



        internal static string SerializeItemStatusForTypeSlider(Slider element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForSlider != null){
               return ItemStatusSerializer.Serialize(SerializeForSlider(element, itemStatus));
            }
            else{
                return ForSlider(element, itemStatus);
            }
        }
        public static Func<Slider,string,object> SerializeForSlider {get;set;}
        public static Func<Slider,string,string> ForSlider {get;set;}



        internal static string SerializeItemStatusForTypeTabControl(TabControl element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTabControl != null){
               return ItemStatusSerializer.Serialize(SerializeForTabControl(element, itemStatus));
            }
            else{
                return ForTabControl(element, itemStatus);
            }
        }
        public static Func<TabControl,string,object> SerializeForTabControl {get;set;}
        public static Func<TabControl,string,string> ForTabControl {get;set;}



        internal static string SerializeItemStatusForTypeTabItem(TabItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTabItem != null){
               return ItemStatusSerializer.Serialize(SerializeForTabItem(element, itemStatus));
            }
            else{
                return ForTabItem(element, itemStatus);
            }
        }
        public static Func<TabItem,string,object> SerializeForTabItem {get;set;}
        public static Func<TabItem,string,string> ForTabItem {get;set;}



        internal static string SerializeItemStatusForTypeTextBlock(TextBlock element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTextBlock != null){
               return ItemStatusSerializer.Serialize(SerializeForTextBlock(element, itemStatus));
            }
            else{
                return ForTextBlock(element, itemStatus);
            }
        }
        public static Func<TextBlock,string,object> SerializeForTextBlock {get;set;}
        public static Func<TextBlock,string,string> ForTextBlock {get;set;}



        internal static string SerializeItemStatusForTypeTextBox(TextBox element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTextBox != null){
               return ItemStatusSerializer.Serialize(SerializeForTextBox(element, itemStatus));
            }
            else{
                return ForTextBox(element, itemStatus);
            }
        }
        public static Func<TextBox,string,object> SerializeForTextBox {get;set;}
        public static Func<TextBox,string,string> ForTextBox {get;set;}



        internal static string SerializeItemStatusForTypeToolBar(ToolBar element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForToolBar != null){
               return ItemStatusSerializer.Serialize(SerializeForToolBar(element, itemStatus));
            }
            else{
                return ForToolBar(element, itemStatus);
            }
        }
        public static Func<ToolBar,string,object> SerializeForToolBar {get;set;}
        public static Func<ToolBar,string,string> ForToolBar {get;set;}



        internal static string SerializeItemStatusForTypeToolTip(ToolTip element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForToolTip != null){
               return ItemStatusSerializer.Serialize(SerializeForToolTip(element, itemStatus));
            }
            else{
                return ForToolTip(element, itemStatus);
            }
        }
        public static Func<ToolTip,string,object> SerializeForToolTip {get;set;}
        public static Func<ToolTip,string,string> ForToolTip {get;set;}



        internal static string SerializeItemStatusForTypeTreeView(TreeView element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTreeView != null){
               return ItemStatusSerializer.Serialize(SerializeForTreeView(element, itemStatus));
            }
            else{
                return ForTreeView(element, itemStatus);
            }
        }
        public static Func<TreeView,string,object> SerializeForTreeView {get;set;}
        public static Func<TreeView,string,string> ForTreeView {get;set;}



        internal static string SerializeItemStatusForTypeTreeViewItem(TreeViewItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForTreeViewItem != null){
               return ItemStatusSerializer.Serialize(SerializeForTreeViewItem(element, itemStatus));
            }
            else{
                return ForTreeViewItem(element, itemStatus);
            }
        }
        public static Func<TreeViewItem,string,object> SerializeForTreeViewItem {get;set;}
        public static Func<TreeViewItem,string,string> ForTreeViewItem {get;set;}



        internal static string SerializeItemStatusForTypeUserControl(UserControl element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForUserControl != null){
               return ItemStatusSerializer.Serialize(SerializeForUserControl(element, itemStatus));
            }
            else{
                return ForUserControl(element, itemStatus);
            }
        }
        public static Func<UserControl,string,object> SerializeForUserControl {get;set;}
        public static Func<UserControl,string,string> ForUserControl {get;set;}



        internal static string SerializeItemStatusForTypeViewport3D(Viewport3D element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForViewport3D != null){
               return ItemStatusSerializer.Serialize(SerializeForViewport3D(element, itemStatus));
            }
            else{
                return ForViewport3D(element, itemStatus);
            }
        }
        public static Func<Viewport3D,string,object> SerializeForViewport3D {get;set;}
        public static Func<Viewport3D,string,string> ForViewport3D {get;set;}



        internal static string SerializeItemStatusForTypeDocumentPageView(DocumentPageView element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForDocumentPageView != null){
               return ItemStatusSerializer.Serialize(SerializeForDocumentPageView(element, itemStatus));
            }
            else{
                return ForDocumentPageView(element, itemStatus);
            }
        }
        public static Func<DocumentPageView,string,object> SerializeForDocumentPageView {get;set;}
        public static Func<DocumentPageView,string,string> ForDocumentPageView {get;set;}



        internal static string SerializeItemStatusForTypeRepeatButton(RepeatButton element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForRepeatButton != null){
               return ItemStatusSerializer.Serialize(SerializeForRepeatButton(element, itemStatus));
            }
            else{
                return ForRepeatButton(element, itemStatus);
            }
        }
        public static Func<RepeatButton,string,object> SerializeForRepeatButton {get;set;}
        public static Func<RepeatButton,string,string> ForRepeatButton {get;set;}



        internal static string SerializeItemStatusForTypeScrollBar(ScrollBar element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForScrollBar != null){
               return ItemStatusSerializer.Serialize(SerializeForScrollBar(element, itemStatus));
            }
            else{
                return ForScrollBar(element, itemStatus);
            }
        }
        public static Func<ScrollBar,string,object> SerializeForScrollBar {get;set;}
        public static Func<ScrollBar,string,string> ForScrollBar {get;set;}



        internal static string SerializeItemStatusForTypeStatusBar(StatusBar element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForStatusBar != null){
               return ItemStatusSerializer.Serialize(SerializeForStatusBar(element, itemStatus));
            }
            else{
                return ForStatusBar(element, itemStatus);
            }
        }
        public static Func<StatusBar,string,object> SerializeForStatusBar {get;set;}
        public static Func<StatusBar,string,string> ForStatusBar {get;set;}



        internal static string SerializeItemStatusForTypeStatusBarItem(StatusBarItem element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForStatusBarItem != null){
               return ItemStatusSerializer.Serialize(SerializeForStatusBarItem(element, itemStatus));
            }
            else{
                return ForStatusBarItem(element, itemStatus);
            }
        }
        public static Func<StatusBarItem,string,object> SerializeForStatusBarItem {get;set;}
        public static Func<StatusBarItem,string,string> ForStatusBarItem {get;set;}



        internal static string SerializeItemStatusForTypeThumb(Thumb element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForThumb != null){
               return ItemStatusSerializer.Serialize(SerializeForThumb(element, itemStatus));
            }
            else{
                return ForThumb(element, itemStatus);
            }
        }
        public static Func<Thumb,string,object> SerializeForThumb {get;set;}
        public static Func<Thumb,string,string> ForThumb {get;set;}



        internal static string SerializeItemStatusForTypeToggleButton(ToggleButton element, string itemStatus){
            if (UseItemStatusDependencyProperty){
                return itemStatus;
            }

            if(SerializeForToggleButton != null){
               return ItemStatusSerializer.Serialize(SerializeForToggleButton(element, itemStatus));
            }
            else{
                return ForToggleButton(element, itemStatus);
            }
        }
        public static Func<ToggleButton,string,object> SerializeForToggleButton {get;set;}
        public static Func<ToggleButton,string,string> ForToggleButton {get;set;}



        
    }
}