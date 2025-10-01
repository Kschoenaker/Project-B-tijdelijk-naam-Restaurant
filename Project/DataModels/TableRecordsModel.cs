public class TableRecordsModel
{
    public int64 ID { get; set; }
    public int Tables_ID { get; set; }
    public int Reservation_ID { get; set; }

    public TableRecordsModel(int id, int tables_ID, int reservation_ID)
    {
        ID = id;
        Tables_ID = tables_ID;
        Reservation_ID = reservation_ID;
    }
}