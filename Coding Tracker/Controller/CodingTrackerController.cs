using Spectre.Console;
using Spectre.Console.Cli;

namespace CodingTracker
{
    internal class CodingTrackerController
    {
        internal static void Menu()
        {
            var isTrackerOn = true;
            do
            {
                Console.Clear();
                AnsiConsole.Write(new Markup("[bold green]Hello![/] [bold]What would you like to do?[/]\n"));
                // Create a table to display the options
                var table = new Table();
                table.AddColumn("[orange1]Option[/]");
                table.AddColumn("[orange1]Description[/]");

                // Add rows to the table for each option
                table.AddRow("[green]0[/]", "[green]View Record[/]");
                table.AddRow("[blue]1[/]", "[blue]Insert Record[/]");
                table.AddRow("[red]2[/]", "[red]Delete Record[/]");
                table.AddRow("[yellow]3[/]", "[yellow]Update Record[/]");
                table.AddRow("[white]4[/]", "[white]Exit Application[/]");

                // Beautify the table
                table.Border(TableBorder.Rounded);

                // Set the table title and align it to the center
                //table.Title = new TableTitle("[bold black]Hello![/] [bold]What would you like to do?[/]");

                table.Alignment = Justify.Center;

                // Render the table
                AnsiConsole.Render(table);

                var selection = Console.ReadLine().ToLower().Trim();

                switch(selection)
                {
                    case "0":
                        Database.ViewRecords();
                        break;
                    case "1":
                        Database.InsertRecord();
                        break;
                    case "2":
                        Database.DeleteRecord();
                        break;
                    case "3":
                        Database.UpdateRecord();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye");
                        isTrackerOn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Press any key to continue");
                        Console.ReadLine();
                        break;
                }

            }while(isTrackerOn);
        }
    }
}
