using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows;

namespace DataForest.View.Dialogs.Converter
{
    class FileInfoToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FileInfo file = value as FileInfo;
            if (file != null)
            {
                Icon icon = Icon.ExtractAssociatedIcon(file.FullName);
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, new Int32Rect(0,0,32,32), BitmapSizeOptions.FromEmptyOptions());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
