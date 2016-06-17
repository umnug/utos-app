using Xamarin.Forms;
using UtahOpenSource.Clients.Portable;
using UtahOpenSource.iOS;
using ToastIOS;
using UIKit;
using CoreGraphics;

[assembly:Dependency(typeof(Toaster))]
namespace UtahOpenSource.iOS
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    Toast.MakeText(message, Toast.LENGTH_LONG).SetCornerRadius(0).Show();
                });
        }
    }
}
