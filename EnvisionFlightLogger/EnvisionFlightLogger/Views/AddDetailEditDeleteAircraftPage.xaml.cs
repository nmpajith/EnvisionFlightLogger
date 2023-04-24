using EnvisionFlightLogger.ViewModels;
using FluentValidation.Results;
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
            MessagingCenter.Subscribe<AddDetailEditDeleteAircraftViewModel, string>(this, "ValidationFailed", OnValidationFailed);
        }

        private async void OnValidationFailed(AddDetailEditDeleteAircraftViewModel arg1, string message)
        {
            await DisplayAlert("Validation Error", message, "OK");
        }
    }
}