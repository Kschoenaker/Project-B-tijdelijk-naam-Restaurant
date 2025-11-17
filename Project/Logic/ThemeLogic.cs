public class ThemeLogic
{
    public static ThemeModel Add(ThemeModel themeModel)
    {
        ThemeAccess themeAccess = new ThemeAccess();
        themeAccess.Add(themeModel);
        return themeAccess.GetThemeByName(themeModel.ThemeName);
    }

    public static ThemeModel Add(string theme)
    {
        ThemeModel themeModel= new ThemeModel(0, theme);
        return Add(themeModel);
    }

    public bool MakeTheme(string theme)
    {
        ThemeAccess themeaccess = new ThemeAccess();

        int? id = themeaccess.GetThemeIdByNamecanbenull(theme);

        if (id is null)
        {
            ThemeModel thememodel = new(0, theme);
            themeaccess.Add(thememodel);
            return true;
        }

        else
        {
            ThemeModel thememodel = new(id.Value, theme);
            themeaccess.Update(thememodel);
            return true;


        }

    }
    public int? CheckIDbyname(string theme)
    {
        ThemeAccess access = new();

        return access.GetThemeIdByNamecanbenull(theme);
    }
    public int ThemeCheck()
    {
        ThemeAccess themeaccess = new ThemeAccess();
        int number = themeaccess.GetLastInsertedId();
        return number;

    }


    public void checkTheme()
    {
        

    }
    public bool AddTheme()
    {

{
        ThemeLogic themelogic = new();

        if (themelogic.ThemeCheck() == 0)
        {
            Console.WriteLine("No themes added !");
            return false;
        }


        List<string> optionNames = new List<string>() { "Year", "Month", "Theme" };
        List<int> optionValues = new List<int>() { 2025, 1, 0 };
        ThemeAccess themeaccess = new();
        List<string> themes = themeaccess.GetAllThemeNames();



        int selectedOption = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Select Year, Month & Theme\n");
            Console.WriteLine("Use ← / → to change option, ↑ / ↓ to change value, Enter to confirm:\n");

            for (int i = 0; i < optionNames.Count; i++)
            {
                if (i == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                if (i == 2) // Theme
                    Console.Write($" {optionNames[i]}: {themes[optionValues[i]]} ");
                else
                    Console.Write($" {optionNames[i]}: {optionValues[i]} ");

                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    selectedOption--;
                    if (selectedOption < 0) selectedOption = optionNames.Count - 1;
                    break;
                case ConsoleKey.RightArrow:
                    selectedOption++;
                    if (selectedOption >= optionNames.Count) selectedOption = 0;
                    break;
                case ConsoleKey.UpArrow:
                    if (selectedOption == 0)
                        optionValues[selectedOption]++;
                    else if (selectedOption == 1)
                    {
                        optionValues[selectedOption]++;
                        if (optionValues[selectedOption] > 12) optionValues[selectedOption] = 1;
                    }
                    else if (selectedOption == 2)
                    {
                        optionValues[selectedOption]++;
                        if (optionValues[selectedOption] >= themes.Count) optionValues[selectedOption] = 0;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOption == 0)
                        optionValues[selectedOption]--;
                    else if (selectedOption == 1)
                    {
                        optionValues[selectedOption]--;
                        if (optionValues[selectedOption] < 1) optionValues[selectedOption] = 12;
                    }
                    else if (selectedOption == 2)
                    {
                        optionValues[selectedOption]--;
                        if (optionValues[selectedOption] < 0) optionValues[selectedOption] = themes.Count - 1;
                    }
                    break;
            }

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        Console.WriteLine("Theme has been added:");
        for (int i = 0; i < optionNames.Count; i++)
        {
            if (i == 2)
                Console.WriteLine(optionNames[i] + ": " + themes[optionValues[i]]);
            else
                Console.WriteLine(optionNames[i] + ": " + optionValues[i]);
        }


        int year = optionValues[0];
        int month = optionValues[1];
        int theme_id = themeaccess.GetThemeIdByName(themes[optionValues[2]]);

        DateTime themeDate = new DateTime(year, month, 1);
        



        ThemeCalanderAccess themecalanderaccess = new();


        int? id = themecalanderaccess.GetIDByDatetime(themeDate);
        if (id is null)
        {
            ThemeCalanderModel themecalandermodel = new(0, themeDate, theme_id);
                themecalanderaccess.Add(themecalandermodel);
                return true;
        }

        else
        {
            ThemeCalanderModel themecalandermodel = new(id.Value, themeDate, theme_id);
                themecalanderaccess.Update(themecalandermodel);
                return true;


        }

        // Console.WriteLine("Select");
        // string theme = Console.ReadLine();
        // ThemeLogic themelogic = new();
        // themelogic.AddTheme(theme);


    }


    }

    public static ThemeModel GetByID(int id)
    {
        ThemeAccess themeAccess= new();
        return themeAccess.GetByThemeID(id);
    }
    // Shiv code 
    public static void Themeoverview()
    {
        int year = 2025;

        // Row 0 = year navigation, rows 1-12 = months
        List<List<string>> maanden = new List<List<string>>
        {
            new List<string>() { " <---", "2025", "--->" },  // year row
            new List<string> { "Januari :", "-" },
            new List<string> { "Februari :", "-" },
            new List<string> { "Maart :", "-" },
            new List<string> { "April :", "-" },
            new List<string> { "Mei :", "-" },
            new List<string> { "Juni :", "-" },
            new List<string> { "Juli :", "-" },
            new List<string> { "Augustus :", "-" },
            new List<string> { "September :", "-" },
            new List<string> { "Oktober :", "-" },
            new List<string> { "November :", "-" },
            new List<string> { "December :", "-" }
        };

        int selectedRow = 0;
        int selectedCol = 1;
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            maanden[0][1] = year.ToString(); // update year dynamically

            Console.WriteLine("Use ↑ / ↓ to move, ← / → to move selection, Enter to confirm:\n");

            // Display all rows
            for (int i = 0; i < maanden.Count; i++)
            {
                for (int j = 0; j < maanden[i].Count; j++)
                {
                    bool isSelected = (i == selectedRow && j == selectedCol);

                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write($" {maanden[i][j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            // Input handling
            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedRow--;
                    if (selectedRow < 0) selectedRow = 0;
                    if (selectedCol >= maanden[selectedRow].Count) selectedCol = maanden[selectedRow].Count - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedRow++;
                    if (selectedRow >= maanden.Count) selectedRow = maanden.Count - 1;
                    if (selectedCol >= maanden[selectedRow].Count) selectedCol = maanden[selectedRow].Count - 1;
                    break;

                case ConsoleKey.LeftArrow:
                    selectedCol--;
                    if (selectedCol < 0) selectedCol = 0;

                    if (selectedRow == 0 && selectedCol == 0)
                    {
                        year--;
                        ResetMonths(ref maanden);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    selectedCol++;
                    if (selectedCol >= maanden[selectedRow].Count) selectedCol = maanden[selectedRow].Count - 1;

                    if (selectedRow == 0 && selectedCol == 2)
                    {
                        year++;
                        ResetMonths(ref maanden);
                    }
                    break;

                case ConsoleKey.Enter:
                    if (selectedCol == 1 && selectedRow != 0)
                    {
                        string theme = Chooseoption();
                        maanden[selectedRow][1] = theme;
                    }
                    break;
            }
        }
    }

    public static void ResetMonths(ref List<List<string>> maanden)
    {
        maanden = new List<List<string>>
        {
            new List<string>() { " <---", "YEAR", "--->" },
            new List<string> { "Januari :", "-" },
            new List<string> { "Februari :", "-" },
            new List<string> { "Maart :", "-" },
            new List<string> { "April :", "-" },
            new List<string> { "Mei :", "-" },
            new List<string> { "Juni :", "-" },
            new List<string> { "Juli :", "-" },
            new List<string> { "Augustus :", "-" },
            new List<string> { "September :", "-" },
            new List<string> { "Oktober :", "-" },
            new List<string> { "November :", "-" },
            new List<string> { "December :", "-" }
        };
    }

    public static string Chooseoption()
    {
        List<string> option = new List<string> { "Delete", "Change/Add theme" };

        int selectedIndex = 0;
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Use ↑ / ↓ to move, Enter to confirm:\n");

            for (int i = 0; i < option.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(option[i]);
                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = 0;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    if (selectedIndex >= option.Count) selectedIndex = option.Count - 1;
                    break;

                case ConsoleKey.Enter:
                    if (selectedIndex == 0)
                        return "";
                    return Choosetheme();
            }
        }
    }

    public static string Choosetheme()
    {
        List<string> themes = new List<string>
        {
            "Italian", "Mexican", "French", "Indian", "Thai", "Greek",
            "Japanese", "Spanish", "Chinese", "Lebanese", "American",
            "Moroccan", "Korean", "Turkish", "Vietnamese", "Brazilian",
            "Mediterranean", "Caribbean", "German", "Russian"
        };

        int selectedIndex = 0;
        ConsoleKey key;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Use ↑ / ↓ to move, Enter to confirm:\n");

            for (int i = 0; i < themes.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(themes[i]);
                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = 0;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    if (selectedIndex >= themes.Count) selectedIndex = themes.Count - 1;
                    break;

                case ConsoleKey.Enter:
                    return themes[selectedIndex];
            }
        }
    }


}