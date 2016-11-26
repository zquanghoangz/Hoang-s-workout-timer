using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WorkoutTimer.Components.FiveMinutesPlank
{
    public class FiveMinutesPlankViewModel : INotifyPropertyChanged
    {
        private FiveMinutesPlankModel _currentModel;
        private IList<FiveMinutesPlankModel> _models;

        public FiveMinutesPlankViewModel()
        {
            LoadData();
        }

        public IList<FiveMinutesPlankModel> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged();
            }
        }

        public FiveMinutesPlankModel CurrentModel
        {
            get { return _currentModel; }
            set
            {
                _currentModel = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            var models = new List<FiveMinutesPlankModel>
            {
                CreateStartModel(),
                CreateRecoverTimeModel("fmpPrepareForFullPlank.png"),
                CreateAModel("Full Plank", 60, "fmpFullPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForElbowPlank.png"),
                CreateAModel("Elbow Plank", 30, "fmpElbowPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForRaisedLegPlank.png"),
                CreateAModel("Raised Leg Plank - Left", 30, "fmpRaisedLegPlank.jpg"),
                 CreateRecoverTimeModel("fmpPrepareForRaisedLegPlank.png"),
                CreateAModel("Raised Leg Plank - Right", 30, "fmpRaisedLegPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForSidePlank.png"),
                CreateAModel("Side Plank - Left", 30, "fmpSidePlank.jpg"),
                 CreateRecoverTimeModel("fmpPrepareForSidePlank.png"),
                CreateAModel("Side Plank - Right", 30, "fmpSidePlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForFullPlank.png"),
                CreateAModel("Full Plank", 30, "fmpFullPlank.jpg"),
                CreateRecoverTimeModel("fmpPrepareForElbowPlank.png"),
                CreateAModel("Elbow Plank", 60, "fmpElbowPlank.jpg"),
                CreateEndModel()
            };

            Models = models;
            _currentIndex = 0;
            CurrentModel = Models[_currentIndex];
            StartPauseText = "START";
        }

        private FiveMinutesPlankModel CreateStartModel()
        {
            return CreateAModel("START", 1, "fmpAreYouReady.png", 1);
        }

        private FiveMinutesPlankModel CreateEndModel()
        {
            return CreateAModel("END", 3, "fmpWellDone.png", 1);
        }

        private FiveMinutesPlankModel CreateRecoverTimeModel(string imageUrl)
        {
            return CreateAModel("PREPARE & RECOVER", 2, imageUrl, 1);
        }

        private FiveMinutesPlankModel CreateAModel(
            string name,
            int timeInSecond,
            string imageUrl = "",
            int bgColorType = 0)
        {
            return new FiveMinutesPlankModel
            {
                Id = Guid.NewGuid(),
                Name = name,
                ImageUrl = imageUrl,
                TimeInSecond = timeInSecond,
                BackgroundColor = bgColorType == 0 ? Color.FromRgb(92, 184, 92) : Color.FromRgb(217, 83, 79)
            };
        }

        #region Actions

        public string StartPauseText
        {
            get { return _startPauseText; }
            set
            {
                _startPauseText = value;
                StartPauseEnabled = true;
                OnPropertyChanged();
            }
        }

        public bool StartPauseEnabled
        {
            get { return _startPauseEnabled; }
            set
            {
                _startPauseEnabled = value;
                OnPropertyChanged();
            }
        }

        private int _currentIndex;
        private string _startPauseText;
        private bool _isReseted;
        private ICommand _startPauseCommand;

        public ICommand StartPauseCommand
            => _startPauseCommand ?? (_startPauseCommand = new Command(StartPauseCommandExec));

        private Task _workoutTask;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void StartPauseCommandExec()
        {
            StartPauseEnabled = false;

            if (_workoutTask != null && _workoutTask.Status == TaskStatus.WaitingForActivation)
            {
                _cancellationTokenSource.Cancel();
                _workoutTask = null;
                return;
            }

            _workoutTask = Task.Run(async () =>
            {
                StartPauseText = "PAUSE";
                for (var i = _currentIndex; i < Models.Count; i++)
                {
                    CurrentModel = Models[i];
                    while (Models[i].TimeInSecond > 0)
                    {
                        if (_cancellationTokenSource.IsCancellationRequested)
                        {
                            DoCancelTimer(i);
                            return;
                        }
                        await Task.Delay(1000); //1s
                        Models[i].TimeInSecond--;
                        if (_cancellationTokenSource.IsCancellationRequested)
                        {
                            DoCancelTimer(i);
                            return;
                        }
                    }
                }
                LoadData();
            });
        }

        private void DoCancelTimer(int i)
        {
            if (_isReseted)
            {
                _isReseted = false;
                LoadData();
            }
            else
            {
                StartPauseText = "RESUME";
                _currentIndex = i;
            }

            _cancellationTokenSource = new CancellationTokenSource();
        }

        private ICommand _resetCommand;
        private bool _startPauseEnabled;
        public ICommand ResetCommand => _resetCommand ?? (_resetCommand = new Command(ResetCommandExec));

        private void ResetCommandExec()
        {
            if (_workoutTask != null && _workoutTask.Status == TaskStatus.WaitingForActivation)
            {
                _isReseted = true;
                _cancellationTokenSource.Cancel();
                _workoutTask = null;
            }
            else
            {
                LoadData();
            }
        }

        #endregion

        #region Notify Properties Changed

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}