public class ReservationLogic
{
    // Makes sure time stays between 17:00 and 21:00
    public static DateTime AdjustTime(DateTime time, int minutes)
    {
        DateTime startTime = DateTime.Today.AddHours(17);
        DateTime endTime = DateTime.Today.AddHours(21);

        time = time.AddMinutes(minutes);

        if (time > endTime)
            time = startTime;
        else if (time < startTime)
            time = endTime;

        return time;
    }

    // Handles date changes (like from DaySelect)
    public static DateTime AdjustDate(DateTime date, int dayChange, int monthChange, int yearChange)
    {
        return date.AddDays(dayChange).AddMonths(monthChange).AddYears(yearChange);
    }

    public static void HandleReservationForm()
    {
        DateTime date = ReservationLogic.ReservationDaySelect();
        DateTime time = ReservationLogic.ReservationTimeSelect();
        //ReservationPresentaion.TableSelect(new List<Tables>());

        // Add time to date
        //date.AddHours(time.Hour);
        date.AddHours(18); // Always start time at 18
        
    }
    
    public static DateTime ReservationDaySelect()
    {
        DateTime selectedDate = DateTime.Today;
        int cursor = 0; // 0 = day, 1 = month, 2 = year
        ConsoleKey key;

        do
        {
            Console.Clear();
            ReservationPresentaion.PrintDaySelectHeader();

            string dayStr = selectedDate.Day.ToString("00");
            string monthStr = selectedDate.Month.ToString("00");
            string yearStr = selectedDate.Year.ToString();

            ReservationPresentaion.PrintDaySelect(cursor, dayStr, monthStr, yearStr);

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow)
            {
                cursor = (cursor == 0) ? 2 : cursor - 1;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                cursor = (cursor == 2) ? 0 : cursor + 1;
            }
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
            {
                int dayChange = 0, monthChange = 0, yearChange = 0;
                int direction = (key == ConsoleKey.UpArrow) ? 1 : -1;

                switch (cursor)
                {
                    case 0:
                        dayChange = direction;
                        break;
                    case 1:
                        monthChange = direction;
                        break;
                    case 2:
                        yearChange = direction;
                        break;
                }

                selectedDate = ReservationLogic.AdjustDate(selectedDate, dayChange, monthChange, yearChange);
            }
        } while (key != ConsoleKey.Enter);

        return selectedDate;
    }

    public static DateTime ReservationTimeSelect()
    {
        DateTime selectedTime = DateTime.Today.AddHours(17);
        ConsoleKey key;

        do
        {
            Console.Clear();
            ReservationPresentaion.PrintTimeSelect(selectedTime);

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selectedTime = ReservationLogic.AdjustTime(selectedTime, +15);
            else if (key == ConsoleKey.DownArrow)
                selectedTime = ReservationLogic.AdjustTime(selectedTime, -15);

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        return selectedTime;
    }

    public static Tables ReservationTableSelect(List<Tables> tables)
    {
        int selectedTable = 0;
        int tablesCount = tables.Count - 1; // -1 to adjust for selected table being 0
        ConsoleKey key;

        do
        {
            Console.Clear();

            int index = 0;

            // Print tables
            foreach (Tables table in tables)
            {
                Console.ResetColor();
                if (selectedTable == index)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{index + 1}. " + table.TablesName);
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedTable++;
                if (selectedTable > tablesCount) { selectedTable = 0; }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedTable--;
                if (selectedTable < 0) { selectedTable = tablesCount; }
            }
        } while (key != ConsoleKey.Enter);

        return tables[selectedTable];
    }
}
