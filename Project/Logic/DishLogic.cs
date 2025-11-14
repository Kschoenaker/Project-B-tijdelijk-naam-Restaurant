public class DishLogic
{

    public static void AddDish(string theme, string dishname, double dishprice, string dishtype)
    {
        ThemeAccess themeaccess = new();

        int theme_id = themeaccess.GetThemeIdByName(theme);
        DishModel dish = new(0, dishname, dishprice, dishtype);
        AddDish(dish);
    }
    
    public static void AddDish(DishModel dish)
    {
        DishAccess dishaccess = new();
        dishaccess.Add(dish);
    }

    public static List<DishModel> GetAll()
    {
        DishAccess dishAccess= new();
        return dishAccess.GetAll();
    }
}