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
    public class RegistroViewModel : ViewModelBase
    {
        public ValidatableObject<string> User { get; set; }
        public ValidatableObject<string> Password { get; set; }
        public ValidatableObject<string> Email { get; set; }
        public ChooseRequest<User> CreateUser { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public PopUp PopUp { get; set; }
        public RegistroViewModel()
        {
            PopUp = new PopUp();
            InitializeRequest();
            InicializarComandos();
            InitializeFields();
        }
        public void InitializeRequest()
        {
            string crearUsuarioEndpoint = EndPoints.URL_SERVIDOR + EndPoints.CREAR_USER;

            CreateUser = new ChooseRequest<User>();
            CreateUser.ElegirEstrategia("POST", crearUsuarioEndpoint);
        }
        public void InicializarComandos()
        {
            LoginCommand = new Command(IrAlLogin);
            RegistrarCommand = new Command(async () => await CrearUsuario(), () => true);
        }
        public void InitializeFields()
        {
            User = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();

            User.Validations.Add(new RequiredRule<string> { ValidationMessage = "El correo es Obligatorio" });
            Password.Validations.Add(new RequiredRule<string> { ValidationMessage = "La contraseña es Obligatoria" });
            Email.Validations.Add(new RequiredRule<string> { ValidationMessage = "El correo es Obligatorio" });
        }
        public void IrAlLogin()
        {
            Application.Current.MainPage = new Login();
        }
        public async Task CrearUsuario()
        {
            try
            {
                User usuario = new User()
                {
                    Correo = Email.Value,
                    Usuario = User.Value,
                    Password = Password.Value
                };
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
            }
            catch (Exception e)
            {
                ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                ((MessageViewModel)PopUp.BindingContext).Message = "Ups algo salio mal de nuestra parte, vuelve luego por favor";
                await PopupNavigation.Instance.PushAsync(PopUp);
            }
        }
    }
}
