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
    public partial class PopUp
    {
        MessageViewModel context = new MessageViewModel();
        public PopUp()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}