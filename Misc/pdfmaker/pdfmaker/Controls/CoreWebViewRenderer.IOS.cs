#if __IOS__
using System;
using System.IO;
using CoreGraphics;
using Foundation;
using pdfmaker.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CoreWebView), typeof(CoreWebViewRenderer))]
namespace pdfmaker.Controls
{
    public class CoreWebViewRenderer: WebViewRenderer
    {
        private CoreWebView cvw;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            cvw = (CoreWebView)e.NewElement;
            cvw.CreatePDF += SavePDFMethod;
        }

        protected override void Dispose(bool disposing)
        {
            cvw.CreatePDF -= SavePDFMethod;
            base.Dispose(disposing);
        }

        private void SavePDFMethod(Action<bool> callback)
        {
            try
            {
                Device.BeginInvokeOnMainThread(()=>{
                    var pdfData = CreatePdfFile(this.NativeView.ViewPrintFormatter);
                    var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    var filename = Path.Combine(documents, cvw.FileName);
                    cvw.FullPath = filename;
                    var success = pdfData.Save(filename, true);
                    callback?.Invoke(success);
                });

            }
            catch (Exception ex)
            {
                cvw.PDFException = ex;
                callback?.Invoke(false);
            }

        }
        private NSData CreatePdfFile(UIViewPrintFormatter printFormatter)
        {

            var renderer = new UIPrintPageRenderer();
            renderer.AddPrintFormatter(printFormatter, 0);
            var point = new CoreGraphics.CGPoint(0, 0);

            var paperSize = new CoreGraphics.CGSize(this.Frame.Size.Width, this.Frame.Size.Height);
            var printableRect = new CoreGraphics.CGRect(point, new CoreGraphics.CGSize(paperSize.Width, paperSize.Height));
            var paperRect = new CoreGraphics.CGRect(point, new CoreGraphics.CGSize(paperSize.Width, paperSize.Height));

            renderer.SetValueForKey(NSValue.FromCGRect(paperRect), new NSString("paperRect"));
            renderer.SetValueForKey(NSValue.FromCGRect(printableRect), new NSString("printableRect"));

            return renderer.PrintToPDF(paperRect);
        }


    }

    public static class CoreWebViewRendererExtension
    {
        public static NSData PrintToPDF(this UIPrintPageRenderer renderer, CGRect paperRect)
        {
            var pdfData = new NSMutableData();
            UIGraphics.BeginPDFContext(pdfData, paperRect, null);
            var range = new NSRange(0, renderer.NumberOfPages);
            renderer.PrepareForDrawingPages(range);
            var bounds = UIGraphics.PDFContextBounds;
            for (int x = 0; x < renderer.NumberOfPages;x++){
                UIGraphics.BeginPDFPage();
                renderer.DrawPage(x, bounds);
            }
            UIGraphics.EndPDFContent();
            return pdfData;
        }
    }

}
#endif
