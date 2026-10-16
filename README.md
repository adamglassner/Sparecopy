# Sparecopy

Self-hosted backup for your own computers and your family's — to a NAS, a server, or a USB drive — with one web dashboard to watch and manage all of it from any device.

> **Status:** mockup / planning. No backup code yet.

## Goals

- **Per-device destinations** — each PC (or phone) chooses where its backups go: NAS share, server folder, or a USB drive plugged into that machine.
- **Multiple backups per device** — e.g. Mom & Dad's PC backs up to a USB drive *and* to the family NAS, as two separate plans.
- **One dashboard for everything** — a Sparecopy server (Docker) hosts a web UI you can open from any device to see every machine, every plan, and every destination at a glance.
- **Works across networks** — monitor and manage devices at different houses (your place and your parents' place) from the same dashboard. Each device runs a small agent that dials *out* to the server through a secure relay, so no port forwarding is needed.
- **Choose what gets backed up** — pick folders, plus skip rules (`*.tmp`, Recycle Bin, very large files…).
- **Mobile backups** — phone app backs up photos & videos and lets you check on the family's backups.
- **Schedules** — every few hours, daily, weekly, when a drive is plugged in, or manual; catch up on missed runs.
- **Browse & restore** — pick any backup version, see exactly which files it contains (new / changed / deleted), and restore to the original PC, another folder, or a zip download.
- **Versions & encryption** — keep N daily / weekly / monthly versions; optional password encryption.

## Planned architecture (draft)

| Piece | What it does |
| --- | --- |
| **Server** (Docker) | Web dashboard + API, stores device/plan config and run history, relays commands to agents |
| **Agent** (Windows / macOS / Linux) | Runs on each computer, does the actual backups on schedule, reports status to the server |
| **Mobile app** (Android / iOS) | Backs up the phone's photos & videos; mobile view of the dashboard |
| **Relay** | Lets agents in other houses reach the server without opening router ports |

Note: iOS limits background work, so iPhone backups will mostly run while the app is open or the phone is charging on Wi-Fi.

## Mockup

The UI mockup lives in [`design/mockup/`](design/mockup):

| File | Screen |
| --- | --- |
| `Main.dc.html` | Overview — all devices grouped by location, plan status, destinations, activity |
| `Device.dc.html` | One device with two backup plans (USB + NAS) and recent runs |
| `Plan.dc.html` | Backup plan editor — destination, folders, schedule, versions, encryption |
| `Restore.dc.html` | Browse backup versions and restore files |
| `Phone.dc.html` | Mobile app home screen |

These are design-canvas source files from a Claude Design artifact (they need that canvas's runtime to render). Device names, sizes and dates are placeholder data.
