using EnvisionFlightLogger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnvisionFlightLogger.ViewModels
{
    public class DeleteAircraftViewModel : AddAircraftViewModel
    {

        public DeleteAircraftViewModel(Aircraft aircraft = null)
            : base()
        {
            if (aircraft != null)
            {
                Aircraft = new Aircraft(aircraft);
            }
            IsTextNameVisible = true;
            IsNonEditableView = true;
            IsDeleteMode = true;
            SetButtonText("Delete");
        }

        protected override async Task ButtonClickedAsync()
        {
            Aircraft.DateAndTime = Date.Add(Time);
            MessagingCenter.Send(this, "DeleteAircraft", Aircraft);
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}

