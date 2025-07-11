using System.Text;
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
            OutputTextBox.Text = output;
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

            return result;
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