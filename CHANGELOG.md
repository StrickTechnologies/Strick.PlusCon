# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Unreleased]
### New Features
- Add CHANGELOG.md
- Grids
  - New constructors to take `Title`, `Subtitle`, `Footer` arguments.

### Changes/Fixes
- Add missing XML comments for `GridSearchExpression` constructor parameters


## [1.3.1] - 2024-02-23
### Changes/Fixes
- Corrections in `GridSearchExpression` to ensure `Type` and `ComparisonType` properties always have valid values
- Add missing XML docs in `GridSearchExpression` class
- `GridSearchExpression` doc updates


## [1.3.0] - 2024-02-21
### New Features
- `StyledText` - new constructor overloads to accept colors
- `W`, `WL` - new overloads to accept `TextStyle`, `StyledText` arguments
- `RK`, `RL` - New overloads to accept colors, `TextStyle`, `StyledText `arguments for prompts
- Grids
  - `Find` methods on `Grid`, `GridRow` and `GridColumn`
    - `Find` extension method for `IEnumerable<GridCell>`
  - `TitleAlignment`, `SubtitleAlignment` and `FooterAlignment` properties to allow horizontal alignment of title, subtitle and footer
  - `FillerChar` property on `GridCell`
    - `SetFillerChar` extension method for `IEnumerable<GridCell>`
  - `AddSeparatorRow` method on `Grid`
  - `AddColumn` methods (shortcuts for `Grid.Columns.Add(…)` methods)
  - `ShowColumnHeaders` property on `Grid` to control visibility of ALL column headers
- Menus
  - Now supports multi-column menus (uses `Grid` to render)
    - `ColumnCount` and `GutterWidth` properties provide control over multi-column layout
  - `TitleAlignment` and `SubtitleAlignment` properties to allow horizontal alignment of title and subtitle

### Changes/Fixes
- Grids
  - `GridColumn.Header` property is now read-only and will always return a valid, non-null `GridHeaderCell` object
  - Fix issue with padding when rendering cell with empty content (""). Empty content is considered to be content, but padding was not rendered properly with empty content in previous versions.
- `W`, `WL` - Improved null/empty string support
- `RK`, `RL` - Improved null/empty string support for prompts
- Miscellaneous doc updates/improvements


## [1.2.0] - 2023-11-14
### New Features
- `About` static class
- Menus: New `BeforeShow` event on `Menu`, `MenuOption` classes
- New `Cursor` static class and escape codes for cursor movement and shape/visibility
- New `ConsoleSize` class (windows-only)
- New `Colorize` overload (`IEnumerable<Color>`)
- New `Ruler` static class
- New `Intersperse` string extension overload
- New `Vertical` string extension method

### Changes/Fixes
- Bug fixes with `Grid` rendering (specifically when grid flows past bottom of console window)
- Fix on `Menu` width calculation (adding 3 extraneous spaces to width when the widest element is a separator option)
- XML Doc correction (`Formatting.Center`)
- Correct "widget" misspelling in Quick start doc, example image


## [1.1.0] - 2023-07-17
### New Features
- `CLS` helper
- Menus -- a simple menu system for easy navigation
- Grids -- easily display data in a tabular "rows and columns" format
- New `TextStyle` and `StyledText` classes for styling text
- New methods for formatting text (`Center`, `Intersperse`, `SpaceOut`)
- New color utilities (`Darken`, `Lighten`, `AdjustBrightness`)

### Changes/Fixes
- Updated documentation 


## [1.0.0] - 2022-09-01

## 0.9.1 - 2022-08-08

## 0.9.0 - 2022-08-08


[1.3.1]: https://github.com/StrickTechnologies/Strick.PlusCon/compare/v1.3.0...v1.3.1
[1.3.0]: https://github.com/StrickTechnologies/Strick.PlusCon/compare/v1.2.0...v1.3.0
[1.2.0]: https://github.com/StrickTechnologies/Strick.PlusCon/compare/v1.1.0...v1.2.0
[1.1.0]: https://github.com/StrickTechnologies/Strick.PlusCon/compare/v1.0.0...v1.1.0
[1.0.0]: https://github.com/StrickTechnologies/Strick.PlusCon/releases/tag/v1.0.0
