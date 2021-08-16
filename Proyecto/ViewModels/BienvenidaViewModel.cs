using Proyecto.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto.ViewModels
{
    public class BienvenidaViewModel : ViewModelBase
    {
        public List<Respuesta> ListaPresupuesto { get; set; }
        public List<Respuesta> ListaAhorro { get; set; }
        public List<Respuesta> ListaGastos { get; set; }
        public Respuesta Presupuesto { get; set; }
        public Respuesta Ahorro { get; set; }
        public Respuesta Gastos { get; set; }
        public String Edad { get; set; }
        public String NombreUsuario { get; set; }
        public ICommand InfoCommand { get; set; }
        public BienvenidaViewModel()
        {
            InicializarListaDesplegables();
            InicializarComandos();
        }
        private void InicializarListaDesplegables()
        {
            ListaPresupuesto = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Presupuesto Si"},
                new Respuesta(){ ID = 2, Nombre = "Presupuesto No"}
            };
            ListaAhorro = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Ahorro Si"},
                new Respuesta(){ ID = 2, Nombre = "Ahorro No"}
            };
            ListaGastos = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Gastos Si"},
                new Respuesta(){ ID = 2, Nombre = "Gastos No"}
            };
        }
        public void InicializarComandos()
        {
            InfoCommand = new Command(async () => await GuardarInformacion(), () => true);
        }

        public async Task GuardarInformacion()
        {
            try
            {
                UserDetail detalleUsuario = new UserDetail()
                {
                    NombreCompleto = NombreUsuario,
                    Edad = Edad,
                };
                /*
                ApiResponse response = await CreateUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    ((MessageViewModel)PopUp.BindingContext).Titulo = "Bienvenido a Edufinanzas!";
                    ((MessageViewModel)PopUp.BindingContext).Message = "Cuenta creada satisfactoriamente.";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                    Application.Current.MainPage = new Login();
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                    ((MessageViewModel)PopUp.BindingContext).Message = "Prueba de nuevo, o intentalo más tarde.";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
                */
            }
            catch (Exception e)
            {/*
                ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                ((MessageViewModel)PopUp.BindingContext).Message = "Ups algo salio mal de nuestra parte, vuelve luego por favor";
                await PopupNavigation.Instance.PushAsync(PopUp);*/
            }
        }
    }    
    public class Respuesta
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
}
