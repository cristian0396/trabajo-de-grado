using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public ChooseRequest<UserDetail> GetUserDetail { get; set; }
        public ICommand btnIngresar { get; set; }
        public ICommand RegistrarCommand { get; set; }
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
            string urlBuscarDetalleUsuario = EndPoints.URL_SERVIDOR + EndPoints.CONSULTAR_DETALLE_USUARIO;

            GetUser = new ChooseRequest<User>();
            GetUser.ElegirEstrategia("POST", urlBuscarUsuario);

            GetUserDetail = new ChooseRequest<UserDetail>();
            GetUserDetail.ElegirEstrategia("GET", urlBuscarDetalleUsuario);
        }
        public void InicializarComandos()
        {
            btnIngresar = new Command(async () => await ConsultarUsuario(), () => true);
            RegistrarCommand = new Command(IrAlRegistro);
        }
        public void InitializeFields()
        {
            User = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            User.Validations.Add(new RequiredRule<string> { ValidationMessage = "El correo es Obligatorio" });
            Password.Validations.Add(new RequiredRule<string> { ValidationMessage = "La contraseña es Obligatoria" });
        }
        public void IrAlRegistro()
        {
            Application.Current.MainPage = new Registro();
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
                UserDetail detail = new UserDetail();
                ApiResponse response = await GetUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    JObject jsonObject = JObject.Parse(response.Response);
                    App.CurrentUser = new User()
                    {
                        Id = (long)jsonObject["id"],
                        Usuario = (string)jsonObject["usuario"],
                        Correo = (string)jsonObject["correo"],
                        Password = (string)jsonObject["contrasena"]
                    };
                    var parametros = new ParametersRequest()
                    {
                        Parameters = new List<string>() { (string)jsonObject["id"] }
                    };
                    ApiResponse responseUserDetail = await GetUserDetail.EjecutarEstrategia(detail, parametros);
                    if( responseUserDetail.Response == "[]")
                    {
                        Application.Current.MainPage = new Bienvenida();
                    } else {
                        Application.Current.MainPage = new MainPage();
                    }
                    
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Titulo = "Algo ocurrio...";
                    ((MessageViewModel)PopUp.BindingContext).Message = "Revisa si el usuario y la contraseña son los correctos.";
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
