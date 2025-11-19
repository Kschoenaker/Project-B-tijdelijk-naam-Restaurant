public static class AdminPresentation
{

    // you have logged in in the Admin with the admin inlog cod ---------------------------
    public static bool AdminStartScreen(int selectedOption)
    {
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("See Reservations");
  
                break;
            case 1:
                Console.WriteLine("Theme Management");
        
                break;
            case 2:
                Console.WriteLine("Theme Planner");
                
                break;
            case 3:
                return false; // go back
                // should go back lo log screen
        }
        //Console.WriteLine("Press a key to continue...");
        //Console.ReadKey();
        return true;
    }
    public static bool ThemeManagement(int selectedOption)
    {
        Console.Clear();
        switch (selectedOption)
        {
            case 0:
                Console.WriteLine("Theme");
                //NavigationLogic.NavigateChoices("Theme", ThemePresentation.ChoiceDishType );
                break;
            case 1:
                Console.WriteLine("Dish");
                NavigationLogic.NavigateChoices("Dish", ThemePresentation.DishMenu );
                break;
            case 2:
                return false; // go back
                // Go back to admin screen
        }
        //Console.WriteLine("Press a key to continue...");
        //Console.ReadKey();
        return true;
    }
    public static void AdminStartScreen()
    {
        Console.WriteLine("Welcome Admin");

        List<string> options = new List<string>
    {
        "See all reservation",
        "Theme Management",
        "Menu Planner Management",
        "Log out"
    };

        int selectedOption = 0;
        ConsoleKey key;
        bool runCode = true;
        ThemeLogic themelogic = new();
        while (runCode)
        {
            Console.Clear();
            Console.WriteLine("Use ↑/↓ to navigate and Enter to select option");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;
            
            if (key == ConsoleKey.UpArrow)
            {
                selectedOption--;
                if (selectedOption < 0) selectedOption = options.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedOption++;
                if (selectedOption >= options.Count) selectedOption = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedOption)
                {
                    case 0:
                        Console.WriteLine("See all reservations");
                        ReservationLogic.HandleSeeAllReservation();
                        break;
                    case 1:
                        Console.WriteLine("Menu's");
                        AdminPresentation.MenuManagement();
                        break;
                    case 2:
                        Console.WriteLine("Menu planner");
                        themelogic.AddTheme();
                        break;
                    case 3:
                    
                        runCode = false; // stop de loop
                        //  toevoegen dat hioj uitlopged

                        break;
                    default:
                        Console.WriteLine("Niet geldig!");
                        break;
                }

                Console.ReadKey();
            }
        }
    }
    public static void MenuManagement()
    {
        Console.WriteLine("Welcome Admin");

        List<string> options = new List<string>
    {
        "Make new Menu",
        "Your Menu's",
        "Exit",
    };

        int selectedOption = 0;
        ConsoleKey key;
        bool runCode = true;


        while (runCode)
        {
            Console.Clear();
            Console.WriteLine("Use ↑/↓ to navigate and Enter to select option");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
            }

            Console.ResetColor();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedOption--;
                if (selectedOption < 0) selectedOption = options.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedOption++;
                if (selectedOption >= options.Count) selectedOption = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedOption)
                {
                    case 0:
                        Console.WriteLine("Add new Theme");
                        AdminPresentation.MakeNewMenu();
                        break;
                    case 1:
                        Console.WriteLine("See all menu's");

                        break;
                    case 2:
                        Console.WriteLine("Exit");
                        runCode = false; // stop de loop
                        AdminPresentation.AdminStartScreen();
                        break;
                    default:
                        Console.WriteLine("Niet geldig!");
                        break;
                }

                //Console.WriteLine("Druk op een toets om door te gaan...");
                Console.ReadKey();
            }
        }
    }
    public static void MakeNewMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Create New Theme ===");

        Console.Write("Voer de naam van het nieuwe thema in: ");
        string? themeName = Console.ReadLine();

        // Vraag bevestiging
        List<string> options = new List<string> { "Yes", "No" };
        int selectedOption = 0;
        ConsoleKey key;
        bool selecting = true;

        while (selecting)
        {
            Console.Clear();
            Console.WriteLine($"Wil je het thema '{themeName}' aanmaken?\n");
            Console.WriteLine("Gebruik ↑/↓ en druk op Enter om te kiezen:\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);
            }

            Console.ResetColor();
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedOption--;
                if (selectedOption < 0) selectedOption = options.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedOption++;
                if (selectedOption >= options.Count) selectedOption = 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                switch (selectedOption)
                {
                    case 0: // YES
                        Console.Clear();
                        Console.WriteLine($" Thema '{themeName}' toegevoegd aan database!");
                        ThemeModel theme = ThemeLogic.Add(themeName);
                        AdminPresentation.AddDishesToTheme(theme);
                        selecting = false;
                        break;
                    case 1: // NO
                        Console.Clear();
                        Console.WriteLine("Thema niet aangemaakt. Terug naar menu...");
                        AdminPresentation.MenuManagement(); // terug naar vorige scherm
                        selecting = false;
                        break;
                }

                //Console.WriteLine("\nDruk op een toets om verder te gaan...");
                Console.ReadKey();
            }
        }
    }
    public static void AddDishesToTheme(ThemeModel theme)
{
    Console.Clear();
    Console.WriteLine($"=== Add new dish to Theme: {theme.ThemeName} ===");

    List<string> options = new List<string>
    {
        "Add Voorgerecht",
        "Add Hoofdgerecht",
        "Add Nagerecht",
        "Geen gerechten meer toevoegen"
    };

    int selectedOption = 0;
    ConsoleKey key;
    bool running = true;

    while (running)
    {
        Console.Clear();
        Console.WriteLine($"=== Add new dish to Theme: {theme.ThemeName} ===\n");
        Console.WriteLine("Gebruik ↑/↓ om te navigeren en druk op Enter om te kiezen:\n");

        // Menu tonen met highlight
        for (int i = 0; i < options.Count; i++)
        {
            if (i == selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(options[i]);
        }

        Console.ResetColor();

        key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.UpArrow)
        {
            selectedOption--;
            if (selectedOption < 0) selectedOption = options.Count - 1;
        }
        else if (key == ConsoleKey.DownArrow)
        {
            selectedOption++;
            if (selectedOption >= options.Count) selectedOption = 0;
        }
        else if (key == ConsoleKey.Enter)
        {
            switch (selectedOption)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine($"=== Nieuw Voorgerecht voor {theme.ThemeName} ===");
                    AdminPresentation.CreateDish("Voorgerecht", theme);
                    
                    // hier kun je straks de invoer voor een nieuw gerecht doen
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine($"=== Nieuw Hoofdgerecht voor {theme.ThemeName} ===");
                    AdminPresentation.CreateDish("Hoofdgerecht", theme);
                    // invoer voor hoofdgerecht
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine($"=== Nieuw Nagerecht voor {theme.ThemeName} ===");
                    AdminPresentation.CreateDish("Nagerecht", theme);
                    // invoer voor nagerecht
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine($"Klaar met gerechten toevoegen aan {theme.ThemeName}.");
                    
                    running = false;
                    break;
            }


                //Console.WriteLine("\nDruk op een toets om verder te gaan...");
                Console.ReadKey();
            
        }
    }
}
    
    // --------------------------------------------------------------------------------------NO



    public static bool AdminMenuManagement(int selectedOption) // als je als admin hebt ingelogt
    {
        switch (selectedOption)
                {
            case 0:
                Console.WriteLine("See all reservations");
                ReservationLogic.HandleSeeAllReservation(); // lijst van alle reservation
                break;
            case 1:
                Console.WriteLine("Theme Management");
                NavigationLogic.NavigateChoices("ThemeManagement", ThemePresentation.DisThemeManagementhMenu); // making dishes and themes
                break;
            case 2:
                Console.WriteLine("Theme Planner"); // deel de maand in
                ThemeLogic.Themeoverview();
                break;
            case 3:
                Console.WriteLine("log out");
                return false ;
                break;
            default:
                Console.WriteLine("Niet geldig!");
                break;
        }



        //Console.WriteLine("Press a key to continue...");
        //Console.ReadKey();
        return true;
    }

    // the theme and menu option screen 

    public static bool DishMenu(int selectedOption)
    {
        switch (selectedOption)
        {
            //PrintToScreen["AdminMenu"] = new List<string> { "See Reservations", "Theme Management", "Theme Planner", "Log out" };
            case 0:
                Console.WriteLine("Dish Overview");
                // gaat naar een list van themes formatted
                AdminPresentation.TestTheme();
                break;
            case 1:
                Console.WriteLine("New Dish");
                break;
            case 2:
                return false; // go back
        }
        //Console.WriteLine("Press a key to continue...");
        //Console.ReadKey();
        return true;
    }


    public static bool ChoiceThemeType(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    Console.WriteLine("Edit theme");
                    // gaat naar een list van themes formatted
                    AdminPresentation.TestTheme();
                    break;
                case 1:
                    Console.WriteLine("Add new theme");
                    // vraagt om een nieuwe nieuwe thwmw
                    //CreateNewTheme();
                    break;
                case 2:
                    return false; // go back
            }
            //Console.WriteLine("Press a key to continue...");
            //Console.ReadKey();
            return true;
        }




    public static void CreateDish(string dishType, ThemeModel theme)
    {
        Console.Clear();
        Console.WriteLine("=== Nieuw gerecht toevoegen ===");

        Console.Write("Voer de naam van het gerecht in: ");
        string dishName = Console.ReadLine();

        Console.Write("Voer de prijs in (bijv. 12.50): ");
        double price = double.Parse(Console.ReadLine().Replace("." , ","));
        

        // Console.Write("Voer het type gerecht in (Voorgerecht / Hoofdgerecht / Nagerecht): ");
        // string type = Console.ReadLine();

        Console.WriteLine($"→ {dishName} ({dishType}) - €{price}");

        Console.WriteLine("\nGerecht succesvol aangemaakt!");
        Console.WriteLine("nog regelen dat hij het nieuwe dish toevoegt");

        DishModel dishModel = new DishModel(0, dishName, price, dishType);
        DishLogic.AddDish(dishModel);

    }


    public static void CreateDish(string dishType, DishModel newDish)
    {
        Console.Clear();
        Console.WriteLine("=== Nieuw gerecht toevoegen ===");

    }

    public static void themeTabel(List<ThemeModel> themes)
    {
        
        var themetable = new TableUI<ThemeModel>("Themes Menu's",
        new Dictionary<string, string>
        {
            {"ThemeName", "Theme Name"}
        },
        themes,
        new List<string> {"ThemeName"});

        themetable.Start();

    }

    public static void TestTheme()
    {
        List<ThemeModel> themeModels = ThemeLogic.AllThemes();
        themeTabel(themeModels);
    }




}