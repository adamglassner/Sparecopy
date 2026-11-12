using System.Windows;
using System.Windows.Controls;
using Sparecopy.Services;

namespace Sparecopy.Views;

public partial class OverviewView : UserControl
{
    private static readonly string[] Columns = ["Device", "Backups → destination", "Last backup", "Next run"];

    public OverviewView()
    {
        InitializeComponent();
        BuildSkeletonTable();
    }

    /// <summary>Grey placeholder rows behind the "No devices yet" message, in two location groups.</summary>
    private void BuildSkeletonTable()
    {
        var random = new Random(7);
        var grid = new Grid { Opacity = 0.9 };
        foreach (var _ in Columns) grid.ColumnDefinitions.Add(new ColumnDefinition());
        int row = 0;

        void AddRow(UIElement element, int column = 0, int span = 1)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            Grid.SetColumnSpan(element, span);
            grid.Children.Add(element);
        }

        void NewRow() => grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // Column headers
        NewRow();
        var header = new Border();
        header.SetResourceReference(Border.BackgroundProperty, "Brush.SurfaceAlt");
        AddRow(header, 0, Columns.Length);
        for (int c = 0; c < Columns.Length; c++)
        {
            var text = new TextBlock
            {
                Text = Columns[c].ToUpperInvariant(),
                FontSize = 11,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(18, 10, 8, 10),
                TextTrimming = TextTrimming.CharacterEllipsis,
            };
            text.SetResourceReference(TextBlock.ForegroundProperty, "Brush.TextMuted");
            AddRow(text, c);
        }

        foreach (var location in new[] { "Your place", "Family's house" })
        {
            row++;
            NewRow();
            var label = new TextBlock
            {
                Text = location,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(18, 14, 8, 6),
            };
            label.SetResourceReference(TextBlock.ForegroundProperty, "Brush.TextMuted");
            AddRow(label, 0, Columns.Length);

            for (int r = 0; r < 3; r++)
            {
                row++;
                NewRow();
                var line = new Border { BorderThickness = new Thickness(0, 1, 0, 0) };
                line.SetResourceReference(Border.BorderBrushProperty, "Brush.Border");
                AddRow(line, 0, Columns.Length);

                for (int c = 0; c < Columns.Length; c++)
                {
                    var bar = new Border
                    {
                        Height = 9,
                        Width = random.Next(40, c == 1 ? 170 : 110),
                        CornerRadius = new CornerRadius(4.5),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(18, 16, 8, 16),
                    };
                    bar.SetResourceReference(Border.BackgroundProperty, "Brush.Skeleton");
                    AddRow(bar, c);
                }
            }
        }

        DevicesTableHost.Children.Insert(0, grid);
    }

    private void Soon_Click(object sender, RoutedEventArgs e) => Toaster.Soon(sender);
}
