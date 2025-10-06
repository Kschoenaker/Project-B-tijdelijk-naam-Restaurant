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
}