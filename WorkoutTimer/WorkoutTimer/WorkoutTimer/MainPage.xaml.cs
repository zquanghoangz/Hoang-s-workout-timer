using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimer.Components.FiveMinutesPlank;
using Xamarin.Forms;

namespace WorkoutTimer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void StartFiveMinutesPlank(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new FiveMinutesPlankPage());
        }
    }
}
