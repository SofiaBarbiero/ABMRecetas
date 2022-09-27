using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.interfaz
{
    interface IDaoRecetas
    {
        int ObtenerProximo();
        List<TipoReceta> ObtenerTipos();
        List<Ingrediente> ObtenerIngredientes();
    }
}
