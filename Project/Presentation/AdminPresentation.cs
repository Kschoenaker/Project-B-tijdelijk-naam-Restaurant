public static class AdminPresentation
{

    // you have logged in in the Admin with the admin inlog code
public static void AdminStartScreen()
    {
        Console.WriteLine("Welcome Admin");

        List<string> options = new List<string>
    {
        "Make reservation",
        "See all reservation",
        "Theme Management",
        "Menu Planner Management",
        "Log out"
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
                        Console.WriteLine("Go to Reservations");
                        break;
                    case 1:
                        Console.WriteLine("See all reservations");
                        break;
                    case 2:
                        Console.WriteLine("Menu's");
                        AdminPresentation.MenuManagement();
                        break;
                    case 3:
                        Console.WriteLine("Menu planner");
                        break;
                    case 4:
                        Console.WriteLine("Log out");
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

    // the theme and menu option screen 
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
                        Console.WriteLine("See all reservations");
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

                Console.WriteLine("Druk op een toets om door te gaan...");
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
                        // ThemeDAL.AddTheme(new ThemeModel(themeName));
                        AdminPresentation.AddDishesToTheme(themeName);
                        selecting = false;
                        break;
                    case 1: // NO
                        Console.Clear();
                        Console.WriteLine("Thema niet aangemaakt. Terug naar menu...");
                        AdminPresentation.MenuManagement(); // terug naar vorige scherm
                        selecting = false;
                        break;
                }

                Console.WriteLine("\nDruk op een toets om verder te gaan...");
                Console.ReadKey();
            }
        }
    }


public static void AddDishesToTheme(string themeName)
{
    Console.Clear();
    Console.WriteLine($"=== Add new dish to Theme: {themeName} ===");

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
        Console.WriteLine($"=== Add new dish to Theme: {themeName} ===\n");
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
                    Console.WriteLine($"=== Nieuw Voorgerecht voor {themeName} ===");
                    AdminPresentation.CreateDish("Voorgerecht");
                    // hier kun je straks de invoer voor een nieuw gerecht doen
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine($"=== Nieuw Hoofdgerecht voor {themeName} ===");
                    AdminPresentation.CreateDish("Hoofdgerecht");
                    // invoer voor hoofdgerecht
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine($"=== Nieuw Nagerecht voor {themeName} ===");
                    AdminPresentation.CreateDish("Nagerecht");
                    // invoer voor nagerecht
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine($"Klaar met gerechten toevoegen aan {themeName}.");
                    
                    running = false;
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nDruk op een toets om verder te gaan...");
                Console.ReadKey();
            }
        }
    }
}

    public static void CreateDish(string dishType)
    {
        Console.Clear();
        Console.WriteLine("=== Nieuw gerecht toevoegen ===");

        Console.Write("Voer de naam van het gerecht in: ");
        string dishName = Console.ReadLine();

        Console.Write("Voer de prijs in (bijv. 12.50): ");
        double price = double.Parse(Console.ReadLine());

        // Console.Write("Voer het type gerecht in (Voorgerecht / Hoofdgerecht / Nagerecht): ");
        // string type = Console.ReadLine();

        Console.WriteLine($"→ {dishName} ({dishType}) - €{price}");

        Console.WriteLine("\nGerecht succesvol aangemaakt!");
        Console.WriteLine("nog regelen dat hij het nieuwe dish toevoegt");

        


    }


    public static void CreateDish(string dishType, DishModel newDish)
    {
        Console.Clear();
        Console.WriteLine("=== Nieuw gerecht toevoegen ===");

    }

 
}