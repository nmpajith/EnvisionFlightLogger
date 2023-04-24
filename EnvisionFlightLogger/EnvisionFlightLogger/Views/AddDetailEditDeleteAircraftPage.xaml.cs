using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnvisionFlightLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDetailEditDeleteAircraftPage : ContentPage
    {
        public AddDetailEditDeleteAircraftPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                { DevicePlatform.Android, new[] { "*/*" } },
                { DevicePlatform.UWP, new[] { ".cbr", ".cbz" } },
                { DevicePlatform.Tizen, new[] { "*/*" } },
                { DevicePlatform.macOS, new[] { "cbr", "cbz" } }, // or general UTType values
            });
            var options = new PickOptions
            {
                PickerTitle = "Please select a photo to display",
                FileTypes = customFileType,
            };
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                status = await Device.InvokeOnMainThreadAsync(async () => await Permissions.RequestAsync<Permissions.StorageRead>());
            }

            var pickResult = await Device.InvokeOnMainThreadAsync(async () => await FilePicker.PickAsync(options));
            if (pickResult != null)
            {
                Debug.WriteLine( pickResult.FileName.ToString());
            }
            else
            {
                await DisplayAlert("Alert", "No File Selected", "OK");
            }
        }
    }
}