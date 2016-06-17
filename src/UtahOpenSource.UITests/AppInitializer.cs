using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UtahOpenSource.UITests
{
    public class AppInitializer
    {
        const string apkfile = "../../../UtahOpenSource.Android/bin/UITest/com.sample.evolve-Signed.apk";
        //        const string appfile = "../../../UtahOpenSource.iOS/bin/iPhoneSimulator/Debug/UtahOpenSourceiOS.app";

        private static IApp app;

        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppInitializer.App' not set. Call 'AppInitializer.StartApp(platform)' before trying to access it.");
                return app;
            }
        }

        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                app = ConfigureApp.Android.ApkFile(apkfile)
                    .StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);
            }
            else
            {
                app = ConfigureApp.iOS.InstalledApp("com.sample.evolve")
                    .StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);
            }

            return app;
        }
    }
}

