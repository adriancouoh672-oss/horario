using System.Collections;
using Microsoft.Maui.Controls.Shapes;
using TiempoEnOrden.App.ViewModels;

namespace TiempoEnOrden.App.Views;

public partial class WeekTimelineView : ContentView
{
    private const int StartHour = 9;
    private const int EndHour = 21;
    private const double PixelsPerMinute = 1;

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource), typeof(IEnumerable), typeof(WeekTimelineView), propertyChanged: OnDataChanged);

    public static readonly BindableProperty WeekStartProperty = BindableProperty.Create(
        nameof(WeekStart), typeof(DateTime), typeof(WeekTimelineView), DateTime.Today, propertyChanged: OnDataChanged);

    public WeekTimelineView()
    {
        InitializeComponent();
        Render();
    }

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public DateTime WeekStart
    {
        get => (DateTime)GetValue(WeekStartProperty);
        set => SetValue(WeekStartProperty, value);
    }

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue) =>
        ((WeekTimelineView)bindable).Render();

    private void Render()
    {
        if (TimeLabels is null || TimelineCanvas is null)
        {
            return;
        }

        TimeLabels.Children.Clear();
        TimelineCanvas.Children.Clear();
        TimelineCanvas.ColumnDefinitions.Clear();

        for (var day = 0; day < 7; day++)
        {
            TimelineCanvas.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            if (day > 0)
            {
                var divider = new BoxView { WidthRequest = 1, Color = Color.FromArgb("#CAD7DA"), HorizontalOptions = LayoutOptions.Start };
                Grid.SetColumn(divider, day);
                TimelineCanvas.Children.Add(divider);
            }
        }

        for (var hour = StartHour; hour <= EndHour; hour++)
        {
            TimeLabels.Children.Add(new Label
            {
                Text = $"{hour:00}:00",
                FontSize = 12,
                TextColor = Color.FromArgb("#52666D"),
                TranslationY = (hour - StartHour) * 60 - 8
            });

            var line = new BoxView
            {
                HeightRequest = 1,
                Color = Color.FromArgb("#CAD7DA"),
                VerticalOptions = LayoutOptions.Start,
                TranslationY = (hour - StartHour) * 60
            };
            Grid.SetColumnSpan(line, 7);
            TimelineCanvas.Children.Add(line);
        }

        if (ItemsSource is null)
        {
            return;
        }

        foreach (var item in ItemsSource.OfType<AgendaEntryViewModel>())
        {
            var dayIndex = (item.Start.Date - WeekStart.Date).Days;
            if (dayIndex is < 0 or > 6)
            {
                continue;
            }

            var minutes = (item.Start.TimeOfDay - TimeSpan.FromHours(StartHour)).TotalMinutes;
            var duration = Math.Max(1, (item.End - item.Start).TotalMinutes);
            if (minutes + duration <= 0 || minutes >= (EndHour - StartHour) * 60)
            {
                continue;
            }

            var block = new Border
            {
                BackgroundColor = Color.FromArgb(item.BackgroundColor),
                Stroke = Color.FromArgb(item.StrokeColor),
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle { CornerRadius = 5 },
                Margin = new Thickness(4, 0),
                Padding = duration >= 28 ? new Thickness(5, 2) : 0,
                HeightRequest = duration * PixelsPerMinute,
                VerticalOptions = LayoutOptions.Start,
                TranslationY = minutes * PixelsPerMinute
            };
            SemanticProperties.SetDescription(block, item.AccessibleText);

            if (duration >= 28)
            {
                block.Content = new Label
                {
                    Text = $"{item.SegmentLabel}\n{item.TimeText}",
                    FontSize = duration >= 45 ? 12 : 10,
                    LineBreakMode = LineBreakMode.TailTruncation,
                    TextColor = Color.FromArgb("#18282D")
                };
            }

            Grid.SetColumn(block, dayIndex);
            TimelineCanvas.Children.Add(block);
        }
    }
}
