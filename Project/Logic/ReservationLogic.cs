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
        int people = ReservationPeopleAsk();
        DateTime date = ReservationDaySelect();
        string? remark = ReservationMarkAsk();

        //Commented out code, cause maybe asking for time was not needed
        //DateTime time = ReservationTimeSelect();

        List<Tables> tables = new List<Tables>(); // Example tables
        tables.Add(new Tables(1, 2, "Name1"));
        tables.Add(new Tables(2, 2, "Name2"));
        tables.Add(new Tables(3, 2, "Name3"));
        Tables table = ReservationTableSelect(tables);

        // Add time to date
        //date.AddHours(time.Hour);
        date.AddHours(18); // Always start time at 18

        ReservationModel reservation = new ReservationModel(0, date, people, remark, -1); // -1 Cause no login system yet
        List<TableRecordsModel> records = new List<TableRecordsModel>(); // Code for future for selecting multiple tables
        records.Add(new TableRecordsModel(0, table.ID, reservation.ID));

        string input = "";
        do
        {
            Console.Clear();
            ReservationPresentaion.PrintReservationConfirm(reservation);
            input = Console.ReadLine();
        } while (!(input == "Y" || input == "N"));

        // Save reservation to database
    }

    public static int ReservationPeopleAsk()
    {
        Console.Clear();
        ReservationPresentaion.PrintPeopleComingQuestion();
        string input = Console.ReadLine();

        try
        {
            return Convert.ToInt32(input);
        }
        catch (System.Exception)
        {
            Console.Clear();
            ReservationPresentaion.PrintInvalidInput();
            return ReservationPeopleAsk(); // Call the function again if input is invalid
        }
    }

    public static string? ReservationMarkAsk()
    {
        Console.Clear();
        ReservationPresentaion.PrintRemarkAsk();
        return Console.ReadLine();
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
        if (tables == null || tables.Count == 0)
        {
            Console.WriteLine("No tables available");
            return null!;
        }

        int selectedTable = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();

            for (int i = 0; i < tables.Count; i++)
            {
                if (i == selectedTable)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ResetColor();
                }

                Console.WriteLine($"{i + 1}. {tables[i].TablesName}");
            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedTable--;
                if (selectedTable < 0)
                    selectedTable = tables.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedTable++;
                if (selectedTable >= tables.Count)
                    selectedTable = 0;
            }

        } while (key != ConsoleKey.Enter);

        Console.ResetColor();
        Console.Clear();

        return tables[selectedTable];
    }
}
