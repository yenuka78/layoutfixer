# Layoutfixer

Layoutfixer fixes text typed with the wrong keyboard layout on **Windows 10/11**.

- Offline by design
- No telemetry
- Global hotkeys
- EN↔HE and EN↔RU support (v1)

## Features (v1)
- Convert selected text globally (`Ctrl+Alt+L`)
- Convert last word globally (`Ctrl+Alt+K`)
- Tray app + persisted settings
- Portable release artifact via GitHub Actions

## Repository structure
- `src/Layoutfixer` — WPF desktop app
- `src/Layoutfixer.Core` — conversion engine and shared models
- `tests/Layoutfixer.Tests` — xUnit tests
- `landing/` — landing page assets
- `docs/` — QA and release docs

## Build & test (Windows)
```bash
cd src/Layoutfixer

dotnet restore

dotnet build -c Release

dotnet test ..\..\tests\Layoutfixer.Tests\Layoutfixer.Tests.csproj -c Release
```

## CI
- `.github/workflows/windows-ci.yml`
  - build app
  - run tests
  - publish win-x64 artifact
- `.github/workflows/release.yml`
  - build release ZIP on version tags (`v*.*.*`)
- `.github/workflows/deploy-landing.yml`
  - deploys `landing/` to GitHub Pages on push

## Releases
Tag a version like `v1.0.0` to trigger `.github/workflows/release.yml`.
This will publish a zipped Windows binary to GitHub Releases.

## Privacy
See [PRIVACY.md](./PRIVACY.md).

## License
MIT
