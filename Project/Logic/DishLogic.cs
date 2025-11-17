public static class DishLogic
{
    public static ThemeAccess newConnect = new();

    public static void AddDish(string theme, string dishname, double dishprice, string dishtype)
    {
        ThemeAccess themeaccess = new();

        int theme_id = themeaccess.GetThemeIdByName(theme);
        DishModel dish = new( theme_id, dishname, dishprice, dishtype);
        AddDish(dish);
    }
    
    public static void AddDish(DishModel dish)
    {
        DishAccess dishaccess = new();
        dishaccess.Add(dish);
    }

        public static void GetThemeDishes(string themeName)
    {
        int ThemeId = newConnect.GetThemeIdByName(themeName);

        
        

        // hoe krijg ik de dishes van deze theme
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


    
}