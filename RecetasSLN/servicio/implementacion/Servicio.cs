using RecetasSLN.datos.implementacion;
using RecetasSLN.datos.interfaz;
using RecetasSLN.dominio;
using RecetasSLN.servicio.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.servicio.implementacion
{
    class Servicio : IServicio
    {
        private IDaoRecetas dao;

        public Servicio()
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

    }
}
