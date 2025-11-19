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
        Console.WriteLine("How many people are coming? (Max is 10)");
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

    public static bool PrintReservation(ReservationModel reservation, List<TablesModel> tables, UsersModel user, List<DishModel> dishes, bool CanChangeReservation = true)
    {
        string tableList = tables.Count > 0 ? string.Join(", ", tables.Select(t => t.TablesName)) : "—";
        tableList = TrimToLength(tableList, 12);

        if (ReservationLogic.CheckReservationCanBeCancelled(reservation) && CanChangeReservation)
        {
            ReservationLogic.HandleChangeReservation(reservation, tables, dishes);

            return false;
        }
        else 
        {
            Console.WriteLine("Reservation:");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"People: {reservation.NumPeople}");
            Console.WriteLine($"Status: {reservation.Status}");
            Console.WriteLine($"Date: {reservation.Time:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Tables: {tableList}");

            if (!string.IsNullOrWhiteSpace(reservation.Remark))
                Console.WriteLine($"Remark: {reservation.Remark}");

            if (dishes == null || dishes.Count == 0)
            {
                Console.WriteLine("Dishes: —");
            }
            else
            {

                Console.WriteLine("Dishes:");

                string[] courseOrder = { "Appetizer", "MainCourse", "Dessert" };

                var grouped = dishes
                    .GroupBy(d => d.DishType)
                    .OrderBy(g => Array.IndexOf(courseOrder, g.Key))
                    .ToList();

                foreach (var group in grouped)
                {
                    Console.Write($"  {group.Key}: ");
                    Console.WriteLine(string.Join(", ", group.Select(x => x.DishName)));
                }
            }

            Console.WriteLine();

            return true;
        }
    }

    public static void PrintReservationFilter(string filterName, string filterValue, bool showHighlight)
    {
        if (showHighlight)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        Console.WriteLine($"{filterName}: {filterValue}");

        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintReservationTableHeader(bool showHighlight, int highlightRight)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╔════════╦════════════════════╦══════════════╦══════════════════════════╦════════════╦══════════╦══════════════╗");

        string lineToPrint = "║   ID   ║       Time         ║  # People    ║         Remark           ║   Status   ║   User   ║   Tables     ";
        string[] lineParts = lineToPrint.Split("║");

        //Console.Write("║");

        for (int i = 0; i < lineParts.Count(); i++)
        {
            if (i == highlightRight + 1 && showHighlight)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ResetColor();
            }

            Console.Write(lineParts[i]);
            Console.ResetColor();
            Console.Write("║");
        }
        Console.WriteLine();

        Console.WriteLine("╠════════╬════════════════════╬══════════════╬══════════════════════════╬════════════╬══════════╬══════════════╣");
        Console.ResetColor();
    }

    public static void PrintReservationTableOneLine(ReservationModel reservation, List<TablesModel> tables, UsersModel user)
    {
        string tableList = tables.Count > 0 ? string.Join(", ", tables.Select(t => t.TablesName)) : "—";
        tableList = TrimToLength(tableList, 12);

        Console.WriteLine($"║ {reservation.ID,-6} ║  {reservation.Time:yyyy-MM-dd HH:mm}  ║ {reservation.NumPeople,-12} ║ {TrimToLength(reservation.Remark, 24),-24} ║ {TrimToLength(reservation.Status, 10),-10} ║ {user.Name,-8} ║ {tableList,-12} ║");
    }

    public static void PrintReservationTableFooter()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("╚════════╩════════════════════╩══════════════╩══════════════════════════╩════════════╩══════════╩══════════════╝");
        Console.ResetColor();
    }

    private static string TrimToLength(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
    }

    public static void PrintReservationConfirm(ReservationModel reservation, List<TablesModel> tables, UsersModel user, List<DishModel> dishes)
    {
        PrintReservation(reservation, tables, user, dishes, false);

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