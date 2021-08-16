﻿using Proyecto.ViewModels;
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
    public partial class MenuHeader : ContentView
    {
        MenuHeaderViewModel context = new MenuHeaderViewModel();
        public MenuHeader()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}