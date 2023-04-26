using EnvisionFlightLogger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnvisionFlightLogger.ViewModels
{
    public class DetailAircraftViewModel : AddAircraftViewModel
    {

        public DetailAircraftViewModel(Aircraft aircraft = null)
            : base()
        {
            if (aircraft != null)
            {
                Aircraft = new Aircraft(aircraft);
            }
            IsTextNameVisible = true;
            IsNonEditableView = true;
            SetButtonText("Close");
        }

        protected override async Task ButtonClickedAsync()
        {
            Aircraft.DateAndTime = Date.Add(Time);
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}