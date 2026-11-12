namespace Sparecopy.Models;

/// <summary>
/// Every section of Sparecopy. In this preview only Overview has its own page; the rest
/// show a "coming soon" page built from the text here.
/// </summary>
public static class ModuleCatalog
{
    public static readonly ModuleInfo Overview = new()
    {
        Key = "overview",
        Title = "Overview",
        ToolbarLabel = "Overview",
        Glyph = "",
        Tagline = "Every computer and phone in your family, in one place.",
    };

    public static readonly IReadOnlyList<ModuleInfo> Toolbar =
    [
        Overview,
        new ModuleInfo
        {
            Key = "devices",
            Title = "Devices",
            ToolbarLabel = "Devices",
            Glyph = "",
            Tagline = "The computers and phones Sparecopy looks after, at every house.",
            Description = "Add each PC, laptop and phone you want to protect: yours, and your family's. Devices are grouped by location, so your place and your parents' house each get their own section, and you can see at a glance which ones are online, when they last backed up and what needs attention.",
            PrimaryAction = "Add device",
            Kpis = ["Devices", "Online now", "Need attention", "Locations"],
            Columns = ["Device", "Owner", "Location", "Backup plans", "Last backup", "Status"],
            Features =
            [
                "Add a PC with a small Sparecopy agent",
                "Group devices by location (house)",
                "Online / offline and last seen",
                "Owner, model and disk space",
                "Windows PCs and laptops first",
                "Android and iPhone (see Mobile)",
                "Removing a device keeps its backups",
                "Alerts when a device goes quiet",
            ],
        },
        new ModuleInfo
        {
            Key = "plans",
            Title = "Backup Plans",
            ToolbarLabel = "Backup Plans",
            Glyph = "",
            Tagline = "What to back up, where it goes and when. As many plans per device as you like.",
            Description = "A backup plan copies one device to one destination. Give a device several plans to keep more than one copy: for example, Mom & Dad's PC backs up to the USB drive plugged into it every evening, and to the family NAS every few hours.",
            PrimaryAction = "New backup plan",
            Kpis = ["Plans", "Running now", "Healthy", "With warnings"],
            Columns = ["Plan", "Device", "Destination", "What", "Schedule", "Last run", "Status"],
            Features =
            [
                "Several plans per device",
                "Choose the destination for each plan",
                "Pick folders (Pictures, Documents…)",
                "Skip rules (*.tmp, Recycle Bin, huge files)",
                "Only changed files after the first run",
                "Keep daily, weekly and monthly versions",
                "Optional password encryption",
                "Run now, pause and resume",
            ],
        },
        new ModuleInfo
        {
            Key = "destinations",
            Title = "Destinations",
            ToolbarLabel = "Destinations",
            Glyph = "",
            Tagline = "The NAS shares, server folders and USB drives your backups are kept on.",
            Description = "Add the places backups can go once, then pick them in any backup plan. Sparecopy keeps an eye on free space and tells you when a drive is getting full, offline or has been unplugged for too long.",
            PrimaryAction = "Add destination",
            Kpis = ["Destinations", "Online", "Space used", "Space free"],
            Columns = ["Destination", "Type", "Location", "Used", "Devices", "Status"],
            Features =
            [
                "NAS shared folders (SMB)",
                "A folder on a server computer",
                "USB drives plugged into a PC",
                "Offsite copies at another house",
                "Free space and capacity",
                "Warnings when nearly full",
                "Reminders when a USB drive is unplugged",
                "Check a destination can be written to",
            ],
        },
        new ModuleInfo
        {
            Key = "schedules",
            Title = "Schedules",
            ToolbarLabel = "Schedules",
            Glyph = "",
            Tagline = "When each backup plan runs.",
            Description = "Every backup plan runs on its own schedule. See them all together here, spot gaps, and pause everything for a while when you need the PC's full attention.",
            PrimaryAction = "Pause all backups",
            Kpis = ["Runs today", "Next run", "Missed this week", "Paused"],
            Columns = ["Plan", "Device", "Runs", "Next run", "Missed"],
            Features =
            [
                "Every few hours within set hours",
                "Daily or weekly at a set time",
                "Whenever a USB drive is plugged in",
                "Only when you press Run",
                "Catch up missed runs at startup",
                "Quiet mode keeps the PC fast",
                "Wake the PC from sleep to run",
                "Pause all backups for a while",
            ],
        },
        new ModuleInfo
        {
            Key = "restore",
            Title = "Browse & Restore",
            ToolbarLabel = "Restore",
            Glyph = "",
            Tagline = "See exactly what was backed up, at any point in time, and bring it back.",
            Description = "Pick a device, a backup plan and a version from the timeline, then browse the files exactly as they were. Restore single files or whole folders to the original PC, to a different folder, or download them as a zip.",
            PrimaryAction = "Restore files",
            Kpis = ["Versions kept", "Oldest version", "Files protected", "Restores this month"],
            Columns = ["Name", "Size", "Modified", "Backup version", "Status"],
            Features =
            [
                "Timeline of every backup version",
                "Browse folders as they were",
                "New, changed and deleted markers",
                "Search inside a backup",
                "Restore to the original PC",
                "Restore to another folder",
                "Download as a .zip",
                "Never overwrite without asking",
            ],
        },
        new ModuleInfo
        {
            Key = "activity",
            Title = "Activity",
            ToolbarLabel = "Activity",
            Glyph = "",
            Tagline = "Every backup and restore, and anything that needs your attention.",
            Description = "A running history of every backup, restore and warning across all your devices, plus the alerts that tell you when something isn't right, such as a failed backup or a phone that hasn't checked in for days.",
            PrimaryAction = "Alert settings",
            Kpis = ["Backups (24 h)", "Completed", "Warnings", "Failed"],
            Columns = ["When", "Device", "Plan", "Result", "Changed", "Time"],
            Features =
            [
                "History of every run",
                "Files skipped because they were in use",
                "Email when a backup fails",
                "Alert when a device isn't seen for days",
                "Reminder on the PC itself",
                "Filter by device or result",
                "Export history to CSV",
                "Who restored what, and when",
            ],
        },
        new ModuleInfo
        {
            Key = "mobile",
            Title = "Mobile",
            ToolbarLabel = "Mobile",
            Glyph = "",
            Tagline = "Back up phones, and check on everyone's backups from your phone.",
            Description = "The Sparecopy app for Android and iPhone backs up photos and videos to your NAS or server, and shows the whole family's backups from wherever you are. On iPhone, backups run while the app is open or the phone is charging on Wi-Fi, because iOS limits background work.",
            PrimaryAction = "Set up a phone",
            Kpis = ["Phones", "Backed up today", "Photos protected", "Not seen lately"],
            Columns = ["Phone", "Owner", "Backs up", "Last backup", "Status"],
            Features =
            [
                "Android and iPhone apps",
                "Photos and videos",
                "Only on home Wi-Fi",
                "Only while charging",
                "Family backup status on the go",
                "Push alerts for failures",
                "Run a backup remotely",
                "Pair a phone with a QR code",
            ],
        },
        new ModuleInfo
        {
            Key = "server",
            Title = "Sparecopy Server",
            ToolbarLabel = "Server",
            Glyph = "",
            Tagline = "One web dashboard for every backup, reachable from any of your devices.",
            Description = "Sparecopy Server runs in Docker on your NAS or a home server. It keeps the list of devices, plans and backup history (your files stay on your own destinations) and serves the web dashboard. Devices at other houses connect to it through a secure relay, so you can check on your parents' PC from your own place without opening ports on anyone's router.",
            PrimaryAction = "Connect to server",
            Kpis = ["Server", "Devices connected", "Houses", "Uptime"],
            Columns = ["Device", "Location", "Connection", "Agent version", "Last check-in"],
            Features =
            [
                "Runs in Docker on a NAS or server",
                "Web dashboard in any browser",
                "Manage devices at other houses",
                "Secure relay, no port forwarding",
                "Family member accounts",
                "Agents update themselves",
                "Your files never leave your hardware",
                "Server settings backed up too",
            ],
        },
    ];
}
