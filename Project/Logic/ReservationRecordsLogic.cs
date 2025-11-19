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

    public static void ReplaceDishRecords(int reservationId, List<DishModel> newDishes)
    {
        var access = new ReservationRecordsAccess();

        // Delete old
        var old = access.GetAll()
            .Where(x => x.Reservation_ID == reservationId)
            .ToList();

        foreach (var record in old)
            access.Delete(record);

        // Add new
        foreach (var dish in newDishes)
        {
            var newRecord = new ReservationRecordsModel(0, dish.ID, reservationId);
            access.Add(newRecord);
        }
    }
}