using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Service.QuickSettings;
using Android.Views;
using Android.Widget;
using EnvisionFlightLogger.Droid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace EnvisionFlightLogger.Droid
{

    public class FileService : IFileService
    {
        public byte[] LoadFile(string filePath)
        {
            byte[] bytes = null;
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, (int)fileStream.Length);
            }
            return bytes;
        }
    }
}