# EnemyBarOnScreen

Source for the EnemyBarOnScreen Valheim mod: the health bar of tall enemies stops at the top edge of the screen instead of going off screen when you fight them up close. Client-side only.

The player-facing description lives in [package/README.md](package/README.md).

## Layout

- `src/` — plugin code (BepInEx + Harmony)
- `package/` — Thunderstore files: `manifest.json`, `README.md`, `CHANGELOG.md`, `icon.png`
- `build.ps1` — builds the DLL and the Thunderstore zip

## Building

Needs Valheim and a BepInEx install (for `BepInEx.dll` and `0Harmony.dll`). No .NET SDK is required; the script uses the C# compiler that ships with Windows.

```powershell
powershell -ExecutionPolicy Bypass -File build.ps1
```

Pass `-Managed` and `-BepInExCore` if your game or BepInEx folders are elsewhere. The output is `bin/EnemyBarOnScreen.dll` and `dist/EnemyBarOnScreen-<version>.zip`, ready to upload to Thunderstore.

## Releasing

Bump the version in both `src/Plugin.cs` (`Version`) and `package/manifest.json` (`version_number`), add a `package/CHANGELOG.md` entry, build, and upload the zip from `dist/`.

## Made with AI

aetuss decided what this mod does and how it should work, and tested it in game. The code, the icon and this description were written by Claude, an AI model by Anthropic (through Claude Code).

## License

MIT. See [LICENSE](LICENSE).
