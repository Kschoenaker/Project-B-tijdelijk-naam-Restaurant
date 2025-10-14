public class ReservationLogic
{
    // ------ Code for making reservations
    // Makes sure time stays between 18:00 and 22:00
    public static DateTime AdjustTime(DateTime time, int minutes)
    {
        DateTime startTime = DateTime.Today.AddHours(18);
        DateTime endTime = DateTime.Today.AddHours(22);

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

    // Makes sure that the date is not in the past
    public static bool ValidateDate(DateTime date)
    {
        if (DateTime.Today > date) return false;

        return true;
    }

    public static bool ValidateReservation(ReservationModel reservation)
    {
        // Date check (not in the past)
        if (DateTime.Today > reservation.Time) return false;

        // Num people check (can't be greater then 6)
        if (reservation.NumPeople > 6) return false;

        // Time check (inbetween 18:00 and 22:00)
        if (!(18 < reservation.Time.Hour && reservation.Time.Hour > 22)) return false;

        return true;
    }

    public static void HandleReservationForm()
    {
        int people = ReservationPeopleAsk();
        DateTime date = ReservationDaySelect();
        string? remark = ReservationMarkAsk();

        //Commented out code, cause maybe asking for time was not needed
        //DateTime time = ReservationTimeSelect();

        // A check to make sure that there are avialable tables
        List<TablesModel> tables = TableLogic.GetUnreservedTablesByDate(date);
        if (!TableLogic.IsThereTableSpace(people, tables))
        {
            ReservationPresentaion.PrintNotEnoughSpace();
            Console.ReadLine();
            return;
        }

        List<TablesModel> selectedTables = ReservationTableSelect(tables, people);

        // Add time to date
        //date.AddHours(time.Hour);
        date.AddHours(18); // Always start time at 18

        int userID = (int)UserLogic.CurrentAccount.ID; // Current account can't be null when making a reservation (so no need for checks)
        ReservationModel reservation = new ReservationModel(0, date, people, remark, userID);

        List<TableRecordsModel> records = new List<TableRecordsModel>(); // Code for future for selecting multiple tables
        foreach (TablesModel table in selectedTables)
        {
            // Make a table record for each table selected
            records.Add(new TableRecordsModel(0, table.ID, reservation.ID));
        }

        string input = "";
        do
        {
            Console.Clear();
            ReservationPresentaion.PrintReservationConfirm(reservation);
            input = Console.ReadLine();
        } while (!(input == "Y" || input == "N"));

        // Save reservation to database
        ReservationAccess reservationAccess = new ReservationAccess();
        reservationAccess.Add(reservation);

        // Save table records
        TableRecordsLogic.AddTableRecords(records); // Use function from Table records logic
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
        bool valid = false;

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

                if (ValidateDate(AdjustDate(selectedDate, dayChange, monthChange, yearChange)))
                    selectedDate = AdjustDate(selectedDate, dayChange, monthChange, yearChange);
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
                selectedTime = AdjustTime(selectedTime, +15);
            else if (key == ConsoleKey.DownArrow)
                selectedTime = AdjustTime(selectedTime, -15);

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        return selectedTime;
    }

    public static List<TablesModel> ReservationTableSelect(List<TablesModel> tables, int NumPeople)
    {
        List<TablesModel> selectedTablesList = new();
        if (tables == null || tables.Count == 0)
        {
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

                ReservationPresentaion.PrintReservationTable(tables[i], i);
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
            else if (key == ConsoleKey.Enter)
            {
                // Add table to selected tables and remove the table (so it can't be selected)
                selectedTablesList.Add(tables[selectedTable]);
                tables.RemoveAt(selectedTable);
            }
        } while (selectedTablesList.Sum(t => t.TableSeats) < NumPeople);

        Console.ResetColor();
        Console.Clear();

        return selectedTablesList;
    }

    //----- Code for seeing reservations
    public static List<ReservationModel> GetReservationByUser(UsersModel user)
    {
        ReservationAccess reservationAccess = new ReservationAccess();
        return reservationAccess.GetByUserID((int)user.ID);
    }

    public static void HandleSeeReservation(UsersModel user)
    {
        List<ReservationModel> reservations = GetReservationByUser(user);
        if (reservations.Count > 0) return;

        int selectedReservation = 0;
        bool selectedBack = false;
        ConsoleKey key;

        do
        {
            Console.Clear();

            for (int i = 0; i < reservations.Count + 1; i++)
            {
                if (i == selectedReservation)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ResetColor();
                }

                if (reservations.Count < i)
                {
                    // Print back
                    Console.WriteLine($"{i}. Back");
                }
                else
                {
                    Console.Write($"{i}. ");
                    ReservationPresentaion.PrintReservationOneLine(reservations[i]);
                }

            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedReservation--;
                if (selectedReservation < 0)
                    selectedReservation = reservations.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedReservation++;
                if (selectedReservation >= reservations.Count + 1) // + 1 for the back function
                    selectedReservation = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                if (selectedReservation > reservations.Count)
                {
                    selectedBack = true;
                    return;
                }
                else
                {
                    Console.Clear();
                    ReservationPresentaion.PrintReservation(reservations[selectedReservation]);
                    Console.ReadLine();
                }
            }
        } while (!selectedBack);

        Console.ResetColor();
        Console.Clear();
    }
}
