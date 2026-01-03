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
                        AnsiConsole.Progress().Start(fun =>
                        {
                            var tarea = fun.AddTask("[Yellow]Cargando Objetos[/]");

                            while (!fun.IsFinished)
                            {
                                tarea.Increment(3);
                                Thread.Sleep(10);
                            }
                        });
                        objetos.listarObjetos();
                        AnsiConsole.MarkupLine("[Green]¡Objetos cargados correctamente![/]");
                        break;
                    case "Agregar Objeto":

                        AnsiConsole.Clear();
                        crearObjeto();
                        break;
                    case "Sacar Objeto":

                        AnsiConsole.Clear();
                        sacarObjeto();

                        ; break;
                    case "Editar Objeto":
                        AnsiConsole.Clear();
                        modificarObjeto();
                        break;
                    case "Guardar Cambios":
                        AnsiConsole.Progress().Start(fun =>
                        {
                            var tarea = fun.AddTask("[Yellow]Guarndo cambios... Por favor espere[/]");

                            while (!fun.IsFinished)
                            {
                                tarea.Increment(1.5);
                                Thread.Sleep(20);
                            }
                        });
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

        public void modificarObjeto() {
            bool encontrado = false;
            bool seguirEditandoItem = true;
            string eleccion;


            string nombre = "";
            string rareza = "";
            int poder = 0;
            decimal precio = 0;

            var tabla = objetos.listarOpciones();
            AnsiConsole.Write(tabla);
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
                var inputNombre = new TextPrompt<string>("Indique el [green]Nombre[/]")
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

                var inputPoder = new TextPrompt<int>("Indique el [green]Poder[/]")
                                .Validate(inputPrecio =>
                                    inputPrecio > 0,
                                    "[Red] ¡Error! [/] El poder debe ser mayor a 0"
                                );
                poder = AnsiConsole.Prompt(inputPoder);


                var inputPrecio = new TextPrompt<decimal>("Indique el [green]Precio[/]")
                                .Validate(inputPrecio =>
                                    inputPrecio > 0,
                                    "[Red] ¡Error! [/] El precio debe ser mayor a 0"
                                );

                precio = AnsiConsole.Prompt(inputPrecio);

                objetoCreado = objetos.crearObjeto(nombre, poder, rareza, precio);

                AnsiConsole.MarkupLine("[Green] Objeto creado correctamente [/] \n" + objetoCreado);


                seguirCreandoItem = AnsiConsole.Confirm("¿Desea agregar otro objeto?");

            } while (seguirCreandoItem);
             


        }
    
        public void sacarObjeto()
        {
            var tabla = objetos.listarOpciones();
            int totalItems = objetos.totalItems();
            AnsiConsole.Write(tabla);
            var input = new TextPrompt<int>("Indique el [green]Poder[/]?")
                                .Validate(inputId =>
                                    {
                                        if (inputId < 0 || inputId > totalItems)
                                        {
                                            
                                            return ValidationResult.Error("[Red] ¡Error! [/] Coloque un valor correcto");

                                        }
                                    
                                        else
                                            if (!objetos.buscarAndEliminar(inputId))
                                            {
                                                return ValidationResult.Error("[Red] ¡Error! [/] Este objeto ya fue eliminado");
                                            }
                                        return ValidationResult.Success();
                                    }
                                );
            int idEliminar = AnsiConsole.Prompt(input);

            AnsiConsole.Write(new Text("------------------------------------------------------\n¡Objeto Eliminado Exitosamente! \n------------------------------------------------------", new Style(Spectre.Console.Color.Green)).Centered());


        }
    }

}
