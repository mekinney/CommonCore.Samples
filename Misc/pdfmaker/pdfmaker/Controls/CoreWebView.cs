using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace pdfmaker.Controls
{
    public class CoreWebView: WebView
    {
        public Action<Action<bool>> CreatePDF { get; set; }

        /// <summary>
        /// Filename that the PDF should be called. Set by the UI.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; } = "data.pdf";

#if __IOS__
        public string FullPath { get; set; }
#endif

        public Exception PDFException { get; set; }

        public async Task<bool> CreatePDFAsync()
        {
            return await Task.Run(async () =>
            {
                var taskCompletionSource = new TaskCompletionSource<bool>();
                this.CreatePDF.Invoke((success) => {
                    taskCompletionSource.SetResult(success);
                });
                return await taskCompletionSource.Task;
            });
        }

    }
}
