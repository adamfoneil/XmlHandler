# XmlHandler - Unicode Escape Processor

A simple WPF application that processes Unicode escape sequences in text, converting them to their unescaped/printed versions.

## Features

- **Split Interface**: The main window is evenly split into top and bottom text areas
- **Input Area**: Top text area where you can paste or type text containing Unicode escape sequences
- **Output Area**: Bottom text area (read-only) that shows the processed output with escape sequences converted
- **Real-time Processing**: As you type in the input area, the output is automatically updated
- **XML Formatting**: When the processed output is valid XML, it's automatically formatted with proper indentation for improved readability
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

**Input with Unicode Escapes:**
```
\u003C\u0072\u006F\u006F\u0074\u003E\u003C\u0063\u0068\u0069\u006C\u0064\u003E\u0076\u0061\u006C\u0075\u0065\u003C\u002F\u0063\u0068\u0069\u006C\u0064\u003E\u003C\u002F\u0072\u006F\u006F\u0074\u003E
```

**Output (Formatted XML):**
```xml
<root>
  <child>value</child>
</root>
```

**Input with Non-XML Unicode Escapes:**
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

### XML Formatting
When the processed output is valid XML, the application automatically formats it with:
- **2-space indentation** for nested elements
- **Proper line breaks** for improved readability
- **Preservation of XML declarations** when present
- **Support for all XML features** including comments, CDATA, namespaces, and mixed content
- **Graceful degradation** - invalid XML or non-XML text remains unchanged