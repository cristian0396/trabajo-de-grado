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
        public ChooseRequest<User> UpdateUser { get; set; }
        public ChooseRequest<User> DeleteUser { get; set; }
        public ChooseRequest<UserDetail> DeleteDetailUser { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
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
            string eliminarUsuarioEndpoint = EndPoints.URL_SERVIDOR + EndPoints.ELIMINAR_USER;
            string eliminarDetalleUsuarioEndpoint = EndPoints.URL_SERVIDOR + EndPoints.ELIMINAR_DETALLE_USUARIO;

            UpdateUser = new ChooseRequest<User>();
            UpdateUser.ElegirEstrategia("PUT", modificarUsuarioEndpoint);

            DeleteUser = new ChooseRequest<User>();
            DeleteUser.ElegirEstrategia("DELETE", eliminarUsuarioEndpoint);

            DeleteDetailUser = new ChooseRequest<UserDetail>();
            DeleteDetailUser.ElegirEstrategia("DELETE", eliminarDetalleUsuarioEndpoint);
        }
        public void InicializarComandos()
        {
            UpdateCommand = new Command(async () => await ModificarUsuario(), () => true);
            DeleteCommand = new Command(async () => await EliminarUsuario(), () => true);
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
        }
        public async Task ModificarUsuario()
        {
            try
            {
                User usuario = new User()
                {
                    Id = App.CurrentUser.Id,
                    Correo = Email.Value,
                    Usuario = User.Value,
                    Password = Password.Value
                };
                ApiResponse response = await UpdateUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    ((MessageViewModel)PopUp.BindingContext).Titulo = "Información importante!";
                    ((MessageViewModel)PopUp.BindingContext).Message = "Cuenta modificada satisfactoriamente.";
                    await PopupNavigation.Instance.PushAsync(PopUp);
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
        public async Task EliminarUsuario()
        {
            try
            {
                var parametros = new ParametersRequest()
                {
                    Parameters = new List<string>() { App.CurrentUser.Id.ToString() }
                };
                UserDetail detail = new UserDetail();
                User usuario = new User();
                ApiResponse responseDetail = await DeleteDetailUser.EjecutarEstrategia(detail, parametros);
                if (responseDetail.IsSuccess)
                {
                    ApiResponse response = await DeleteUser.EjecutarEstrategia(usuario, parametros);
                    if (response.IsSuccess)
                    {
                        Application.Current.MainPage = new Login();
                        ((MessageViewModel)PopUp.BindingContext).Titulo = "Información importante!";
                        ((MessageViewModel)PopUp.BindingContext).Message = "Cuenta eliminada satisfactoriamente.";
                        await PopupNavigation.Instance.PushAsync(PopUp);
                    }                        
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
