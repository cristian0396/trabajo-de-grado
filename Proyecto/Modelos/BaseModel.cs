using Newtonsoft.Json;
using Proyecto.Propagacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Modelos
{
    public class BaseModel : NotificationObjectModel
    {
        #region Properties
        [JsonIgnore]
        public long ID { get; set; }
        #endregion Properties
    }
}
