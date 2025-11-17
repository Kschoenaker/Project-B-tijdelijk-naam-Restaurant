public class ReservationRecordsLogic
{
    public static void Add(ReservationRecordsModel reservationRecords)
    {
        ReservationRecordsAccess reservationRecordsAccess = new ReservationRecordsAccess();
        reservationRecordsAccess.Add(reservationRecords);
    }
}