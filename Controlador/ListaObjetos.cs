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
                tablaObjetos.AddRow(item.id.ToString(), item.name, item.poder.ToString(), item.rareza.ToString(), item.precio.ToString());
            }

            AnsiConsole.Write(tablaObjetos);
        }

        public void listarOpciones()
        {

            var tablaObjetos = new Table()
                       .Border(TableBorder.Rounded)
                       .BorderColor(Color.Blue)
                       .AddColumns("ID", "Nombre")
                       ;

            foreach (ObjetoEncantado item in listaObjetos)
            {
                tablaObjetos.AddRow(item.id.ToString(), item.name);
            }

            AnsiConsole.Write(tablaObjetos);
        }

        public bool buscarObjeto(int val)
        {
            if(diccionarioObjetos.TryGetValue(val, out ObjetoEncantado i)) {  
                AnsiConsole.WriteLine(i.ToString()); 
                return true; 
            } 
            return false;
        }

        public void modificarObjeto()
        {

        }

        public void guardarObjetos()
        {
            objetos.guardar(listaObjetos);
            //Recuperamos la nueva lista de objetos guardada
            listaObjetos = objetos.recuperarObjetos();
            diccionarioObjetos = listaObjetos.ToDictionary(x => x.id);
        }

        
        

        
    }
}
