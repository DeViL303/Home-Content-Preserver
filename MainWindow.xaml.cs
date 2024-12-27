using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Home_Content_Preserver
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Path to the icon file next to the executable
            string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icon.ico");

            if (File.Exists(iconPath))
            {
                using (FileStream iconStream = new FileStream(iconPath, FileMode.Open))
                {
                    // Set the icon from the stream
                    this.Icon = BitmapFrame.Create(iconStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
            else
            {
                // Log or handle the case where the icon file does not exist
                Console.WriteLine("Icon file not found.");
            }
        }


        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Allow window dragging
            this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuildListButton_Click(object sender, RoutedEventArgs e)
        {
            string newDomain = DomainTextBox.Text;

            // Regex to match URLs in the input text
            var urlRegex = new Regex(
                @"http[s]?://[\w\.-]+(/[\w\.-]+)*",
                RegexOptions.IgnoreCase);

            // Find matches in LinksTextBox.Text
            var matches = urlRegex.Matches(LinksTextBox.Text);

            HashSet<string> uniqueUrls = new HashSet<string>();
            List<string> orderedUrls = new List<string>();

            // Process each match as a URL
            foreach (Match match in matches)
            {
                string modifiedUrl = ReplaceDomain(match.Value, newDomain);
                AddUniqueUrl(uniqueUrls, orderedUrls, modifiedUrl);

                // Check radio buttons and generate additional URLs as required
                if (RadioButtonT80.IsChecked == true && ContainsTNumber(modifiedUrl))
                {
                    GenerateAndLogUrls(orderedUrls, uniqueUrls, modifiedUrl, 31, 80);
                }
                else if (RadioButtonT100.IsChecked == true && ContainsTNumber(modifiedUrl))
                {
                    GenerateAndLogUrls(orderedUrls, uniqueUrls, modifiedUrl, 1, 100);
                }
                else if (RadioButtonT200.IsChecked == true && ContainsTNumber(modifiedUrl))
                {
                    GenerateAndLogUrls(orderedUrls, uniqueUrls, modifiedUrl, 1, 200);
                }
                else if (RadioButtonT300.IsChecked == true && ContainsTNumber(modifiedUrl))
                {
                    GenerateAndLogUrls(orderedUrls, uniqueUrls, modifiedUrl, 1, 300);
                }
                else if (RadioButtonT999.IsChecked == true && ContainsTNumber(modifiedUrl))
                {
                    GenerateAndLogUrls(orderedUrls, uniqueUrls, modifiedUrl, 1, 999);
                }
            }

            // Process the list to add .sdc and .png URLs for each .sdat URL, if CheckBoxAutoGrabSDCODCPNG is checked
            if (CheckBoxAutoGrabSDCODCPNG.IsChecked == true)
            {
                List<string> additionalUrls = new List<string>();

                foreach (string url in orderedUrls)
                {
                    if (url.EndsWith(".sdat", StringComparison.OrdinalIgnoreCase))
                    {
                        // Add .sdc or .odc version of the URL based on the path segment
                        string extension = url.Contains("/Scenes/") ? ".sdc" : url.Contains("/Objects/") ? ".odc" : null;
                        if (extension != null)
                        {
                            string conditionalUrl = url.Substring(0, url.Length - 5) + extension;
                            additionalUrls.Add(conditionalUrl);
                        }

                        // Add .png version of the URL
                        string pngUrl = GeneratePngUrl(url);
                        additionalUrls.Add(pngUrl);
                    }
                }

                // Add the additional URLs to the uniqueUrls set and orderedUrls list
                foreach (string additionalUrl in additionalUrls)
                {
                    AddUniqueUrl(uniqueUrls, orderedUrls, additionalUrl);
                }
            }

            // Now log the unique and ordered URLs
            LogsTextBox.Clear(); // Clear previous logs if needed
            foreach (string url in orderedUrls)
            {
                Log(url);
            }
        }




        private void AddUniqueUrl(HashSet<string> uniqueUrls, List<string> orderedUrls, string url)
        {
            if (uniqueUrls.Add(url)) // Will return false if the URL is already in the set
            {
                orderedUrls.Add(url);
            }
        }

        private string GeneratePngUrl(string sdatUrl)
        {
            // Find the last underscore before the ".sdat" extension and ensure it is preceded by "_T"
            int lastUnderscoreIndex = sdatUrl.LastIndexOf('_');
            int tIndex = sdatUrl.LastIndexOf("_T", StringComparison.OrdinalIgnoreCase);

            if (lastUnderscoreIndex > 0 && lastUnderscoreIndex == tIndex)
            {
                // Get the base URL up to the last underscore
                string baseUrlUpToT = sdatUrl.Substring(0, lastUnderscoreIndex);

                // Find the last slash before the "_Txxx"
                int lastSlashIndex = baseUrlUpToT.LastIndexOf('/');

                if (lastSlashIndex >= 0)
                {
                    // Assemble the new URL
                    string startUrl = sdatUrl.Substring(0, lastSlashIndex + 1);
                    string tNumberAndExtension = sdatUrl.Substring(lastUnderscoreIndex);
                    string pngUrl = $"{startUrl}large{tNumberAndExtension.Replace(".sdat", ".png")}";
                    return pngUrl;
                }
            }

            // If the URL is not in the expected format, log an error or handle as needed
            Log($"-- Error generating PNG URL from '{sdatUrl}'");
            return null;
        }


        private bool ContainsTNumber(string url)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(url, @"T\d{3}");
        }


        private void GenerateAndLogUrls(List<string> orderedUrls, HashSet<string> uniqueUrls, string baseUrl, int startRange, int endRange)
        {
            string basePattern = "T" + baseUrl.Substring(baseUrl.LastIndexOf('T') + 1, 3);
            for (int i = startRange; i <= endRange; i++)
            {
                string newTNumber = $"T{i:D3}";
                string newUrl = baseUrl.Replace(basePattern, newTNumber);
                if (uniqueUrls.Add(newUrl)) // Only add if not a duplicate
                {
                    orderedUrls.Add(newUrl);
                }
            }
        }

        private string ReplaceDomain(string originalUrl, string newDomain)
        {
            try
            {
                Uri uri = new Uri(originalUrl);
                string newPath = "/" + uri.AbsolutePath.Split('/').Skip(1).Aggregate((a, b) => a + "/" + b); // Skip the domain and the first two slashes
                string newUrl = $"{newDomain.TrimEnd('/')}{newPath}"; // Ensure the new domain doesn't end with a slash
                return newUrl;
            }
            catch (Exception ex)
            {
                Log($"-- Error processing URL '{originalUrl}': {ex.Message}");
                return null;
            }
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string userAgent = UserAgentTextBox.Text;
            string downloadFolderPath = DownloadFolderTextBox.Text;

            // Verify and create the download folder if it doesn't exist
            if (!Directory.Exists(downloadFolderPath))
            {
                Directory.CreateDirectory(downloadFolderPath);
            }

            // Configure HttpClient with the user agent
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

                // Read each line from LogsTextBox and download if it doesn't start with "--"
                foreach (string line in LogsTextBox.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!line.StartsWith("--"))
                    {
                        await DownloadFileAsync(httpClient, line, downloadFolderPath);

                        // Check if the checkbox for 1-second delay is checked
                        if (CheckBox1SecondDelay.IsChecked == true)
                        {
                            // Introduce a 1-second delay before the next download
                            await Task.Delay(2000);
                        }
                    }
                }
            }
        }


        private async Task DownloadFileAsync(HttpClient httpClient, string fileUrl, string baseDownloadFolderPath)
        {
            // Log starting download
            Log($"-- {fileUrl}: Downloading...");

            try
            {
                Uri uri = new Uri(fileUrl);
                string relativePath = uri.AbsolutePath.Substring(1);
                string fullLocalPath = Path.Combine(baseDownloadFolderPath, relativePath.Replace('/', Path.DirectorySeparatorChar));

                string directoryPath = Path.GetDirectoryName(fullLocalPath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var response = await httpClient.GetAsync(fileUrl);

                if (!response.IsSuccessStatusCode)
                {
                    // Log failure
                    Log($"-- {fileUrl}: FAILED! Status code: {response.StatusCode}");
                    return;
                }

                using (var fileStream = new FileStream(fullLocalPath, FileMode.Create))
                using (var httpStream = await response.Content.ReadAsStreamAsync())
                {
                    await httpStream.CopyToAsync(fileStream);
                    // Log success
                    Log($"-- {fileUrl}: SUCCESS!");
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Log($"-- {fileUrl}: FAILED! Exception: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            // Use Dispatcher to ensure thread-safety when accessing the UI
            Dispatcher.Invoke(() =>
            {
                LogsTextBox.AppendText(message + Environment.NewLine);
                // Automatically scroll to the bottom
                LogsTextBox.ScrollToEnd();
            });
        }



        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            LogsTextBox.Clear();
            LinksTextBox.Clear();
        }

        private void StartDownloads()
        {
            // Example function to initiate downloads
            // You'd need to implement actual downloading logic based on the selected options and input URLs
            Log("-- Download started...");
        }


        // Event handlers for radio buttons
        private void RadioButtonSingle_Checked(object sender, RoutedEventArgs e)
        {
            if (LogsTextBox != null)
            {
                Log("-- Single download mode selected.");
            }
        }


        private void RadioButtonT080_Checked(object sender, RoutedEventArgs e)
        {
            Log("-- Remember trying too many URLs at once might trigger security");
        }

        private void RadioButtonT100_Checked(object sender, RoutedEventArgs e)
        {
            Log("-- Remember trying too many URLs at once might trigger security");
        }

        private void RadioButtonT200_Checked(object sender, RoutedEventArgs e)
        {
            Log("-- Remember trying too many URLs at once might trigger security");
        }

        private void RadioButtonT300_Checked(object sender, RoutedEventArgs e)
        {
            Log("-- Remember trying too many URLs at once might trigger security");
        }
        private void RadioButtonT999_Checked(object sender, RoutedEventArgs e)
        {
            Log("-- Remember trying too many URLs at once might trigger security");
        }

        private void CheckBox1SecondDelay_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxAutoGrabSDCODCPNG_Checked(object sender, RoutedEventArgs e)
        {

        }

       


    }



}
