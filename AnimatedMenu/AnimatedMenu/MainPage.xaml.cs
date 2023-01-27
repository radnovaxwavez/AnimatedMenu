using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnimatedMenu
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MenuItems = GetMenus(); //Menu class fed in to getter-setter class
            this.BindingContext = this; //Setting BindingContext to application itself to ensure data passes through correctly
        }

        //Getter-setter class for data to be fed into
        public ObservableCollection<Menu> MenuItems { get; set; }
        //Data itself
        private ObservableCollection<Menu> GetMenus()
        {
            return new ObservableCollection<Menu>
            {
                new Menu { Title = "PROFILE", Icon = "profile.png" },
                new Menu { Title = "FEED", Icon = "feed.png" },
                new Menu { Title = "ACTIVITY", Icon = "activity.png" },
                new Menu { Title = "SETTINGS", Icon = "settings.png" }
            };
        }
        private async void Show() //Animation that displays the menu, note asynchronous class
        {
            _ = TitleText.FadeTo(0); //Note use of underscores as placeholders, kind of like void class
            _ = MenuItemsView.FadeTo(1);
            await MainMenuView.RotateTo(0, 300, Easing.BounceOut); //Note 3 commands, was also happy with 1 so is not fixed to need 3 values exclusively
        }

        private async void Hide()  //Animation that hides the menu (believe it or not lol)
        {
            _ = TitleText.FadeTo(1); 
            _ = MenuItemsView.FadeTo(0);
            await MainMenuView.RotateTo(-90, 300, Easing.BounceOut); //Note coordinates, speed, animation order for RotateTo()
        }
        private void ShowMenu(object sender, EventArgs e)
        {
            Show(); //Class was created outside of the function that is attached to the object itself, possible due to asynchronous command
        }
        private void MenuTapped(object sender, EventArgs e) //Note how sender object is set to StackLayout
        {
            TitleText.Text = ((sender as StackLayout).BindingContext as Menu).Title; //Setting the TitleText
            Hide(); //Finally, Hide() command is called for when a menu item has been clicked
        }
    }
    //Getter-setter components designed to define what format the data is in
    public class Menu
    {
        public string Title { get; set; }
        public string Icon { get; set; }
    }
}
