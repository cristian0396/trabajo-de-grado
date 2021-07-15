using System;
using System.Collections.Generic;
using System.Text;
using Proyecto.Modelos.ModelosAux;
using Proyecto.Configuracion;
using System.Threading.Tasks;

namespace Proyecto.Servicios.ApiRest
{
    public class ChooseRequest<T>
    {
        #region Properties
        public Request<T> EstrategiaEnvio { get; set; }
        public ConfiguracionRest ConfigurationRest { get; set; }
        #endregion Properties

        #region Initialize
        public ChooseRequest()
        {
            ConfigurationRest = new ConfiguracionRest();
        }
        #endregion Initialize

        #region Métodos
        public void ElegirEstrategia(string verb, string url)
        {
            var diccionario = ConfigurationRest.VerbsConfiguration;
            string nombreClase;
            if (diccionario.TryGetValue(verb?.ToUpper(), out nombreClase))
            {
                Type tipoClase = Type.GetType(nombreClase);
                Type[] typeArgs = { typeof(T) };
                var genericClass = tipoClase?.MakeGenericType(typeArgs);
                if (genericClass != null)
                {
                    EstrategiaEnvio = (Request<T>)Activator.CreateInstance(genericClass, url, verb?.ToUpper());
                }                
            }
        }

        public async Task<ApiResponse> EjecutarEstrategia(T objecto, ParametersRequest parametersRequest = null)
        {
            parametersRequest = parametersRequest ?? new ParametersRequest();
            await EstrategiaEnvio?.ContruirURL(parametersRequest);
            var response = await EstrategiaEnvio?.SendRequest(objecto);
            return response;
        }
        #endregion Métodos
    }
}
