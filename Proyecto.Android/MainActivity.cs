﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Proyecto.Droid
{
    [Activity(Label = "Edufinanzas", Icon = "@drawable/iconoApp", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            //codigo PopUp
            Rg.Plugins.Popup.Popup.Init(this);
            //end
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //codigo creado para obtener el ancho, altura y densidad del dispositivo Android
            var metrics = Resources.DisplayMetrics;
            var width = metrics.WidthPixels / metrics.Density;
            var height = metrics.HeightPixels / metrics.Density;
            App.Width = (int)width;
            App.Height = (int)height;
            App.Density = (int)metrics.Density;
            //con esta linea se inicia la aplicación
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}