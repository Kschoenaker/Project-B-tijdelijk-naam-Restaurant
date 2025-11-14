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

        Dictionary<int, List<DishModel>> peopleDishSelection = ReservationPeopleDishAsk(people, date);

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

    public static Dictionary<int, List<DishModel>> ReservationPeopleDishAsk(int numPeople, DateTime date)
    {
        var result = new Dictionary<int, List<DishModel>>();

        ThemeCalanderModel? currentThemeCalander = ThemeCalanderLogic.GetCurrentThemeCalander();

        if (currentThemeCalander == null)
        {
            var CalanderNotValidMenu = new OptionsMenu(
                new() { "Continue"},
                $"The dish theme is not set yet for that date."
            );
            return result;
        }

        ThemeModel currentTheme = ThemeLogic.GetByID(currentThemeCalander.Theme_ID);

        var allDishes = DishLogic.GetAllByTheme(currentTheme.ThemeName); // Gets all dishes by the correct theme

        for (int i = 0; i < numPeople; i++)
        {
            var mainChoiceMenu = new OptionsMenu(
                new() { "Select no dishes", "Select dishes" },
                $"Would you like to select dishes for person {i + 1}?"
            );

            if (mainChoiceMenu.Selected == 0)
            {
                result[i] = new List<DishModel>();
                continue;
            }

            // Fixed 3-course system: Appetizer, Main Course, Dessert
            string[] courseTypes = { "Appetizer", "Main Course", "Dessert" };
            var selectedDishes = new List<DishModel>();

            for (int c = 0; c < courseTypes.Length; c++)
            {
                string chosenType = courseTypes[c];
                var wantCourseMenu = new OptionsMenu(
                    new() { "No", "Yes" },
                    $"Would person {i + 1} like a {chosenType}?"
                );

                if (wantCourseMenu.Selected == 0)
                    continue;

                var filteredDishes = allDishes.FindAll(d => d.DishType == chosenType);
                var dishMenu = new OptionsMenu(
                    filteredDishes.ConvertAll(d => d.DishName),
                    $"Select a {chosenType} for person {i + 1}:"
                );

                selectedDishes.Add(filteredDishes[dishMenu.Selected]);
            }

            result[i] = selectedDishes;
        }
        return result;
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
        DateTime selectedTime = DateTime.Today.AddHours(18);
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

    public static List<TablesModel> GetAvailableTables(List<TablesModel> tables, int numPeople)
    {
        if (tables == null || tables.Count == 0)
            return new();

        // Exact seat match (e.g., 4 people → only 4-seat tables)
        var exactMatch = tables.Where(t => t.TableSeats == numPeople).ToList();
        if (exactMatch.Count > 0)
            return exactMatch;

        // If no exact match, find combinations that fit the number of people
        List<TablesModel> result = new();
        int remaining = numPeople;

        // Sort from largest to smallest for greedy pick
        var sortedTables = tables.OrderByDescending(t => t.TableSeats).ToList();

        foreach (var table in sortedTables)
        {
            if (remaining <= 0) break;

            // Take a table only if it helps fill the remaining seats
            if (table.TableSeats <= remaining + 1) // +1 for some small tolerance
            {
                result.Add(table);
                remaining -= table.TableSeats;
            }
        }

        // If still not filled completely, try adding smallest tables left
        if (remaining > 0)
        {
            foreach (var table in sortedTables.Where(t => !result.Contains(t)))
            {
                result.Add(table);
                remaining -= table.TableSeats;
                if (remaining <= 0) break;
            }
        }

        return result;
    }

    public static List<TablesModel> ReservationTableSelect(List<TablesModel> tables, int numPeople)
    {
        List<TablesModel> selectedTablesList = new();

        TableAccess tableAccess = new TableAccess();
        List<TablesModel> allTables = tableAccess.GetAll();

        if (tables == null || tables.Count == 0)
            return null!;

        // Get the tables that can be used for this reservation
        List<TablesModel> suitableTables = GetAvailableTables(tables, numPeople);

        if (suitableTables.Count == 0)
        {
            suitableTables = tables
                .OrderByDescending(t => t.TableSeats)
                .ToList();
        }

        // Build the floor with positions and sizes
        FloorBuilder floor = new(allTables, tables, suitableTables); // All available initially

        ConsoleKey key;
        bool done = false;

        do
        {
            Console.Clear();
            Header.PrintHeader();

            floor.DrawFloor();

            Console.WriteLine("\nUse the arrow keys to move.");
            Console.WriteLine($"Seats remaining to assign: {numPeople}\n");

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    floor.MoveSelection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    floor.MoveSelection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    floor.MoveSelection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    floor.MoveSelection(1, 0);
                    break;
                case ConsoleKey.Enter:
                    var hovered = floor.AllFloorTables.FirstOrDefault(ft => ft.Status == FloorTableStatus.HoveredOn);
                    if (hovered != null && floor.AvailableFloorTables.Contains(hovered))
                    {
                        selectedTablesList.Add(hovered.Table);
                        numPeople -= hovered.Table.TableSeats;

                        floor.SelectedTables.Add(hovered);
                        hovered.Status = FloorTableStatus.Reserved;

                        // Remove from available
                        floor.AvailableFloorTables.Remove(hovered);

                        // Stop if all people are seated
                        if (numPeople <= 0)
                            done = true;
                    }
                    break;
                case ConsoleKey.Escape:
                    done = true;
                    break;
            }

        } while (!done);

        Console.ResetColor();
        Console.Clear();

        return selectedTablesList;
    }


    public static bool CancelReservation(ReservationModel reservation)
    {
        if (UserLogic.AccessLevel > 0 || reservation.Time > DateTime.Now.AddHours(24))
        {
            reservation.Status = "Cancelled";

            ReservationAccess reservationAccess = new();
            reservationAccess.Update(reservation);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckReservationCanBeCancelled(ReservationModel reservation)
    {
        return UserLogic.AccessLevel > 0 || reservation.Time > DateTime.Now.AddHours(24);
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

    public static void HandleSeeAllReservation()
    {
        ReservationAccess reservationAccess = new();
        List<ReservationModel> reservations = reservationAccess.GetAll();
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
        string sortBy = "ID";
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

            Console.WriteLine("Table filters: (Selected one and press enter to start filtering)");
            ReservationPresentaion.PrintReservationFilter("Name", filterName, selectedReservation == -4);
            ReservationPresentaion.PrintReservationFilter("Date", filterDate, selectedReservation == -3);
            ReservationPresentaion.PrintReservationFilter("Table", filterTable, selectedReservation == -2);

            Console.WriteLine("Sort the table by selecting one of the headers.");
            Console.WriteLine($"Currently sorting on: {sortBy} ({(ascending ? "Ascending" : "Descending")})");
            ReservationPresentaion.PrintReservationTableHeader(selectedReservation == -1, reservationSelectRight);

            if (filteredReservations.Count() <= 0)
            {
                Console.WriteLine("No reservations");
            }

            for (int i = 0; i < filteredReservations.Count + 1; i++)
            {
                Console.ResetColor();

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

            if (reservation.Status == "Cancelled" || !CheckReservationCanBeCancelled(reservation))
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
