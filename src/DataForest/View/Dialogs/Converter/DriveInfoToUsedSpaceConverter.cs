using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.IO;

namespace DataForest.View.Dialogs.Converter
{
    class DriveInfoToUsedSpaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DriveInfo drive = value as DriveInfo;
            if (drive != null && drive.IsReady)
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
