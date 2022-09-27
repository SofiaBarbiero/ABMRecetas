using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.servicio.interfaz
{
    interface IServicio
    {
        int ObtenerProximo();
        List<TipoReceta> ObtenerTipos();
        List<Ingrediente> ObtenerIngredientes();
    }
}
