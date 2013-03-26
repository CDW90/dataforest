using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace DataForest.View.Dialogs.Converter
{
    class DriveInfoToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DriveInfo drive = value as DriveInfo;
            if (drive != null)
            {
                return drive.TotalSize - drive.TotalFreeSpace;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
