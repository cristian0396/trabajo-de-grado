using Proyecto.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Actividades.Entidades
{
    public class TAhorro : BaseModel
    {
        private int id;

        private int mes;

        private float cuota;

        private float total;

        private string totalFormat;

        private string cuotaFormat;

        private DateTime fecha;

        public int Mes
        {
            get { return mes; }
            set
            {
                mes = value;
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
        public float Cuota
        {
            get { return cuota; }
            set
            {
                cuota = value;
                OnPropertyChanged();
            }
        }

        public float Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }
        public string TotalFormat
        {
            get { return totalFormat; }
            set
            {
                totalFormat = value;
                OnPropertyChanged();
            }
        }
        public string CuotaFormat
        {
            get { return cuotaFormat; }
            set
            {
                cuotaFormat = value;
                OnPropertyChanged();
            }
        }
    }
}
