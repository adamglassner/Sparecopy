using System.Windows;
using System.Windows.Controls;
using Sparecopy.Services;

namespace Sparecopy.Views;

public partial class SettingsView : UserControl
{
    private readonly bool _ready;

    public SettingsView()
    {
        InitializeComponent();

        (App.Settings.Theme switch
        {
            AppTheme.Light => ThemeLight,
            AppTheme.Dark => ThemeDark,
            _ => ThemeSystem,
        }).IsChecked = true;

        AboutTitle.Text = $"Sparecopy {App.Version} (preview)";
        DataFolderText.Text = $"Settings are saved in {AppSettings.Folder}";
        _ready = true;
    }

    private void Theme_Checked(object sender, RoutedEventArgs e)
    {
        if (_ready && sender is RadioButton { Tag: string mode })
            App.SetThemePreference(mode);
    }

    private void Soon_Click(object sender, RoutedEventArgs e) => Toaster.Soon(sender);
}
