public enum DishTypeEnum
{
    
}

public class DishModel
{
    public int ID { get; set; }
    public string DishName { get; set; }
    public double DishPrice { get; set; }
    public string DishType { get; set; }

    public DishModel() { }
 
    public DishModel(int id, string dishName, double dishPrice, string dishType)
    {
        ID = id;
        DishName = dishName;
        DishPrice = dishPrice;
        DishType = dishType;
    }
}