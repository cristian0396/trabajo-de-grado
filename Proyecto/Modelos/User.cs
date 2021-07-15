using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Modelos
{
    public class User : BaseModel
    {
        #region properties
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("usuario")]
        private string usuario;

        [JsonProperty("contrasena")]
        public string Password { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }

        #endregion properties

        #region Initialize
        public User() { }
        #endregion Initialize

        #region Getters & Setters
        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters
    }
}
