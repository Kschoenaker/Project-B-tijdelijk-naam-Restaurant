public static class NavigationLogic
{
    
    // "Database" van alle menu-opties
    public static Dictionary<string, List<string>> PrintToScreen = new();

    // Initialiseer menu's (zorg dat keys uniek zijn)
    static NavigationLogic()
    {
        PrintToScreen["AdminMenu"] = new List<string> { "See Reservations", "Theme Management", "Theme Planner", "Log out" };
        PrintToScreen["ThemeManagement"] = new List<string> { "Theme", "Dish", "Go back" };
        PrintToScreen["Theme"] = new List<string> { "Edit Theme", "Add New Theme", "Go back" };
        PrintToScreen["Dish"] = new List<string> { "Dish Overview", "New Dish", "Go back" };
        PrintToScreen["DishTypeSelect"] = new List<string> { "Appitizer","Main course", "dessert", "Go back" };

        PrintToScreen["DishOverview"] = new List<string> { "Edit Dish", "Delete Dish", "Go back" };
        PrintToScreen["ThemePlanner"] = new List<string> { "Edit Planner", "Planner Overview", "Go back" };
    }

    // Genereer menu opties op basis van naam
    public static List<string> ScreenOptions(string menuName)
    {
        if (PrintToScreen.ContainsKey(menuName))
            return PrintToScreen[menuName];
        else
            return new List<string> { "No options available" };
    }

    // Scroll door de opties en kies met Enter
    public static void NavigateChoices(string menuName, Func<int, bool> handleSelection)
    {
        List<string> options = ScreenOptions(menuName);
        int selectedOption = 0;
        ConsoleKey key;
        bool runCode = true;

        while (runCode)
        {
            Console.Clear();
            Console.WriteLine($"--- {menuName} ---\nUse ↑/↓ to navigate and Enter to select\n");

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
                runCode = handleSelection(selectedOption);
            }
        }
    }
}


