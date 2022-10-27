using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasBack.dominio
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; }

        public Ingrediente(int ingredienteId, string nombre)
        {
            IngredienteId = ingredienteId;
            Nombre = nombre;
        }

        public Ingrediente()
        {
            IngredienteId = 0;
            Nombre = string.Empty;
            Unidad = string.Empty;          
        }

        public override string ToString()
        {
            return "Ingrediente: " + Nombre; 
        }
    }
}
