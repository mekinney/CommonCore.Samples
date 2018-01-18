#if __ANDROID__
using System;
using System.IO;
using Android.Content;
using Android.Widget;
using pdfmaker.Dependencies;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileReader))]
namespace pdfmaker.Dependencies
{
    public class FileReader : IFileReader
    {
        public Context Ctx
        {
            get => CrossCurrentActivity.Current.Activity;
        }

        public void Read(string filePath)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var bytes = File.ReadAllBytes(filePath);

                //Copy the private file's data to the EXTERNAL PUBLIC location
                var fileName = Path.GetFileName(filePath);
                string externalStorageState = Android.OS.Environment.ExternalStorageState;
                var externalPath = Android.OS.Environment.ExternalStorageDirectory.Path + "/" + Android.OS.Environment.DirectoryDownloads + "/" + fileName;
                File.WriteAllBytes(externalPath, bytes);

                Java.IO.File file = new Java.IO.File(externalPath);
                file.SetReadable(true);

                string application = "";
                string extension = Path.GetExtension(filePath);

                // get mimeTye
                switch (extension.ToLower())
                {
                    case ".txt":
                        application = "text/plain";
                        break;
                    case ".doc":
                    case ".docx":
                        application = "application/msword";
                        break;
                    case ".pdf":
                        application = "application/pdf";
                        break;
                    case ".xls":
                    case ".xlsx":
                        application = "application/vnd.ms-excel";
                        break;
                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        application = "image/jpeg";
                        break;
                    default:
                        application = "*/*";
                        break;
                }

                Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
                Intent intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, application);
                intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

                try
                {
                    Ctx.StartActivity(intent);
                }
                catch (Exception ex)
                {

                    Toast.MakeText(Ctx, "No Application Available to View PDF", ToastLength.Short).Show();


                }
            });
        }
    }
}
#endif
