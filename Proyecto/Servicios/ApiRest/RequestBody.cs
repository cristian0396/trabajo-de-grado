using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Proyecto.Modelos.ModelosAux;
using Proyecto.Servicios.ApiRest;

namespace Proyecto.Servicios
{
    public class RequestBody<T> : Request<T>
    {
        public RequestBody(string url, string verb)
        {
            Url = url;
            Verb = verb;
        }

        public override async Task<ApiResponse> SendRequest(T objecto)
        {
            /*Verbos POST y PUT*/

            ApiResponse respuesta = new ApiResponse()
            {
                Code = 400,
                IsSuccess = false,
                Response = ""
            };

            string objetoJson = JsonConvert.SerializeObject(objecto);
            HttpContent content = new StringContent(objetoJson, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var verbHttp = (Verb == "POST") ? HttpMethod.Post : HttpMethod.Put;
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verbHttp, UrlParameters);
                    //requestMessage = ServicioHeaders.AgregarCabeceras(requestMessage);
                    requestMessage.Content = content;
                    client.Timeout = TimeSpan.FromSeconds(50);
                    HttpResponseMessage HttpResponse = client.SendAsync(requestMessage).Result;
                    respuesta.Code = Convert.ToInt32(HttpResponse.StatusCode);
                    respuesta.IsSuccess = HttpResponse.IsSuccessStatusCode;
                    respuesta.Response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return respuesta;
        }
    }
}
