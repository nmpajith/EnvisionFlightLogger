using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnvisionFlightLogger
{
    public class NavigationDispatcher
    {
        private static NavigationDispatcher _instance;

        private INavigation _navigation;

        public static NavigationDispatcher Instance =>
                      _instance ?? (_instance = new NavigationDispatcher());

        public INavigation Navigation =>
                     _navigation ?? throw new Exception("NavigationDispatcher is not initialized");

        public void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}
