using Windows.UI.Popups;
using Xamarin.Forms;
using UtahOpenSource.Clients.Portable;
using UtahOpenSource.UWP;

[assembly: Dependency(typeof(Toaster))]
namespace UtahOpenSource.UWP
{
    public class Toaster : IToast
    {
        public void SendToast(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = new MessageDialog(message);
                dialog.ShowAsync();
            });
        }
    }
}
