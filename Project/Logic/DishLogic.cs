public class DishLogic
{




    public void AddDish(string theme ,string dishname, double dishprice, string dishtype)
    {



        DishAccess dishaccess = new();
        ThemeAccess themeaccess = new();

        int theme_id = themeaccess.GetThemeIdByName(theme);
        DishModel dish = new(0,theme_id, dishname, dishprice, dishtype);
        dishaccess.Add(dish);
        

        


    }
}