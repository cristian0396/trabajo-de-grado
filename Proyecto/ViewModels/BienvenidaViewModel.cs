using Proyecto.Configuracion;
using Proyecto.Modelos;
using Proyecto.Modelos.ModelosAux;
using Proyecto.Servicios.ApiRest;
using Proyecto.Vistas;
using Rg.Plugins.Popup.Services;
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
        public PopUp PopUp { get; set; }
        public ChooseRequest<UserDetail> CreateUserDetail { get; set; }
        public BienvenidaViewModel()
        {
            PopUp = new PopUp();
            InitializeRequest();
            InicializarListaDesplegables();
            InicializarComandos();
        }
        private void InicializarListaDesplegables()
        {
            ListaPresupuesto = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Presupuesto Si", Presupuesto = true},
                new Respuesta(){ ID = 2, Nombre = "Presupuesto No", Presupuesto = false}
            };
            ListaAhorro = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Ahorro Si", Ahorro = true},
                new Respuesta(){ ID = 2, Nombre = "Ahorro No", Ahorro = false}
            };
            ListaGastos = new List<Respuesta>()
            {
                new Respuesta(){ ID = 1, Nombre = "Gastos Si", Gastos = true},
                new Respuesta(){ ID = 2, Nombre = "Gastos No", Gastos = false}
            };
        }
        public void InicializarComandos()
        {
            InfoCommand = new Command(async () => await GuardarInformacion(), () => true);
        }
        public void InitializeRequest()
        {
            string urlBuscarUsuario = EndPoints.URL_SERVIDOR + EndPoints.CREAR_INFO_USER;

            CreateUserDetail = new ChooseRequest<UserDetail>();
            CreateUserDetail.ElegirEstrategia("POST", urlBuscarUsuario);
        }

        public async Task GuardarInformacion()
        {
            try
            {
                List<String> knowledge = new List<string>();
                if (Presupuesto.Presupuesto)
                {
                    knowledge.Add("Presupuesto");
                }
                if (Gastos.Gastos)
                {
                    knowledge.Add("Gastos");
                }
                if (Ahorro.Ahorro)
                {
                    knowledge.Add("Ahorro");
                }
                UserDetail detalleUsuario = new UserDetail()
                {
                    NombreCompleto = NombreUsuario,
                    Edad = Edad,
                    Conocimientos = knowledge,
                    IdUsuario = App.CurrentUser.Id,
                };
                ApiResponse response = await CreateUserDetail.EjecutarEstrategia(detalleUsuario);
                if (response.IsSuccess)
                {                    
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                    ((MessageViewModel)PopUp.BindingContext).Message = "Prueba de nuevo, o intentalo más tarde.";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {
                ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                ((MessageViewModel)PopUp.BindingContext).Message = "Ups algo salio mal de nuestra parte, vuelve luego por favor";
                await PopupNavigation.Instance.PushAsync(PopUp);
            }
        }
    }    
    public class Respuesta
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public bool Presupuesto { get; set; }
        public bool Gastos { get; set; }
        public bool Ahorro { get; set; }
    }
}
