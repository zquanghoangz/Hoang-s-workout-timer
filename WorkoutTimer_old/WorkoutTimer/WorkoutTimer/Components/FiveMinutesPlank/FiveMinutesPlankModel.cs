using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WorkoutTimer.Components.FiveMinutesPlank
{
    public class FiveMinutesPlankModel : INotifyPropertyChanged
    {
        private Color _backgroundColor;
        private int _timeInSecond;
        private string _imageUrl;
        private string _name;
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string ImageUrl
        {
            get
            {
                return string.IsNullOrEmpty(_imageUrl) ?
                    "icon.png" : _imageUrl;
            }
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        public int TimeInSecond
        {
            get { return _timeInSecond; }
            set
            {
                _timeInSecond = value;
                OnPropertyChanged();
            }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();
            }
        }

        #region Notify Properties Changed

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}