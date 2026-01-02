using AplicacionConsola.Modelo;
using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsola.Controlador
{
    internal class RecoverObjetos
    {
        private static string _path = @"C:\Users\noliva\source\repos\NewRepo\Proyecto-Consola\Private\Objetos.json";

        public RecoverObjetos() { }

        //Recuperamos el json y lo guardamos en una variable
        public List<ObjetoEncantado> recuperarObjetos()
        {
            string objetosJson;
            using (var leer = new StreamReader(_path))
            {
                objetosJson = leer.ReadToEnd();
            }

            return parsearJson(objetosJson);
        }

        private static List<ObjetoEncantado> parsearJson(string json)
        {  
            //Convertimos la variable string en una lista de objetos definidos.
            List<ObjetoEncantado> Objetos = JsonConvert.DeserializeObject<List<ObjetoEncantado>>(json);

            return Objetos;
            

        }

        public bool guardar(List<ObjetoEncantado> objetos)
        {
            bool gaurdado = false;
            try
            {
                //Pasamos el json a texto plano
                string pasarString = JsonConvert.SerializeObject(objetos.ToArray(), Formatting.Indented);
                try
                {
                    //Creamos el archivo .json en el escritorio
                    File.WriteAllText(_path, pasarString);
                    gaurdado = true;
                }
                catch
                {
                    AnsiConsole.MarkupLine("[Reed] Ha ocurrido un error a la hora de guardar los cambios [/]");
                }

                
            }
            catch (Exception ex) {
                AnsiConsole.MarkupLine("[Reed] Ha ocurrido un error a la hora de transcribir los cambios [/]");
            }
            return gaurdado;
            
        }
    }
}
