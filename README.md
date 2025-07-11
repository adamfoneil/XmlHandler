# XmlHandler - Unicode Escape Processor

A simple WPF application that processes Unicode escape sequences in text, converting them to their unescaped/printed versions.

## Features

- **Split Interface**: The main window is evenly split into top and bottom text areas
- **Input Area**: Top text area where you can paste or type text containing Unicode escape sequences
- **Output Area**: Bottom text area (read-only) that shows the processed output with escape sequences converted
- **Real-time Processing**: As you type in the input area, the output is automatically updated
- **Copy to Clipboard**: Button to copy the processed output to clipboard

## Unicode Escape Processing

The application automatically converts Unicode escape sequences like:
- `\u003C` → `<`
- `\u003E` → `>`
- `\u0022` → `"`
- `\u0026` → `&`
- `\u0027` → `'`
- And many others...

## Usage

1. **Build and Run**: Open the project in Visual Studio or use `dotnet run` (requires Windows)
2. **Input Text**: Paste or type text containing Unicode escape sequences in the top text area
3. **View Output**: The processed text appears automatically in the bottom area
4. **Copy Results**: Click "Copy Output to Clipboard" to copy the processed text

## Example

**Input:**
```
This is a test: \u003C\u0022Hello World\u0022\u003E
```

**Output:**
```
This is a test: <"Hello World">
```

## Requirements

- .NET 8.0 or higher
- Windows (WPF requires Windows)
- Visual Studio or VS Code (recommended)

## Technical Details

The application uses regular expressions to find and replace Unicode escape sequences in the format `\uXXXX` where XXXX is a 4-digit hexadecimal number representing the Unicode code point.