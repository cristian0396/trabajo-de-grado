using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Configuracion
{
    public class EndPoints
    {
        //Here we put the enpoints created on NodeJS
        public static readonly string URL_SERVIDOR = "http://ec2-52-14-153-170.us-east-2.compute.amazonaws.com:3000";
        //users endpoints
        public static readonly string CONSULTAR_ALL_USERS = "/users/getUser";
        public static readonly string CONSULTAR_USER = "/users/login";
        public static readonly string CREAR_USER = "/users/create";
        public static readonly string ELIMINAR_USER = "/users/delete";
        public static readonly string MODIFICAR_USER = "/users/update";
        //detail endpoints
        public static readonly string CREAR_INFO_USER = "/detailUser/createDetail";
        public static readonly string CONSULTAR_DETALLE_USUARIO = "/detailUser/getUserDetail";
        public static readonly string ELIMINAR_DETALLE_USUARIO = "/detailUser/delete";
    }
}
