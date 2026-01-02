using AplicacionConsola.Modelo;
using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AplicacionConsola.Controlador
{
    public class ListaObjetos
    {
        public  List<ObjetoEncantado> listaObjetos;
        Dictionary<int, ObjetoEncantado> diccionarioObjetos;
        private RecoverObjetos objetos = new RecoverObjetos();
        public ListaObjetos() {

            listaObjetos = objetos.recuperarObjetos();
            diccionarioObjetos = listaObjetos.ToDictionary(x => x.id);
        }


        public void listarObjetos()
        {

            var tablaObjetos = new Table()
                       .Border(TableBorder.Rounded)
                       .BorderColor(Color.Blue)
                       .AddColumns("ID", "Nombre", "Poder", "Rareza", "Precio")
                       ;

            foreach (ObjetoEncantado item in listaObjetos)
            {
                if(item.baja == 0)
                {
                    tablaObjetos.AddRow(item.id.ToString(), item.name, item.poder.ToString(), item.rareza.ToString(), item.precio.ToString());
                }
            }

            AnsiConsole.Write(tablaObjetos);
        }

        public Table listarOpciones()
        {

            var tablaObjetos = new Table()
                       .Border(TableBorder.Rounded)
                       .BorderColor(Color.Blue)
                       .AddColumns("ID", "Nombre")
                       ;

            foreach (ObjetoEncantado item in listaObjetos)
            {
                if(item.baja == 0)
                tablaObjetos.AddRow(item.id.ToString(), item.name);
            }

            return tablaObjetos;
        }

        public bool buscarObjeto(int val)
        {
            if(diccionarioObjetos.TryGetValue(val, out ObjetoEncantado i)) {  
                if(i.baja == 0) {
                    AnsiConsole.WriteLine(i.ToString()); 
                    return true;
                }
            } 
            return false;
        }

        public void modificarObjeto(int id, string n, int p, string r, decimal pre)
        {
            bool hayCambio = false;
            if (diccionarioObjetos.TryGetValue(id, out ObjetoEncantado i))
            {
               
                if (n != "") { i.name = n; hayCambio = true; }

                if (p > 0 ) { i.poder = p; hayCambio = true; }

                if (r != "") { i.rareza = r; hayCambio = true; }

                if (pre > 0) { i.precio = pre; hayCambio = true; }

                if (hayCambio)
                {
                    //Si hubo cambio modifico el objeto en diccionario y lista
                    diccionarioObjetos[id] = i; 

                    int index = listaObjetos.IndexOf(i);
                    if(index != -1)
                    {
                        listaObjetos[index] = i;
                    }


                }
                else
                {
                    AnsiConsole.MarkupLine("[Red] No hubo cambios registrados [/]");
                }
            }


        }

        public bool guardarObjetos()
        {
            bool guardado = false;
            //Mandamos la lista para actualizar la bd
            guardado = objetos.guardar(listaObjetos);

            //Recuperamos la nueva lista de objetos guardada
            listaObjetos = objetos.recuperarObjetos();
            diccionarioObjetos = listaObjetos.ToDictionary(x => x.id);
            return guardado;
        }


        public string crearObjeto(string n, int p, string r, decimal pre)
        {
            ObjetoEncantado obj = new ObjetoEncantado(listaObjetos.Count()+1,n,p,r,pre);
            listaObjetos.Add(obj);
            return obj.ToString();
        }


        public int totalItems()
        {
            int i = 0; 
            foreach(ObjetoEncantado obj in listaObjetos)
            {
                if(obj.baja == 0)
                {
                    i++;
                }
            }  
            return i;
        }
        
        public bool buscarAndEliminar(int id)
        {
            if(diccionarioObjetos.TryGetValue(id, out ObjetoEncantado item)){
                if(item.baja == 0)
                {
                    item.baja = 1;
                    diccionarioObjetos[id] = item;

                    int index = listaObjetos.IndexOf(item);
                    if (index != -1)
                    {
                        listaObjetos[index] = item;
                    }

                    return true;
                }
            }
            return false;
        }
        
    }
}
