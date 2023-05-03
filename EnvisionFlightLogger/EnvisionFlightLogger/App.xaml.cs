using EnvisionFlightLogger.DataAccess;
using EnvisionFlightLogger.DataAccess.Services;
using EnvisionFlightLogger.DataAccess.UnitOfWork;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnvisionFlightLogger
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var appRun = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DependencyService.RegisterSingleton<DataContext>(
                new DataContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EnvisionFlightLogger.db3")));
            DependencyService.RegisterSingleton<IUnitOfWork>(new UnitOfWork(DependencyService.Resolve<DataContext>()));

            DependencyService.RegisterSingleton<IAircraftService>(new AircraftService(DependencyService.Resolve<IUnitOfWork>()));

            MainPage = new NavigationPage(new MainPage());
            NavigationDispatcher.Instance.Initialize(MainPage.Navigation);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
