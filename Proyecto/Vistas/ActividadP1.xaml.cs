﻿using Proyecto.Actividades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadP1 : ContentPage
    {
        public ActividadP1()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            Shell.Current.GoToAsync("..");
            return base.OnBackButtonPressed();
        }
    }
}