public class TableLogic
{
    public static List<TablesModel> GetAllTables()
    {
        TableAccess tableAccess = new TableAccess();
        return tableAccess.GetAll();
    }

    public static List<TablesModel> GetUnreservedTablesByDate(DateTime date)
    {
        TableAccess tableAccess = new TableAccess();
        return tableAccess.GetUnreservedTablesByDate(date);
    }

    public static bool IsThereTableSpace(int NumPeople, DateTime date)
    {
        TableAccess tableAccess = new TableAccess();
        List<TablesModel> list = tableAccess.GetUnreservedTablesByDate(date);
        return IsThereTableSpace(NumPeople, list);
    }

    public static bool IsThereTableSpace(int NumPeople, List<TablesModel> tables)
    {
        return tables.Sum(t => t.TableSeats) >= NumPeople;
    }

    // Adds default tables (from assignment giver)
    public static void AddDefaultTables()
    {
        TableAccess tableAccess = new TableAccess();
        if (!tableAccess.IsTableEmpty()) return;

        List<TablesModel> tablesToAdd = new();

        // Adding tables for 2 people
        for (int i = 0; i < 6; i++)
        {
            tablesToAdd.Add(new TablesModel(0, 2, $"A{i + 1}"));
        }

        // Adding tables for 4 people
        for (int i = 0; i < 6; i++)
        {
            tablesToAdd.Add(new TablesModel(0, 4, $"B{i + 1}"));
        }

        // Adding tables for 6 people
        for (int i = 0; i < 4; i++)
        {
            tablesToAdd.Add(new TablesModel(0, 6, $"C{i + 1}"));
        }

        foreach (TablesModel item in tablesToAdd)
        {
            tableAccess.Add(item);
        }
    }
}