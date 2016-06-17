using Xamarin.Forms;
using UtahOpenSource.DataStore.Abstractions;

using MvvmHelpers;
using Plugin.Share;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Share.Abstractions;
using System;

namespace UtahOpenSource.Clients.Portable
{
    public class ViewModelBase : BaseViewModel
    {
        protected INavigation Navigation { get; }

        public ViewModelBase(INavigation navigation = null)
        {
            Navigation = navigation;
        }

        public static void Init (bool mock = true)
        {

#if ENABLE_TEST_CLOUD && !DEBUG
                DependencyService.Register<ISessionStore, UtahOpenSource.DataStore.Mock.SessionStore>();
                DependencyService.Register<IFavoriteStore, UtahOpenSource.DataStore.Mock.FavoriteStore>();
                DependencyService.Register<IFeedbackStore, UtahOpenSource.DataStore.Mock.FeedbackStore>();
                DependencyService.Register<ISpeakerStore, UtahOpenSource.DataStore.Mock.SpeakerStore>();
                DependencyService.Register<ISponsorStore, UtahOpenSource.DataStore.Mock.SponsorStore>();
                DependencyService.Register<ICategoryStore, UtahOpenSource.DataStore.Mock.CategoryStore>();
                DependencyService.Register<IEventStore, UtahOpenSource.DataStore.Mock.EventStore>();
                DependencyService.Register<INotificationStore, UtahOpenSource.DataStore.Mock.NotificationStore>();
                DependencyService.Register<IMiniHacksStore, UtahOpenSource.DataStore.Mock.MiniHacksStore>();
                DependencyService.Register<ISSOClient, UtahOpenSource.Clients.Portable.Auth.XamarinSSOClient>();
                DependencyService.Register<IStoreManager, UtahOpenSource.DataStore.Mock.StoreManager>();
#else
            if (mock) 
            {
                DependencyService.Register<ISessionStore, UtahOpenSource.DataStore.Mock.SessionStore> ();
                DependencyService.Register<IFavoriteStore, UtahOpenSource.DataStore.Mock.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, UtahOpenSource.DataStore.Mock.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, UtahOpenSource.DataStore.Mock.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, UtahOpenSource.DataStore.Mock.SponsorStore> ();
                DependencyService.Register<ICategoryStore, UtahOpenSource.DataStore.Mock.CategoryStore> ();
                DependencyService.Register<IEventStore, UtahOpenSource.DataStore.Mock.EventStore> ();
                DependencyService.Register<INotificationStore, UtahOpenSource.DataStore.Mock.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, UtahOpenSource.DataStore.Mock.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, UtahOpenSource.Clients.Portable.Auth.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, UtahOpenSource.DataStore.Mock.StoreManager> ();
            } 
            else 
            {
                DependencyService.Register<ISessionStore, UtahOpenSource.DataStore.Azure.SessionStore> ();
                DependencyService.Register<IFavoriteStore, UtahOpenSource.DataStore.Azure.FavoriteStore> ();
                DependencyService.Register<IFeedbackStore, UtahOpenSource.DataStore.Azure.FeedbackStore> ();
                DependencyService.Register<ISpeakerStore, UtahOpenSource.DataStore.Azure.SpeakerStore> ();
                DependencyService.Register<ISponsorStore, UtahOpenSource.DataStore.Azure.SponsorStore> ();
                DependencyService.Register<ICategoryStore, UtahOpenSource.DataStore.Azure.CategoryStore> ();
                DependencyService.Register<IEventStore, UtahOpenSource.DataStore.Azure.EventStore> ();
                DependencyService.Register<INotificationStore, UtahOpenSource.DataStore.Azure.NotificationStore> ();
                DependencyService.Register<IMiniHacksStore, UtahOpenSource.DataStore.Azure.MiniHacksStore> ();
                DependencyService.Register<ISSOClient, UtahOpenSource.Clients.Portable.Auth.Azure.XamarinSSOClient> ();
                DependencyService.Register<IStoreManager, UtahOpenSource.DataStore.Azure.StoreManager> ();
            }


            #endif


            DependencyService.Register<FavoriteService>();
        }


        protected ILogger Logger { get; } = DependencyService.Get<ILogger>();
        protected IStoreManager StoreManager { get; }  = DependencyService.Get<IStoreManager>();
        protected IToast Toast { get; }  = DependencyService.Get<IToast>();

        protected FavoriteService FavoriteService { get; } = DependencyService.Get<FavoriteService>();


        public Settings Settings
        {
            get { return Settings.Current; }
        }

        ICommand  launchBrowserCommand;
        public ICommand LaunchBrowserCommand =>
        launchBrowserCommand ?? (launchBrowserCommand = new Command<string>(async (t) => await ExecuteLaunchBrowserAsync(t))); 

        async Task ExecuteLaunchBrowserAsync(string arg)
        {
            if(IsBusy)
                return;

            if (!arg.StartsWith ("http://", StringComparison.OrdinalIgnoreCase) && !arg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                arg = "http://" + arg;
            
            Logger.Track(EvolveLoggerKeys.LaunchedBrowser, "Url", arg);

            var lower = arg.ToLowerInvariant();
            if(Device.OS == TargetPlatform.iOS && lower.Contains("twitter.com"))
            {
                try
                {
                    var id = arg.Substring(lower.LastIndexOf("/", StringComparison.Ordinal) + 1);
                    var launchTwitter = DependencyService.Get<ILaunchTwitter>();
                    if(lower.Contains("/status/"))
                    {
                        //status
                        if(launchTwitter.OpenStatus(id))
                            return;
                    }
                    else
                    {
                        //user
                        if(launchTwitter.OpenUserName(id))
                            return;
                    }
                }
                catch
                {
                }
            }

            try 
            {
                await CrossShare.Current.OpenBrowser (arg, new BrowserOptions {
                    ChromeShowTitle = true,
                    ChromeToolbarColor = new ShareColor {
                        A = 255,
                        R = 118,
                        G = 53,
                        B = 235
                    },
                    UseSafairReaderMode = true,
                    UseSafariWebViewController = true
                });
            } 
            catch 
            {
            }
        }

       

    }
}


