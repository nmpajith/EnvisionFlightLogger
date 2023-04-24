using EnvisionFlightLogger.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace EnvisionFlightLogger.iOS
{
    public class FileService : IFileService
    {
        public byte[] LoadFile(string filePath)
        {
            NSData imageData = NSData.FromFile(filePath);
            byte[] bytes = new byte[imageData.Length];
            System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, (int)imageData.Length);
            return bytes;
        }
    }
}