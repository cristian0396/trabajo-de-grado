using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto.Modelos.ModelosAux
{
    public class ApiResponse
    {
        #region Properties
        public int Code { get; set; }
        public string Response { get; set; }
        public bool IsSuccess { get; set; }
        #endregion Properties

        #region Initialize
        public ApiResponse() { }
        #endregion Initialize
    }
}
