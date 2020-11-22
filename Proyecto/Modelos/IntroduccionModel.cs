using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyecto.Modelos
{
    public class IntroduccionModel : BaseModel
    {
        public string DirImagen { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public View CarouselItem { get; set; }

    }
}
