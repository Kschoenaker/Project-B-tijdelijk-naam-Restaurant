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

    public static void ReplaceTableRecords(int reservationId, List<TablesModel> newTables)
    {
        var access = new TableRecordsAccess();

        // Delete old
        var old = access.GetByReservation(reservationId);
        foreach (var r in old)
            access.Delete(r);

        // Add new
        foreach (var t in newTables)
        {
            var newRecord = new TableRecordsModel(0, t.ID, reservationId);
            access.Add(newRecord);
        }
    }
}