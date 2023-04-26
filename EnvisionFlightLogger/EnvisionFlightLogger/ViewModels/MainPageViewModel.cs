using EnvisionFlightLogger.DataAccess.Entities;
using EnvisionFlightLogger.DataAccess.Services;
using EnvisionFlightLogger.Models;
using EnvisionFlightLogger.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnvisionFlightLogger.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly IAircraftService _aircraftService;
        private ObservableCollection<Aircraft> _aircraftList;
        public ObservableCollection<Aircraft> AircraftList
        {
            get { return _aircraftList; }
            set { SetProperty(ref _aircraftList, value); }
        }

        private Aircraft _selectedAircraft;
        public Aircraft SelectedAircraft
        {
            get { return _selectedAircraft; }
            set { SetProperty(ref _selectedAircraft, value); }
        }

        public ICommand AddAircraftCommand { get; private set; }
        public ICommand UpdateAircraftCommand { get; private set; }
        public ICommand DeleteAircraftCommand { get; private set; }
        public ICommand FilterAircraftCommand { get; private set; }
        public ICommand ContentPageLoadedCommand { get; private set; }
        public ICommand ItemTappedCommand { get; private set; }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public MainPageViewModel()
        {
            AircraftList = new ObservableCollection<Aircraft>();
            AddAircraftCommand = new Command(AddAircraft);
            UpdateAircraftCommand = new Command<int>((int id) => UpdateAircraft(id));
            DeleteAircraftCommand = new Command<int>((int id) => DeleteAircraft(id));
            ItemTappedCommand = new Command<Aircraft>((Aircraft aircraft) => ShowDetails(aircraft));
            FilterAircraftCommand = new Command(FilterAircraft);
            ContentPageLoadedCommand = new Command(ContentPageLoaded);
            _aircraftService = DependencyService.Resolve<IAircraftService>();
            MessagingCenter.Subscribe<AddAircraftViewModel, Aircraft>(this, "AddAircraft", (page, aircraft) => AddAircraft(aircraft));
            MessagingCenter.Subscribe<EditAircraftViewModel, Aircraft>(this, "EditAircraft", (page, aircraft) => UpdateAircraft(aircraft));
            MessagingCenter.Subscribe<DeleteAircraftViewModel, Aircraft>(this, "DeleteAircraft", (page, aircraft) => DeleteAircraft(aircraft));
        }

        private async void ContentPageLoaded()
        {
            await Task.Run(() => {
                var allAircraftEntities = _aircraftService.GetAllAircrafts();
                if (allAircraftEntities != null)
                {
                    AircraftList.Clear();
                    foreach (var airCraftEntity in allAircraftEntities)
                    {
                        AircraftList.Add(new Aircraft(airCraftEntity));
                    }
                }
            });            
        }

        private void AddAircraft()
        {
            var addAircraftPage = new AddDetailEditDeleteAircraftPage();
            NavigationDispatcher.Instance.Navigation.PushAsync(addAircraftPage);
        }

        private void AddAircraft(Aircraft aircraft)
        {
            if (aircraft == null)
                return;
            var aircraftEntity = aircraft.GetAircraftEntity();
            _aircraftService.AddAircraft(aircraftEntity);
            AircraftList.Add(new Aircraft(aircraftEntity));
        }

        private void ShowDetails(Aircraft aircraft)
        {
            var detailAircraftViewModel = new DetailAircraftViewModel(aircraft);
            var detailAircraftPage = new AddDetailEditDeleteAircraftPage();
            detailAircraftPage.BindingContext = detailAircraftViewModel;
            NavigationDispatcher.Instance.Navigation.PushAsync(detailAircraftPage);
        }

        private void UpdateAircraft(int id)
        {
            var aircraft = AircraftList.Where(ac=>ac.Id==id).First();
            if (aircraft != null)
            {
                var editAircraftViewModel = new EditAircraftViewModel(aircraft);
                var editAircraftPage = new AddDetailEditDeleteAircraftPage();
                editAircraftPage.BindingContext = editAircraftViewModel;
                NavigationDispatcher.Instance.Navigation.PushAsync(editAircraftPage);
            }
        }

        private void UpdateAircraft(Aircraft aircraft)
        {
            if (aircraft == null)
                return;
            var aircraftEntity = aircraft.GetAircraftEntity();
            _aircraftService.UpdateAircraft(aircraftEntity);
            var aircraaftToUpdate = AircraftList.Where(ac => ac.Id == aircraft.Id).First();
            aircraaftToUpdate = new Aircraft(aircraftEntity);
        }

        private void DeleteAircraft(int id)
        {
            var aircraft = AircraftList.Where(ac => ac.Id == id).First();
            if (aircraft != null)
            {
                var deleteAircraftViewModel = new DeleteAircraftViewModel(aircraft);
                var deleteAircraftPage = new AddDetailEditDeleteAircraftPage();
                deleteAircraftPage.BindingContext = deleteAircraftViewModel;
                NavigationDispatcher.Instance.Navigation.PushAsync(deleteAircraftPage);
            }
        }

        private void DeleteAircraft(Aircraft aircraft)
        {
            if (aircraft == null)
                return;
            var aircraftEntity = aircraft.GetAircraftEntity();
            _aircraftService.DeleteAircraft(aircraftEntity.Id);
            var aircraaftToDelete = AircraftList.Where(ac => ac.Id == aircraft.Id).First();
            AircraftList.Remove(aircraaftToDelete);
        }

        private void FilterAircraft()
        {
            // Filter the list of aircraft based on the search text
            if (!string.IsNullOrEmpty(SearchText))
            {
                var filteredList = AircraftList.Where(a => a.Make.ToLower().Contains(SearchText.ToLower()) ||
                                                           a.Model.ToLower().Contains(SearchText.ToLower()) ||
                                                           a.Registration.ToLower().Contains(SearchText.ToLower()));
                AircraftList = new ObservableCollection<Aircraft>(filteredList);
            }
            else
            {
                AircraftList = new ObservableCollection<Aircraft>(AircraftList);
            }
        }
    }
}
