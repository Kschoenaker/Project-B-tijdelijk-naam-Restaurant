using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
        DateTime time = ReservationTimeSelect();
        string? remark = ReservationMarkAsk();

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
        date = date.AddHours(time.Hour).AddMinutes(time.Minute);

        int userID = (int)UserLogic.CurrentAccount.ID; // Current account can't be null when making a reservation (so no need for checks)
        ReservationModel reservation = new ReservationModel(0, date, people, remark, "Active", userID);

        string input = "";
        do
        {
            Console.Clear();
            Header.PrintHeader();
            ReservationPresentaion.PrintReservationConfirm(reservation, selectedTables, UserLogic.CurrentAccount);
            input = Console.ReadLine().ToUpper();
        } while (!(input == "Y" || input == "N"));

        // Save reservation to database
        ReservationAccess reservationAccess = new ReservationAccess();
        reservationAccess.Add(reservation);

        int newReservationId = reservationAccess.GetLastInsertedId(); // Get new id
        reservation.ID = newReservationId;

        List<TableRecordsModel> records = new List<TableRecordsModel>(); // Code for future for selecting multiple tables
        foreach (TablesModel table in selectedTables)
        {
            // Make a table record for each table selected
            records.Add(new TableRecordsModel(0, table.ID, reservation.ID));
        }

        // Save table records
        TableRecordsLogic.AddTableRecords(records); // Use function from Table records logic
    }

    public static int ReservationPeopleAsk()
    {
        bool invalidInput = false;
        do
        {
            Console.Clear();
            Header.PrintHeader();

            if (invalidInput) { ReservationPresentaion.PrintInvalidInput(); }

            ReservationPresentaion.PrintPeopleComingQuestion();
            string input = Console.ReadLine();

            try
            {
                int num = Convert.ToInt32(input);

                if (num > 10)
                {
                    invalidInput = true;
                }
                else
                {
                    return num;
                }
            }
            catch (System.Exception)
            {
                Console.Clear();
            }
        } while (true);

        return 0;
    }

    public static string? ReservationMarkAsk()
    {
        Console.Clear();
        Header.PrintHeader();
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
            Header.PrintHeader();
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
            Header.PrintHeader();
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
            return null!;

        List<TablesModel> suitableTables = tables
            .Where(t => t.TableSeats >= NumPeople)
            .OrderBy(t => t.TableSeats) // Prefer smallest that fits
            .ToList();

        if (suitableTables.Count == 0)
        {
            suitableTables = tables
                .OrderByDescending(t => t.TableSeats)
                .ToList();
        }

        int selectedTable = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Header.PrintHeader();

            for (int i = 0; i < suitableTables.Count; i++)
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

                ReservationPresentaion.PrintReservationTable(suitableTables[i], i);
            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedTable = (selectedTable - 1 + suitableTables.Count) % suitableTables.Count;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedTable = (selectedTable + 1) % suitableTables.Count;
            }
            else if (key == ConsoleKey.Enter)
            {
                // 🍽️ Add the selected table
                selectedTablesList.Add(suitableTables[selectedTable]);
                NumPeople -= suitableTables[selectedTable].TableSeats;

                suitableTables.RemoveAt(selectedTable);

                // If no more people left to seat, stop function
                if (NumPeople <= 0)
                    break;

                // Refilter smaller tables if we still need seats
                suitableTables = suitableTables
                    .OrderByDescending(t => t.TableSeats)
                    .ToList();

                selectedTable = 0;
            }

        } while (suitableTables.Count > 0 && NumPeople > 0);

        Console.ResetColor();
        Console.Clear();

        return selectedTablesList;
    }


    public static void CancelReservation(ReservationModel reservation)
    {
        reservation.Status = "Cancelled";

        ReservationAccess reservationAccess = new();
        reservationAccess.Update(reservation);
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
        ShowReservation(reservations);
    }

    public static void ShowReservation(List<ReservationModel> reservations)
    {
        Console.Clear();

        if (reservations.Count <= 0)
        {
            Console.WriteLine("You don't have any reservations");
            Console.WriteLine("Press enter to go back to the main menu");
            Console.ReadLine();
            return;
        }

        int selectedReservation = 0;
        bool selectedBack = false;
        ConsoleKey key;

        // Prepare data
        List<TablesModel> allTables = TableLogic.GetAllTables();
        Dictionary<int, List<TablesModel>> tablesDict = new();
        Dictionary<int, UsersModel> usersDict = new();
        for (int i = 0; i < reservations.Count; i++)
        {
            List<TableRecordsModel> tableRecords = TableRecordsLogic.GetTableRecordsByReservation(reservations[i].ID);

            List<TablesModel> tablesForReservation = allTables.Where(t => tableRecords.Any(tr => tr.Tables_ID == t.ID)).ToList();
            tablesDict.Add(reservations[i].ID, tablesForReservation);

            UsersModel reservationUser = UserLogic.GetUserByID(reservations[i].Users_ID);
            usersDict.Add(reservations[i].ID, reservationUser);
        }

        string filterName = "";
        string filterDate = "";
        string filterTable = "";

        int reservationSelectRight = 0;
        string sortBy = "Date";
        bool ascending = true;

        do
        {
            Console.Clear();
            Header.PrintHeader();

            var filteredReservations = reservations
                .Where(r => string.IsNullOrEmpty(filterName) || usersDict[r.ID].Name.Contains(filterName, StringComparison.OrdinalIgnoreCase))
                .Where(r => string.IsNullOrEmpty(filterDate) || r.Time.ToString("yyyy-MM-dd HH:mm").Contains(filterDate))
                .Where(r =>
                {
                    var tablesForRes = tablesDict[r.ID];
                    string tableNames = string.Join(",", tablesForRes.Select(t => t.TablesName));
                    return string.IsNullOrEmpty(filterTable) || tableNames.Contains(filterTable, StringComparison.OrdinalIgnoreCase);
                })
                .ToList();

            filteredReservations = sortBy switch
            {
                "ID" => (ascending ? filteredReservations.OrderBy(r => r.ID) : filteredReservations.OrderByDescending(r => r.ID)).ToList(),
                "Time" => (ascending ? filteredReservations.OrderBy(r => r.Time) : filteredReservations.OrderByDescending(r => r.Time)).ToList(),
                "People" => (ascending ? filteredReservations.OrderBy(r => r.NumPeople) : filteredReservations.OrderByDescending(r => r.NumPeople)).ToList(),
                "Remark" => (ascending ? filteredReservations.OrderBy(r => r.Remark) : filteredReservations.OrderByDescending(r => r.Remark)).ToList(),
                "Status" => (ascending ? filteredReservations.OrderBy(r => r.Status) : filteredReservations.OrderByDescending(r => r.Status)).ToList(),
                "User" => (ascending ? filteredReservations.OrderBy(r => usersDict[r.ID].Name) : filteredReservations.OrderByDescending(r => usersDict[r.ID].Name)).ToList(),
                "Tables" => (ascending ? filteredReservations.OrderBy(r => string.Join(",", tablesDict[r.ID].Select(t => t.TablesName))) : filteredReservations.OrderByDescending(r => string.Join(",", tablesDict[r.ID].Select(t => t.TablesName)))).ToList(),
                _ => filteredReservations
            };

            ReservationPresentaion.PrintReservationFilter("Name", filterName, selectedReservation == -4);
            ReservationPresentaion.PrintReservationFilter("Date", filterDate, selectedReservation == -3);
            ReservationPresentaion.PrintReservationFilter("Table", filterTable, selectedReservation == -2);

            ReservationPresentaion.PrintReservationTableHeader(selectedReservation == -1, reservationSelectRight);

            if (filteredReservations.Count() <= 0)
            {
                Console.WriteLine("No reservations");
            }

            for (int i = 0; i < filteredReservations.Count + 1; i++)
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

                if (filteredReservations.Count <= i)
                {
                    // Print back
                    Console.WriteLine($"Back");
                }
                else
                {
                    var r = filteredReservations[i];
                    ReservationPresentaion.PrintReservationTableOneLine(r, tablesDict[r.ID], usersDict[r.ID]);
                }
            }

            Console.ResetColor();

            ReservationPresentaion.PrintReservationTableFooter();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedReservation--;
                if (selectedReservation < -4)
                    selectedReservation = reservations.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedReservation++;
                if (selectedReservation >= reservations.Count + 1) // + 1 for the back function
                    selectedReservation = -4;
            }
            else if (key == ConsoleKey.LeftArrow && selectedReservation == -1) // -1 So only when its on the header
            {
                reservationSelectRight--;
                if (reservationSelectRight < 0)
                {
                    reservationSelectRight = 6;
                }
            }
            else if (key == ConsoleKey.RightArrow && selectedReservation == -1) // -1 So only when its on the header
            {
                reservationSelectRight++;
                if (reservationSelectRight > 6)
                {
                    reservationSelectRight = 0;
                }
            }
            else if (key == ConsoleKey.Enter)
            {
                if (selectedReservation == -1)
                {
                    // Header sorting (based on current column)
                    sortBy = reservationSelectRight switch
                    {
                        0 => "ID",
                        1 => "Time",
                        2 => "People",
                        3 => "Remark",
                        4 => "Status",
                        5 => "User",
                        6 => "Tables",
                        _ => sortBy
                    };
                    ascending = !ascending;
                }
                else if (selectedReservation >= filteredReservations.Count)
                {
                    selectedBack = true;
                    return;
                }
                else if (selectedReservation < -1)
                {
                    switch (selectedReservation)
                    {
                        case -4:
                            Console.WriteLine("Enter name filter");
                            filterName = Console.ReadLine();
                            break;
                        case -3:
                            Console.WriteLine("Enter date filter");
                            filterDate = Console.ReadLine();
                            break;
                        case -2:
                            Console.WriteLine("Enter table filter");
                            filterTable = Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    var r = filteredReservations[selectedReservation];
                    HandlePrintReservation(r, tablesDict[r.ID], usersDict[r.ID]);
                }
            }
        } while (!selectedBack);

        Console.ResetColor();
        Console.Clear();
    }

    private static void HandlePrintReservation(ReservationModel reservation, List<TablesModel> tables, UsersModel user)
    {
        bool SelectedBack = false;
        int selectedInt = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Header.PrintHeader();
            ReservationPresentaion.PrintReservation(reservation, tables, user);

            if (reservation.Status == "Cancelled")
            {
                selectedInt = 1;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("Back");

                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == selectedInt)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ResetColor();
                    }

                    switch (i)
                    {
                        case 0:
                            Console.WriteLine("Cancel reservation");
                            break;
                        case 1:
                            Console.WriteLine("Back");
                            break;
                    }
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedInt--;
                if (selectedInt < 0)
                    selectedInt = 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedInt++;
                if (selectedInt > 1)
                    selectedInt = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedInt)
                {
                    case 0:
                        Console.WriteLine();
                        Console.WriteLine("Confirm cancel (Y/N)");
                        string input = "";
                        do
                        {
                            input = Console.ReadLine().ToUpper();
                        } while (!(input == "Y" || input == "N"));

                        if (input == "Y")
                        {
                            CancelReservation(reservation);
                        }
                        break;
                    case 1:
                        SelectedBack = true;
                        break;
                }
            }
        } while (!SelectedBack);
    }
}
