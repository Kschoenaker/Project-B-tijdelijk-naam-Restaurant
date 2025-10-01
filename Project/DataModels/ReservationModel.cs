public class ReservationModel
{
    public int64 ID { get; set; }
    public DateTime Time { get; set; }
    public int NumPeople { get; set; }
    public string Remark { get; set; }
    public int TableRowID { get; set; }
    public int Users_ID { get; set; }

    public ReservationModel(int id, DateTime time, int numPeople, string remark, int tableRowID)
    {
        ID = id;
        Time = time;
        NumPeople = numPeople;
        Remark = remark;
        TableRowID = tableRowID;
        Users_ID = Users_ID;
    }
}