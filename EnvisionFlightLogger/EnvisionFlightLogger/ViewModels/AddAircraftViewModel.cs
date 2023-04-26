using EnvisionFlightLogger.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnvisionFlightLogger.ViewModels
{
    public class AddAircraftViewModel : BaseViewModel
    {
        private string _validationMessage;
        public string ValidationMessage
        {
            get { return _validationMessage; }
            set { SetProperty(ref _validationMessage, value); }
        }

        private bool _hasValidationErrors;
        public bool HasValidationErrors
        {
            get { return _hasValidationErrors; }
            set { SetProperty(ref _hasValidationErrors, value); }
        }

        private bool _isDeleteMode;
        public bool IsDeleteMode
        {
            get { return _isDeleteMode; }
            set { SetProperty(ref _isDeleteMode, value); }
        }

        private bool _isTextNameVisible;
        public bool IsTextNameVisible
        {
            get { return _isTextNameVisible; }
            set { SetProperty(ref _isTextNameVisible, value); }
        }

        private bool _isNonEditableView;
        public bool IsNonEditableView
        {
            get { return _isNonEditableView; }
            set { SetProperty(ref _isNonEditableView, value); }
        }

        private Aircraft _aircraft;
        public Aircraft Aircraft
        {
            get { return _aircraft; }
            set { SetProperty(ref _aircraft, value); }
        }

        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set { SetProperty(ref _buttonText, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
            }
        }

        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                SetProperty(ref _time, value);
            }
        }

        public ICommand ButtonCommand { get; set; }
        public ICommand BrowseCommand { get; set; }

        public AddAircraftViewModel()
        {
            Aircraft = new Aircraft();
            Date = Aircraft.DateAndTime.Date;
            Time = Aircraft.DateAndTime.Subtract(Date);
            SetButtonText("Save");
            ButtonCommand = new Command(async () => await ButtonClickedAsync());
            BrowseCommand = new Command(async () => await BrowseImageAsync());
        }

        protected virtual async Task ButtonClickedAsync()
        {
            Aircraft.DateAndTime = Date.Add(Time);
            MessagingCenter.Send(this, "AddAircraft", Aircraft);
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }

        protected async Task BrowseImageAsync()
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
            await TryReadPhotoAsync(options);
        }

        protected bool ValidateAirCraft(Aircraft aircraft)
        {
            var validator = new AirCraftValidator();
            var result = validator.Validate(aircraft);

            if (!result.IsValid)
            {
                MessagingCenter.Send(this, "ValidationFailed", result.Errors.First().ErrorMessage);
                return false;
            }
            return true;
        }

        protected void SetButtonText(string text)
        {
           ButtonText = text;
        }

        private async Task TryReadPhotoAsync(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        Aircraft.Photo = GetBytes(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }

        private byte[] GetBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
