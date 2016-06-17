using System;
using UtahOpenSource.Clients.Portable;
using Xamarin.Forms;
using UtahOpenSource.Droid;
using Android.App;
using Plugin.CurrentActivity;
using Android.Widget;

[assembly:Dependency(typeof(Toaster))]
namespace UtahOpenSource.Droid
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            var context = CrossCurrentActivity.Current.Activity ?? Android.App.Application.Context;  
            Device.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(context, message, ToastLength.Long).Show();
                });

        }
    }
}

