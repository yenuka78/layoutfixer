# Layoutfixer

Fix text typed in the wrong keyboard layout on Windows 10/11. Offline, private, fast.

**MVP features**
- Convert **selected text** (global hotkey)
- Convert **last word** (global hotkey)
- Language pairs: **EN↔HE**, **EN↔RU**
- Tray app with settings
- No telemetry

## Status
Work in progress. CI builds on Windows via GitHub Actions.

## Build (Windows)
```bash
# Requires .NET 8 SDK
cd src/Layoutfixer

dotnet restore

dotnet build

dotnet test ..\..\tests\Layoutfixer.Tests
```

## Hotkeys (default)
- Convert selection: **Ctrl+Alt+L**
- Convert last word: **Ctrl+Alt+K**

## License
MIT
