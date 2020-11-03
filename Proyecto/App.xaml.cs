﻿using Proyecto.Servicios.Navegacion;
using Proyecto.Vistas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto
{
    public partial class App : Application
    {
        #region Properties
        static NavegationService navigationService;
        #endregion

        #region Getters & Setters
        public static NavegationService NavigationService
        {
            get
            {
                if (navigationService == null)
                {
                    navigationService = new NavegationService();
                }
                return navigationService;
            }
        }
        #endregion Getters & Setters
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new Aterrizaje());
            //MainPage = new MainPage();
            MainPage = new Aterrizaje();
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
