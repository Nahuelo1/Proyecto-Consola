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
            bool terminar = false;
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
                    case "Agregar Objeto":
                        crearObjeto();
                        break;
                    case "Editar Objeto":
                        AnsiConsole.Clear();
                        buscarObjeto();
                        break;
                    case "Guardar Cambios":
                        AnsiConsole.Clear();
                        if (objetos.guardarObjetos())
                            AnsiConsole.Write(new Text("------------------------------------------------------\n¡Todos los cambios Fueron Guardados Correctamente! \n------------------------------------------------------", new Style(Spectre.Console.Color.Green)).Centered());
                        else AnsiConsole.MarkupLine("[Red] Hubo un error al guardar los cambios [/]");

                        break;
                    case "Finalizar": 
                        terminar = true; 
                        break;

                    default: 
                        AnsiConsole.Clear(); 
                        AnsiConsole.MarkupLine("[Red] Presione un valor correcto [/]");  
                        break;
                }
            } while ( !terminar );


        }

        public void buscarObjeto() {
            bool encontrado = false;
            bool seguirEditandoItem = true;
            string eleccion;


            string nombre = "";
            string rareza = "";
            int poder = 0;
            decimal precio = 0;

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
                            objetos.modificarObjeto(idObjeto,nombre,poder,rareza, precio);
                        } while (seguirEditandoItem);
                    }
                }
            } while (!encontrado);
            

        }

        public void crearObjeto()
        {
            string objetoCreado = "";
            bool seguirCreandoItem = true;


            string nombre = "";
            string rareza = "";
            int poder = 0;
            decimal precio = 0;


            
                
            do
            {
                var inputNombre = new TextPrompt<string>("Indique el [green]Nombre[/]?")
                                .Validate(input =>
                                {
                                    if (input.Equals(""))
                                        return ValidationResult.Error("[Red] Error! [/] Nombre Incorrecto");

                                    if (input.Length < 4)
                                    {
                                        return ValidationResult.Error("[Red] Error! [/] El nombre debe tener mas de 3 caracteres");
                                    }

                                    return ValidationResult.Success();
                                });
                
                nombre = AnsiConsole.Prompt(inputNombre);
                
                rareza = AnsiConsole.Prompt(
                    new SelectionPrompt<String>()
                    .Title("[Yellow]Elija la rareza[/]")
                    .AddChoices(
                        "Comun",
                        "Poco Comun",
                        "Raro",
                        "Epico",
                        "Legendario"
                    ));

                var inputPoder = new TextPrompt<int>("Indique el [green]Poder[/]?")
                                .Validate(inputPrecio =>
                                    inputPrecio > 0,
                                    "[Red] ¡Error! [/] El poder debe ser mayor a 0"
                                );
                poder = AnsiConsole.Prompt(inputPoder);


                var inputPrecio = new TextPrompt<decimal>("Indique el [green]Precio[/]?")
                                .Validate(inputPrecio =>
                                    inputPrecio > 0,
                                    "[Red] ¡Error! [/] El precio debe ser mayor a 0"
                                );

                precio = AnsiConsole.Prompt(inputPrecio);

                objetoCreado = objetos.crearObjeto(nombre, poder, rareza, precio);

                AnsiConsole.MarkupLine("[Green] Objeto creado correctamente [/] \n" + objetoCreado);


                seguirCreandoItem = AnsiConsole.Confirm("¿Desea seguir editando el item?");

            } while (seguirCreandoItem);
             


        }
    }
}
