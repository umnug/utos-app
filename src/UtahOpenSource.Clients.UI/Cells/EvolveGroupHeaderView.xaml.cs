using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace UtahOpenSource.Clients.UI
{

    public class EvolveGroupHeader : ViewCell
    {
        public EvolveGroupHeader()
        {
            View = new EvolveGroupHeaderView();
        }
    }
    public partial class EvolveGroupHeaderView : ContentView
    {
        public EvolveGroupHeaderView()
        {
            InitializeComponent();
        }
    }
}

