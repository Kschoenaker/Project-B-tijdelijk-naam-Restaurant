public class ReservationModel
{
    public int ID { get; set; }
    public DateTime Time { get; set; }
    public int NumPeople { get; set; }
    public string Remark { get; set; }
    public string Status { get; set; }
    public int Users_ID { get; set; }

    public ReservationModel() { }

    public ReservationModel(int id, DateTime time, int numPeople, string remark, string status, int users_ID)
    {
        ID = id;
        Time = time;
        NumPeople = numPeople;
        Remark = remark;
        Status = status;
        Users_ID = users_ID;
    }
}