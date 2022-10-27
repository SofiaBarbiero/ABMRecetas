using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasBack.dominio
{
    public class Receta
    {
        public int RecetaNro { get; set; }
        public string Nombre { get; set; } 
        public TipoReceta TipoReceta { get; set; }
        
        public string Cheff { get; set; }
        public List<DetalleReceta> Detalles { get; set; }

        public Receta(int recetaNro, string nombre, TipoReceta tipoReceta, string chef, List<DetalleReceta> detalles)
        {
            RecetaNro = recetaNro;
            Nombre = nombre;
            TipoReceta = tipoReceta;
            Cheff = chef;
            Detalles = detalles;
        }

        public Receta()
        {
            RecetaNro = 0;
            Nombre = string.Empty;
            TipoReceta = null;
            Cheff = string.Empty;
            Detalles = new List<DetalleReceta>();
        }

        public int CalcularIngredientes()
        {
            int total = 0;
            foreach(DetalleReceta item in Detalles)
            {
                if(item.Cantidad > 0)
                total += item.Cantidad;
            }
            return total;
        }

        public void AgregarDetalle(DetalleReceta nuevo)
        {
            Detalles.Add(nuevo);
        }
    }
}
