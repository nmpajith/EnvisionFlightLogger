using EnvisionFlightLogger.Enums;
using EnvisionFlightLogger.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EnvisionFlightLogger.ViewModels
{
    public class AddDetailEditDeleteAircraftViewModel : BaseViewModel
    {
        private EAircraftViewDisplayMode _aircraftViewDisplayMode;
        public EAircraftViewDisplayMode AircraftViewDisplayMode
        {
            get { return _aircraftViewDisplayMode; }
            set
            {
                SetProperty(ref _aircraftViewDisplayMode, value);
                IsTextNameVisible = _aircraftViewDisplayMode == EAircraftViewDisplayMode.Edit ||
                    _aircraftViewDisplayMode == EAircraftViewDisplayMode.Detail ||
                    _aircraftViewDisplayMode == EAircraftViewDisplayMode.Delete;
                IsNonEditableView = _aircraftViewDisplayMode != EAircraftViewDisplayMode.Edit &&
                    _aircraftViewDisplayMode != EAircraftViewDisplayMode.Add;
                IsDeleteMode = _aircraftViewDisplayMode == EAircraftViewDisplayMode.Delete;
            }
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

        public ICommand ButtonCommand { get; private set; }
        public ICommand BrowseCommand { get; private set; }
        public ICommand CameraCommand { get; private set; }


        public AddDetailEditDeleteAircraftViewModel()
        {
            Aircraft = new Aircraft();
            Date = Aircraft.DateAndTime.Date;
            Time = Aircraft.DateAndTime.Subtract(Date);
            SetButtonText(EAircraftViewDisplayMode.Add);
            ButtonCommand= new Command(async () => await ButtonClickedAsync());
            BrowseCommand = new Command(async () => await BrowseImageAsync());
        }       

        public AddDetailEditDeleteAircraftViewModel(Aircraft aircraft = null, EAircraftViewDisplayMode aircraftViewDisplayMode = EAircraftViewDisplayMode.Add):
            this()
        {
            if (aircraft != null)
            {
                Aircraft = new Aircraft(aircraft);
            }
            AircraftViewDisplayMode = aircraftViewDisplayMode;
            SetButtonText(aircraftViewDisplayMode);
        }
        
        private async Task BrowseImageAsync()
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


        private async Task ButtonClickedAsync()
        {
            Aircraft.DateAndTime = Date.Add(Time);
            switch (AircraftViewDisplayMode)
            {
                case EAircraftViewDisplayMode.Add:
                    MessagingCenter.Send(this, "AddAircraft", Aircraft);
                    break;
                case EAircraftViewDisplayMode.Edit:
                    MessagingCenter.Send(this, "EditAircraft", Aircraft);
                    break;
                case EAircraftViewDisplayMode.Detail:
                    break;
                case EAircraftViewDisplayMode.Delete:
                    MessagingCenter.Send(this, "DeleteAircraft", Aircraft);
                    break;
                default:
                    MessagingCenter.Send(this, "AddAircraft", Aircraft);
                    break;
            }
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }

        private void SetButtonText(EAircraftViewDisplayMode aircraftViewDisplayMode)
        {
            switch (aircraftViewDisplayMode)
            {
                case EAircraftViewDisplayMode.Add:
                    ButtonText = "Save";
                    break;
                case EAircraftViewDisplayMode.Detail:
                    ButtonText = "Close";
                    break;
                case EAircraftViewDisplayMode.Edit:
                    ButtonText = "Save";
                    break;
                case EAircraftViewDisplayMode.Delete:
                    ButtonText = "Delete";
                    break;
                default:
                    ButtonText = "Save";
                    break;
            }
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
                        Aircraft.Photo=GetBytes(stream);
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
