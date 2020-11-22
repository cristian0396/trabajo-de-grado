using Proyecto.Modelos;
using Proyecto.Vistas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PropertyChanged;

namespace Proyecto.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class IntroPresupuestoViewModel 
    {
        public ObservableCollection<IntroduccionModel> Introduccion { get; set; }

        public int PosicionIndice { get; set; }
        public string TextoBotonSiguiente
        {
            get
            {
                if (PosicionIndice ==  Introduccion.Count - 1)
                    return "HECHO";
                return "SIGUIENTE";
            }
        }
        public ICommand ComandoSiguiente { get; set; }
        public ICommand ComandoOmitir { get; set; }
        public IntroPresupuestoViewModel()
        {
            Introduccion = new ObservableCollection<IntroduccionModel>
            {
                new IntroduccionModel()
                {
                    DirImagen = "presupuesto.png",
                    Titulo = "Modulo de Presupuesto",
                    Contenido = "Esta es la corta definición de presupuesto",
                    CarouselItem = new RecorridoItemPage()
                },
                new IntroduccionModel()
                {
                    DirImagen = "diccionario.png",
                    Titulo = "Importancia",
                    Contenido = "Esta es la corta descripcion de la importancia del presupuesto",
                    CarouselItem = new RecorridoItemPage()
                },
                new IntroduccionModel()
                {
                    DirImagen = "edificio2.png",
                    Titulo = "Ejemplos",
                    Contenido = "Aqui hay un ejemplo",
                    CarouselItem = new RecorridoItemPage()
                }
            };
            
            foreach(var intro in Introduccion)
            {
                intro.CarouselItem.BindingContext = intro;

            }

            ComandoSiguiente = new Command(Siguiente);
            ComandoOmitir = new Command(Omitir);
        }
        
        private void Siguiente(object obj)
        {
            if (PosicionIndice < Introduccion.Count - 1)
            {
                PosicionIndice++;
            }
            else
            {
                Omitir();
            }
        }

        private void Omitir(){
            Application.Current.MainPage = new Presupuesto();
        }
    }
}
