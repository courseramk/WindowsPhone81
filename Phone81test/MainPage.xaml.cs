using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Phone81test.Resources;
using CommonLib;

namespace Phone81test
{
    public partial class MainPage : PhoneApplicationPage
    {
        //************************************************************
        // API Software's standard:  Set the below tags to fit your needs
        //************************************************************

        // Your publisher name
        static String companyname = "MattKingIT.com";

        // How to search for your other apps in the store
        String searchLink = "http://www.windowsphone.com/en-us/search?q=" + Uri.EscapeDataString(companyname);

        // Subject line when sending email
        String messageTitle = "New App!";

        // Body of email or SMS-Text
        String leadinLine = "I found this great app for my phone.";

        // Buffer used to build store link to email or sms-text
        String appLink = "";  // Note if this is not built below, the "send" routines will provide the "search" link instead....

        // API Software's standard: HTML page for the WebControl
        private string MainUri = "/Content/Default.html";


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            appLink = string.Format("http://www.windowsphone.com/s?appid={0}", AppExtras.GetId());

        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.IsScriptEnabled = true;

            // Add your URL here

            Browser.Navigate(new Uri(MainUri, UriKind.Relative));
        }

        // Handle navigation failures.
        private void Browser_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e)
        {
            MessageBox.Show("Navigation to this page failed, check your internet connection");
        }

        // Look for "Eixt" as a prompt to exit...
        void Browser_ScriptNotify(object sender, NotifyEventArgs e)
        {
            // API Software's standard:  If the JavaScript returns the string "Exit", then do so!

            //  *TODO* Have the JavaScript call this to exit:
            //          window.external.notify("Exit");

            if (e.Value == "Exit")
            {
                MessageBoxResult mb = MessageBox.Show("Exit this app?", "Confirm", MessageBoxButton.OKCancel);

                if (mb == MessageBoxResult.OK)
                {
                    Application.Current.Terminate();
                }
            }
        }

        // This function relies on an "goBack" function inside the JavaScript...
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Set to TRUE to stop the app from exiting.
            try
            {
                if (Browser.CanGoBack)
                    Browser.GoBack();
                else
                {
                    // *TODO* - Place your JavaScript "goback" routine call here for Single-Page apps
                    // The code below looks safely for on a "goBack" method in the JavaScript, or else tells the app to exit.
                    Browser.InvokeScript("eval", "if(typeof(goBack) == 'function') goBack(); else if(typeof(window.external.notify) == 'function') window.external.notify('Exit');");

                    //  *TODO* If you do have a "goBack" function, have the "goBack()" JavaScript call this if it wants the app to exit:
                    //          window.external.notify("Exit");
                }
            }
            catch
            { };
        }

        private void btnShareMe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/SharePage.xaml", UriKind.Relative));
            AppExtras.ShareSocial(messageTitle, leadinLine, searchLink, appLink);
        }

        private String composeBody(bool longversion)
        {
            String body;

            body = leadinLine;

            if (appLink != "")
                body += "\n\nYou can get it here:\n" + appLink;

            // If we are sending email OR if we don't have an app Link
            if (searchLink != "" && (longversion == true || appLink == ""))
                body += "\n\nYou can find more just like it here:\n" + searchLink;

            return (body);
        }

        private void btnRateMe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppExtras.RateApp();
        }

        private void btnFindApps_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AppExtras.FindInStore(companyname);
        }

        private void btnShareViaSocial_Click(object sender, EventArgs e)
        {
            AppExtras.ShareSocial(messageTitle, leadinLine, searchLink, appLink);
        }

        private void btnShareViaSMS_Click(object sender, EventArgs e)
        {
            AppExtras.SendSMS("", composeBody(false));
        }

        private void btnShareViaEmail_Click(object sender, EventArgs e)
        {
            AppExtras.SendEmail("", messageTitle, composeBody(true));
        }

        private void iconEmail_Click(object sender, EventArgs e)
        {
            AppExtras.SendEmail("", messageTitle, composeBody(true));
        }

        private void iconShare_Click(object sender, EventArgs e)
        {
            AppExtras.ShareSocial(messageTitle, leadinLine, searchLink, appLink);
        }

        private void iconSMS_Click(object sender, EventArgs e)
        {
            AppExtras.SendSMS("", composeBody(false));
        }

        private void iconRate_Click(object sender, EventArgs e)
        {
            AppExtras.RateApp();
        }

        private void menuFind_Click(object sender, EventArgs e)
        {
            AppExtras.FindInStore(companyname);
        }

        private void menuShare_Click(object sender, EventArgs e)
        {
            AppExtras.ShareSocial(messageTitle, leadinLine, searchLink, appLink);
        }

        private void menuRate_Click(object sender, EventArgs e)
        {
            AppExtras.RateApp();
        }


        private void menuEmail_Click(object sender, EventArgs e)
        {
            AppExtras.SendEmail("", messageTitle, composeBody(true));
        }

        private void menuSMS_Click(object sender, EventArgs e)
        {
            AppExtras.SendSMS("", composeBody(false));
        }

    }
}