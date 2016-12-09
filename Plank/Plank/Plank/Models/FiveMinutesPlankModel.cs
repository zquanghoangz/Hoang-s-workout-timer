using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Plank.Models
{
    public class FiveMinutesPlankModel : BindableBase
    {
        #region Create

        public static FiveMinutesPlankModel Create(
          string title,
          int timeInSeconds = 30,
          string imageUrl = "",
          BgColorType bgColorType = BgColorType.Active)
        {
            return new FiveMinutesPlankModel
            {
                Id = Guid.NewGuid(),
                Title = title,
                TimeInSeconds = timeInSeconds,
                ImageUrl = imageUrl,
                BackgroundColor = bgColorType == BgColorType.Active ?
                    Color.FromRgb(92, 184, 92) : Color.FromRgb(217, 83, 79)
            };
        }

        #endregion

        #region Fields

        private Guid _id;
        private string _title;
        private int _timeInSeconds;
        private string _imageUrl;
        private Color _backgroundColor;

        #endregion Fields

        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public int TimeInSeconds
        {
            get { return _timeInSeconds; }
            set { SetProperty(ref _timeInSeconds, value); }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { SetProperty(ref _backgroundColor, value); }
        }
    }

    public enum BgColorType
    {
        Active = 0,
        Waiting = 1
    }
}
