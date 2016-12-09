using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace WorkoutTimer.Droid
{
    [Activity(
        Label = "WorkoutTimer",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.OnCreate(bundle);

            SetScreenOrientation();

            Forms.Init(this, bundle);

            SetWindowManagerFlags();

            LoadApplication(new App());
        }

        private void SetWindowManagerFlags()
        {
            //Window.AddFlags(WindowManagerFlags.Fullscreen);
            //Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            //Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            //Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
        }

        private void SetScreenOrientation()
        {
            //// This block your orientation
            //((AppDelegate)UIApplication.SharedApplication.Delegate).CurrentDeviceOrientation = orientationMask;

            //// First call force orientation to change second allows to rotate from 
            //// LandscapeLeft/LandscapeRight or Portrait/PortraitUpsideDown
            //UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromInt32((int)orientation), (NSString)"orientation");
            //UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromInt32((int)UIInterfaceOrientation.Unknown), (NSString)"orientation");
        }
    }
}