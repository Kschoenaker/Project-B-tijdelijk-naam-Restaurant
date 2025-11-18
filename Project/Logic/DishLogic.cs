public static class DishLogic
{
    public static DishAccess newConnect = new();

    public static void AddDish(string dishname, double dishprice, string dishtype)
    {
        //int theme_id = themeaccess.GetThemeIdByName(theme);
        DishModel dish = new( 0, dishname, dishprice, dishtype);
        AddDish(dish);
    }
    
    public static void AddDish(DishModel dish)
    {
        DishAccess dishaccess = new();
        dishaccess.Add(dish);
    }

        public static void GetThemeDishes(string themeName)
    {
        ThemeModel FoundTheme = ThemeLogic.GetByName(themeName);

        if (FoundTheme is null)
        {
            return;
        }

        int ThemeID = FoundTheme.ID; 

        List<DishModel> ThemesDishes = DishThemeRecordsLogic.GetDishesByThemeID(ThemeID);

    }



    public static bool ValidateDishName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        bool containsLetter = false;

        foreach (char c in name)
        {
            if (char.IsDigit(c))          
                return false;

            if (char.IsLetter(c))        
                containsLetter = true;
        }

        return containsLetter;
    }

    public static bool ValidateDishPrice(string price)
    {
        if (string.IsNullOrWhiteSpace(price))
            return false;
        foreach (char c in price)
            {
            if (!char.IsDigit(c) && c != '.' && c != ',')
            return false;
            }

        string priceInput = price.Replace(".", ",");
        double Newprice = double.Parse(priceInput);

        if (Newprice < 0 || Newprice > 100)
            return false;

        return true;
    }


    
    public static List<DishModel> GetAll()
    {
        DishAccess dishAccess= new();
        return dishAccess.GetAll();
    }

    public static List<DishModel> GetAllByTheme(string theme)
    {
        return null;
    }

    public static DishModel GetById(int id)
    {
        DishAccess dishAccess= new();
        return dishAccess.GetById(id);
    }
}