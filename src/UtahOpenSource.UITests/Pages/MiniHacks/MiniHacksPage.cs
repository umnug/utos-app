﻿using System;
using NUnit.Framework;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UtahOpenSource.UITests
{
    public class MiniHacksPage : BasePage
    {
        public MiniHacksPage()
            : base ("Mini-Hacks", "Mini-Hacks")
        {
        }

        public void SelectMiniHack(string title)
        {
            app.ScrollDownTo(title);
            app.Screenshot("Selecting Mini-Hack: " + title);
            app.Tap(title);
        }
    }
}

