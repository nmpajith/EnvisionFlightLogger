using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace EnvisionFlightLogger.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] bytes)
            {
                ImageSource imageSource = null;

                if (bytes != null && bytes.Length > 0)
                {
                    imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
                }

                return imageSource;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
