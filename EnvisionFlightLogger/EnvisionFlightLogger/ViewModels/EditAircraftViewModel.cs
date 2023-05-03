using EnvisionFlightLogger.DataAccess.Entities;
using EnvisionFlightLogger.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EnvisionFlightLogger.ViewModels
{
    public class EditAircraftViewModel : AddAircraftViewModel
    {  

        public EditAircraftViewModel(Aircraft aircraft = null)
            :base()
        {
            if (aircraft != null)
            {
                Aircraft = new Aircraft(aircraft);
            }
            IsTextNameVisible = true;
            SetButtonText("Save");
        }

        protected override async Task ButtonClickedAsync()
        {
            Aircraft.DateAndTime = Date.Add(Time);
            if (!ValidateAirCraft(Aircraft))
                return;
            MessagingCenter.Send(this, "EditAircraft", Aircraft);
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
