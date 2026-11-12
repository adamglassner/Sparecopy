using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Sparecopy.Models;

namespace Sparecopy.Services;

/// <summary>
/// Headless checks run by CI: <c>Sparecopy.exe --self-test result.txt</c>.
/// Opens every page in both themes and shows a toast. Exit code = number of failures.
/// </summary>
public static class SelfTest
{
    public static string? OutputFile { get; set; }
    public static bool IsRunning => OutputFile != null;

    private static readonly StringBuilder Report = new();
    private static int _failed;

    /// <summary>Called by App's unhandled-exception handler while the self-test runs.</summary>
    public static void RecordUnhandled(Exception ex)
    {
        _failed++;
        Report.AppendLine($"FAIL  Unhandled exception — {ex}");
    }

    private static void Check(string name, bool ok, string detail = "")
    {
        if (!ok) _failed++;
        Report.AppendLine($"{(ok ? "PASS" : "FAIL")}  {name}{(detail.Length > 0 ? "  — " + detail : "")}");
    }

    private static async Task Step(string name, Action action)
    {
        try
        {
            action();
            // Let layout, data binding and rendering run so XAML errors surface here.
            await Dispatcher.Yield(DispatcherPriority.ApplicationIdle);
            Application.Current.MainWindow?.UpdateLayout();
            Check(name, true);
        }
        catch (Exception ex)
        {
            Check(name, false, ex.ToString());
        }
    }

    public static async Task<int> RunAsync(MainWindow window)
    {
        Report.AppendLine($"Sparecopy {App.Version} self-test {DateTime.Now:u}");

        foreach (var theme in new[] { AppTheme.Light, AppTheme.Dark })
        {
            await Step($"[{theme}] Apply theme", () => App.ApplyTheme(theme));
            foreach (var module in ModuleCatalog.Toolbar)
                await Step($"[{theme}] Page: {module.Title}", () => window.GoTo(module.Key));
            await Step($"[{theme}] Page: Settings", window.ShowSettings);
        }

        await Step("Coming-soon toast", () => Toaster.Show("Self-test is coming soon."));
        await Step("Back to Overview", () => window.GoTo(ModuleCatalog.Overview.Key));

        Report.AppendLine(_failed == 0 ? "All checks passed." : $"{_failed} check(s) failed.");
        try { File.WriteAllText(OutputFile!, Report.ToString()); }
        catch { /* Nothing else we can do; the exit code still reports failures. */ }
        return _failed;
    }
}
