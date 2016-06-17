using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using UtahOpenSource.Clients.Portable;
using UtahOpenSource.DataObjects;

namespace UtahOpenSource.Clients.UI
{
    public partial class SponsorDetailsPage : ContentPage
    {
        SponsorDetailsViewModel ViewModel => vm ?? (vm = BindingContext as SponsorDetailsViewModel);
        SponsorDetailsViewModel vm;

        public SponsorDetailsPage()
        {
            InitializeComponent();
           
        }


        public Sponsor Sponsor
        {
            get { return ViewModel.Sponsor; }
            set { BindingContext = new SponsorDetailsViewModel(Navigation, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = null;
            var adjust = Device.OS != TargetPlatform.Android ? 1 : -ViewModel.FollowItems.Count + 1;
            ListViewFollow.HeightRequest = (ViewModel.FollowItems.Count * ListViewFollow.RowHeight) - adjust;
        }
    }
}

