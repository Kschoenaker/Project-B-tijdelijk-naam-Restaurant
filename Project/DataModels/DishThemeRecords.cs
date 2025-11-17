public class DishThemeRecordModel
{
    public int ID { get; set; }
    public int Dish_ID { get; set; }
    public int Theme_ID { get; set; }

    public DishThemeRecordModel() { }

    public DishThemeRecordModel(int id, int dish_ID, int theme_ID)
    {
        ID = id;
        Dish_ID = dish_ID;
        Theme_ID = theme_ID;
    }
}