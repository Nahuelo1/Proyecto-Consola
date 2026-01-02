using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsola.Controlador
{
    internal class ModificarObjeto
    {
        public ModificarObjeto() { 
        }

        public bool validarPoder(int poder)
        {
            if(poder > 0  && poder < 50000) return true;

            return false;
        }


    }
}
