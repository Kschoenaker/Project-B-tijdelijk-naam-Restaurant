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
        DishAccess dishAccess = new();
        return dishAccess.GetAll();
    }

    public static List<DishModel> GetAllByTheme(string theme)
    {
        return null;
    }

    public static DishModel GetById(int id)
    {
        DishAccess dishAccess = new();
        return dishAccess.GetById(id);
    }

    public static DishModel GetByName(string name)
    {
        DishAccess dishAccess = new();
        return dishAccess.GetByName(name);
    }

    public static void AddDefaultDishesTimes()
    {
        // 8 themes
        List<string> themes = new()
        {
            "Italian",
            "French",
            "Japanese",
            "Mexican",
            "Indian",
            "American",
            "Mediterranean",
            "Vegetarian"
        };

        // Add themes to the database
        foreach (var theme in themes)
        {
            ThemeModel themeModel = new ThemeModel(0, theme);
            ThemeLogic.Add(themeModel);
        }

        List<(string Name, double Price, DishTypeEnum Type, string Theme)> dishes = new()
        {
            // Italian
            ("Bruschetta", 6.50, DishTypeEnum.Appetizer, "Italian"),
            ("Caprese Salad", 7.00, DishTypeEnum.Appetizer, "Italian"),
            ("Spaghetti Carbonara", 12.50, DishTypeEnum.MainCourse, "Italian"),
            ("Lasagna", 13.00, DishTypeEnum.MainCourse, "Italian"),
            ("Margherita Pizza", 11.00, DishTypeEnum.MainCourse, "Italian"),
            ("Tiramisu", 6.00, DishTypeEnum.Dessert, "Italian"),
            ("Panna Cotta", 5.50, DishTypeEnum.Dessert, "Italian"),

            // French
            ("Creme Brulee", 7.00, DishTypeEnum.Dessert, "French"),
            ("Ratatouille", 11.00, DishTypeEnum.MainCourse, "French"),
            ("Baguette with Cheese", 5.50, DishTypeEnum.Appetizer, "French"),
            ("Quiche Lorraine", 9.50, DishTypeEnum.MainCourse, "French"),
            ("Escargots", 8.50, DishTypeEnum.Appetizer, "French"),
            ("Macaron", 4.50, DishTypeEnum.Dessert, "French"),

            // Japanese
            ("Sushi Roll", 10.50, DishTypeEnum.MainCourse, "Japanese"),
            ("Miso Soup", 4.50, DishTypeEnum.Appetizer, "Japanese"),
            ("Tempura", 9.50, DishTypeEnum.MainCourse, "Japanese"),
            ("Sashimi", 12.50, DishTypeEnum.MainCourse, "Japanese"),
            ("Green Tea Ice Cream", 5.50, DishTypeEnum.Dessert, "Japanese"),
            ("Edamame", 3.50, DishTypeEnum.Appetizer, "Japanese"),

            // Mexican
            ("Churros", 5.50, DishTypeEnum.Dessert, "Mexican"),
            ("Tacos", 8.50, DishTypeEnum.MainCourse, "Mexican"),
            ("Guacamole", 6.00, DishTypeEnum.Appetizer, "Mexican"),
            ("Enchiladas", 9.50, DishTypeEnum.MainCourse, "Mexican"),
            ("Quesadilla", 7.50, DishTypeEnum.Appetizer, "Mexican"),
            ("Flan", 5.50, DishTypeEnum.Dessert, "Mexican"),

            // Indian
            ("Butter Chicken", 12.00, DishTypeEnum.MainCourse, "Indian"),
            ("Samosa", 4.50, DishTypeEnum.Appetizer, "Indian"),
            ("Gulab Jamun", 5.50, DishTypeEnum.Dessert, "Indian"),
            ("Palak Paneer", 11.00, DishTypeEnum.MainCourse, "Indian"),
            ("Naan", 2.50, DishTypeEnum.Appetizer, "Indian"),
            ("Rasgulla", 4.50, DishTypeEnum.Dessert, "Indian"),

            // American
            ("Burger", 10.00, DishTypeEnum.MainCourse, "American"),
            ("Caesar Salad", 7.50, DishTypeEnum.Appetizer, "American"),
            ("Cheesecake", 6.50, DishTypeEnum.Dessert, "American"),
            ("BBQ Ribs", 14.00, DishTypeEnum.MainCourse, "American"),
            ("Buffalo Wings", 8.50, DishTypeEnum.Appetizer, "American"),
            ("Apple Pie", 5.50, DishTypeEnum.Dessert, "American"),

            // Mediterranean
            ("Hummus", 5.50, DishTypeEnum.Appetizer, "Mediterranean"),
            ("Greek Salad", 7.50, DishTypeEnum.Appetizer, "Mediterranean"),
            ("Grilled Lamb", 13.50, DishTypeEnum.MainCourse, "Mediterranean"),
            ("Falafel", 6.50, DishTypeEnum.Appetizer, "Mediterranean"),
            ("Baklava", 5.00, DishTypeEnum.Dessert, "Mediterranean"),
            ("Moussaka", 12.50, DishTypeEnum.MainCourse, "Mediterranean"),

            // Vegetarian
            ("Stuffed Mushrooms", 6.50, DishTypeEnum.Appetizer, "Vegetarian"),
            ("Vegetable Stir Fry", 10.50, DishTypeEnum.MainCourse, "Vegetarian"),
            ("Veggie Burger", 9.50, DishTypeEnum.MainCourse, "Vegetarian"),
            ("Caprese Skewers", 5.50, DishTypeEnum.Appetizer, "Vegetarian"),
            ("Fruit Tart", 5.50, DishTypeEnum.Dessert, "Vegetarian"),
            ("Chocolate Mousse", 6.00, DishTypeEnum.Dessert, "Vegetarian")
        };

        // Add dishes
        foreach (var dish in dishes)
        {
            DishModel dishModel = new DishModel(0, dish.Name, dish.Price, dish.Type.ToString());
            AddDish(dish.Theme, dish.Name, dish.Price, dish.Type.ToString());

            int themeID = ThemeLogic.GetByName(dish.Theme).ID;
            int dishID = GetByName(dish.Name).ID;
            DishThemeRecordModel dishThemeRecord = new DishThemeRecordModel(0, dishID, themeID);
        }
    }
}