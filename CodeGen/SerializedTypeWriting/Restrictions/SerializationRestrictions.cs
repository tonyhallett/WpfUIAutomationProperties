using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Shell;

namespace CodeGen
{
    public static class SerializationRestrictions
    {
        // of course this is all or nothing
        // later change to include by DependencyProperty

        public static readonly List<Type> SafeTypes = new()
        {
            typeof(DateTime),
            typeof(string),

            typeof(Uri),
            typeof(FontStretch),
            typeof(FontStyle),
            typeof(FontWeight),
            typeof(Point),
            typeof(Thickness),
            typeof(Rect),
            typeof(CornerRadius),
            typeof(FontFamily),
            typeof(DoubleCollection),
            typeof(PointCollection),
            typeof(XmlLanguage),
            typeof(DrawingAttributes)
        };

        public static readonly List<ExplainedType> IgnoreTypes = new()
        {
            ExplainedType.ToExplain(typeof(CacheMode)),
            ExplainedType.ToExplain(typeof(Geometry)),
            ExplainedType.ToExplain(typeof(ImageSource)),
            ExplainedType.ToExplain(typeof(Transform)),
            ExplainedType.ToExplain(typeof(IInputElement)),
            ExplainedType.ToExplain(typeof(ICommand)),
            ExplainedType.ToExplain(typeof(ItemContainerTemplateSelector)),
            ExplainedType.ToExplain(typeof(DataTemplateSelector)),
            ExplainedType.ToExplain(typeof(StyleSelector)),
            ExplainedType.ToExplain(typeof(DataGridColumn)),
            ExplainedType.ToExplain(typeof(IDocumentPaginatorSource)),
            ExplainedType.ToExplain(typeof(ViewBase)),
            ExplainedType.ToExplain(typeof(Camera)),
            ExplainedType.ToExplain(typeof(Visual3DCollection)),
            ExplainedType.ToExplain(typeof(FlowDocument)),
            ExplainedType.ToExplain(typeof(ContextMenu)),
            ExplainedType.ToExplain(typeof(ControlTemplate)),
            ExplainedType.ToExplain(typeof(BindingGroup)),
            ExplainedType.ToExplain(typeof(DataTemplate)),
            ExplainedType.ToExplain(typeof(InputScope)),
            ExplainedType.ToExplain(typeof(BitmapEffectInput)),
            ExplainedType.ToExplain(typeof(TaskbarItemInfo)),
            ExplainedType.ToExplain(typeof(Style)),
            ExplainedType.ToExplain(typeof(ItemsPanelTemplate)),
            ExplainedType.ToExplain(typeof(UIElement)),
            ExplainedType.ToExplain(typeof(GridViewColumn)),
            // could just check if is a delegate
            new ExplainedType(typeof(GroupStyleSelector), "Delegate"),
            new ExplainedType(typeof(CustomPopupPlacementCallback), "Delegate"),
            ExplainedType.ToExplain(typeof(TextDecorationCollection)),
            ExplainedType.ToExplain(typeof(TextEffectCollection)),
            ExplainedType.ToExplain(typeof(StrokeCollection)),
            ExplainedType.ToExplain(typeof(GridViewColumnCollection)),
            ExplainedType.ToExplain(typeof(DataGridCellInfo)),
            ExplainedType.ToExplain(typeof(DataGridLength)),
            new ExplainedType(typeof(Cursor),"Only for file but loses scale"),
            ExplainedType.ToExplain(typeof(IList)),
            ExplainedType.ToExplain(typeof(IEnumerable)),
            ExplainedType.ToExplain(typeof(object)),
            ExplainedType.ToExplain(typeof(BitmapEffect)),
            ExplainedType.ToExplain(typeof(Effect)),

        };

        public static readonly List<ExplainedType> ConditionallySafeTypes = new()
        {
            new ExplainedType(typeof(Brush),"SolidColorBrush serializes.")
        };

        public static readonly List<DependencyProperty> IgnoredDependencyProperties = new()
        {
            FrameworkElement.DataContextProperty,
            FrameworkElement.TagProperty,
            ButtonBase.CommandParameterProperty,
            ToolTipService.ToolTipProperty,
            GridView.ColumnHeaderToolTipProperty,
            MenuItem.IconProperty,
            FixedPage.PrintTicketProperty,
            TreeView.SelectedItemProperty
        };

    }
}
