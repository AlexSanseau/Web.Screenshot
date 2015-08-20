
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Alx.Web
{
    public static class Screenshot
    {
        public static Image Take(string url, Devices device = Devices.Desktop)
        {
            var size = GetSize(device);
            return Take(url, size);
        }

        public static Image Take(string url, Size size)
        {
            var result = Capture(url, size);
            return result;
        }

        public static void Save(string url, string path, ImageFormat format, Devices device = Devices.Desktop)
        {
            var size = GetSize(device);
            Save(url, path, format, size);
        }

        public static void Save(string url, string path, ImageFormat format, Size size)
        {
            var image = Take(url, size);

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                var bytes = stream.ToArray();
                File.WriteAllBytes(path, bytes);
            }
        }

        public static Size GetSize(Devices device)
        {
            var attribute = device.GetAttribute<SizeAttribute>();

            if (attribute == null)
            {
                throw new DeviceSizeException(device);
            }

            return attribute.Size;
        }

        private static Image Capture(string url, Size size)
        {
            Image result = new Bitmap(size.Width, size.Height);

            var thread = new Thread(() =>
            {
                using (var browser = new WebBrowser())
                {
                    browser.ScrollBarsEnabled = false;
                    browser.AllowNavigation = true;
                    browser.Navigate(url);
                    browser.Width = size.Width;
                    browser.Height = size.Height;
                    browser.ScriptErrorsSuppressed = true;
                    browser.DocumentCompleted += (sender,args) => DocumentCompleted(sender, args, ref result);

                    while (browser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            return result;
        }

        private static void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e, ref Image image)
        {
            var browser = sender as WebBrowser;

            if (browser == null) throw new Exception("Sender should be browser");
            if (browser.Document == null) throw new Exception("Document is missing");
            if (browser.Document.Body == null) throw new Exception("Body is missing");

            using (var bitmap = new Bitmap(browser.Width, browser.Height))
            {
                browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
                image = (Image)bitmap.Clone();
            }
        }
    }
}
