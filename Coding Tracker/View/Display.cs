using Spectre.Console;
using System;

namespace CodingTracker
{
    internal class Display
    {
        internal static void DisplayMenu()
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

            // Render the table
            AnsiConsole.Render(table);
        }

        internal static CodingSessionModel GetRecord(bool iSGivenSessionIdPresent = true)
        {
            string TimeDateInput;
            CodingSessionModel model = new CodingSessionModel();
            if (iSGivenSessionIdPresent)
            {
                do
                {
                    Console.WriteLine("Enter valid Start-time in the format HH:mm (e.g., 12:30)");
                    TimeDateInput = Console.ReadLine();
                } while (!Validation.ValidateStartSessionTime(TimeDateInput));
                model.SessionStartTime = TimeDateInput;
                do
                {
                    Console.WriteLine("Enter valid End-time in the format HH:mm (e.g., 12:45), End-time should be greater than start time.");
                    TimeDateInput = Console.ReadLine();
                } while (!Validation.ValidateEndSessionTime(TimeDateInput));
                model.SessionEndTime = TimeDateInput;
                model.SessionDuration = Validation.SessionTotalTime();

                do
                {
                    Console.WriteLine("Enter a date in the format yyyy-MM-dd (e.g., 2024-03-14)");
                    TimeDateInput = Console.ReadLine();
                } while (!Validation.ValidateInputDate(TimeDateInput));
                model.SessionCodingDate = TimeDateInput;
            }
            return model;
        }

        internal static void ViewRecords(IEnumerable<CodingSessionModel> records)
        {
            if(records.Count() == 0)
            {
                Console.WriteLine("No Records inserted. Kindly Insert some records .Press any key to continue");
                Console.ReadLine();
                return;
            }
            foreach (var session in records)
            {
                Console.WriteLine($"Session ID: {session.SessionId}");
                Console.WriteLine($"Start Time: {session.SessionStartTime}");
                Console.WriteLine($"End Time: {session.SessionEndTime}");
                Console.WriteLine($"SessionDuration: {session.SessionDuration}");
                Console.WriteLine($"Date: {session.SessionCodingDate}");
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to continue\n");
            Console.ReadLine();
        }

        internal static int GetSessionIdToDeleteRecord()
        {
            Console.WriteLine("Enter the session id you want to delete: ");
            var input = Console.ReadLine();

            bool intVal = Validation.ValidateInputInteger(input);

            while (!intVal)
            {
                Console.WriteLine("Invalid input pls enter integer value.");
                input = Console.ReadLine();
                intVal = Validation.ValidateInputInteger(input);
            }
            return int.Parse(input.ToString());
        }

        internal static int GetSessionIdToUpdateRecord()
        {
            Console.WriteLine("Enter the session id you want to update: ");
            var input = Console.ReadLine();
            bool intVal = Validation.ValidateInputInteger(input);
            while (!intVal)
            {
                Console.WriteLine("Invalid input pls enter integer value.");
                input = Console.ReadLine();
                intVal = Validation.ValidateInputInteger(input);
            }
            int SessionId = Validation.ReturnSessionID();
            return SessionId;

        }

        internal static void ExitApplicationMessage()
        {
            Console.WriteLine("Thanks for using Goodbye");
            Console.ReadLine();
        }

        internal static void InvalidInputMessage()
        {
            Console.WriteLine("Invalid input. Press any key to continue");
            Console.ReadLine();
        }

        internal static void DisplayNoRecordMessage(bool iSGivenSessionIdPresent)
        {
            if (!iSGivenSessionIdPresent)
            {
                Console.WriteLine("The session id you entered is not present in the database.Cannot delete. Press any key to continue");
                Console.ReadLine();
            }
        }
    }
}
