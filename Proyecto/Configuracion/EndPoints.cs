using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Configuracion
{
    public class EndPoints
    {
        //Here we put the enpoints created on NodeJS
        public static readonly string URL_SERVIDOR = "http://192.168.1.56:3000";
        public static readonly string CONSULTAR_ALL_USERS = "/users/getUser";
        public static readonly string CONSULTAR_USER = "/users/login";
        public static readonly string CREAR_USER = "/users/create";
        public static readonly string CONSULTAR_ALL_PLATES = "/plates/getPlate";
        public static readonly string CONSULTAR_PLATE = "/plates/getPlate/";
        public static readonly string CREAR_PLATO = "/plates/create";
        //public static readonly string EDITAR_CATEGORIA = "/update";
        //public static readonly string ELIMINAR_CATEGORIA = "/delete";
    }
}
