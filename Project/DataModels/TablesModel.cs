public class Tables
{
    public int ID { get; set; }
    public int TableSeats { get; set; }
    public string TablesName { get; set; }

    public Tables(int id, int tableSeats, string tableName)
    {
        ID = id;
        TableSeats = tableSeats;
        TablesName = tableName;
    }
}