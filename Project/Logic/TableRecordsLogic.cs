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
    
    public static List<TableRecordsModel> GetTableRecordsByReservation(int reservationId)
    {
        var tableRecordsAccess = new TableRecordsAccess();
        return tableRecordsAccess.GetByReservation(reservationId);
    }
}