using System.Reflection;
using System.Windows;
using Microsoft.Win32;
using Sparecopy.Services;

namespace Sparecopy;

public partial class App : Application
{
    public static AppSettings Settings { get; private set; } = new();

    /// <summary>True when the dark palette is currently applied.</summary>
    public static bool IsDark { get; private set; }

    /// <summary>Raised after the light/dark palette has been swapped.</summary>
    public static event EventHandler? ThemeChanged;

    /// <summary>Optional first page from the command line: <c>Sparecopy.exe --page devices</c>.</summary>
    public static string? StartPage { get; private set; }

    /// <summary>Version shown in the status bar and Settings, e.g. "0.1.0".</summary>
    public static string Version { get; } =
        typeof(App).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
        ?? typeof(App).Assembly.GetName().Version?.ToString(3)
        ?? "0.0.0";

    protected override void OnStartup(StartupEventArgs e)
    {
        for (int i = 0; i < e.Args.Length - 1; i++)
        {
            if (e.Args[i] == "--self-test") SelfTest.OutputFile = e.Args[i + 1];
            if (e.Args[i] == "--page") StartPage = e.Args[i + 1].ToLowerInvariant();
        }

        Settings = AppSettings.Load();
        ApplyTheme(Settings.Theme);

        // Last-resort safety net: report unexpected errors instead of closing the app.
        DispatcherUnhandledException += (_, args) =>
        {
            args.Handled = true;
            if (SelfTest.IsRunning) { SelfTest.RecordUnhandled(args.Exception); return; }
            MessageBox.Show("Something went wrong.\n\n" + args.Exception.Message,
                "Sparecopy", MessageBoxButton.OK, MessageBoxImage.Warning);
        };

        // Follow Windows when the user picked "System".
        SystemEvents.UserPreferenceChanged += (_, args) =>
        {
            if (args.Category == UserPreferenceCategory.General && Settings.Theme == AppTheme.System)
                Current.Dispatcher.Invoke(() => ApplyTheme(AppTheme.System));
        };

        base.OnStartup(e);
    }

    /// <summary>Apply "System", "Light" or "Dark".</summary>
    public static void ApplyTheme(string mode)
    {
        bool dark = mode switch
        {
            AppTheme.Dark => true,
            AppTheme.Light => false,
            _ => SystemTheme.WindowsAppsUseDarkMode(),
        };

        IsDark = dark;
        var palette = new ResourceDictionary
        {
            Source = new Uri(dark
                ? "pack://application:,,,/Themes/Dark.xaml"
                : "pack://application:,,,/Themes/Light.xaml", UriKind.Absolute)
        };

        var dictionaries = Current.Resources.MergedDictionaries;
        if (dictionaries.Count > 0) dictionaries[0] = palette;
        else dictionaries.Insert(0, palette);

        ThemeChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void SetThemePreference(string mode)
    {
        Settings.Theme = mode;
        Settings.Save();
        ApplyTheme(mode);
    }
}
