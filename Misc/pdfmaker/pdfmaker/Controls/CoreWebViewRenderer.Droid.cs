#if __ANDROID__
using System;
using Android.Content;
using Android.OS;
using Android.Print;
using pdfmaker.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CoreWebView), typeof(CoreWebViewRenderer))]
namespace pdfmaker.Controls
{
    public class CoreWebViewRenderer : WebViewRenderer
    {
        private Context ctx;
        private CoreWebView cvw;
        private Android.Webkit.WebView NativeView { get; set; }
        private PrintJob job { get; set; }
        public CoreWebViewRenderer(Context context) : base(context)
        {
            ctx = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            cvw = (CoreWebView)e.NewElement;
            cvw.CreatePDF += SavePDFMethod;
            NativeView = this.Control;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    using (var printMgr = (PrintManager)ctx.GetSystemService(Context.PrintService))
                    {
                        var wrapper = new PrintDocumentAdapterWrapper(NativeView.CreatePrintDocumentAdapter($"{cvw.FileName}"), () =>
                        {
                            callback?.Invoke(true);
                        });
                        job = printMgr.Print("CoreWebView PrintJob", wrapper, null);
                     
                    }
                });
            }
            catch (Exception ex)
            {
                cvw.PDFException = ex;
                callback?.Invoke(false);
            }

        }

    }

    public class PrintDocumentAdapterWrapper : PrintDocumentAdapter
    {

        private PrintDocumentAdapter adapter;
        private Action completed;

        public PrintDocumentAdapterWrapper(PrintDocumentAdapter adapter, Action completed) : base()
        {

            this.adapter = adapter;
            this.completed = completed;
        }

        public override void OnFinish()
        {
            base.OnFinish();
            this.completed?.Invoke();
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            adapter.OnLayout(oldAttributes, newAttributes, cancellationSignal, callback, extras);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            adapter.OnWrite(pages, destination, cancellationSignal, callback);

        }
    }

}
#endif
