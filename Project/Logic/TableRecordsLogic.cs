public class TableRecordsLogic
{
    public static void AddTableRecords(List<TableRecordsModel> records)
    {
        TableRecordsAccess tableRecordsAccess = new TableRecordsAccess();
        
        foreach (TableRecordsModel record in records)
        {
            tableRecordsAccess.Add(record);
        }
    }
}