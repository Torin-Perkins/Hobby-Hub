﻿using HobbyHub;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hobby_Hub
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignUp());
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
