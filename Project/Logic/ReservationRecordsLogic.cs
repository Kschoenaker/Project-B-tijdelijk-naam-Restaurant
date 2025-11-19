public class ReservationRecordsLogic
{
    public static void Add(ReservationRecordsModel reservationRecords)
    {
        ReservationRecordsAccess reservationRecordsAccess = new ReservationRecordsAccess();
        reservationRecordsAccess.Add(reservationRecords);
    }
    public static List<DishModel> GetDishesByReservation(int reservationId)
    {
        ReservationRecordsAccess access = new();
        DishAccess dishAccess = new();

        var records = access.GetAll()
            .Where(r => r.Reservation_ID == reservationId)
            .ToList();

        List<DishModel> dishes = new();

        foreach (var rec in records)
        {
            var dish = dishAccess.GetById(rec.Dish_ID);
            if (dish != null)
                dishes.Add(dish);
        }

        return dishes;
    }

    public static void UpdateReservationDishes(int reservationId, List<DishModel> newDishes)
    {
        var recordsAccess = new ReservationRecordsAccess();

        // delete old records
        var oldRecords = recordsAccess.GetAll()
            .Where(r => r.Reservation_ID == reservationId)
            .ToList();

        foreach (var rec in oldRecords)
            recordsAccess.Delete(rec);

        // add new records
        foreach (var dish in newDishes)
        {
            var newRec = new ReservationRecordsModel(0, dish.ID, reservationId);
            recordsAccess.Add(newRec);
        }
    }
}