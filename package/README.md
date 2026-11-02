# EnemyBarOnScreen

Keeps the health bar of tall enemies on screen when you fight them up close.

## What changes?

In vanilla, an enemy's health bar is drawn above its head. With tall creatures like trolls, that spot is above the top of the screen once you're close, so the bar disappears right when you're fighting them.

With this mod, the bar stops at the top edge of the screen instead of leaving it. It stays above the creature (left and right still follow it) and moves back to its normal place as soon as the creature's head is in view again. Nothing else changes: bars still show and hide like in vanilla, and boss bars are untouched.

## Configuration

Change these in game with F1 (Configuration Manager) or in your mod manager's config editor:

- `Enabled`: turns the mod on or off.
- `TopMarginPercent`: how far below the top edge the bar stops, in percent of the screen height (default 10). Raise it if the creature's name is cut off or overlaps your hotbar.

## Multiplayer

Client-side only. It only changes what you see on your own screen, so other players and the server don't need it.

## Feedback and bug reports

Found a bug or have an idea? Open an issue on [GitHub](https://github.com/aetuss/EnemyBarOnScreen/issues). A screenshot of the problem helps a lot.
