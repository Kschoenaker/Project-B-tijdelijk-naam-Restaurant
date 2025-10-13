using System.Runtime.ConstrainedExecution;

public class ReservationPresentaion
{
    public static void PrintDaySelectHeader()
    {
        Console.WriteLine("Use ←/→ to switch, ↑/↓ to change, Enter to confirm:\n");
    }

    public static void PrintDaySelect(int cursor, string dayStr, string monthStr, string yearStr)
    {
        if (cursor == 0) Console.BackgroundColor = ConsoleColor.White;
        Console.Write(dayStr);
        Console.ResetColor();
        Console.Write(" / ");

        if (cursor == 1) Console.BackgroundColor = ConsoleColor.White;
        Console.Write(monthStr);
        Console.ResetColor();
        Console.Write(" / ");

        if (cursor == 2) Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine(yearStr);
        Console.ResetColor();
    }

    public static void PrintTimeSelect(DateTime selectedTime)
    {
        Console.WriteLine("Use ↑/↓ to change time in 15-min intervals, Enter to confirm:\n");

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine($"   {selectedTime:HH:mm}   ");
        Console.ResetColor();
    }

    public static void PrintPeopleComingQuestion()
    {
        Console.WriteLine("How many people are coming?");
    }

    public static void PrintRemarkAsk()
    {
        Console.WriteLine("Would you like to leave a remark?");
        Console.WriteLine("Leave it empty if not.");
    }

    public static void PrintInvalidInput()
    {
        Console.WriteLine("The input given is invalid");
    }

    public static void PrintReservationTable(TablesModel table, int counter)
    {
        Console.WriteLine($"{counter + 1}. {table.TablesName} (has {table.TableSeats} seats)");
    }

    public static void PrintReservation(ReservationModel reservation)
    {
        Console.WriteLine("Reservation:");
        Console.WriteLine($"Amount of people coming: {reservation.NumPeople}");
        Console.WriteLine($"Reservation date: {reservation.Time.ToString()}");

        if (reservation.Remark is not null && reservation.Remark != "")
        {
            Console.WriteLine($"Remark: {reservation.Remark}");
        }
    }

    public static void PrintReservationOneLine(ReservationModel reservation)
    {
        Console.WriteLine($"Time: {reservation.Time.ToString()}, Number of people: {reservation.NumPeople}");
    }

    public static void PrintReservationConfirm(ReservationModel reservation)
    {
        PrintReservation(reservation);

        Console.WriteLine();
        Console.WriteLine("Confirm? (Y/N)");
    }

    public static void PrintNotEnoughSpace()
    {
        Console.Clear();
        Console.WriteLine("We are so sorry, there is not enough space for you today.");
        Console.WriteLine("Please make a new reservation with a different date, if you would still like the come.");
        Console.WriteLine("Thank you for choosing us!");

        Console.WriteLine();
        Console.WriteLine("'Enter' to go back to main menu");
    }
}