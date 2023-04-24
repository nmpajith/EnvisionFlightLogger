using EnvisionFlightLogger.Extentions;
using EnvisionFlightLogger.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnvisionFlightLogger
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainListView.ItemTapped += ListViewExtensions.OnItemTapped;
        }
    }
}
