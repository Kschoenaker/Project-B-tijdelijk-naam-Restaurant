public class ReservationRecordsModel
{
    public int64 ID { get; set; }
    public int Dish_ID { get; set; }
    public int Reservation_ID { get; set; }

    public ReservationRecordsModel(int id, int dish_ID, int reservation_ID)
    {
        ID = id;
        Dish_ID = dish_ID;
        Reservation_ID = reservation_ID;
    }
}