using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plank.ViewModels;
using Xamarin.Forms;

namespace Plank.Converters
{
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converting(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Converting(value);
        }

        private static object Converting(object value)
        {
            if (value is bool)
            {
                return !(bool)value;
            }

            return false;
        }
    }

    public class IndexToButtonTextConverter : IValueConverter
    {
        public string StartText = "START";
        public string ResumeText = "RESUME";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return (int)value == 0 ? StartText : ResumeText;
            }

            return StartText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WorkingStatusToTextConverter : IValueConverter
    {
        public string StartText = "START";
        public string ResumeText = "RESUME";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WorkingStatus)
            {
                return (WorkingStatus)value == WorkingStatus.Paused ?
                    ResumeText : StartText;
            }

            return StartText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
