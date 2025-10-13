public class TablesModel
{
    public int ID { get; set; }
    public int TableSeats { get; set; }
    public string TablesName { get; set; }

    public TablesModel() { }
    
    public TablesModel(int id, int tableSeats, string tableName)
    {
        ID = id;
        TableSeats = tableSeats;
        TablesName = tableName;
    }

    public TablesModel(Int64 id, Int64 tableSeats, string tableName)
        : this((int)id, (int)tableSeats, tableName) {}
}