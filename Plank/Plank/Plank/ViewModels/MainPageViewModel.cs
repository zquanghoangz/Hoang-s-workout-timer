using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Plank.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace Plank.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private int _countDownNumber;
        private int _itemIndex;
        private FiveMinutesPlankModel _model;
        private DelegateCommand _pauseCommand;
        private DelegateCommand _resetCommand;
        private DelegateCommand _startCommand;

        private Task _workoutTask;
        private WorkingStatus _status;

        public MainPageViewModel()
        {
            LoadWelcome();
            Status = WorkingStatus.Stopped;
        }
        private void LoadWelcome()
        {
            Models = FiveMinutesPlankModelCreator.CreateFiveMinutesPlankList();
            Model = FiveMinutesPlankModel.Create("Welcome to 5 minutes plank");
            ItemIndex = 0;
            CountDownNumber = 0;
        }

        public ObservableCollection<FiveMinutesPlankModel> Models;

        public FiveMinutesPlankModel Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public int CountDownNumber
        {
            get { return _countDownNumber; }
            set { SetProperty(ref _countDownNumber, value); }
        }

        public int ItemIndex
        {
            get { return _itemIndex; }
            set { SetProperty(ref _itemIndex, value); }
        }

        public WorkingStatus Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
                OnPropertyChanged("StartButtonVisible");
            }
        }

        public bool StartButtonVisible => Status != WorkingStatus.Started;

        public DelegateCommand StartCommand =>
            _startCommand ?? (_startCommand = new DelegateCommand(StartAndResumeCommandExec));

        public DelegateCommand PauseCommand =>
            _pauseCommand ?? (_pauseCommand = new DelegateCommand(PauseCommandExec));

        public DelegateCommand ResetCommand =>
            _resetCommand ?? (_resetCommand = new DelegateCommand(ResetCommandExec));

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //Load data
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public void StartAndResumeCommandExec()
        {
            _workoutTask = Task.Run(async () =>
            {
                if (Status == WorkingStatus.Paused)
                {
                    Model = Models[ItemIndex];
                    //Keep the count
                }
                else
                {
                    Model = Models[0];//index = 0
                    ItemIndex = 0;
                    CountDownNumber = Model.TimeInSeconds;
                }

                Status = WorkingStatus.Started;
                do
                {
                    while (CountDownNumber > 0)
                    {
                        await Task.Delay(1000); //1s
                        if (_cancellationTokenSource.IsCancellationRequested)
                        {
                            _cancellationTokenSource = new CancellationTokenSource();
                            return;
                        }
                        CountDownNumber--;
                    }

                    ItemIndex++;
                    //Set model
                    Model = Models[ItemIndex];
                    CountDownNumber = Model.TimeInSeconds;
                } while (ItemIndex < Models.Count);

                Status = WorkingStatus.Stopped;
                ItemIndex = 0;
            });
        }

        public void PauseCommandExec()
        {
            _cancellationTokenSource.Cancel();
            _workoutTask = null;
            Status = WorkingStatus.Paused;
        }

        public void ResetCommandExec()
        {
            _cancellationTokenSource.Cancel();
            _workoutTask = null;

            LoadWelcome();

            Status = WorkingStatus.Stopped;
        }
    }

    public enum WorkingStatus
    {
        Started,
        Paused,
        Stopped
    }
}