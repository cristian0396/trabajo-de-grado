﻿using Proyecto.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
    public class TPresupuesto : BaseModel
    {
        private int id;

        private string descripcion;

        private string ingresos;

        private string gastos;

        private string total;

        private DateTime fecha;

        public string Descripcion
        {
            get{ return descripcion; }
            set
            {
                descripcion = value;
                OnPropertyChanged();
            }
        }
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set
            {
                fecha = value;
                OnPropertyChanged();
            }
        }
        public string Ingresos
        {
            get { return ingresos; }
            set
            {
                ingresos = value;
                OnPropertyChanged();
            }
        }

        public string Gastos
        {
            get { return gastos; }
            set
            {
                gastos = value;
                OnPropertyChanged();
            }
        }

        public string Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }
    }
}
