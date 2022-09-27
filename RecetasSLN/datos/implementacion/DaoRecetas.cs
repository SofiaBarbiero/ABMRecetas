using RecetasSLN.datos.interfaz;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasSLN.datos.implementacion

{
    internal class DaoRecetas : IDaoRecetas
    {
        public int ObtenerProximo()
        {
            string sp = "sp_ObtenerProximo";
            string nombrePOut = "@next";
            return Helper.ObtenerInstancia().ObtenerProximo(sp, nombrePOut);
        }

        public List<TipoReceta> ObtenerTipos()
        {
            List<TipoReceta> lst = new List<TipoReceta>();
            string sp = "sp_ConsultarTipos";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp, null);
            foreach (DataRow dr in table.Rows)
            {
                //Mapear un registro de la table a un objeto de dominio
                int id = int.Parse(dr["tipo_receta"].ToString()); //nombre de la col de sql
                string tipo = dr["tipoNombre"].ToString(); //nombre de la col de sql
                TipoReceta aux = new TipoReceta(id, tipo);
                lst.Add(aux);
            }
            return lst;
        }

        public List<Ingrediente> ObtenerIngredientes()
        {
            List<Ingrediente> lst = new List<Ingrediente>();
            string sp = "SP_CONSULTAR_INGREDIENTES";
            DataTable table = Helper.ObtenerInstancia().CargarCombo(sp, null);
            foreach (DataRow dr in table.Rows)
            {
                //Mapear un registro de la table a un objeto de dominio
                int id = int.Parse(dr["id_ingrediente"].ToString()); //nombre de la col de sql
                string ingrediente = dr["n_ingrediente"].ToString(); //nombre de la col de sql
                Ingrediente aux = new Ingrediente(id, ingrediente);
                lst.Add(aux);
            }
            return lst;
        }

    }
}
