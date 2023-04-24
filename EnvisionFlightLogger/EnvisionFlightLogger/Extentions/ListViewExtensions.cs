using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnvisionFlightLogger.Extentions
{
    public static class ListViewExtensions
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.CreateAttached("ItemTappedCommand", typeof(ICommand), typeof(ListViewExtensions), null);

        public static ICommand GetItemTappedCommand(BindableObject obj)
        {
            return (ICommand)obj.GetValue(ItemTappedCommandProperty);
        }

        public static void SetItemTappedCommand(BindableObject obj, ICommand value)
        {
            obj.SetValue(ItemTappedCommandProperty, value);
        }

        public static void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = sender as ListView;
            var command = GetItemTappedCommand(listView);
            if (command != null && command.CanExecute(e.Item))
            {
                command.Execute(e.Item);
            }
            listView.SelectedItem = null;
        }
    }
}
