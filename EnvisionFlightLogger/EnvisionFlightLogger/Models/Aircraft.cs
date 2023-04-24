using EnvisionFlightLogger.DataAccess.Entities;
using EnvisionFlightLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnvisionFlightLogger.Models
{
    public class Aircraft : BaseViewModel
    {
        public int Id { get; set; }

        private string _make;
        public string Make 
        { 
            get { return _make; }
            set { SetProperty(ref _make, value); }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private string _registration;
        public string Registration
        {
            get { return _registration; }
            set { SetProperty(ref _registration, value); }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        private DateTime _dateAndTime;
        public DateTime DateAndTime
        {
            get { return _dateAndTime; }
            set { SetProperty(ref _dateAndTime, value); }
        }

        private byte[] _photo;
        public byte[] Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Aircraft() 
        { 
            DateAndTime = DateTime.Now;
        }

        public Aircraft(Aircraft aircraft) 
        { 
            Id = aircraft.Id;
            Make = aircraft.Make;
            Model = aircraft.Model;
            Registration = aircraft.Registration;
            Location = aircraft.Location;
            DateAndTime = aircraft.DateAndTime;
            if(aircraft.Photo!=null) 
            {
                Photo = new byte[aircraft.Photo.Length];
                for(int i=0;i<aircraft.Photo.Length;i++)
                {
                    Photo[i]= aircraft.Photo[i];
                }
            }
            Latitude = aircraft.Latitude;
            Longitude = aircraft.Longitude;
        }
        public Aircraft(AirCraftEntity aircraftEntity)
        {
            if (aircraftEntity == null)
                return;
            Id = aircraftEntity.Id;
            Make = aircraftEntity.Make;
            Model = aircraftEntity.Model;
            Registration = aircraftEntity.Registration;
            Location = aircraftEntity.Location;
            DateAndTime = aircraftEntity.DateAndTime;
            if (aircraftEntity.Photo != null)
            {
                Photo = new byte[aircraftEntity.Photo.Length];
                for (int i = 0; i < aircraftEntity.Photo.Length; i++)
                {
                    Photo[i] = aircraftEntity.Photo[i];
                }
            }
            Latitude = aircraftEntity.Latitude;
            Longitude = aircraftEntity.Longitude;
        }

        public AirCraftEntity GetAircraftEntity()
        {
            var aircraftEntity = new AirCraftEntity();
            aircraftEntity.Id = Id; ;
            aircraftEntity.Make = Make;
            aircraftEntity.Model = Model;
            aircraftEntity.Registration = Registration;
            aircraftEntity.Location = Location;
            aircraftEntity.DateAndTime = DateAndTime;
            aircraftEntity.Photo = Photo;
            aircraftEntity.Latitude = Latitude;
            aircraftEntity.Longitude = Longitude;
            return aircraftEntity;
        }
    }
}
