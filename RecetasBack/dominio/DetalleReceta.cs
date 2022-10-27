﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetasBack.dominio
{
    public class DetalleReceta
    {
        public Ingrediente Ingrediente { get; set; }
        public int Cantidad { get; set; }

        public DetalleReceta(Ingrediente ingrediente, int cantidad)
        {
            Ingrediente = ingrediente;
            Cantidad = cantidad;
        }
        
        public DetalleReceta()
        {
            Ingrediente = null;
            Cantidad = 0;
        }
    }
}
