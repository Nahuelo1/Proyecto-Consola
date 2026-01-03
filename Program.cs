using System;
using System.ComponentModel.DataAnnotations;
using AplicacionConsola.Views;
using Spectre.Console;

namespace ProyectoEximoConsola
{
    public class Inicio
    {
        static void Main(string[] arg)
        {
            Menu inicio = new Menu();
            AnsiConsole.Progress().Start(fun =>
            {
                var tarea = fun.AddTask("[blue]Cargando aplicación[/]");

                while (!fun.IsFinished)
                {
                    tarea.Increment(2);
                    Thread.Sleep(50);
                }
            });

            AnsiConsole.Clear();
            var intro = new Text("#####################################################\n" +
                                "#------- ¡Bienvenidos a la tienda Nahuelito! -------#\n" +
                                "#####################################################\n", new Style( foreground: Color.Green)).Centered();
            AnsiConsole.Write(intro);
     
            inicio.iniciarApp();

            var fin = new Text("#####################################################\n" +
                                "#------- !Gracias Vuelvas Prontoss! -------#\n" +
                                "#####################################################\n", new Style(foreground: Color.Yellow)).Centered();
            AnsiConsole.Write(fin);

        }
    }
}
