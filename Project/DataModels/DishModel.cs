public class DishModel
{
    public int ID { get; set; }
    public int Theme_ID { get; set; }
    public string DishName { get; set; }
    public double DishPrice { get; set; }
    public string DishType { get; set; }

    public DishModel(int id, int theme_id, string dishName, double dishPrice, string dishType)
    {
        ID = id;
        Theme_ID = theme_id;
        DishName = dishName;
        DishPrice = dishPrice;
        DishType = dishType;
    }
}