using Wodsoft.UI.Media;

namespace Wodsoft.UI.Controls;

/// <summary>
/// Defines an area where you can arrange child elements either horizontally or vertically, relative to each other.
/// </summary>
public partial class DockPanel : Panel
{
    private static void DockChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is Visual v && VisualTreeHelper.GetParent(v) is DockPanel dockPanel)
        {
            dockPanel.InvalidateMeasure();
        }
    }

    private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var dockPanel = (DockPanel)sender;
        dockPanel.InvalidateMeasure();
    }

    /// <inheritdoc />
    protected override Size ArrangeOverride(Size finalSize)
    {
        if (Children.Count is 0)
            return finalSize;

        var currentBounds = new Rect(
            Padding.Left,
            Padding.Top,
            GetPositiveOrZero(finalSize.Width - Padding.Left - Padding.Right),
            GetPositiveOrZero(finalSize.Height - Padding.Top - Padding.Bottom));
        var childrenCount = LastChildFill ? Children.Count - 1 : Children.Count;

        for (var index = 0; index < childrenCount; ++index)
        {
            var child = Children[index];
            var dock = (Dock)child.GetValue(DockProperty);
            float width, height;
            switch (dock)
            {
                case Dock.Left:

                    width = MathF.Min(child.DesiredSize.Width, currentBounds.Width);
                    child.Arrange(new Rect(currentBounds.X, currentBounds.Y, width, currentBounds.Height));
                    width += HorizontalSpacing;
                    currentBounds.X += width;
                    currentBounds.Width = GetPositiveOrZero(currentBounds.Width - width);

                    break;
                case Dock.Top:

                    height = MathF.Min(child.DesiredSize.Height, currentBounds.Height);
                    child.Arrange(new Rect(currentBounds.X, currentBounds.Y, currentBounds.Width, height));
                    height += VerticalSpacing;
                    currentBounds.Y += height;
                    currentBounds.Height = GetPositiveOrZero(currentBounds.Height - height);

                    break;
                case Dock.Right:

                    width = Math.Min(child.DesiredSize.Width, currentBounds.Width);
                    child.Arrange(new Rect(currentBounds.X + currentBounds.Width - width, currentBounds.Y, width, currentBounds.Height));
                    width += HorizontalSpacing;
                    currentBounds.Width = GetPositiveOrZero(currentBounds.Width - width);

                    break;
                case Dock.Bottom:

                    height = Math.Min(child.DesiredSize.Height, currentBounds.Height);
                    child.Arrange(new Rect(currentBounds.X, currentBounds.Y + currentBounds.Height - height, currentBounds.Width, height));
                    height += VerticalSpacing;
                    currentBounds.Height = GetPositiveOrZero(currentBounds.Height - height);

                    break;
            }
        }

        if (LastChildFill)
        {
            var child = Children[^1];
            child.Arrange(new Rect(currentBounds.X, currentBounds.Y, currentBounds.Width, currentBounds.Height));
        }

        return finalSize;
    }

    /// <inheritdoc />
    protected override Size MeasureOverride(Size availableSize)
    {
        var parentWidth = 0f;
        var parentHeight = 0f;
        var accumulatedWidth = Padding.Left + Padding.Right;
        var accumulatedHeight = Padding.Top + Padding.Bottom;

        var leftSpacing = false;
        var topSpacing = false;
        var rightSpacing = false;
        var bottomSpacing = false;

        foreach (var child in Children)
        {
            var childConstraint = new Size(
                GetPositiveOrZero(availableSize.Width - accumulatedWidth),
                GetPositiveOrZero(availableSize.Height - accumulatedHeight));

            child.Measure(childConstraint);
            var childDesiredSize = child.DesiredSize;

            switch ((Dock)child.GetValue(DockProperty))
            {
                case Dock.Left:
                    if (childConstraint.Width is not 0)
                        accumulatedWidth += HorizontalSpacing;
                    leftSpacing = true;
                    parentHeight = MathF.Max(parentHeight, accumulatedHeight + childDesiredSize.Height - VerticalSpacing);
                    accumulatedWidth += childDesiredSize.Width;
                    break;

                case Dock.Right:
                    if (childConstraint.Width is not 0)
                        accumulatedWidth += HorizontalSpacing;
                    rightSpacing = true;
                    parentHeight = MathF.Max(parentHeight, accumulatedHeight + childDesiredSize.Height - VerticalSpacing);
                    accumulatedWidth += childDesiredSize.Width;
                    break;

                case Dock.Top:
                    if (childConstraint.Height is not 0)
                        accumulatedHeight += VerticalSpacing;
                    topSpacing = true;
                    parentWidth = Math.Max(parentWidth, accumulatedWidth + childDesiredSize.Width - HorizontalSpacing);
                    accumulatedHeight += childDesiredSize.Height;
                    break;

                case Dock.Bottom:
                    if (childConstraint.Height is not 0)
                        accumulatedHeight += VerticalSpacing;
                    bottomSpacing = true;
                    parentWidth = Math.Max(parentWidth, accumulatedWidth + childDesiredSize.Width - HorizontalSpacing);
                    accumulatedHeight += childDesiredSize.Height;
                    break;
            }
        }

        if (leftSpacing || rightSpacing)
            accumulatedWidth -= HorizontalSpacing;
        if (bottomSpacing || topSpacing)
            accumulatedHeight -= VerticalSpacing;

        parentWidth = MathF.Max(parentWidth, accumulatedWidth);
        parentHeight = MathF.Max(parentHeight, accumulatedHeight);
        return new Size(parentWidth, parentHeight);
    }

    private static float GetPositiveOrZero(float value) => MathF.Max(value, 0);
}
