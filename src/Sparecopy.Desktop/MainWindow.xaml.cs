using System.Windows;
using System.Windows.Threading;
using Sparecopy.Models;
using Sparecopy.Services;
using Sparecopy.Views;

namespace Sparecopy;

public partial class MainWindow : Window
{
    public static MainWindow? Instance { get; private set; }

    private readonly DispatcherTimer _toastTimer = new() { Interval = TimeSpan.FromSeconds(2.8) };

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;

        ModuleBar.ItemsSource = ModuleCatalog.Toolbar;
        StatusText.Text = $"Sparecopy {App.Version} (preview) · © 2026 Bytehawk Studio · free & open source (MIT)";

        _toastTimer.Tick += (_, _) =>
        {
            _toastTimer.Stop();
            Toast.Visibility = Visibility.Collapsed;
        };

        App.ThemeChanged += OnThemeChanged;
        SourceInitialized += (_, _) => SystemTheme.ApplyTitleBar(this, App.IsDark);
        UpdateThemeButton();

        Loaded += async (_, _) =>
        {
            if (App.StartPage == "settings") ShowSettings();
            else GoTo(App.StartPage ?? ModuleCatalog.Overview.Key);

            if (SelfTest.IsRunning)
            {
                int failed = await SelfTest.RunAsync(this);
                Application.Current.Shutdown(failed);
            }
        };
    }

    protected override void OnClosed(EventArgs e)
    {
        App.ThemeChanged -= OnThemeChanged;
        base.OnClosed(e);
    }

    // ----- Navigation -----

    private void Module_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { DataContext: ModuleInfo module })
            Show(module);
    }

    /// <summary>Selects a toolbar section by key ("overview", "devices", …). Unknown keys open Overview.</summary>
    public void GoTo(string key)
    {
        var module = ModuleCatalog.Toolbar.FirstOrDefault(m => m.Key == key) ?? ModuleCatalog.Overview;
        if (module.IsSelected) Show(module);
        else module.IsSelected = true; // fires Module_Checked → Show
    }

    private void Show(ModuleInfo module)
    {
        SetPage(module.Key == ModuleCatalog.Overview.Key
            ? new OverviewView()
            : new ComingSoonView(module));
    }

    private void SetPage(object page)
    {
        PageHost.Content = page;
        PageScroller.ScrollToTop();
    }

    // ----- Header buttons -----

    private void Settings_Click(object sender, RoutedEventArgs e) => ShowSettings();

    public void ShowSettings()
    {
        foreach (var m in ModuleCatalog.Toolbar) m.IsSelected = false;
        SetPage(new SettingsView());
    }

    private void Theme_Click(object sender, RoutedEventArgs e) =>
        App.SetThemePreference(App.IsDark ? AppTheme.Light : AppTheme.Dark);

    // ----- Theme -----

    private void OnThemeChanged(object? sender, EventArgs e)
    {
        UpdateThemeButton();
        SystemTheme.ApplyTitleBar(this, App.IsDark);
    }

    private void UpdateThemeButton()
    {
        // Sun when dark (click for light), moon when light (click for dark).
        ThemeGlyph.Text = App.IsDark ? "" : "";
        ThemeButton.ToolTip = App.IsDark ? "Switch to light mode" : "Switch to dark mode";
        System.Windows.Automation.AutomationProperties.SetName(ThemeButton, (string)ThemeButton.ToolTip);
    }

    // ----- Toast -----

    public void ShowToast(string message)
    {
        ToastText.Text = message;
        Toast.Visibility = Visibility.Visible;
        _toastTimer.Stop();
        _toastTimer.Start();
    }
}
