﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace XmlHandler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputTextBox == null || OutputTextBox == null) return;
            
            string input = InputTextBox.Text;
            string output = ProcessUnicodeEscapes(input);
            output = ReplaceControlCharacters(output);
			OutputTextBox.Text = output;
        }

		private string ReplaceControlCharacters(string output)
		{
			// remove \t, \r, \n and any other control characters
			if (string.IsNullOrEmpty(output)) return string.Empty;

            string[] replace = ["\\t", "\\r", "\\n", "\\b", "\\f"];

			foreach (var item in replace)
			{
				output = output.Replace(item, string.Empty);
			}

            return output;
		}

		private string ProcessUnicodeEscapes(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // Process unicode escape sequences like \u003C, \u003E, \u0022
            // Use regex to find all \uXXXX patterns
            string result = Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
            {
                string hexValue = match.Groups[1].Value;
                try
                {
                    int unicode = Convert.ToInt32(hexValue, 16);
                    return char.ConvertFromUtf32(unicode);
                }
                catch
                {
                    // If conversion fails, return the original match
                    return match.Value;
                }
            });

            // Try to format as XML if it's valid XML
            return FormatXmlIfValid(result);
        }

        private string FormatXmlIfValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            try
            {
                // Try to parse as XML
                var doc = XDocument.Parse(text);
                
                // If parsing succeeds, format with indentation
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ", // 2 spaces for indentation
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace,
                    OmitXmlDeclaration = !text.TrimStart().StartsWith("<?xml", StringComparison.OrdinalIgnoreCase)
                };

                using (var stringWriter = new StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        doc.Save(xmlWriter);
                    }
                    return stringWriter.ToString();
                }
            }
            catch
            {
                // If XML parsing fails, return the original text
                return text;
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OutputTextBox.Text))
            {
                try
                {
                    Clipboard.SetText(OutputTextBox.Text);
                    MessageBox.Show("Output copied to clipboard!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to copy to clipboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No output to copy.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}