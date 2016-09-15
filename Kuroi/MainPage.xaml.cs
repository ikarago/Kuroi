using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Kuroi
{

    public sealed partial class MainPage : Page
    {
        ApplicationView appview = ApplicationView.GetForCurrentView();
        bool _fullScreenOn = false;

        public MainPage()
        {
            this.InitializeComponent();
            SwitchToFullScreen();
            ShowMessage();
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (!_fullScreenOn)
            {
                SwitchToFullScreen();
                ShowMessage();
            }
            else
            {
                ExitFullScreen();
                ShowMessage();
            }

        }

        private async void SwitchToFullScreen()
        {
            // Try to switch to fullscreen
            try
            {
                appview.TryEnterFullScreenMode();
                appview.SuppressSystemOverlays = true;

                // Try to hide the statusbar on the phone
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                    await statusBar.HideAsync();
                }

                _fullScreenOn = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void ExitFullScreen()
        {
            // Try to exit the fullscreen
            try
            {
                appview.ExitFullScreenMode();
                appview.SuppressSystemOverlays = false;

                // Now show the Statusbar again if the device is a phone
                if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
                    await statusBar.ShowAsync();
                }

                _fullScreenOn = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private async void ShowMessage()
        {
            // Show the open/collapse-message for 10 seconds
            tblInstructions.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(10));
            tblInstructions.Visibility = Visibility.Collapsed;
        }
    }
}
