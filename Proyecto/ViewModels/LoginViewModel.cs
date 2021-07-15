using Proyecto.Configuracion;
using Proyecto.Modelos;
using Proyecto.Modelos.ModelosAux;
using Proyecto.Servicios.ApiRest;
using Proyecto.Validations.Base;
using Proyecto.Validations.Rules;
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
    public class LoginViewModel : ViewModelBase
    {
        public ValidatableObject<string> User { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> Password { get; set; }
        public PopUp PopUp { get; set; }
        public ChooseRequest<User> GetUser { get; set; }
        public ICommand btnIngresar { get; set; }
        public LoginViewModel()
        {
            PopUp = new PopUp();
            InitializeRequest();
            InicializarComandos();            
            InitializeFields();
        }
        public void InitializeRequest()
        {
            string urlBuscarUsuario = EndPoints.URL_SERVIDOR + EndPoints.CONSULTAR_USER;

            GetUser = new ChooseRequest<User>();
            GetUser.ElegirEstrategia("POST", urlBuscarUsuario);
        }
        public void InicializarComandos()
        {
            btnIngresar = new Command(async () => await ConsultarUsuario(), () => true);
        }        
        public void InitializeFields()
        {
            User = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            User.Validations.Add(new RequiredRule<string> { ValidationMessage = "El correo es Obligatorio" });
            Password.Validations.Add(new RequiredRule<string> { ValidationMessage = "La contraseña es Obligatoria" });
        }

        public async Task ConsultarUsuario()
        {
            try
            {
                User usuario = new User()
                {
                    Usuario = User.Value,
                    Password = Password.Value

                };
                ApiResponse response = await GetUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    //await NavigationService.PushPage(new Inicio());
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    //((MessageViewModel)PopUp.BindingContext).Message = "Error al acceder";
                    //await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
