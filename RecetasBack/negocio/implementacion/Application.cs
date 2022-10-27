using RecetasBack.datos.implementacion;
using RecetasBack.datos.interfaz;
using RecetasBack.dominio;
using RecetasBack.negocio.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasBack.negocio.implementacion
{
    public class Application : IApplication
    {
        private IDaoRecetas dao;

        public Application()
        {
            dao = new DaoRecetas();
        }

        public int ObtenerProximo()
        {
            return dao.ObtenerProximo();
        }

        public List<TipoReceta> ObtenerTipos()
        {
            return dao.ObtenerTipos();
        }

        public List<Ingrediente> ObtenerIngredientes()
        {
            return dao.ObtenerIngredientes();
        }

        public bool Save(Receta oReceta)
        {
            return dao.Save(oReceta);
        }
    }
}
