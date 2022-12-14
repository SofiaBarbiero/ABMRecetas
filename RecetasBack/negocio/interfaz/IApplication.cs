using RecetasBack.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasBack.negocio.interfaz
{
    public interface IApplication
    {
        int ObtenerProximo();
        List<TipoReceta> ObtenerTipos();
        List<Ingrediente> ObtenerIngredientes();

        bool Save(Receta oReceta);   
    }
}
