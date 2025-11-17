public static class DishThemeRecordsLogic
{
    public static void Add(DishThemeRecordModel record)
    {
        DishThemeRecordsAccess dishThemeRecordsAccess = new DishThemeRecordsAccess();
        dishThemeRecordsAccess.Add(record);
    }

    public static List<DishModel> GetDishesByThemeID(int themeID)
    {
        DishThemeRecordsAccess access = new DishThemeRecordsAccess();
        List<DishThemeRecordModel> records = access.GetByThemeID(themeID);

        List<DishModel> dishes = new();

        foreach (var record in records)
        {
            DishModel dish = DishLogic.GetById(record.Dish_ID);
            if (dish != null)
                dishes.Add(dish);
        }

        return dishes;
    }
}