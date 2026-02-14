# Layoutfixer QA Checklist (Windows 10/11)

## Pre-flight
- [ ] Windows 10 and Windows 11 test machines available
- [ ] EN+HE and EN+RU keyboard layouts installed
- [ ] App launched from published artifact

## Functional tests
- [ ] `Ctrl+Alt+L` converts selected EN->HE text (example: `akuo` -> `שלום`)
- [ ] `Ctrl+Alt+L` converts selected HE->EN text (example: `שלום` -> `akuo`)
- [ ] `Ctrl+Alt+L` converts selected EN->RU text (`ghbdtn` -> `привет`)
- [ ] `Ctrl+Alt+L` converts selected RU->EN text (`привет` -> `ghbdtn`)
- [ ] `Ctrl+Alt+K` converts last word in Notepad
- [ ] Works in Word / browser textareas / Slack / VS Code

## Edge cases
- [ ] Mixed text (`hello שלום`) does not get corrupted
- [ ] Uppercase text is handled reasonably (`GHBDTN` -> `ПРИВЕТ`)
- [ ] Punctuation stays intact where possible
- [ ] Empty selection does nothing
- [ ] Clipboard text is restored after conversion

## Reliability
- [ ] Hotkeys work after sleep/resume
- [ ] App exits cleanly from tray
- [ ] Settings persist across restarts
- [ ] Start with Windows option works (if enabled)

## Privacy/Security
- [ ] No outbound network traffic while converting text
- [ ] No telemetry files generated

## Performance
- [ ] Conversion latency < 150ms on typical laptop
- [ ] No noticeable CPU spike while idle
