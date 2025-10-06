public class ReservationPresentaion
{
    public static void DaySelect()
    {
        DateTime selectedDate = DateTime.Today;
        int cursor = 0; // 0 = day, 1 = month, 2 = year
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Use ←/→ to switch, ↑/↓ to change, Enter to confirm:\n");

            string dayStr = selectedDate.Day.ToString("00");
            string monthStr = selectedDate.Month.ToString("00");
            string yearStr = selectedDate.Year.ToString();

            // Highlight the selected part
            if (cursor == 0) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(dayStr);
            Console.ResetColor();
            Console.Write(" / ");

            if (cursor == 1) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(monthStr);
            Console.ResetColor();
            Console.Write(" / ");

            if (cursor == 2) Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(yearStr);
            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow)
            {
                cursor = (cursor == 0) ? 2 : cursor - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                cursor = (cursor == 2) ? 0 : cursor + 1;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                switch (cursor)
                {
                    case 0:
                        selectedDate = selectedDate.AddDays(1);
                        break;
                    case 1:
                        selectedDate = selectedDate.AddMonths(1);
                        break;
                    case 2:
                        selectedDate = selectedDate.AddYears(1);
                        break;
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                switch (cursor)
                {
                    case 0:
                        selectedDate = selectedDate.AddDays(-1);
                        break;
                    case 1:
                        selectedDate = selectedDate.AddMonths(-1);
                        break;
                    case 2:
                        selectedDate = selectedDate.AddYears(-1);
                        break;
                }
            }

        } while (key != ConsoleKey.Enter);
    }
}