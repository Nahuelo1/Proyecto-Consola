using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsola.Modelo
{
    public class ObjetoEncantado
    {
        public int id { get; set; }
        public string name { get; set; }
        public int poder { get; set; }
        public string rareza { get; set; }
        public decimal precio  { get; set; }

        public int baja { get; set; }

        public ObjetoEncantado(int id, string nombre, int poder, string rareza, decimal precio) {
            this.id = id;
            this.name = nombre;
            this.poder = poder;
            this.rareza = rareza;
            this.precio = decimal.Round(precio,2);
            this.baja = 0;
        }


        public override string ToString() {
            return "Id " + id + "\nNombre " + name + "\nPoder " + poder + "\nRareza " + rareza + "\nPrecio " + precio ;
        }

    }
}
