using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Modelos
{
    public class UserDetail : BaseModel
    {
        #region properties
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string NombreCompleto { get; set; }

        [JsonProperty("edad")]
        public string Edad { get; set; }

        [JsonProperty("idUsuario")]
        public long IdUsuario { get; set; }

        [JsonProperty("conocimientos")]
        public List<String> Conocimientos { get; set; }

        #endregion properties

        public UserDetail() { }
    }
}
