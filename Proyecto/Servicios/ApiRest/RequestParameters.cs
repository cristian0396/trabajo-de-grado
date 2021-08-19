﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using Proyecto.Modelos.ModelosAux;
using Proyecto.Servicios.ApiRest;

namespace Proyecto.Servicios
{
    public class RequestParameters<T> : Request<T>
    {
        /*Verbos GET y DEL*/

        public RequestParameters(string url, string verb)
        {
            Url = url;
            Verb = verb;
        }

        #region methods
        public override async Task<ApiResponse> SendRequest(T objecto)
        {
            ApiResponse respuesta = new ApiResponse()
            {
                Code = 400,
                IsSuccess = false,
                Response = ""
            };

            try
            {
                using (var client = new HttpClient())
                {
                    var verbHttp = (Verb == "GET") ? HttpMethod.Get : HttpMethod.Delete;
                    client.Timeout = TimeSpan.FromSeconds(50);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verbHttp, UrlParameters);
                    requestMessage = HeaderService.AddHeaders(requestMessage);
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
        #endregion methods
    }
}
