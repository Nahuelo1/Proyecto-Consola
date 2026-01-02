using AplicacionConsola.Controlador;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsola.Views
{
    public class Menu 
    {
        public Menu() {
        }
        //Recuperamos la lista de objetos de json apenas empieza la app

        private ListaObjetos objetos = new ListaObjetos();  


        public void iniciarApp()
        {
            bool termianr = false;
            string eleccion;


            do {

                eleccion = AnsiConsole.Prompt(
                        new SelectionPrompt<String>()
                        .Title("[Green]Elija una opción[/]")
                        .AddChoices(
                            "Ver Objetos de la tienda",
                            "Agregar Objeto",
                            "Sacar Objeto",
                            "Editar Objeto",
                            "Guardar Cambios",
                            "Finalizar"
                        )
                    );

                switch (eleccion){
                    case "Ver Objetos de la tienda":
                        objetos.listarObjetos();
                        break;
                    case "": break;
                    case "Editar Objeto":
                        buscarObjeto();
                        break;
                    case "Guardar Cambios":
                        objetos.guardarObjetos(); 
                        break;
                    case "Finalizar": 
                        termianr = true; 
                        break;

                    default: 
                        AnsiConsole.Clear(); 
                        AnsiConsole.MarkupLine("[Red] Presione un valor correcto [/]");  
                        break;
                }
                AnsiConsole.Clear();
            } while ( !termianr );


        }

        public void buscarObjeto() {
            bool encontrado = false;
            bool seguirEditandoItem = true;
            string eleccion;

            string nombre;
            string rareza;
            int poder;
            decimal precio;

            objetos.listarOpciones();

            do{
            int idObjeto = AnsiConsole.Ask<int>("[Yellow]Porfavor, introduzca el ID del objeto que desea editar (o 0 para salir)[/]");
                
                if(idObjeto == 0)
                {
                    encontrado = true;
                }
                else
                {
                    encontrado = objetos.buscarObjeto(idObjeto);
                    if (!encontrado)
                    {
                        AnsiConsole.MarkupLine("[Red]Porfavor seleccione una opción correcta[/]");
                    }
                    else
                    {
                        do
                        {
                            eleccion = AnsiConsole.Prompt(
                                new SelectionPrompt<String>()
                                .Title("[Green]Elija el atributo a editar[/]")
                                .AddChoices(
                                    "Nombre",
                                    "Rareza",
                                    "Precio",
                                    "Poder",
                                    "Volver"
                                )
                            );

                            switch (eleccion)
                            {
                                case "Nombre":
                                    nombre = AnsiConsole.Ask<String>("[Yellow]Introduzca el nuevo nombre[/]");

                                    ; break;
                                case "Rareza":
                                    rareza = AnsiConsole.Prompt(
                                        new SelectionPrompt<String>()
                                        .Title("[Yellow]Elija la rareza[/]")
                                        .AddChoices(
                                            "Comun",
                                            "Poco Comun",
                                            "Raro",
                                            "Epico",
                                            "Legendario"
                                        )
                                    );
                                    break;
                                case "Precio":
                                    precio = AnsiConsole.Ask<decimal>("[Yellow]Coloque el precio[/]")
                                    ; break;
                                case "Poder":
                                    poder = AnsiConsole.Ask<int>("[Yellow]Coloque el poder[/]"); break;
                            }

                            seguirEditandoItem = AnsiConsole.Confirm("¿Desea seguir editando el item?");


                            //Guardamos las modificaciones
                            objetos.modificarObjeto();
                        } while (seguirEditandoItem);
                    }
                }
            } while (!encontrado);
            

        }
    }
}
