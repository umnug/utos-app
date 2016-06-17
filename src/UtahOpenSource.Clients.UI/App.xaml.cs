﻿using System;
using System.Collections.Generic;
using FormsToolkit;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using UtahOpenSource.Clients.Portable;

namespace UtahOpenSource.Clients.UI
{
    public partial class App : Application
    {
        public static App current;
        public App()
        {
            current = this;
            InitializeComponent();
            ViewModelBase.Init();
            // The root page of your application
            switch (Device.OS)
            {
                case TargetPlatform.Android:
                    MainPage = new RootPageAndroid();
                    break;
                case TargetPlatform.iOS:
                    MainPage = new EvolveNavigationPage(new RootPageiOS());
                    break;
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    MainPage = new RootPageWindows();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        static ILogger logger;
        public static ILogger Logger => logger ?? (logger = DependencyService.Get<ILogger>());


        protected override void OnStart()
        {
           OnResume();
        }

        public void SecondOnResume()
        {
            OnResume();
        }

        bool registered;
        bool firstRun = true;
        protected override void OnResume()
        {
            if (registered)
                return;
            registered = true;
            // Handle when your app resumes
            Settings.Current.IsConnected = CrossConnectivity.Current.IsConnected;
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;

            // Handle when your app starts
            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

                    if(task == null)
                        return;  

                    await task;
                    info?.OnCompleted?.Invoke();
                });


            MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.Question, async (m, q) =>
                {
                    var task =  Application.Current?.MainPage?.DisplayAlert(q.Title, q.Question, q.Positive, q.Negative);
                    if(task == null)
                        return;
                    var result = await task;
                    q?.OnCompleted?.Invoke(result);
                });

            MessagingService.Current.Subscribe<MessagingServiceChoice>(MessageKeys.Choice, async (m, q) =>
                {
                    var task =  Application.Current?.MainPage?.DisplayActionSheet(q.Title, q.Cancel, q.Destruction, q.Items);
                    if(task == null)
                        return;
                    var result = await task;
                    q?.OnCompleted?.Invoke(result);
                });

            MessagingService.Current.Subscribe(MessageKeys.NavigateLogin, async m =>
                {
                    if(Device.OS == TargetPlatform.Android)
                    {
                        ((RootPageAndroid)MainPage).IsPresented = false;
                    }

                    Page page = null;
                    if(Settings.Current.FirstRun && Device.OS == TargetPlatform.Android)
                        page = new LoginPage();
                    else
                        page = new EvolveNavigationPage(new LoginPage());

                   
                    var nav = Application.Current?.MainPage?.Navigation;
                    if(nav == null)
                        return;
                   
                    await NavigationService.PushModalAsync(nav, page);

                });

            try
            {
                if (firstRun || Device.OS != TargetPlatform.iOS)
                    return;

                var mainNav = MainPage as NavigationPage;
                if (mainNav == null)
                    return;

                var rootPage = mainNav.CurrentPage as RootPageiOS;
                if (rootPage == null)
                    return;

                var rootNav = rootPage.CurrentPage as NavigationPage;
                if (rootNav == null)
                    return;
                

                var about = rootNav.CurrentPage as AboutPage;
                if (about != null)
                {
                    about.OnResume();
                    return;
                }
                var sessions = rootNav.CurrentPage as SessionsPage;
                if (sessions != null)
                {
                    sessions.OnResume();
                    return;
                }
                var feed = rootNav.CurrentPage as FeedPage;
                if (feed != null)
                {
                    feed.OnResume();
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                firstRun = false;
            }
        }

 

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            var data = uri.ToString().ToLowerInvariant();
            //only if deep linking
            if (!data.Contains("/session/"))
                return;

            var id = data.Substring(data.LastIndexOf("/", StringComparison.Ordinal) + 1);

            if (!string.IsNullOrWhiteSpace(id))
            {
                MessagingService.Current.SendMessage<DeepLinkPage>("DeepLinkPage", new DeepLinkPage
                {
                    Page = AppPage.Session,
                    Id = id
                });
            }

            base.OnAppLinkRequestReceived(uri);
        }


        protected override void OnSleep()
        {
            if (!registered)
                return;
            
            registered = false;
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateLogin);
            MessagingService.Current.Unsubscribe<MessagingServiceQuestion>(MessageKeys.Question);
            MessagingService.Current.Unsubscribe<MessagingServiceAlert>(MessageKeys.Message);
            MessagingService.Current.Unsubscribe<MessagingServiceChoice>(MessageKeys.Choice);

            // Handle when your app sleeps
            CrossConnectivity.Current.ConnectivityChanged -= ConnectivityChanged;
        }

        protected async void ConnectivityChanged (object sender, ConnectivityChangedEventArgs e)
        {
            //save current state and then set it
            var connected = Settings.Current.IsConnected;
            Settings.Current.IsConnected = e.IsConnected;
            if (connected && !e.IsConnected)
            {
                //we went offline, should alert the user and also update ui (done via settings)
                var task = Application.Current?.MainPage?.DisplayAlert("Offline", "Uh Oh, It looks like you have gone offline. Please check your internet connection to get the latest data and enable syncing data.", "OK");
                if (task != null)
                    await task;
            }
        }
            
    }
}

