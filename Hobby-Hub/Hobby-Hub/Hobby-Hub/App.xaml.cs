﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Hobby_Hub.Views;

namespace Hobby_Hub
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ArtPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
