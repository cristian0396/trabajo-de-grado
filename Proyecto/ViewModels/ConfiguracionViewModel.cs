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
    public class ConfiguracionViewModel : ViewModelBase
    {
        public ValidatableObject<string> User { get; set; }
        public ValidatableObject<string> Password { get; set; }
        public ValidatableObject<string> Email { get; set; }
        public ChooseRequest<User> CreateUser { get; set; }
        public ICommand UpdateCommand { get; set; }
        public PopUp PopUp { get; set; }
        public ConfiguracionViewModel()
        {
            PopUp = new PopUp();
            InitializeRequest();
            InicializarComandos();
            InitializeFields();
        }
        public void InitializeRequest()
        {
            string modificarUsuarioEndpoint = EndPoints.URL_SERVIDOR + EndPoints.MODIFICAR_USER;

            CreateUser = new ChooseRequest<User>();
            CreateUser.ElegirEstrategia("PUT", modificarUsuarioEndpoint);
        }
        public void InicializarComandos()
        {
            UpdateCommand = new Command(async () => await ModificarUsuario(), () => true);
        }
        public void InitializeFields()
        {
            User = new ValidatableObject<string>() {
                Value = App.CurrentUser.Usuario
            };
            Password = new ValidatableObject<string>() {
                Value = App.CurrentUser.Password
            };
            Email = new ValidatableObject<string>() {
                Value = App.CurrentUser.Correo
            };
            Console.WriteLine(App.CurrentUser.Usuario + " " + App.CurrentUser.Password);
            Console.WriteLine(User.Value + " " + Password.Value);
        }
        public async Task ModificarUsuario()
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
